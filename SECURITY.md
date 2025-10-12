# Política de Segurança

## Reportando uma Vulnerabilidade

A segurança deste projeto é levada a sério. Se você descobrir uma vulnerabilidade de segurança, por favor, siga as diretrizes abaixo.

### NÃO

- **NÃO** abra uma issue pública sobre a vulnerabilidade
- **NÃO** divulgue a vulnerabilidade publicamente até que seja corrigida
- **NÃO** tente explorar a vulnerabilidade além do necessário para demonstrá-la

### FAÇA

1. **Reporte privadamente** enviando um email para o mantenedor do projeto
2. **Inclua detalhes** suficientes para reproduzir a vulnerabilidade
3. **Aguarde resposta** antes de divulgar publicamente
4. **Colabore** com os mantenedores para resolver o problema

## Como Reportar

Para reportar uma vulnerabilidade de segurança, utilize um dos seguintes métodos:

### GitHub Security Advisory (Recomendado)

1. Vá para a aba **Security** do repositório
2. Clique em **Report a vulnerability**
3. Preencha o formulário com os detalhes
4. Envie o relatório

### Email Direto

Se preferir contato direto, você pode reportar através do GitHub criando uma issue privada ou entrando em contato através de outras formas de comunicação estabelecidas no perfil do mantenedor.

## Informações a Incluir

Ao reportar uma vulnerabilidade, inclua:

### Informações Essenciais

- **Descrição**: Descrição clara da vulnerabilidade
- **Impacto**: Qual é o impacto potencial?
- **Tipo**: Que tipo de vulnerabilidade é? (ex: XSS, SQL Injection, etc.)
- **Localização**: Onde no código a vulnerabilidade existe?

### Reprodução

- **Passos**: Passos detalhados para reproduzir
- **Código POC**: Proof of Concept (se aplicável)
- **Screenshots**: Capturas de tela ou vídeos demonstrando o problema
- **Logs**: Logs relevantes ou mensagens de erro

### Ambiente

- **Versão do Unity**: Qual versão do Unity você estava usando?
- **Versão do Projeto**: Qual versão/commit do projeto?
- **Sistema Operacional**: SO onde foi descoberto
- **Configuração**: Qualquer configuração específica relevante

## Tempo de Resposta

Você pode esperar:

- **Confirmação inicial**: Dentro de 48 horas
- **Avaliação de severidade**: Dentro de 5 dias úteis
- **Plano de ação**: Dentro de 10 dias úteis
- **Correção**: Dependendo da severidade e complexidade

## Processo de Resolução

1. **Recebimento**: Recebemos e confirmamos o relatório
2. **Triagem**: Avaliamos a severidade e impacto
3. **Desenvolvimento**: Desenvolvemos uma correção
4. **Teste**: Testamos a correção completamente
5. **Release**: Lançamos a correção em uma nova versão
6. **Divulgação**: Publicamos detalhes após a correção

## Reconhecimento

Agradecemos a pesquisadores de segurança que reportam vulnerabilidades de forma responsável. Com sua permissão, você será:

- Creditado em notas de release
- Mencionado na documentação de segurança
- Adicionado ao hall da fama de segurança (se houver)

## Severidade

Classificamos vulnerabilidades usando a seguinte escala:

### Crítica
- Comprometimento completo do sistema
- Execução remota de código
- Acesso não autorizado a dados sensíveis
- **SLA**: Correção em até 7 dias

### Alta
- Acesso parcial não autorizado
- Exposição de dados não sensíveis
- Bypass de autenticação em cenários específicos
- **SLA**: Correção em até 14 dias

### Média
- Problemas que requerem condições específicas
- Vazamento de informações limitadas
- Vulnerabilidades que requerem interação do usuário
- **SLA**: Correção em até 30 dias

### Baixa
- Problemas menores de segurança
- Melhores práticas não seguidas
- Configurações inseguras opcionais
- **SLA**: Correção em release futuro

## Boas Práticas de Segurança

### Para Contribuidores

- Nunca commite secrets (API keys, senhas, tokens)
- Use GitHub Secrets para informações sensíveis
- Revise código para vulnerabilidades comuns
- Mantenha dependências atualizadas
- Siga princípio do menor privilégio

### Para Usuários

- Mantenha o Unity e dependências atualizadas
- Use senhas fortes para contas de serviço
- Rotacione regularmente API keys e tokens
- Revise permissões de acesso
- Monitore logs de acesso e atividades suspeitas

## Recursos de Segurança

### Unity Security

- [Unity Security Best Practices](https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity.html)
- [Unity Cloud Security](https://unity.com/solutions/gaming-services)

### GitHub Actions Security

- [Security Hardening for GitHub Actions](https://docs.github.com/actions/security-guides/security-hardening-for-github-actions)
- [Encrypted Secrets](https://docs.github.com/actions/security-guides/encrypted-secrets)

### Geral

- [OWASP Top 10](https://owasp.org/www-project-top-ten/)
- [CWE Common Weakness Enumeration](https://cwe.mitre.org/)

## Política de Divulgação

Seguimos a **divulgação coordenada**:

1. Vulnerabilidade reportada privadamente
2. Correção desenvolvida e testada
3. Patch lançado para usuários
4. Período de graça de 90 dias para adoção
5. Divulgação pública completa após período

## Escopo

Esta política de segurança cobre:

- Código do projeto Unity
- Scripts de CI/CD
- Configurações do GitHub Actions
- Dependências diretas do projeto
- Documentação que possa causar problemas de segurança

Não cobre:

- Vulnerabilidades em dependências de terceiros (reporte ao upstream)
- Problemas de infraestrutura do GitHub/Itch.io
- Ataques de engenharia social
- Problemas já conhecidos e documentados

## Histórico de Segurança

Nenhuma vulnerabilidade reportada até o momento.

## Agradecimentos

Agradecemos a todos os pesquisadores de segurança que contribuem para manter este projeto seguro.

---

**Última atualização**: Outubro 2025  
**Versão da política**: 1.0
