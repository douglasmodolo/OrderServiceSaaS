# 📜 Guia de Conventional Commits

Este projeto segue a especificação **Conventional Commits**. Este padrão define um conjunto de regras para criar um histórico de commits explícito, o que facilita a automação de ferramentas de versionamento e geração de *changelogs*.

---

## 1. Tipos Comuns (`<type>`)

O `<type>` é **obrigatório** e define a natureza da mudança.

| Tipo | Descrição | Exemplo | Impacto no Versionamento |
| :--- | :--- | :--- | :--- |
| **feat** | Uma nova funcionalidade (Feature) | `feat(orders): Implement read-only order service query` | MINOR (Nova funcionalidade) |
| **fix** | Correção de um bug | `fix(di): Register IOrderServiceRepository correctly` | PATCH (Correção de erro) |
| **docs** | Mudanças apenas na Documentação | `docs(readme): Update commit guidelines` | Nenhuma |
| **refactor** | Refatoração de código sem mudar o comportamento | `refactor(domain): Simplify BaseEntity class` | Nenhuma |
| **style** | Mudanças de formatação (espaços, ponto e vírgula, etc.) | `style(api): Apply consistent bracing style` | Nenhuma |
| **test** | Adicionando ou corrigindo Testes | `test(handlers): Add unit tests for AuthHandler` | Nenhuma |
| **chore** | Tarefas de manutenção (configurações, scripts, pacotes) | `chore(deps): Update MediatR package to latest` | Nenhuma |
| **perf** | Melhoria de Performance | `perf(query): Use AsNoTracking for query operations` | Nenhuma |

---

## 2. Escopo Opcional (`<scope>`)

O `<scope>` é **opcional** e fornece um contexto para a mudança. Use o nome do projeto ou da camada afetada.

> **Exemplos de Escopo:** `api`, `application`, `infrastructure`, `crosscutting`, `auth`, `orders`.

**Exemplo:** `feat(orders): Implement read-only order service query`

---

## 3. Mensagem do Corpo e Breaking Changes

### Mensagem do Corpo (Body)

Use a seção `body` opcional para fornecer detalhes contextuais adicionais sobre a mudança, explicando o "**porquê**" da mudança.

### Quebra de Compatibilidade (BREAKING CHANGE)

Se o commit introduzir uma mudança que **quebra a compatibilidade** (isto é, exige uma mudança de código do lado do consumidor da API ou de uma biblioteca), ele deve ser sinalizado. Isso geralmente aciona uma atualização de versão **MAJOR**.

* **Sinalização no Tipo (Recomendado):** Use o `!` após o `<type>` ou `(<scope>)`.

| Exemplo de Commit | Tipo de Mudança |
| :--- | :--- |
| `feat(api)!: Remove client V1 endpoints` | Major Version (**MAJOR**) |
| `fix: Correct typo in DbContext` | Patch Version (**PATCH**) |

---

## Exemplos de Commits Válidos

feat(orders): Adiciona endpoint POST para criar Ordens de Serviço

A criação de OS agora é validada pelo ValidationBehaviour para garantir a presença do TenantId.


fix(deps): Corrige erro de ambiguidade do FluentValidation

O namespace OS.Domain.Exceptions estava conflitando com o namespace do FluentValidation. Foi utilizado o nome completo (Fully Qualified Name) no ValidationBehaviour para resolver.


refactor(infra): Simplifica a injeção do TenantContext


---

## Fluxo de Trabalho com Pull Requests (PRs)

1.  **Criar Branch:** Inicie o desenvolvimento em uma branch com prefixo: `feat/`, `fix/`, ou `chore/`. (Ex: `git checkout -b feat/order-service-query`).
2.  **Desenvolver e Commitar:** Use os padrões acima.
3.  **Abrir PR:** Abra um Pull Request para a branch `main`. O **título da PR deve seguir o padrão** de Conventional Commits (Ex: `feat(orders): Implement read-only query flow`).
4.  **Merge:** O merge só deve ser feito após a revisão e aprovação.
