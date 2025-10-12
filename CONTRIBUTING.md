# Guia de Contribuição

Obrigado pelo seu interesse em contribuir para este projeto! Este documento fornece diretrizes e boas práticas para contribuições.

## Código de Conduta

Este projeto adere a um código de conduta profissional. Ao participar, você concorda em:
- Ser respeitoso e inclusivo com todos os colaboradores
- Aceitar feedback construtivo de forma profissional
- Focar no que é melhor para a comunidade e o projeto

## Como Contribuir

### Reportando Bugs

Se você encontrou um bug:

1. **Verifique as Issues existentes** para evitar duplicatas
2. **Crie uma nova Issue** com as seguintes informações:
   - Título claro e descritivo
   - Descrição detalhada do bug
   - Passos para reproduzir o problema
   - Comportamento esperado vs. comportamento atual
   - Screenshots ou vídeos (se aplicável)
   - Ambiente (SO, versão do Unity, etc.)

### Sugerindo Melhorias

Para sugerir novas funcionalidades ou melhorias:

1. **Abra uma Issue** com a tag `enhancement`
2. Descreva claramente:
   - O problema que a funcionalidade resolve
   - A solução proposta
   - Alternativas consideradas
   - Impacto no projeto atual

### Enviando Pull Requests

#### Antes de começar

1. **Fork o repositório** para sua conta GitHub
2. **Clone seu fork** localmente:
   ```bash
   git clone https://github.com/seu-usuario/cicd-for-unity-games.git
   ```
3. **Configure o remote upstream**:
   ```bash
   git remote add upstream https://github.com/bsmvictor/cicd-for-unity-games.git
   ```

#### Desenvolvendo sua contribuição

1. **Crie uma branch** para sua feature ou correção:
   ```bash
   git checkout -b feature/nome-da-feature
   # ou
   git checkout -b fix/nome-do-bug
   ```

2. **Faça suas alterações** seguindo as diretrizes de código (veja abaixo)

3. **Teste suas alterações**:
   - Execute todos os testes existentes
   - Adicione novos testes para suas mudanças
   - Verifique se a build está funcionando

4. **Commit suas alterações** com mensagens claras:
   ```bash
   git commit -m "feat: adiciona nova funcionalidade X"
   # ou
   git commit -m "fix: corrige bug no sistema Y"
   ```

#### Convenção de Commits

Seguimos o padrão [Conventional Commits](https://www.conventionalcommits.org/):

- `feat:` - Nova funcionalidade
- `fix:` - Correção de bug
- `docs:` - Alterações na documentação
- `style:` - Formatação, ponto e vírgula faltando, etc.
- `refactor:` - Refatoração de código
- `test:` - Adição ou correção de testes
- `chore:` - Atualizações de tarefas de build, configurações, etc.

Exemplos:
```bash
git commit -m "feat: adiciona sistema de pontuação"
git commit -m "fix: corrige movimento do jogador em superfícies inclinadas"
git commit -m "docs: atualiza guia de instalação"
git commit -m "test: adiciona testes para PlayerController"
```

#### Enviando o Pull Request

1. **Atualize sua branch** com as últimas mudanças:
   ```bash
   git fetch upstream
   git rebase upstream/main
   ```

2. **Push para seu fork**:
   ```bash
   git push origin feature/nome-da-feature
   ```

3. **Abra um Pull Request** no GitHub:
   - Título claro e descritivo
   - Descrição detalhada das mudanças
   - Referência a Issues relacionadas (se aplicável)
   - Screenshots ou GIFs demonstrando as mudanças (se aplicável)

4. **Aguarde a revisão**:
   - Responda aos comentários e sugestões
   - Faça as alterações solicitadas
   - Mantenha a discussão profissional e construtiva

## Diretrizes de Código

### C# / Unity

1. **Nomenclatura**:
   - Classes e métodos: `PascalCase`
   - Variáveis privadas: `camelCase` ou `_camelCase`
   - Constantes: `UPPER_CASE`
   - Interfaces: prefixo `I` (ex: `IMovable`)

2. **Organização**:
   - Um comportamento (MonoBehaviour) por arquivo
   - Organize scripts em namespaces apropriados
   - Use regiões para agrupar código relacionado

3. **Boas Práticas**:
   - Comente código complexo
   - Evite "magic numbers" - use constantes
   - Prefira composição sobre herança
   - Use SerializeField em vez de campos públicos

4. **Performance**:
   - Evite `Find()` e `GetComponent()` em Update()
   - Use Object Pooling para objetos frequentemente instanciados
   - Cache referências de componentes

### Testes

1. **Coverage**: Sempre adicione testes para novas funcionalidades
2. **Nomenclatura**: Use nomes descritivos (ex: `PlayerJump_WhenOnGround_IncreasesYVelocity`)
3. **Isolamento**: Cada teste deve ser independente
4. **Asserções**: Use asserções claras e específicas

### Commits e Branches

1. **Commits pequenos e focados**: Um commit = uma alteração lógica
2. **Mensagens descritivas**: Explique o "por quê" das mudanças
3. **Branches organizadas**: Use prefixos `feature/`, `fix/`, `hotfix/`

## Processo de Revisão

### O que esperamos em uma revisão:

1. **Código limpo e legível**
2. **Testes passando** na pipeline de CI
3. **Documentação atualizada** (se aplicável)
4. **Sem conflitos** com a branch main
5. **Performance considerada**

### Timeline

- **Revisão inicial**: Dentro de 1-3 dias úteis
- **Feedback**: Iterações podem levar alguns dias
- **Merge**: Após aprovação e testes passando

## Estrutura do Projeto

```
cicd-for-unity-games/
├── .github/
│   └── workflows/       # GitHub Actions workflows
├── docs/                # Documentação do projeto
├── UnityGame/           # Projeto Unity principal
│   ├── Assets/
│   │   ├── Scripts/     # Scripts C#
│   │   ├── Scenes/      # Cenas do jogo
│   │   ├── Prefabs/     # Prefabs
│   │   └── Tests/       # Testes automatizados
│   ├── Packages/        # Dependências do projeto
│   └── ProjectSettings/ # Configurações do Unity
├── .gitignore
├── LICENSE
└── README.md
```

## Checklist de Pull Request

Antes de enviar seu PR, verifique:

- [ ] Código segue as diretrizes de estilo
- [ ] Testes foram adicionados/atualizados
- [ ] Todos os testes passam localmente
- [ ] Documentação foi atualizada (se necessário)
- [ ] Commits seguem a convenção especificada
- [ ] Branch está atualizada com main
- [ ] Sem conflitos com a branch principal
- [ ] CI/CD pipeline está passando

## Áreas Prioritárias

Contribuições são bem-vindas em todas as áreas, mas estas são especialmente valiosas:

1. **Testes**: Aumentar cobertura de testes
2. **Documentação**: Melhorias e traduções
3. **Performance**: Otimizações
4. **Acessibilidade**: Melhorias de UI/UX
5. **CI/CD**: Melhorias na pipeline

## Perguntas

Se você tiver dúvidas sobre como contribuir:

1. Verifique a [documentação](docs/)
2. Procure em [Issues fechadas](https://github.com/bsmvictor/cicd-for-unity-games/issues?q=is%3Aissue+is%3Aclosed)
3. Abra uma Issue com a tag `question`

## Agradecimentos

Agradecemos a todos que contribuem para tornar este projeto melhor!

---

**Obrigado por contribuir!**
