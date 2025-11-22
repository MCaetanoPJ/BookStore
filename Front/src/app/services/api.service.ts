import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Livro, Autor, Assunto, RelatorioLivrosPorAutor, CreateLivro, CreateAutor, CreateAssunto } from '../models/livro.model';
import { ApiResponse } from '../models/api-response.model';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl: string;

  constructor(private http: HttpClient, private config: ConfigService) {
    this.baseUrl = this.config.getApiUrl();
  }

  // Livros - CRUD completo
  getLivros(): Observable<Livro[]> {
    return this.http.get<ApiResponse<Livro[]>>(`${this.baseUrl}/livro`)
      .pipe(map(response => response.data || []));
  }

  getLivro(id: number): Observable<Livro> {
    return this.http.get<ApiResponse<Livro>>(`${this.baseUrl}/livro/${id}`)
      .pipe(map(response => response.data!));
  }

  createLivro(livro: any): Observable<{data: Livro, message: string}> {
    return this.http.post<ApiResponse<Livro>>(`${this.baseUrl}/livro`, livro)
      .pipe(map(response => ({data: response.data!, message: response.message})));
  }

  updateLivro(id: number, livro: any): Observable<{data: Livro, message: string}> {
    return this.http.put<ApiResponse<Livro>>(`${this.baseUrl}/livro/${id}`, livro)
      .pipe(map(response => ({data: response.data!, message: response.message})));
  }

  deleteLivro(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/livro/${id}`);
  }

  searchLivros(termo: string): Observable<Livro[]> {
    const params = new HttpParams().set('termo', termo);
    return this.http.get<ApiResponse<Livro[]>>(`${this.baseUrl}/livro/search`, { params })
      .pipe(map(response => response.data || []));
  }

  getLivrosByAutor(autorId: number): Observable<Livro[]> {
    return this.http.get<ApiResponse<Livro[]>>(`${this.baseUrl}/livro/autor/${autorId}`)
      .pipe(map(response => response.data || []));
  }

  getLivrosByAssunto(assuntoId: number): Observable<Livro[]> {
    return this.http.get<ApiResponse<Livro[]>>(`${this.baseUrl}/livro/assunto/${assuntoId}`)
      .pipe(map(response => response.data || []));
  }

  // Autores - CRUD completo
  getAutores(): Observable<Autor[]> {
    return this.http.get<ApiResponse<Autor[]>>(`${this.baseUrl}/autor`)
      .pipe(map(response => response.data || []));
  }

  getAutor(id: number): Observable<Autor> {
    return this.http.get<ApiResponse<Autor>>(`${this.baseUrl}/autor/${id}`)
      .pipe(map(response => response.data!));
  }

  createAutor(autor: any): Observable<{data: Autor, message: string}> {
    return this.http.post<ApiResponse<Autor>>(`${this.baseUrl}/autor`, autor)
      .pipe(map(response => ({data: response.data!, message: response.message})));
  }

  updateAutor(id: number, autor: any): Observable<{data: Autor, message: string}> {
    return this.http.put<ApiResponse<Autor>>(`${this.baseUrl}/autor/${id}`, autor)
      .pipe(map(response => ({data: response.data!, message: response.message})));
  }

  deleteAutor(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/autor/${id}`);
  }

  searchAutores(termo: string): Observable<Autor[]> {
    const params = new HttpParams().set('termo', termo);
    return this.http.get<ApiResponse<Autor[]>>(`${this.baseUrl}/autor/search`, { params })
      .pipe(map(response => response.data || []));
  }

  // Assuntos - CRUD completo
  getAssuntos(): Observable<Assunto[]> {
    return this.http.get<ApiResponse<Assunto[]>>(`${this.baseUrl}/assunto`)
      .pipe(map(response => response.data || []));
  }

  getAssunto(id: number): Observable<Assunto> {
    return this.http.get<ApiResponse<Assunto>>(`${this.baseUrl}/assunto/${id}`)
      .pipe(map(response => response.data!));
  }

  createAssunto(assunto: any): Observable<{data: Assunto, message: string}> {
    return this.http.post<ApiResponse<Assunto>>(`${this.baseUrl}/assunto`, assunto)
      .pipe(map(response => ({data: response.data!, message: response.message})));
  }

  updateAssunto(id: number, assunto: any): Observable<{data: Assunto, message: string}> {
    return this.http.put<ApiResponse<Assunto>>(`${this.baseUrl}/assunto/${id}`, assunto)
      .pipe(map(response => ({data: response.data!, message: response.message})));
  }

  deleteAssunto(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/assunto/${id}`);
  }

  searchAssuntos(termo: string): Observable<Assunto[]> {
    const params = new HttpParams().set('termo', termo);
    return this.http.get<ApiResponse<Assunto[]>>(`${this.baseUrl}/assunto/search`, { params })
      .pipe(map(response => response.data || []));
  }

  // Relatórios
  getRelatorioLivrosPorAutor(): Observable<RelatorioLivrosPorAutor[]> {
    return this.http.get<ApiResponse<RelatorioLivrosPorAutor[]>>(`${this.baseUrl}/relatorio/livros-por-autor`)
      .pipe(map(response => response.data || []));
  }

  downloadRelatorioPdf(): Observable<Blob> {
    return this.http.get(`${this.baseUrl}/relatorio/livros-por-autor/pdf`, { responseType: 'blob' });
  }

  // Métodos auxiliares para validação de conectividade
  testConnection(): Observable<any> {
    return this.http.get<ApiResponse<Autor[]>>(`${this.baseUrl}/autor`)
      .pipe(map(response => response.data || []));
  }
}