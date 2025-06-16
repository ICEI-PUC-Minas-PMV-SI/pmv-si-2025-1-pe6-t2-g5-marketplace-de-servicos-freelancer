# Front-end Móvel

Nosso aplicativo móvel foi projetado para ser um portal intuitivo e de fácil acesso, unindo contratantes que buscam desenvolver projetos e freelancers em busca de novas oportunidades. Acreditamos na democratização do acesso ao trabalho remoto, eliminando as barreiras de entrada e conectando talentos a demandas de forma eficiente.

Desenvolvido em React Native, nosso app garante compatibilidade tanto com dispositivos Android quanto iOS, permitindo que você gerencie seus projetos ou encontre trabalho de onde estiver. Nosso foco primordial é a experiência do usuário, unindo usabilidade com velocidade de processamento e criptografia dos dados.

## Projeto da Interface

A aplicação foi desenvolvida pensando em uma experiência fluída, garantindo uma navegação intuitiva e acessível para público geral.
Sendo então as páginas principais:
- Login;
- Registro;
- Tela de Meus projetos (contratante);
- Tela de Solicitar novos serviços (Contratante);
- Tela de Dashboard de projetos disponíveis (freelancer);
- Modal de comunicação por Whatsaapp (contratante).

### Wireframes

- Tela de login e registro, respectivamente:

