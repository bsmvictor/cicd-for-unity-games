# Arquitetura do Projeto

Este documento descreve a arquitetura e organização do projeto CI/CD for Unity Games.

## Estrutura de Diretórios

```
cicd-for-unity-games/
│
├── .github/                       # Configurações do GitHub
│   ├── ISSUE_TEMPLATE/            # Templates para issues
│   │   ├── bug_report.md          # Template de bug report
│   │   └── feature_request.md     # Template de feature request
│   ├── workflows/                 # GitHub Actions workflows
│   │   └── unity-tests.yml        # Pipeline principal CI/CD
│   ├── FUNDING.yml                # Configuração de funding
│   └── PULL_REQUEST_TEMPLATE.md   # Template de PR
│
├── docs/                          # Documentação do projeto
│   ├── images/                    # Imagens para documentação
│   ├── ARCHITECTURE.md            # Este arquivo
│   ├── SETUP.md                   # Guia de instalação
│   └── CI-CD-GUIDE.md             # Guia detalhado de CI/CD
│
├── UnityGame/                     # Projeto Unity principal
│   ├── Assets/                    # Assets do jogo
│   │   ├── AddressableAssetsData/ # Configurações de Addressables
│   │   ├── Art/                   # Assets artísticos
│   │   │   ├── Animations/        # Animações
│   │   │   ├── Sprites/           # Sprites e texturas
│   │   │   └── Tilemap/           # Tilemaps
│   │   ├── Materials/             # Materiais do Unity
│   │   ├── Prefabs/               # GameObjects pré-configurados
│   │   ├── Scenes/                # Cenas do jogo
│   │   │   └── MainScene.unity    # Cena principal
│   │   ├── Scripts/               # Scripts C# do jogo
│   │   │   ├── Controllers/       # Controllers de jogo
│   │   │   └── Input/             # Sistema de input
│   │   ├── Settings/              # Configurações do projeto
│   │   │   ├── Rendering/         # Configurações de renderização
│   │   │   └── Scenes/            # Configurações de cenas
│   │   └── Tests/                 # Testes automatizados
│   │       └── EditMode/          # Testes EditMode
│   │
│   ├── Packages/                  # Dependências do Unity
│   │   ├── manifest.json          # Manifesto de pacotes
│   │   └── packages-lock.json     # Lock de versões
│   │
│   └── ProjectSettings/           # Configurações do Unity
│       ├── ProjectVersion.txt     # Versão do Unity
│       ├── ProjectSettings.asset  # Configurações gerais
│       └── ...                    # Outras configurações
│
├── .editorconfig                  # Configuração do editor
├── .gitattributes                 # Configuração do Git LFS
├── .gitignore                     # Arquivos ignorados pelo Git
├── CONTRIBUTING.md                # Guia de contribuição
├── LICENSE                        # Licença MIT
├── README.md                      # Documentação principal
└── SECURITY.md                    # Política de segurança
```

## Componentes Principais

### 1. Pipeline CI/CD (`.github/workflows/`)

A pipeline de CI/CD é o coração da automação do projeto.

**Responsabilidades:**
- Executar testes automatizados
- Compilar o jogo para diferentes plataformas
- Fazer deploy automático
- Gerenciar cache para otimização

**Tecnologias:**
- GitHub Actions
- GameCI (Unity Test Runner, Unity Builder)
- Butler (Itch.io CLI)

### 2. Projeto Unity (`UnityGame/`)

O projeto Unity contém todo o código do jogo e configurações.

**Estrutura de Assets:**
- **Scripts**: Organizado por responsabilidade (Controllers, Input, etc.)
- **Scenes**: Cenas do jogo
- **Prefabs**: GameObjects reutilizáveis
- **Tests**: Testes automatizados (EditMode e PlayMode)
- **Art**: Assets visuais (sprites, animações, tilemaps)
- **Materials**: Materiais e física
- **Settings**: Configurações de renderização e cenas

**Assembly Definitions:**
- `MeuJogo.Scripts.asmdef`: Assembly principal dos scripts
- Testes em assemblies separadas para isolamento

### 3. Documentação (`docs/`)

Documentação completa e organizada para facilitar contribuições.

**Guias Disponíveis:**
- **SETUP.md**: Como configurar o ambiente
- **CI-CD-GUIDE.md**: Funcionamento da pipeline
- **ARCHITECTURE.md**: Este arquivo

