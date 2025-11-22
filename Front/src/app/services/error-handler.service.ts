import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  getErrorMessage(error: any): string {
    // Erro de conexão (API offline, CORS, etc.)
    if (error.status === 0) {
      return 'Não foi possível conectar ao servidor. Verifique sua conexão com a internet ou se o servidor está funcionando.';
    }

    // Erro 404 - Não encontrado
    if (error.status === 404) {
      return 'Recurso não encontrado no servidor.';
    }

    // Erro 500 - Erro interno do servidor
    if (error.status === 500) {
      if (error.error && error.error.message) {
        return error.error.message;
      }
      return 'Erro interno do servidor. Tente novamente mais tarde.';
    }

    // Erro 400 - Bad Request com mensagens da API
    if (error.status === 400 && error.error) {
      if (error.error.message) {
        let message = error.error.message;
        if (error.error.errors && error.error.errors.length > 0) {
          message += ': ' + error.error.errors.join(', ');
        }
        return message;
      }
    }

    // Outros erros HTTP
    if (error.status) {
      return `Erro ${error.status}: ${error.statusText || 'Erro desconhecido'}`;
    }

    // Erro genérico
    return error.message || 'Ocorreu um erro inesperado. Tente novamente.';
  }
}