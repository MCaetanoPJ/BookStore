export interface Livro {
  codL: number;
  titulo: string;
  editora: string;
  edicao: number;
  anoPublicacao: string;
  autoresIds: number[];
  assuntosIds: number[];
  valores: LivroValor[];
}

export interface CreateLivro {
  titulo: string;
  editora: string;
  edicao: number;
  anoPublicacao: string;
  autoresIds: number[];
  assuntosIds: number[];
  valores: LivroValor[];
}

export interface LivroValor {
  tipoVendaId: number;
  valor: number;
  valorFormatado?: string;
}

export interface Autor {
  codAu: number;
  nome: string;
}

export interface CreateAutor {
  nome: string;
}

export interface Assunto {
  codAs: number;
  descricao: string;
}

export interface CreateAssunto {
  descricao: string;
}

export interface TipoVenda {
  codTv: number;
  descricao: string;
}

export interface RelatorioLivrosPorAutor {
  nomeAutor: string;
  titulo: string;
  editora: string;
  edicao: number;
  anoPublicacao: string;
  assunto: string;
  valorBalcao?: number;
  valorInternet?: number;
  valorEvento?: number;
  valorSelfService?: number;
}