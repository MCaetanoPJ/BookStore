import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { DownloadService } from '../../services/download.service';
import { ErrorHandlerService } from '../../services/error-handler.service';
import { RelatorioLivrosPorAutor } from '../../models/livro.model';

@Component({
  selector: 'app-relatorios',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './relatorios.component.html',
  styleUrl: './relatorios.component.scss'
})
export class RelatoriosComponent implements OnInit {
  relatorioData: RelatorioLivrosPorAutor[] = [];
  loading = false;
  error: string | null = null;

  constructor(
    private apiService: ApiService,
    private downloadService: DownloadService,
    private errorHandler: ErrorHandlerService
  ) {}

  ngOnInit(): void {
    this.loadRelatorio();
  }

  loadRelatorio(): void {
    this.loading = true;
    this.error = null;
    
    this.apiService.getRelatorioLivrosPorAutor().subscribe({
      next: (data) => {
        this.relatorioData = data;
        this.loading = false;
      },
      error: (error) => {
        this.error = this.errorHandler.getErrorMessage(error);
        this.loading = false;
      }
    });
  }

  downloadPdf(): void {
    this.loading = true;
    this.apiService.downloadRelatorioPdf().subscribe({
      next: (blob) => {
        this.downloadService.downloadFile(blob, `relatorio-livros-por-autor-${new Date().toISOString().split('T')[0]}.pdf`);
        this.loading = false;
      },
      error: (error) => {
        this.error = this.errorHandler.getErrorMessage(error);
        this.loading = false;
      }
    });
  }


}