# BookStore Frontend

## Descrição
Aplicação frontend desenvolvida em Angular 17 para interface de usuário do sistema de gerenciamento de livraria, consumindo a API BookStore.

## Tecnologias Utilizadas

### Framework e Versão
- **Angular 17.3.0** - Framework principal
- **TypeScript 5.4.2** - Linguagem de programação
- **Node.js** - Ambiente de execução

### Estilização e UI
- **Bootstrap 5.3.8** - Framework CSS para design responsivo
- **SCSS** - Pré-processador CSS
- **Font Awesome 7.1.0** - Biblioteca de ícones
- **ng-bootstrap 16.0.0** - Componentes Bootstrap para Angular

### Gerenciamento de Estado e HTTP
- **RxJS 7.8.0** - Programação reativa e gerenciamento de observables
- **Angular HTTP Client** - Para comunicação com a API
- **Angular Forms** - Formulários reativos e template-driven

### Roteamento e Navegação
- **Angular Router** - Sistema de roteamento SPA

### Testes
- **Jasmine 5.1.0** - Framework de testes
- **Karma 6.4.0** - Test runner
- **Angular Testing Utilities** - Utilitários para testes de componentes
- **Arquivos de Teste (.spec.ts):**
  - `app.component.spec.ts` - Componente principal
  - `api.service.spec.ts` - Serviço de API
  - `autores.component.spec.ts` - Componente de autores
  - `assuntos.component.spec.ts` - Componente de assuntos
  - `livros.component.spec.ts` - Componente de livros
  - `relatorios.component.spec.ts` - Componente de relatórios

### Build e Desenvolvimento
- **Angular CLI 17.3.17** - Interface de linha de comando
- **Vite** - Build tool (através do Angular CLI)
- **TypeScript Compiler** - Compilação TypeScript

### Estrutura do Projeto

```
Front/
├── src/
│   ├── app/
│   │   ├── components/          # Componentes da aplicação
│   │   │   ├── autores/        # Gerenciamento de autores
│   │   │   │   ├── autores.component.ts
│   │   │   │   └── autores.component.spec.ts  # Testes unitários
│   │   │   ├── assuntos/       # Gerenciamento de assuntos
│   │   │   │   └── assuntos.component.spec.ts # Testes unitários
│   │   │   ├── livros/         # Gerenciamento de livros
│   │   │   │   └── livros.component.spec.ts   # Testes unitários
│   │   │   └── relatorios/     # Visualização de relatórios
│   │   │       └── relatorios.component.spec.ts # Testes unitários
│   │   ├── services/           # Serviços para comunicação com API
│   │   │   └── api.service.spec.ts            # Testes unitários
│   │   ├── models/             # Modelos/interfaces TypeScript
│   │   ├── interceptors/       # Interceptadores HTTP
│   │   ├── constants/          # Constantes da aplicação
│   │   ├── config/             # Configurações
│   │   └── app.component.spec.ts              # Testes unitários
│   ├── environments/           # Configurações de ambiente
│   └── assets/                # Recursos estáticos
├── angular.json               # Configuração do Angular CLI
├── package.json              # Dependências e scripts
├── tsconfig.json            # Configuração TypeScript
└── tsconfig.spec.json       # Configuração específica para testes
```

### Funcionalidades
- **Interface Responsiva** - Design adaptável para diferentes dispositivos
- **CRUD de Autores** - Interface para gerenciar autores
- **CRUD de Assuntos** - Interface para gerenciar categorias
- **CRUD de Livros** - Interface completa para gerenciar livros
- **Relatórios** - Visualização e download de relatórios
- **Tratamento de Erros** - Interceptador para tratamento de erros HTTP
- **Validação de Formulários** - Validação client-side
- **Navegação SPA** - Single Page Application com roteamento

### Configurações de Ambiente
- **Development:** `environment.ts`
- **Production:** `environment.prod.ts`

### Como Executar

1. **Pré-requisitos:**
   - Node.js (versão 18+)
   - npm ou yarn

2. **Instalação:**
   ```bash
   # Instalar dependências
   npm install
   ```

3. **Desenvolvimento:**
   ```bash
   # Executar em modo desenvolvimento
   npm start
   # ou
   ng serve
   ```

4. **Build:**
   ```bash
   # Build para produção
   npm run build
   # ou
   ng build --configuration production
   ```

5. **Testes:**
   ```bash
   # Executar testes unitários
   npm test
   # ou
   ng test
   ```

6. **Acessar:**
   - Aplicação: `http://localhost:4200`

### Scripts Disponíveis
- `npm start` - Inicia servidor de desenvolvimento
- `npm run build` - Build para produção
- `npm test` - Executa testes unitários
- `npm run watch` - Build em modo watch

### Configurações Importantes
- **Proxy para API:** Configurado para desenvolvimento local
- **SCSS:** Configurado como pré-processador padrão
- **Bootstrap:** Integrado globalmente via angular.json
- **Font Awesome:** Disponível em toda aplicação
- **TypeScript Strict Mode:** Habilitado para maior segurança de tipos