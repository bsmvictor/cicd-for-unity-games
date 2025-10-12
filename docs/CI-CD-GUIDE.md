# Guia de CI/CD - GitHub Actions

Este guia detalha a pipeline de Integração Contínua (CI) e Entrega Contínua (CD) configurada para o projeto Unity utilizando GitHub Actions.

## 📑 Índice

- [Visão Geral](#visão-geral)
- [Configuração de Secrets](#configuração-de-secrets)
- [Estrutura da Pipeline](#estrutura-da-pipeline)
- [Jobs Detalhados](#jobs-detalhados)
- [Como Acessar Resultados](#como-acessar-resultados)
- [Troubleshooting](#troubleshooting)

## 🎯 Visão Geral

A pipeline automatiza três processos principais:
1. **Testes** - Validação automática do código
2. **Build** - Compilação para WebGL
3. **Deploy** - Publicação no Itch.io

### Triggers da Pipeline

A pipeline é acionada por:

```yaml
on:
  push:
    branches: [main]           # Push para branch principal
  pull_request:
    branches: [main]           # PRs direcionados à main
  workflow_dispatch:           # Execução manual
```

- **Push na main**: Executa testes, build e deploy
- **Pull Request**: Executa apenas testes (validação)
- **Manual**: Execução completa sob demanda

## 🔐 Configuração de Secrets

Para que a pipeline funcione, configure os seguintes secrets no GitHub:

### Como Adicionar Secrets

1. Acesse seu repositório no GitHub
2. Vá em `Settings` → `Secrets and variables` → `Actions`
3. Clique em `New repository secret`
4. Adicione cada secret conforme descrito abaixo

### Secrets Necessários

#### 1. `UNITY_LICENSE`

**Descrição**: Arquivo de licença completo do Unity (.ulf)

**Como obter**:

**Licença Pessoal/Estudantil**:
1. Abra o Unity Hub
2. Vá em `Preferences` → `Licenses`
3. Clique em `Manual Activation`
4. Siga as instruções para gerar o arquivo `.ulf`
5. Abra o arquivo com um editor de texto
6. Copie todo o conteúdo XML

**Licença Organizacional**:
- Solicite ao administrador da licença

**Formato esperado**:
```xml
<?xml version="1.0" encoding="UTF-8"?>
<root>
  <License id="...">
    <!-- Conteúdo completo do arquivo .ulf -->
  </License>
</root>
```

#### 2. `UNITY_EMAIL`

**Descrição**: Email da conta Unity

**Como obter**:
- Use o email associado à sua conta Unity

**Exemplo**:
```
seu.email@example.com
```

#### 3. `UNITY_PASSWORD`

**Descrição**: Senha da conta Unity

**Como obter**:
- Use a senha da sua conta Unity

⚠️ **Importante**: 
- Nunca compartilhe esta senha
- Use uma senha forte
- Considere usar senha específica para CI/CD

#### 4. `BUTLER_CREDENTIALS`

**Descrição**: API Key do Itch.io para deploy

**Como gerar**:
1. Acesse [itch.io](https://itch.io)
2. Faça login na sua conta
3. Vá em `Settings` → `API keys`
4. Clique em `Generate new API key`
5. Nomeie a chave (ex: "GitHub Actions")
6. Copie a chave imediatamente (ela não será exibida novamente)

**Formato**:
```
itch-xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
```

## 🏗️ Estrutura da Pipeline

A pipeline consiste em três jobs sequenciais:

```
┌─────────┐     ┌─────────┐     ┌─────────┐
│  TEST   │────>│  BUILD  │────>│ DEPLOY  │
└─────────┘     └─────────┘     └─────────┘
   ✓ ✓             ✓               ✓
EditMode        WebGL          Itch.io
PlayMode
```

## 📋 Jobs Detalhados

### Job 1: Test 🧪

**Objetivo**: Executar testes automatizados do Unity

**Quando executa**: 
- Sempre (push, PR, manual)

**Estratégia Matrix**:
```yaml
strategy:
  fail-fast: false
  matrix:
    testMode:
      - EditMode    # Testes em modo de edição
      - PlayMode    # Testes em modo de execução
```

**Passos**:

1. **Checkout do Repositório**
   ```yaml
   - uses: actions/checkout@v4
     with:
       lfs: true    # Habilita Git LFS
   ```

2. **Cache da Library**
   - Cacheia a pasta `Library` do Unity
   - Reduz tempo de importação de assets
   - Chave específica por modo de teste
   ```yaml
   - uses: actions/cache@v4
     with:
       path: UnityGame/Library
       key: Library-${{ runner.os }}-${{ matrix.testMode }}-...
   ```

3. **Execução dos Testes**
   ```yaml
   - uses: game-ci/unity-test-runner@v4
     with:
       projectPath: UnityGame
       unityVersion: 6000.0.50f1
       testMode: ${{ matrix.testMode }}
   ```

4. **Upload dos Resultados**
   - Sempre executa (sucesso ou falha)
   - Artefatos disponíveis para download
   ```yaml
   - uses: actions/upload-artifact@v4
     if: success() || failure()
   ```

**Artefatos Gerados**:
- `Resultados-Testes-EditMode`
- `Resultados-Testes-PlayMode`

### Job 2: Build 📦

**Objetivo**: Compilar o jogo para WebGL

**Quando executa**:
- Push na main
- Execução manual
- **NÃO executa em PRs** (economia de recursos)

**Condição**:
```yaml
if: github.event_name == 'push' && github.ref == 'refs/heads/main' 
    || github.event_name == 'workflow_dispatch'
```

**Passos**:

1. **Checkout e LFS**
   - Similar ao job de teste

2. **Liberar Espaço em Disco**
   ```yaml
   - uses: jlumbroso/free-disk-space@main
     with:
       android: true
       dotnet: true
       haskell: true
   ```
   - Remove ferramentas não utilizadas
   - Libera ~30GB de espaço
   - Essencial para builds do Unity

3. **Cache da Library (Build)**
   - Cache específico para WebGL
   - Inclui hash de `manifest.json` para invalidação
   ```yaml
   key: Library-UnityGame-WebGL-${{ hashFiles(...) }}
   ```

4. **Build WebGL**
   ```yaml
   - uses: game-ci/unity-builder@v4
     with:
       projectPath: UnityGame
       unityVersion: 6000.0.50f1
       targetPlatform: WebGL
   ```
   - Tempo médio: 15-30 minutos
   - Requer ~20GB de espaço

5. **Upload do Artefato**
   - Toda pasta `build/` é carregada
   - Disponível para o job de deploy

**Artefatos Gerados**:
- `build-WebGL` (pasta completa do jogo)

### Job 3: Deploy 🚀

**Objetivo**: Publicar o jogo no Itch.io

**Quando executa**:
- Após build bem-sucedido
- Apenas em push/manual (não em PRs)

**Dependência**:
```yaml
needs: build
```

**Passos**:

1. **Checkout**
   - Necessário para contexto do repositório

2. **Download do Artefato**
   ```yaml
   - uses: actions/download-artifact@v4
     with:
       name: build-WebGL
       path: build
   ```

3. **Deploy para Itch.io**
   ```yaml
   - uses: manleydev/butler-publish-itchio-action@v1.0.3
     env:
       BUTLER_CREDENTIALS: ${{ secrets.BUTLER_CREDENTIALS }}
       ITCH_GAME: tested-out
       ITCH_USER: bsmvictor
       CHANNEL: html5
       PACKAGE: build
       VERSION: ${{ github.run_number }}
   ```

**Configurações Importantes**:
- `ITCH_GAME`: Nome do jogo no Itch.io
- `ITCH_USER`: Seu username no Itch.io
- `CHANNEL`: Plataforma (`html5` para WebGL)
- `VERSION`: Versionamento automático

## 📊 Como Acessar Resultados

### No GitHub Actions

1. Acesse a aba `Actions` no repositório
2. Selecione a execução desejada
3. Visualize:
   - ✅ Status de cada job
   - 📝 Logs detalhados
   - 📦 Artefatos para download

### Artefatos Disponíveis

**Resultados de Testes**:
- Arquivo XML com resultados
- Logs de execução
- Screenshots de falhas (se houver)

**Build WebGL**:
- Pasta completa do jogo
- Pronta para hospedagem local
- Tamanho típico: 50-200MB

### No Itch.io

Após deploy bem-sucedido:
1. Acesse `https://itch.io/dashboard`
2. Selecione seu jogo
3. Vá em `Edit game` → `Uploads`
4. A nova versão estará listada

## 🐛 Troubleshooting

### Erro: "Unity License Invalid"

**Causa**: Secret `UNITY_LICENSE` incorreto ou expirado

**Solução**:
1. Verifique se copiou o arquivo `.ulf` completo
2. Gere nova licença se necessário
3. Atualize o secret no GitHub

### Erro: "Out of Disk Space"

**Causa**: Espaço insuficiente no runner

**Solução**:
- Verifique se o passo "Liberar Espaço em Disco" está ativado
- Considere reduzir tamanho de assets
- Use Addressables para assets grandes

### Erro: "Butler Push Failed"

**Causa**: Credenciais do Itch.io inválidas

**Solução**:
1. Verifique `BUTLER_CREDENTIALS`
2. Gere nova API key
3. Verifique `ITCH_GAME` e `ITCH_USER`

### Testes Falhando

**Diagnóstico**:
1. Baixe artefato de testes
2. Analise logs e resultados XML
3. Execute testes localmente

**Soluções Comuns**:
- Verifique configurações de teste em `ProjectSettings`
- Garanta que assembly definitions estão corretas
- Valide referências de assets nos testes

### Cache Não Funcionando

**Sintomas**: Builds muito lentas

**Solução**:
1. Limpe cache nas configurações do workflow
2. Verifique as chaves de cache
3. Force rebuild deletando cache antigo

## ⚙️ Customização

### Alterar Versão do Unity

No workflow, atualize:
```yaml
unityVersion: 6000.0.50f1    # Sua versão aqui
```

### Adicionar Plataformas

No job `build`, adicione:
```yaml
strategy:
  matrix:
    targetPlatform:
      - WebGL
      - StandaloneWindows64
      - StandaloneOSX
```

### Deploy em Múltiplas Plataformas

Ajuste job `deploy`:
```yaml
- name: Deploy Windows
  if: matrix.targetPlatform == 'StandaloneWindows64'
  # ... configuração específica
```

## 📚 Recursos Adicionais

- [GameCI Documentation](https://game.ci/docs)
- [GitHub Actions Docs](https://docs.github.com/actions)
- [Unity Manual - Build Settings](https://docs.unity3d.com/Manual/BuildSettings.html)
- [Butler (Itch.io CLI)](https://itch.io/docs/butler/)

## 🔄 Boas Práticas

1. **Sempre teste localmente antes do push**
2. **Use PRs para validação antes do merge**
3. **Monitore tempo de execução da pipeline**
4. **Mantenha secrets atualizados e seguros**
5. **Revise logs regularmente**
6. **Documente mudanças no workflow**

---

**Última atualização**: Outubro 2025
