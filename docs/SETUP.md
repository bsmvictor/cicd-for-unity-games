# Guia de Configuração do Projeto

Este documento detalha as etapas necessárias para configurar o ambiente de desenvolvimento e executar o projeto localmente.

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

### Software Obrigatório

- **Unity Hub** (versão mais recente)
  - Download: [unity.com/download](https://unity.com/download)
- **Unity Editor 6000.0.50f1** (versão específica do projeto)
  - Instalar através do Unity Hub
- **Git**
  - Download: [git-scm.com](https://git-scm.com/)
- **Git LFS** (Large File Storage)
  - Download: [git-lfs.github.com](https://git-lfs.github.com/)

### Conhecimentos Recomendados

- Fundamentos de Unity e C#
- Conceitos básicos de Git e controle de versão
- Familiaridade com CI/CD (opcional, mas útil)

## 🚀 Instalação

### 1. Clonar o Repositório

```bash
# Clone o repositório
git clone https://github.com/bsmvictor/cicd-for-unity-games.git

# Entre no diretório do projeto
cd cicd-for-unity-games
```

### 2. Configurar Git LFS

O projeto utiliza Git LFS para gerenciar arquivos grandes (assets, texturas, etc.).

```bash
# Inicializar Git LFS
git lfs install

# Baixar os arquivos LFS
git lfs pull
```

### 3. Abrir o Projeto no Unity

1. Abra o **Unity Hub**
2. Clique em **"Add"** (Adicionar) ou **"Open"** (Abrir)
3. Navegue até a pasta `UnityGame` dentro do repositório clonado
4. Selecione a pasta e clique em **"Select Folder"** (Selecionar Pasta)
5. O Unity Hub detectará a versão necessária (6000.0.50f1)
   - Se você não tiver essa versão instalada, o Unity Hub oferecerá a opção de instalá-la
6. Clique no projeto para abri-lo no Unity Editor

### 4. Aguardar a Importação Inicial

Na primeira vez que você abrir o projeto:
- O Unity importará todos os assets
- Este processo pode levar alguns minutos
- Aguarde até que a barra de progresso no canto inferior direito seja concluída

## 🎮 Executando o Projeto

### No Unity Editor

1. Na janela **Project**, navegue até `Assets/Scenes`
2. Abra a cena `MainScene.unity`
3. Clique no botão **Play** (▶️) no topo do Editor
4. O jogo será executado na janela **Game**

### Build Local

Para gerar uma build do jogo:

1. No Unity Editor, vá em **File > Build Settings**
2. Selecione a plataforma desejada (ex: WebGL, Windows, etc.)
3. Clique em **Switch Platform** (se necessário)
4. Clique em **Build** ou **Build and Run**
5. Selecione a pasta de destino para a build

## 🧪 Executando Testes

### Testes no Unity Editor

1. Abra o **Test Runner**: **Window > General > Test Runner**
2. Selecione a aba **EditMode** ou **PlayMode**
3. Clique em **Run All** para executar todos os testes
4. Verifique os resultados na mesma janela

### Testes via Linha de Comando

```bash
# Navegar até a pasta do Unity
cd UnityGame

# Executar testes em EditMode
unity -runTests -batchmode -projectPath . -testPlatform EditMode -testResults results-editmode.xml

# Executar testes em PlayMode
unity -runTests -batchmode -projectPath . -testPlatform PlayMode -testResults results-playmode.xml
```

> **Nota**: O caminho do executável Unity pode variar dependendo do sistema operacional.

## ⚙️ Configuração do Ambiente de CI/CD

Se você deseja configurar o pipeline de CI/CD para seu próprio repositório, consulte o [Guia de CI/CD](CI-CD-GUIDE.md) para instruções detalhadas sobre como configurar os secrets do GitHub e personalizar a pipeline.

## 🐛 Solução de Problemas

### Problema: Unity não abre o projeto

**Solução:**
- Verifique se a versão correta do Unity (6000.0.50f1) está instalada
- Certifique-se de que o Git LFS foi configurado corretamente
- Tente executar `git lfs pull` novamente

### Problema: Arquivos faltando ou corrompidos

**Solução:**
```bash
# Verificar integridade do LFS
git lfs fsck

# Redownload de arquivos LFS
git lfs fetch --all
git lfs pull
```

### Problema: Erro de compilação no Unity

**Solução:**
- Verifique a janela **Console** no Unity para detalhes do erro
- Tente reimportar todos os assets: **Assets > Reimport All**
- Limpe o cache do Unity: feche o Unity e delete as pastas `Library` e `Temp` dentro de `UnityGame`

### Problema: Testes falhando

**Solução:**
- Certifique-se de que todas as dependências foram importadas corretamente
- Verifique se as configurações de teste estão corretas em **Project Settings > Player**
- Consulte os logs de erro no Test Runner para detalhes específicos

## 📚 Recursos Adicionais

- [Documentação Oficial do Unity](https://docs.unity3d.com/)
- [Unity Learn - Tutoriais](https://learn.unity.com/)
- [Git LFS - Documentação](https://git-lfs.github.com/)
- [GitHub Actions - Documentação](https://docs.github.com/actions)

## 💬 Suporte

Se você encontrar problemas não listados aqui:
1. Verifique as [Issues abertas](https://github.com/bsmvictor/cicd-for-unity-games/issues) no GitHub
2. Crie uma nova issue descrevendo o problema em detalhes
3. Consulte o [Guia de Contribuição](CONTRIBUTING.md) para mais informações

---

**Última atualização:** Outubro 2025