![image](https://github.com/user-attachments/assets/07b8aec0-7ab4-462f-a894-bb95aec66c76)
![image](https://github.com/user-attachments/assets/ba717887-e1e1-4997-8430-0956788fca1a)

- Tela de Meus projetos (contratante) e Tela de Solicitar novos serviços (Contratante), respectivamente;

![image](https://github.com/user-attachments/assets/e3660f06-bafc-447c-bf5b-4b9d404aa90e)
![image](https://github.com/user-attachments/assets/0fd22e18-8c10-4839-9545-5a391d500907)

- Tela de Dashboard de projetos disponíveis (freelancer) e Modal de comunicação por Whatsaapp (contratante), respectivamente:

![image](https://github.com/user-attachments/assets/f52a5b41-8ca2-4825-9ad9-38bc74619b52)
![image](https://github.com/user-attachments/assets/7f5aa74f-cf52-4b49-a545-e5f1076b63e0)



### Design Visual

Esquema de cores:
- Roxo com duas variações sutis (#a855f7 e #9333ea): Usado em títulos, botões de destaque (a habilitados) e fundo de páginas.
- Cinza (#6b7280): Aplicado em botões desabilitados.
- Branco: Fundo dos formulários e campos de entrada.
- Preto: Textos simples como informação do modal de comunicação entre freelancer e contratante.

Elementos:
- Nossos elementos de interface são dimensionados para facilitar o toque e a interação.
- Apresentamos formulários e informações centralizadas verticalmente.

## Fluxo de Dados

[Diagrama ou descrição do fluxo de dados na aplicação.]

## Tecnologias Utilizadas
- React Native;
- Tailwind: Framework de estrutura CSS;
- Expo: Ambiente para desenvolvimento e build;
- React Navigation: Transição navegável entre telas no react;
- Ngrok: Expondo API para viabilizar testes;
- TypeScript: Linguagem de programação utilizada no front.

## Considerações de Segurança
Autenticação e Gestão de Acesso:
- Validação de Sessão com JWT: Utilizamos JSON Web Tokens (JWT) para gerenciar as sessões dos usuários. Isso assegura que cada sessão seja autenticada, tenha uma validade controlada e expiração definida, minimizando riscos de acesso não autorizado.
- Armazenamento Seguro de Tokens: Para proteção contra exposições inadvertidas, os tokens de autenticação são armazenados de forma criptografada no AsyncStorage do dispositivo móvel.
Proteção de Dados Sensíveis.

Criptografia de Credenciais: 
- As informações de senha e confirmação de senha são submetidas a um processo robusto de criptografia antes de qualquer transmissão ou armazenamento, garantindo a confidencialidade e integridade das suas credenciais.

Compromisso com a Segurança e Atualização Tecnológica:


- Garantimos que todas as tecnologias, pacotes e bibliotecas empregados em nosso projeto são meticulosamente mantidos em suas versões recentes. Essa prática assegura a incorporação das últimas atualizações de segurança,

## Implantação

Sistema Operacional:
- Ubuntu 22.04 LTS (ou Amazon Linux 2023)

Instância EC2 mínima recomendada:
- t3.medium (2 vCPUs, 4 GB RAM)

Armazenamento:
- 20 GB SSD (gp3 ou gp2)

Runtime:
- .NET 8 (ou 7, dependendo do seu projeto)

Servidor Web:
- Nginx (como reverse proxy) ou usar o próprio Kestrel direto (se quiser simplificar)

Banco de dados:
- Amazon RDS (PostgreSQL)

Frontend Mobile:
- O app será compilado e distribuído via Expo ou Google Play / App Store
- Expo CLI instalado
- Conta no Expo
- EAS Build

## Testes

[Descreva a estratégia de teste, incluindo os tipos de teste a serem realizados (unitários, integração, carga, etc.) e as ferramentas a serem utilizadas.]

### Requisitos Funcionais

| ID     | Cenários de testes                                                            | Tipo | Resultado Esperado | Evidência |
|--------|----------------------------------------------------------------------------------------|------------|--------------|--------------|
| CT-001 | Registro de usuário com dados válidos     (RF-002)                                               | Unitário/Integração    | O usuário criado com os dados armazenados   | [EV-001](https://github.com/user-attachments/assets/998f436c-f107-470c-af76-25ab277ae9b5)              |    
| CT-002 | Autenticação de login falho  (RF-001)                                         | Segurança      | Falha ao logar, por erro de senha/Email    |  [EV-002](https://github.com/user-attachments/assets/b62e5d8f-63ed-488f-be80-033996585611)   |
| CT-003 | Autenticação de login válido (RF-001)                                                   | Segurança      | Dados de login validados e acesso liberado a plataforma   | [EV-003](https://github.com/user-attachments/assets/f3923048-60a9-4a8d-8728-32fd6e81e8b8)    | 
| CT-004 | Cadastro de Projeto (RF-003)                                                      | Unitário/Integração     | O projeto é cadastrado com sucesso e disponibilizado para os freelancers    | [EV-004](https://github.com/user-attachments/assets/480272ef-78ce-4a13-80bd-68ad7806973a)  | 
| CT-005 | Exclusão de projeto (RF-005)                                | Unitário/Integração      | O projeto é excluído da lista de projetos do contratante e não aparece mais para os freelancers   | [EV-005](https://github.com/user-attachments/assets/7cf082fd-5dc0-438f-ba7c-30d5f47b34f3)   | 
| CT-006 | Freelancer assume projeto    (RF-006)               | Unitário/Integração       |Ao assumir o projeto, o contratante recebe os dados para contat, o projeto não pode ser assumido por outro freelancer| [EV-006](https://github.com/user-attachments/assets/14f4c051-6a04-462a-84a5-b4d64e570c73) |
| CT-007 | Busca de projetos por filtro                |  Unitário e Usabilidade     | Ao selecionar o tipo de projeto, o freelancer visualiza somente os projetos da categoria escolhida |[EV-007](https://github.com/user-attachments/assets/e716fc74-a62b-413e-b43c-d29c42cd0ad3)  | 
| CT-008 | Modal de contato com os dados do freelancer   (RF-006)                 | Unitário/Integração      | Após um projeto assumido, o contratante tem acesso ao whatsapp do freelancer | [EV-008](https://github.com/user-attachments/assets/40d808da-30e5-404e-a0f3-a1f7210adcfc) | 
| CT-009 |                 |       | |  | 

### Requisitos Não Funcionais


a aplicação.
4. Execute testes de carga para avaliar o desempenho da aplicação sob carga significativa.
5. Utilize ferramentas de teste adequadas, como frameworks de teste e ferramentas de automação de teste, para agilizar o
   processo de teste.

# Referências





Documentações oficiais das tecnologias utilizadas:
- React Native: https://reactnative.dev/docs/intro-react-native-components
- Tailwind: https://v2.tailwindcss.com/docs
- Ngrok: https://ngrok.com/docs/
- Expo: https://docs.expo.dev/
- TypeScript: https://www.typescriptlang.org/docs/



