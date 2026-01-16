# 🌍 DotNetTrip

Sistema de gerenciamento para agência de turismo desenvolvido em ASP.NET Core com Razor Pages, Entity Framework Core e C#.

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120)](https://docs.microsoft.com/en-us/dotnet/csharp/)

![Tela de Login](DotNetTrip/wwwroot/img/TelaLogin.png)
![Tela de Pacotes](DotNetTrip/wwwroot/img/TelaPacotes.png)

## 📋 Sobre o Projeto

DotNetTrip é uma aplicação web completa para gerenciamento de agências de turismo, desenvolvida como projeto acadêmico para demonstrar competências em:

- **Delegates e Events** em C#
- **ASP.NET Core Razor Pages**
- **Entity Framework Core**
- **Autenticação e Autorização**
- **Operações CRUD completas**

### 🎯 Funcionalidades Principais

- ✅ Cadastro e gerenciamento de clientes
- ✅ Registro de destinos turísticos
- ✅ Criação de pacotes turísticos
- ✅ Sistema de reservas com validações
- ✅ Controle de capacidade e disponibilidade
- ✅ Sistema de notas com arquivos
- ✅ Autenticação de usuários
- ✅ Exclusão lógica de registros

## 🏗️ Arquitetura

### Entidades Principais

```
Cliente
├── Id (int)
├── Nome (string)
├── Email (string)
└── Reservas (List<Reserva>)

Destino
├── Id (int)
├── Cidade (string)
└── Pais (string)

PacoteTuristico
├── Id (int)
├── Titulo (string)
├── DataInicio (DateTime)
├── CapacidadeMaxima (int)
├── Preco (decimal)
├── Destinos (List<Destino>)
└── Reservas (List<Reserva>)

Reserva
├── Id (int)
├── ClienteId (int)
├── PacoteTuristicoId (int)
└── DataReserva (DateTime)
```

### Regras de Negócio

1. **Pacotes Múltiplos**: Um pacote turístico pode incluir vários destinos
2. **Reservas Únicas**: Cliente não pode reservar o mesmo pacote mais de uma vez para a mesma data
3. **Controle de Capacidade**: Reservas bloqueadas ao atingir capacidade máxima
4. **Disponibilidade**: Apenas pacotes futuros com vagas podem ser reservados

## 🚀 Tecnologias Utilizadas

- **Framework**: ASP.NET Core 8.0
- **Linguagem**: C# 12.0
- **ORM**: Entity Framework Core
- **UI**: Razor Pages
- **Banco de Dados**: SQL Server / SQLite
- **Autenticação**: ASP.NET Core Identity
- **Gerenciamento de Arquivos**: System.IO

## 📦 Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)
- SQL Server / SQLite

## ⚙️ Configuração e Instalação

### 1. Clone o repositório

```bash
git clone https://github.com/acadl-dev/DotNetTrip.git
cd DotNetTrip
```

### 2. Configure a string de conexão

Edite `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DotNetTripDB;Trusted_Connection=True;"
  }
}
```

### 3. Execute as migrações

```bash
dotnet ef database update
```

### 4. Execute o projeto

```bash
dotnet run
```

Acesse: `https://localhost:5001` ou `http://localhost:5000`

## 🔧 Funcionalidades Técnicas Implementadas

### Parte 1: Delegates e Events

#### ✅ 1. Delegate para Cálculo de Descontos
- Delegate personalizado `CalculateDelegate` 
- Aplica 10% de desconto no preço do pacote
- Interface interativa para simulação

#### ✅ 2. Multicast Delegate para Logs
- Três métodos de logging: Console, File, Memory
- Registro de operações do sistema
- Encadeamento de ações

#### ✅ 3. Func com Lambda
- `Func<int, int, decimal>` para cálculo de valor total
- Integração com Razor Pages
- Simulação de valor final da reserva

#### ✅ 4. Evento de Capacidade
- Evento `CapacityReached` na classe Reserva
- Disparo automático ao atingir limite
- Registro via delegate no console

### Parte 2: Razor Pages

#### ✅ 5. Cadastro com Validação (Reserva)
- Model binding completo
- Validação de campos obrigatórios
- Mensagens de erro personalizadas

#### ✅ 6. Cadastro de Pacote Turístico
- Validação de comprimento mínimo (3 caracteres)
- Data Annotations
- ModelState.IsValid

#### ✅ 7. Objetos Complexos
- Cadastro com múltiplos campos
- Atributos: [Required], [MinLength], [StringLength]
- Validações contextuais

#### ✅ 8. Roteamento Dinâmico
- Página de detalhes com parâmetro na URL
- Pattern: `/EntityDetails/{id}`
- Exibição completa dos dados

#### ✅ 9. Sistema de Notas
- Criação de anotações em arquivo .txt
- Armazenamento em `wwwroot/files`
- Listagem e visualização de arquivos
- Manipulação segura com System.IO

### Parte 3: Entity Framework Core

#### ✅ 10. DbContext
- Classe `DotNetTripContext` implementada
- DbSets para todas as entidades
- Registro no Program.cs
- Configuração de conexão

#### ✅ 11. Relacionamentos
- Relacionamentos 1:N e N:N
- Fluent API e Data Annotations
- Propriedades de navegação
- Chaves estrangeiras configuradas

### Parte 4: Scaffolding e Autenticação

#### ✅ 12. CRUD com Scaffolding
- Páginas CRUD geradas automaticamente
- Personalização de views
- Formatação de dados (moeda, datas)
- Exibição de relacionamentos

#### ✅ 13. Exclusão Lógica e Auth
- Campo `IsDeleted` ou `DeletedAt`
- Deleção não destrutiva
- Sistema de autenticação simples
- Atributo `[Authorize]` em páginas sensíveis
- Middlewares configurados

## 📁 Estrutura do Projeto

```
DotNetTrip/
├── Data/
│   ├── DotNetTripContext.cs
│   └── Migrations/
├── Models/
│   ├── Cliente.cs
│   ├── Destino.cs
│   ├── PacoteTuristico.cs
│   └── Reserva.cs
├── Pages/
│   ├── Clientes/
│   ├── Destinos/
│   ├── Pacotes/
│   ├── Reservas/
│   └── ViewNotes.cshtml
├── Services/
│   └── Delegates/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── files/
├── appsettings.json
└── Program.cs
```

## 🔐 Autenticação

O sistema implementa autenticação simples baseada em credenciais definidas em código:

**Usuário de teste:**
- Login: `admin`
- Senha: `admin123`

Páginas protegidas requerem autenticação via atributo `[Authorize]`.

## 📝 Exemplos de Uso

### Criar uma Reserva

1. Acesse `/Reservas/Create`
2. Selecione um cliente
3. Escolha um pacote turístico disponível
4. Confirme a data da reserva
5. Sistema valida:
   - Disponibilidade de vagas
   - Data futura
   - Não duplicação

### Calcular Desconto

1. Acesse a página de simulação
2. Informe o preço do pacote
3. Delegate aplica 10% de desconto automaticamente
4. Veja o resultado final

## 🧪 Testes

Para executar os testes (se implementados):

```bash
dotnet test
```

## 📊 Banco de Dados

### Criar Migration

```bash
dotnet ef migrations add NomeDaMigracao
```

### Atualizar Banco

```bash
dotnet ef database update
```

### Reverter Migration

```bash
dotnet ef database update MigracaoAnterior
```

## 🤝 Contribuindo

Contribuições são bem-vindas! Para contribuir:

1. Faça um Fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE.txt](LICENSE.txt) para mais detalhes.

## 👨‍💻 Autor

**acadl-dev**

- GitHub: [@acadl-dev](https://github.com/acadl-dev)
- Projeto: [DotNetTrip](https://github.com/acadl-dev/DotNetTrip)

## 🎓 Contexto Acadêmico

Este projeto foi desenvolvido como avaliação final da disciplina de Programação Web com ASP.NET Core, demonstrando:

- Domínio técnico em C# e .NET
- Capacidade de implementação de regras de negócio
- Conhecimento em padrões arquiteturais
- Boas práticas de desenvolvimento

## 📞 Suporte

Para dúvidas ou sugestões, abra uma [issue](https://github.com/acadl-dev/DotNetTrip/issues) no repositório.

---

⭐ Se este projeto foi útil para você, considere dar uma estrela no repositório!