### 4. Templates do GitHub (`.github/`)

Templates padronizados para melhor organização.

**Tipos:**
- **Issue Templates**: Bug reports e feature requests
- **PR Template**: Estrutura de pull requests
- **Funding**: Configuração de doações (opcional)

## Fluxo de Trabalho

### Desenvolvimento Local

```
1. Clone do repositório
2. Configuração do Git LFS
3. Abertura no Unity Hub
4. Desenvolvimento de features
5. Execução de testes locais
6. Commit e push
```

### Pipeline Automática

```
1. Trigger (push/PR)
2. Checkout do código
3. Restauração de cache
4. Execução de testes
5. Build (se aprovado)
6. Deploy (se main)
7. Artefatos gerados
```

## Arquitetura de Testes

### Estrutura de Testes

```
Tests/
├── EditMode/              # Testes sem execução
│   └── [Scripts de teste]
└── PlayMode/              # Testes com execução (futuro)
    └── [Scripts de teste]
```

### Tipos de Testes

1. **EditMode Tests**
   - Testes unitários
   - Validação de lógica
   - Testes sem dependências de runtime

2. **PlayMode Tests** (planejado)
   - Testes de integração
   - Comportamento em runtime
   - Interações entre componentes

## Segurança

### Secrets Gerenciados

- `UNITY_LICENSE`: Licença do Unity
- `UNITY_EMAIL`: Email da conta Unity
- `UNITY_PASSWORD`: Senha da conta Unity
- `BUTLER_CREDENTIALS`: API key do Itch.io

### Boas Práticas

- Secrets nunca no código
- Rotação regular de credenciais
- Princípio do menor privilégio
- Git LFS para assets grandes

## Deploy

### Plataformas Suportadas

- **WebGL**: Itch.io (automatizado)
- **Outras plataformas**: Extensível via workflow

### Versionamento

- Versão automática via `github.run_number`
- Tags Git para releases

## Métricas e Monitoramento

### Disponíveis no GitHub Actions

- Tempo de execução de jobs
- Taxa de sucesso de testes
- Tamanho de artefatos
- Uso de cache

### Logs

- Logs de testes salvos como artefatos
- Logs de build acessíveis na pipeline
- Histórico completo no GitHub Actions

## Tecnologias e Dependências

### Core

- **Unity 6000.0.50f1**: Game engine
- **C#**: Linguagem de programação
- **Git LFS**: Gerenciamento de arquivos grandes

### CI/CD

- **GitHub Actions**: Plataforma de CI/CD
- **GameCI Actions**: Actions específicas para Unity
- **Butler**: Deploy no Itch.io

### Desenvolvimento

- **Unity Test Framework**: Framework de testes
- **Addressables**: Sistema de gerenciamento de assets
- **Universal Render Pipeline (URP)**: Pipeline de renderização

## Princípios de Design

### Código

- **SOLID**: Princípios de design orientado a objetos
- **DRY**: Don't Repeat Yourself
- **KISS**: Keep It Simple, Stupid
- **Separation of Concerns**: Responsabilidades bem definidas

### Documentação

- **Completa**: Tudo documentado
- **Atualizada**: Mantida sincronizada com código
- **Acessível**: Fácil de encontrar e entender
- **Exemplos**: Código de exemplo onde relevante

### CI/CD

- **Automatização**: Máximo possível automatizado
- **Feedback Rápido**: Testes rápidos
- **Confiabilidade**: Pipeline estável
- **Extensibilidade**: Fácil adicionar novos steps

## Manutenção

### Atualizações Regulares

- Unity Editor version
- Dependências de packages
- GitHub Actions versions
- Documentação

### Backlog Técnico

- Refatoração contínua
- Melhoria de cobertura de testes
- Otimizações de performance
- Atualização de dependências

## Recursos Adicionais

### Links Úteis

- [Unity Documentation](https://docs.unity3d.com/)
- [GitHub Actions Docs](https://docs.github.com/actions)
- [GameCI Documentation](https://game.ci/docs)
- [Git LFS](https://git-lfs.github.com/)

### Comunidade

- GitHub Issues para perguntas
- Pull Requests para contribuições

---

**Última atualização**: Outubro 2025  
**Versão**: 1.0
