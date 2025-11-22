import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { ErrorHandlerService } from '../../services/error-handler.service';
import { Assunto, CreateAssunto } from '../../models/livro.model';

@Component({
  selector: 'app-assuntos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './assuntos.component.html',
  styleUrl: './assuntos.component.scss'
})
export class AssuntosComponent implements OnInit {
  assuntos: Assunto[] = [];
  loading = false;
  error: string | null = null;
  successMessage: string | null = null;
  searchTerm = '';
  showForm = false;
  editingId: number | null = null;
  assuntoForm: CreateAssunto = { descricao: '' };

  constructor(private apiService: ApiService, private errorHandler: ErrorHandlerService) {}

  ngOnInit(): void {
    this.loadAssuntos();
  }

  loadAssuntos(): void {
    this.loading = true;
    this.apiService.getAssuntos().subscribe({
      next: (assuntos) => {
        this.assuntos = assuntos;
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
      this.apiService.searchAssuntos(this.searchTerm).subscribe({
        next: (assuntos) => {
          this.assuntos = assuntos;
          this.loading = false;
        },
        error: (error) => {
          this.error = this.errorHandler.getErrorMessage(error);
          this.loading = false;
        }
      });
    } else {
      this.loadAssuntos();
    }
  }

  openForm(assunto?: Assunto): void {
    this.showForm = true;
    if (assunto) {
      this.editingId = assunto.codAs;
      this.assuntoForm = { descricao: assunto.descricao };
    } else {
      this.editingId = null;
      this.assuntoForm = { descricao: '' };
    }
  }

  closeForm(): void {
    this.showForm = false;
    this.editingId = null;
    this.assuntoForm = { descricao: '' };
  }

  save(): void {
    if (!this.assuntoForm.descricao.trim()) return;

    this.loading = true;
    
    // Converter para o formato esperado pela API (PascalCase)
    const assuntoData = {
      Descricao: this.assuntoForm.descricao.trim()
    };

    const operation = this.editingId 
      ? this.apiService.updateAssunto(this.editingId, assuntoData)
      : this.apiService.createAssunto(assuntoData);

    operation.subscribe({
      next: (response) => {
        this.successMessage = response.message;
        this.error = null;
        this.loading = false;
        this.loadAssuntos();
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
    if (confirm('Tem certeza que deseja excluir este assunto?')) {
      this.loading = true;
      this.apiService.deleteAssunto(id).subscribe({
        next: () => this.loadAssuntos(),
        error: (error) => {
          this.error = this.errorHandler.getErrorMessage(error);
          this.loading = false;
        }
      });
    }
  }
}