import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { ErrorHandlerService } from '../../services/error-handler.service';
import { Autor, CreateAutor } from '../../models/livro.model';

@Component({
  selector: 'app-autores',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './autores.component.html',
  styleUrl: './autores.component.scss'
})
export class AutoresComponent implements OnInit {
  autores: Autor[] = [];
  loading = false;
  error: string | null = null;
  successMessage: string | null = null;
  searchTerm = '';

  // Formulário
  showForm = false;
  editingId: number | null = null;
  autorForm: CreateAutor = {
    nome: ''
  };

  constructor(private apiService: ApiService, private errorHandler: ErrorHandlerService) {}

  ngOnInit(): void {
    this.loadAutores();
  }

  loadAutores(): void {
    this.loading = true;
    this.error = null;
    
    this.apiService.getAutores().subscribe({
      next: (autores) => {
        this.autores = autores;
        this.loading = false;
      },
      error: (error) => {
        this.error = this.errorHandler.getErrorMessage(error);
        this.loading = false;
      }
    });
  }

  search(): void {
    if (this.searchTerm.trim()) {
      this.loading = true;
      this.apiService.searchAutores(this.searchTerm).subscribe({
        next: (autores) => {
          this.autores = autores;
          this.loading = false;
        },
        error: (error) => {
          this.error = this.errorHandler.getErrorMessage(error);
          this.loading = false;
        }
      });
    } else {
      this.loadAutores();
    }
  }

  openForm(autor?: Autor): void {
    this.showForm = true;
    if (autor) {
      this.editingId = autor.codAu;
      this.autorForm = { nome: autor.nome };
    } else {
      this.editingId = null;
      this.autorForm = { nome: '' };
    }
  }

  closeForm(): void {
    this.showForm = false;
    this.editingId = null;
    this.autorForm = { nome: '' };
  }

  save(): void {
    if (!this.autorForm.nome.trim()) return;

    this.loading = true;
    
    // Converter para o formato esperado pela API (PascalCase)
    const autorData = {
      Nome: this.autorForm.nome.trim()
    };

    const operation = this.editingId 
      ? this.apiService.updateAutor(this.editingId, autorData)
      : this.apiService.createAutor(autorData);

    operation.subscribe({
      next: (response) => {
        this.successMessage = response.message;
        this.error = null;
        this.loading = false;
        this.loadAutores();
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
    if (confirm('Tem certeza que deseja excluir este autor?')) {
      this.loading = true;
      this.apiService.deleteAutor(id).subscribe({
        next: () => {
          this.loadAutores();
        },
        error: (error) => {
          this.error = this.errorHandler.getErrorMessage(error);
          this.loading = false;
        }
      });
    }
  }
}