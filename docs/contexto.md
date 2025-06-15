# Introdução

O avanço da tecnologia tem transformado os processos de contratação de serviços, aprimorando a eficiência e a
comunicação entre profissionais e clientes (Davenport & Short, 1990). No setor de serviços digitais, estratégias focadas
na experiência do usuário e na confiabilidade da plataforma são essenciais para garantir a satisfação e a fidelização
dos clientes. Neste contexto, marketplaces de freelancers desempenham um papel crucial ao conectar profissionais
autônomos a empresas e indivíduos que necessitam de serviços especializados (Parker, Van Alstyne & Choudary, 2016).

Sistemas de informação são fundamentais para otimizar a intermediação entre freelancers e clientes. Este trabalho propõe
o desenvolvimento de uma plataforma de freelancers que facilita a busca por profissionais qualificados, permitindo a
conexão entre eles sem intermediar a comunicação ou transações financeiras.

A plataforma será estruturada em três frentes principais:

- API – Responsável por toda a lógica de negócio, garantindo a integração entre os diferentes módulos da plataforma.
- Frontend Web – Interface para clientes e freelancers gerenciarem perfis e buscas.
- Aplicativo Mobile – Oferece acessibilidade e mobilidade para os usuários, permitindo a gestão dos serviços de forma
  eficiente.

Por meio dessa abordagem, o marketplace proporcionará confiabilidade, acessibilidade e versatilidade, garantindo um
ambiente gratuito e eficiente para freelancers e clientes.

#### Referências:

Davenport, T. H., & Short, J. E. (1990). The New Industrial Engineering: Information Technology and Business Process
Redesign. Sloan Management Review, 31(4), 11-27.
Margetts, H., & Dunleavy, P. (2013). The Impact of the Internet on Governance: A Review. Government & Opposition, 48(2),
225-253.
Parker, G. G., Van Alstyne, M. W., & Choudary, S. P. (2016). Platform Revolution: How Networked Markets Are Transforming
the Economy and How to Make Them Work for You. W.W. Norton & Company.

## Problema

A falta de uma plataforma eficiente para conectar freelancers e clientes pode levar a dificuldades na busca por
profissionais qualificados e na divulgação de serviços, resultando em oportunidades perdidas para ambos os lados.
Atualmente, muitos freelancers dependem de redes sociais e recomendações informais para captar clientes, um processo que
pode ser ineficiente e limitado. Sem uma estrutura centralizada e confiável, clientes enfrentam dificuldades para
encontrar prestadores de serviços adequados, e freelancers lutam para se destacar em um mercado competitivo.

## Objetivos

#### Objetivo Geral:

Desenvolver um sistema web de marketplace para freelancers, permitindo a conexão eficiente entre profissionais autônomos
e clientes, sem intermediar a comunicação ou a negociação entre as partes.

#### Objetivos Específicos:

- Desenvolver uma Plataforma Centralizada de Conexão – Criar um sistema web que permita a conexão entre freelancers e
  clientes de forma estruturada e intuitiva, facilitando o processo de busca por profissionais qualificados.
- Garantir Acessibilidade e Gratuidade – Disponibilizar a plataforma de forma gratuita para usuários, permitindo que
  freelancers e clientes se conectem sem custos adicionais.

## Justificativa

A criação desta plataforma é motivada pela necessidade de oferecer um ambiente centralizado e eficiente para a conexão
entre freelancers e clientes. A atual fragmentação do mercado, onde profissionais dependem de redes sociais e indicações
informais, limita a visibilidade e acessibilidade dos serviços prestados. Ao desenvolver um marketplace gratuito,
buscamos democratizar o acesso a oportunidades, promovendo a transparência e a confiança na contratação de serviços.

## Público-Alvo

O público-alvo da plataforma compreende dois grupos principais:

- **Freelancers:** Profissionais autônomos que desejam divulgar seus serviços e captar clientes de forma mais eficiente.
- **Contratantes:** Empresas e indivíduos que necessitam contratar serviços especializados e buscam um meio confiável
  para encontrar profissionais qualificados.

Esses usuários possuem diferentes níveis de familiaridade com tecnologia, sendo essencial oferecer uma interface
intuitiva e acessível, garantindo uma experiência fluida para todos.

## Especificações do Projeto

### Requisitos Funcionais

| ID     | Descrição do Requisito                                                                 | Prioridade | Responsáveis |
|--------|----------------------------------------------------------------------------------------|------------|--------------|
| RF-001 | Permitir a autenticação de usuários                                                    | ALTA       | A definir    |   
| RF-002 | Permitir o cadastro de freelancers                                                     | ALTA       | A definir    |   
| RF-003 | Permitir o cadastro de contratantes                                                    | ALTA       | A definir    |
| RF-004 | Permitir o cadastro dos projetos                                                       | ALTA       | A definir    |
| RF-005 | Permitir que o contratante edite e exclua seus projetos                                    | ALTA       | A definir    |
| RF-006 | Permitir a busca de projetos por necessidade do contratante (freelancer)               | ALTA       | A definir    |
| RF-007 | Possibilitar conexão entre contratante e freelancer por meio de projetos assumidos                     | MÉDIA      | A definir    |

