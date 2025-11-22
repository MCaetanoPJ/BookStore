import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { ErrorHandlerService } from '../../services/error-handler.service';
import { Livro, CreateLivro, Autor, Assunto } from '../../models/livro.model';

@Component({
  selector: 'app-livros',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './livros.component.html',
  styleUrl: './livros.component.scss'
})
export class LivrosComponent implements OnInit {
  livros: Livro[] = [];
  autores: Autor[] = [];
  assuntos: Assunto[] = [];
  loading = false;
  error: string | null = null;
  successMessage: string | null = null;
  searchTerm = '';
  showForm = false;
  editingId: number | null = null;
  
  livroForm: CreateLivro = {
    titulo: '',
    editora: '',
    edicao: 1,
    anoPublicacao: '',
    autoresIds: [],
    assuntosIds: [],
    valores: []
  };

  constructor(private apiService: ApiService, private errorHandler: ErrorHandlerService) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loadLivros();
    this.loadAutores();
    this.loadAssuntos();
  }

  loadLivros(): void {
    this.loading = true;
    this.apiService.getLivros().subscribe({
      next: (livros) => {
        this.livros = livros;
        this.loading = false;
      },
      error: (error) => {
        this.error = this.errorHandler.getErrorMessage(error);
        this.loading = false;
      }
    });
  }

  loadAutores(): void {
    this.apiService.getAutores().subscribe({
      next: (autores) => this.autores = autores,
      error: (error) => {
        this.error = this.errorHandler.getErrorMessage(error);
        console.error('Erro ao carregar autores:', error);
      }
    });
  }

  loadAssuntos(): void {
    this.apiService.getAssuntos().subscribe({
      next: (assuntos) => this.assuntos = assuntos,
      error: (error) => {
        this.error = this.errorHandler.getErrorMessage(error);
        console.error('Erro ao carregar assuntos:', error);
      }
    });
  }

  search(): void {
    if (this.searchTerm.trim()) {
      this.loading = true;
      this.apiService.searchLivros(this.searchTerm).subscribe({
        next: (livros) => {
          this.livros = livros;
          this.loading = false;
        },
        error: (error) => {
          this.error = this.errorHandler.getErrorMessage(error);
          this.loading = false;
        }
      });
    } else {
      this.loadLivros();
    }
  }

  openForm(livro?: Livro): void {
    this.showForm = true;
    if (livro) {
      this.editingId = livro.codL;
      this.livroForm = {
        titulo: livro.titulo,
        editora: livro.editora,
        edicao: livro.edicao,
        anoPublicacao: livro.anoPublicacao,
        autoresIds: [...livro.autoresIds],
        assuntosIds: [...livro.assuntosIds],
        valores: livro.valores.map(v => ({
          ...v,
          valorFormatado: v.valor.toLocaleString('pt-BR', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
          })
        }))
      };
    } else {
      this.editingId = null;
      this.livroForm = {
        titulo: '',
        editora: '',
        edicao: 1,
        anoPublicacao: new Date().getFullYear().toString(),
        autoresIds: [],
        assuntosIds: [],
        valores: []
      };
    }
  }

  closeForm(): void {
    this.showForm = false;
    this.editingId = null;
  }

  save(): void {
    if (!this.livroForm.titulo.trim()) return;

    this.loading = true;
    
    // Converter para o formato esperado pela API (PascalCase)
    const livroData = {
      Titulo: this.livroForm.titulo.trim(),
      Editora: this.livroForm.editora.trim(),
      Edicao: Number(this.livroForm.edicao),
      AnoPublicacao: this.livroForm.anoPublicacao.trim(),
      AutoresIds: this.livroForm.autoresIds,
      AssuntosIds: this.livroForm.assuntosIds,
      Valores: this.livroForm.valores.map(v => ({
        TipoVendaId: Number(v.tipoVendaId),
        Valor: Number(v.valor)
      }))
    };

    const operation = this.editingId 
      ? this.apiService.updateLivro(this.editingId, livroData)
      : this.apiService.createLivro(livroData);

    operation.subscribe({
      next: (response) => {
        this.successMessage = response.message;
        this.error = null;
        this.loading = false;
        this.loadLivros();
        setTimeout(() => {
          this.successMessage = null;
          this.closeForm();
        }, 2000);
      },
      error: (error) => {
        this.successMessage = null;
        this.error = this.errorHandler.getErrorMessage(error);
        this.loading = false;
      }
    });
  }

  delete(id: number): void {
    if (confirm('Tem certeza que deseja excluir este livro?')) {
      this.loading = true;
      this.apiService.deleteLivro(id).subscribe({
        next: () => this.loadLivros(),
        error: (error) => {
          this.error = this.errorHandler.getErrorMessage(error);
          this.loading = false;
        }
      });
    }
  }

  toggleAutor(autorId: number): void {
    const index = this.livroForm.autoresIds.indexOf(autorId);
    if (index > -1) {
      this.livroForm.autoresIds.splice(index, 1);
    } else {
      this.livroForm.autoresIds.push(autorId);
    }
  }

  toggleAssunto(assuntoId: number): void {
    const index = this.livroForm.assuntosIds.indexOf(assuntoId);
    if (index > -1) {
      this.livroForm.assuntosIds.splice(index, 1);
    } else {
      this.livroForm.assuntosIds.push(assuntoId);
    }
  }

  addValor(): void {
    this.livroForm.valores.push({ tipoVendaId: 1, valor: 0, valorFormatado: '0,00' });
  }

  formatCurrency(valor: any, event: any): void {
    let input = event.target.value;
    // Remove tudo que não é dígito
    input = input.replace(/\D/g, '');
    
    // Converte para número e divide por 100 para ter centavos
    const number = parseFloat(input) / 100;
    
    // Formata como moeda brasileira
    if (isNaN(number)) {
      valor.valorFormatado = '0,00';
    } else {
      valor.valorFormatado = number.toLocaleString('pt-BR', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      });
    }
  }

  updateValor(valor: any): void {
    // Converte o valor formatado de volta para número
    const numericValue = parseFloat(valor.valorFormatado.replace(/\./g, '').replace(',', '.'));
    valor.valor = isNaN(numericValue) ? 0 : numericValue;
  }

  onlyNumbers(event: KeyboardEvent): boolean {
    const charCode = event.which ? event.which : event.keyCode;
    // Permite apenas números (0-9)
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
      return false;
    }
    return true;
  }

  onlyNumbersAndComma(event: KeyboardEvent): boolean {
    const charCode = event.which ? event.which : event.keyCode;
    const inputValue = (event.target as HTMLInputElement).value;
    
    // Permite números (0-9)
    if (charCode >= 48 && charCode <= 57) {
      return true;
    }
    
    // Permite vírgula (44) apenas se não houver uma já
    if (charCode === 44 && !inputValue.includes(',')) {
      return true;
    }
    
    // Bloqueia outros caracteres
    event.preventDefault();
    return false;
  }

  removeValor(index: number): void {
    this.livroForm.valores.splice(index, 1);
  }

  getAutorNome(id: number): string {
    const autor = this.autores.find(a => a.codAu === id);
    return autor ? autor.nome : 'N/A';
  }

  getAssuntoDescricao(id: number): string {
    const assunto = this.assuntos.find(a => a.codAs === id);
    return assunto ? assunto.descricao : 'N/A';
  }
}