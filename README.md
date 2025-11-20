# CI/CD for Unity Games

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Unity Version](https://img.shields.io/badge/Unity-6000.0.50f1-blue.svg)](https://unity.com/)
[![CI/CD Pipeline](https://github.com/bsmvictor/cicd-for-unity-games/actions/workflows/unity-tests.yml/badge.svg)](https://github.com/bsmvictor/cicd-for-unity-games/actions)
[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://unity3d.com)

> Projeto de Conclusão de Curso (TCC) demonstrando a implementação de pipelines CI/CD para desenvolvimento de jogos Unity utilizando GitHub Actions.

## Sobre o Projeto

Este repositório apresenta uma implementação completa e profissional de **Integração Contínua (CI)** e **Entrega Contínua (CD)** para projetos Unity. O sistema automatiza testes, builds e deploys, garantindo qualidade e agilidade no desenvolvimento de jogos.

### Objetivos

- Demonstrar boas práticas de CI/CD no desenvolvimento de jogos
- Automatizar processos de teste e validação de código
- Facilitar o deploy contínuo em plataformas de distribuição
- Servir como referência para desenvolvedores Unity
- Documentar todo o processo de forma educacional

### Características Principais

- **Testes Automatizados** - EditMode e PlayMode
- **Build Automatizado** - Compilação para WebGL
- **Deploy Contínuo** - Publicação automática no Itch.io
- **Cache Inteligente** - Otimização de tempo de build
- **Versionamento Automático** - Controle de versões por execução
- **Artefatos Persistentes** - Histórico de builds e testes

## Início Rápido

### Pré-requisitos

- Unity Hub e Unity Editor 6000.0.50f1
- Git e Git LFS instalados
- Conta GitHub (para CI/CD)
- Conta Itch.io (opcional, para deploy)

### Instalação

```bash
# Clone o repositório
git clone https://github.com/bsmvictor/cicd-for-unity-games.git

# Entre no diretório
cd cicd-for-unity-games

# Configure Git LFS
git lfs install
git lfs pull

# Abra o projeto no Unity Hub
# Pasta: UnityGame/
```

Para instruções detalhadas de instalação e configuração, consulte o [**Guia de Setup**](docs/SETUP.md).

## Documentação

A documentação completa está organizada na pasta `docs/`:

| Documento | Descrição |
|-----------|-----------|
| [**Setup Guide**](docs/SETUP.md) | Instalação e configuração do ambiente |
| [**CI/CD Guide**](docs/CI-CD-GUIDE.md) | Detalhamento completo da pipeline |
| [**Architecture**](docs/ARCHITECTURE.md) | Arquitetura e estrutura do projeto |
| [**Contributing**](CONTRIBUTING.md) | Guia de contribuição para o projeto |

## Estrutura do Projeto

```
cicd-for-unity-games/
├── .github/
│   └── workflows/
│       └── unity-tests.yml          # Pipeline CI/CD principal
├── docs/
│   ├── images/                      # Imagens da documentação
│   ├── SETUP.md                     # Guia de instalação
│   └── CI-CD-GUIDE.md               # Guia detalhado de CI/CD
├── UnityGame/                       # Projeto Unity principal
│   ├── Assets/
│   │   ├── Scripts/                 # Scripts C# do jogo
│   │   ├── Scenes/                  # Cenas do Unity
│   │   ├── Prefabs/                 # GameObjects pré-configurados
│   │   ├── Tests/                   # Testes automatizados
│   │   └── ...
│   ├── Packages/                    # Dependências do projeto
│   └── ProjectSettings/             # Configurações do Unity
├── .gitignore                       # Arquivos ignorados pelo Git
├── LICENSE                          # Licença MIT
├── README.md                        # Este arquivo
└── CONTRIBUTING.md                  # Guia de contribuição
```

## Pipeline CI/CD

A pipeline é executada automaticamente em três situações:

1. **Push na branch `main`** - Execução completa (teste, build, deploy)
2. **Pull Requests** - Apenas testes (validação)
3. **Manual** - Execução sob demanda

### Fluxo de Trabalho

```
┌─────────────┐
│  Push/PR    │
└──────┬──────┘
       │
       ▼
┌─────────────┐
│  Checkout   │
└──────┬──────┘
       │
       ├──────────────────┐
       │                  │
       ▼                  ▼
┌──────────────┐   ┌──────────────┐
│ Test         │   │ Test         │
│ (EditMode)   │   │ (PlayMode)   │
└──────┬───────┘   └──────┬───────┘
       │                  │
       └────────┬─────────┘
                │
                ▼
         ┌──────────────┐
         │  Sucesso?    │
         └──────┬───────┘
                │
                ├─── Não ──→ [Falha]
                │
                ▼ Sim
         ┌──────────────┐
         │ Build WebGL  │
         └──────┬───────┘
                │
                ▼
         ┌──────────────┐
         │ Branch main? │
         └──────┬───────┘
                │
                ├─── Não ──→ [Fim]
                │
                ▼ Sim
         ┌──────────────┐
         │Deploy Itch.io│
         └──────────────┘
```

### Jobs da Pipeline

| Job | Descrição | Duração Média |
|-----|-----------|---------------|
| **Test** | Executa testes EditMode e PlayMode | 5-10 min |
| **Build** | Compila o jogo para WebGL | 15-30 min |
| **Deploy** | Publica no Itch.io | 2-5 min |

Para configuração detalhada dos secrets e funcionamento da pipeline, consulte o [**Guia de CI/CD**](docs/CI-CD-GUIDE.md).

## Testes

O projeto utiliza o Unity Test Framework para garantir qualidade do código:

### Executar Testes Localmente

**No Unity Editor:**
1. Abra `Window` → `General` → `Test Runner`
2. Selecione `EditMode` ou `PlayMode`
3. Clique em `Run All`

**Via Linha de Comando:**
```bash
# EditMode
unity -runTests -batchmode -projectPath UnityGame -testPlatform EditMode

# PlayMode
unity -runTests -batchmode -projectPath UnityGame -testPlatform PlayMode
```

### Cobertura de Testes

Os testes estão organizados em:
- **EditMode**: Testes unitários de lógica de jogo
- **PlayMode**: Testes de integração e comportamento em runtime

## Contribuindo

Contribuições são bem-vindas! Este é um projeto educacional e toda ajuda é valiosa.

### Como Contribuir

1. Fork o repositório
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'feat: Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

Leia o [**Guia de Contribuição**](CONTRIBUTING.md) para detalhes sobre nosso código de conduta e processo de submissão.

## Tecnologias Utilizadas

- **Unity 6000.0.50f1** - Game Engine
- **C#** - Linguagem de programação
- **GitHub Actions** - CI/CD Pipeline
- **GameCI** - Actions para Unity
- **Git LFS** - Gerenciamento de arquivos grandes
- **Butler** - CLI para deploy no Itch.io

## Status do Projeto

- Pipeline CI/CD funcional
- Testes automatizados implementados
- Deploy automático para Itch.io
- Documentação completa
- Em desenvolvimento contínuo

## Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.

## Autor

**Victor Boaventura**

- GitHub: [@bsmvictor](https://github.com/bsmvictor)
- Itch.io: [bsmvictor](https://bsmvictor.itch.io/)

## Agradecimentos

- [GameCI](https://game.ci/) - Por fornecer actions para Unity
- [Unity Technologies](https://unity.com/) - Pela game engine
- [GitHub](https://github.com/) - Por fornecer infraestrutura de CI/CD gratuita
- [Itch.io](https://itch.io/) - Pela plataforma de distribuição

## Contato e Suporte

- **Issues**: Para reportar bugs ou solicitar features
- **Discussions**: Para discussões gerais e perguntas
- **Pull Requests**: Para contribuir com código

## Links Úteis

- [Jogar no Itch.io](https://bsmvictor.itch.io/unity-tcc-game)
- [Documentação do Unity](https://docs.unity3d.com/)
- [GameCI Documentation](https://game.ci/docs)
- [GitHub Actions Docs](https://docs.github.com/actions)

---

<div align="center">

**Feito com dedicação para a comunidade de desenvolvimento de jogos**

Se este projeto foi útil, considere dar uma estrela!

</div>
