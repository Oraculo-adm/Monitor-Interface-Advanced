
# Monitor Interface Advanced (MIA) 🖥️📡

**MIA** é uma aplicação avançada de monitoramento de interfaces de rede desenvolvida em **.NET 8 + WPF**, com foco em modularidade, desempenho, compatibilidade visual com o Windows 10+ e expansibilidade futura. Foi idealizada e projetada para uso profissional e técnico por administradores de rede, analistas de infraestrutura e entusiastas que necessitam de uma solução leve, visualmente integrada ao Windows e sem dependências externas manuais.

---

## 🚀 Funcionalidades principais

- Monitoramento de **interfaces de rede locais**
- Ping automatizado ao **gateway** e à **internet** (Google)
- **Gráficos visuais em tempo real** do desempenho por interface
- **Carregamento dinâmico de módulos** via `/modules`
- Interface 100% compatível com **tema escuro/claro** e **Aero/Transparência**
- Menu modular com **inserção de funções por módulos**
- Gerenciamento completo de dependências entre módulos
- Base preparada para futuras expansões: DNS, rotas, portas, exportações, etc.

---

## 🧩 Arquitetura modular

A aplicação é dividida em camadas e módulos:

### 🧠 Núcleo:
- `MIA.Core`: Carrega módulos, gerencia a interface, controla dependências
- `UIManager`: Permite que módulos modifiquem a UI dinamicamente sem tocar no MainWindow

### 🧱 Projetos:
- `MIA.UI`: Interface WPF base (não modificada diretamente pelos módulos)
- `MIA.Shared`: Utilitários e tipos comuns

### 📦 Módulos principais:
- `NetInterfaces`: Coleta IP, gateway, MAC e tipo de todas interfaces
- `PingMonitor`: Executa ping no gateway e Google por interface
- `Graphic`: Gera gráficos visuais coloridos por desempenho
- `Routes`: Estrutura inicial para testes de rota (em desenvolvimento)

> ⚠️ Cada módulo possui um arquivo `module.json` com:
> - Nome, versão, autor
> - Ponto de entrada
> - Dependências
> - Instruções para a UI (`ui.submenu`, `ui.inject`)
> - Repositório

---

## 🧑‍💻 Desenvolvimento

Este projeto foi 99% desenvolvido pelo **ChatGPT-4 (GPT-4o)** com auxílio direto do usuário [Oraculo-adm](https://github.com/Oraculo-adm), que idealizou, testou, validou e organizou cada etapa.

Todo o código foi gerado com base em **prompts técnicos e extremamente específicos**, visando qualidade, organização, escalabilidade e performance.

> O arquivo `prompt.gpt` incluso no repositório contém o **prompt completo** utilizado para gerar a aplicação, linha por linha.

---

## 🛠️ Requisitos

- Windows 10 ou superior (x64)
- .NET 8.0 SDK ou superior
- Visual Studio ou Visual Studio Code

---

## ⚙️ Como executar

1. Clone o repositório
2. Restaure os pacotes:
   ```bash
   dotnet restore
   ```
3. Compile:
   ```bash
   dotnet build
   ```
4. Execute:
   ```bash
   dotnet run --project MIA.UI
   ```

---

## 🧩 Adicionando novos módulos

- Coloque a pasta do módulo dentro de `/modules`
- Certifique-se de que o `module.json` está presente e correto
- Ao iniciar o app, o módulo será carregado se estiver com dependências resolvidas
- Módulos podem criar janelas, menus, gráficos e mais

---

## 📬 Futuro

- Empacotamento automático com instalação sem dependências manuais
- Atualizações via GitHub ou Microsoft Store
- Módulo de alertas e notificações
- Monitoramento remoto via agente + painel

---

## 📘 Licença

Este projeto está em desenvolvimento e será futuramente licenciado de forma open source.

---
Criado com amor, foco e GPT.
