export const API_ENDPOINTS = {
  AUTOR: '/autor',
  ASSUNTO: '/assunto',
  LIVRO: '/livro',
  RELATORIO: '/relatorio'
} as const;

export const API_CONFIG = {
  BASE_URL: {
    DEVELOPMENT: 'https://localhost:51720/api',
    PRODUCTION: ''
  },
  TIMEOUT: 30000,
  RETRY_ATTEMPTS: 3
} as const;

export const getApiUrl = (production: boolean = false): string => {
  return production ? API_CONFIG.BASE_URL.PRODUCTION : API_CONFIG.BASE_URL.DEVELOPMENT;
};

export const MESSAGES = {
  SUCCESS: {
    CREATE: 'Registro criado com sucesso!',
    UPDATE: 'Registro atualizado com sucesso!',
    DELETE: 'Registro excluído com sucesso!'
  },
  ERROR: {
    GENERIC: 'Ocorreu um erro inesperado',
    NETWORK: 'Erro de conexão com o servidor',
    NOT_FOUND: 'Registro não encontrado',
    VALIDATION: 'Dados inválidos'
  }
} as const;