### Requisitos Não Funcionais

| ID      | Descrição do Requisito                                                                     | Prioridade |
|---------|--------------------------------------------------------------------------------------------|------------|
| RNF-001 | O sistema deve ser responsivo para dispositivos móveis                                     | ALTA       |
| RNF-002 | O tempo de resposta para busca deve ser inferior a 2s                                      | MÉDIA      |
| RNF-003 | A plataforma deve garantir segurança na autenticação dos usuários                          | ALTA       |
| RNF-004 | O sistema deve ser intuitivo e fácil de usar, garantindo uma boa experiência aos usuários. | MÉDIA      |

## Restrições

O projeto está restrito pelos itens apresentados na tabela a seguir.

| ID      | Restrição                                                    |
|---------|--------------------------------------------------------------|
| RES-001 | Não haverá intermediação de pagamentos                       |
| RES-002 | A comunicação entre usuários será livre, sem intermediação   |
| RES-003 | Falta de capital monetário para o desenvolvimento do projeto |

## Catálogo de Serviços

### 1. Visão Geral

Este catálogo apresenta os serviços oferecidos pela plataforma de marketplace para freelancers, destacando as
funcionalidades disponíveis para freelancers e contratantes. O objetivo é proporcionar uma conexão eficiente entre
profissionais autônomos e clientes, sem intermediação de comunicação ou transações financeiras.

### 2. Serviços Disponíveis

#### 2.1 Cadastro e Gerenciamento de Perfis

- **Cadastro de Freelancers**: Permite que profissionais criem um perfil com suas informações, habilidades e portfólio.
- **Cadastro de Contratantes**: Empresas e indivíduos podem criar perfis para buscar freelancers.
- **Edição de Perfil**: Atualização de informações, incluindo descrição, habilidades e experiência.
- **Gerenciamento de Perfis**: Possibilidade de ativar ou desativar o perfil na plataforma.

#### 2.2 Busca e Filtros

- **Busca por Freelancers**: Contratantes podem buscar profissionais por nome, habilidade ou localização.
- **Busca por Projetos**: Freelancers podem pesquisar oportunidades de trabalho por categoria e descrição.
- **Filtros Avançados**: Permite refinar buscas por experiência, preço e disponibilidade.

#### 2.3 Publicação e Gerenciamento de Projetos

- **Criação de Projetos**: Contratantes podem cadastrar projetos com descrição detalhada e requisitos.
- **Edição de Projetos**: Permite atualizar informações dos projetos publicados.
- **Exclusão de Projetos**: Possibilidade de remover projetos da plataforma.

#### 2.4 Segurança e Acessibilidade

- **Autenticação Segura**: Login e registro por e-mail e senha, com suporte para autenticação de dois fatores.
- **Privacidade de Dados**: Proteção das informações dos usuários conforme normas de segurança.
- **Interface Responsiva**: Compatibilidade com dispositivos desktop e mobile.

### 3. Benefícios

- **Acesso Gratuito**: Plataforma sem cobrança para freelancers e contratantes.
- **Conexão Direta**: Comunicação aberta entre profissionais e clientes.
- **Eficiência**: Facilidade na busca e contratação de serviços especializados.

## Arquitetura da Solução

A solução será estruturada nos seguintes componentes:

1. **API** – Responsável por toda a lógica de negócio, garantindo a integração entre os diferentes módulos da
   plataforma.
2. **Frontend Web** – Interface para clientes e freelancers gerenciarem perfis e buscas.
3. **Aplicativo Mobile** – Oferece acessibilidade e mobilidade para os usuários, permitindo a gestão dos serviços de
   forma eficiente.

![arq](https://github.com/user-attachments/assets/b9402e05-8445-47c3-9d47-f11696e38a3d)

## Tecnologias Utilizadas

A implementação da solução utilizará as seguintes tecnologias:

- **Backend:** .NET Core para a API
- **Frontend Web:** React.js
- **Aplicativo Mobile:** React Native
- **Banco de Dados:** PostgreSQL
- **Hospedagem:** AWS

## Hospedagem

Explique como a hospedagem e o lançamento da plataforma foi feita.

> **Links Úteis**:
>
> - [Website com GitHub Pages](https://pages.github.com/)
> - [Programação colaborativa com Repl.it](https://repl.it/)
> - [Getting Started with Heroku](https://devcenter.heroku.com/start)
> - [Publicando Seu Site No Heroku](http://pythonclub.com.br/publicando-seu-hello-world-no-heroku.html)
