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

| ID     | Descrição do Requisito                                                                 | Prioridade | Responsáveis |
|--------|----------------------------------------------------------------------------------------|------------|--------------|
| RF-001 | Permitir a autenticação de usuários                                                    | ALTA       | A definir    |   
| RF-002 | Permitir o cadastro de freelancers                                                     | ALTA       | A definir    |   
| RF-003 | Permitir o cadastro de contratantes                                                    | ALTA       | A definir    |
| RF-004 | Permitir o cadastro dos projetos                                                       | ALTA       | A definir    |
| RF-005 | Permitir que o contratante edite e exclua seus projetos                                    | ALTA       | A definir    |
| RF-006 | Permitir a busca de projetos por necessidade do contratante (freelancer)               | ALTA       | A definir    |
| RF-007 | Possibilitar conexão entre contratante e freelancer por meio de projetos assumidos                     | MÉDIA      | A definir    |
| RF-008 | Não permitir edição ou exclusão de projetos já assumidos por freelancers                     | MÉDIA      |

### Requisitos Não Funcionais

| ID      | Descrição do Requisito                                                                     | Prioridade |
|---------|--------------------------------------------------------------------------------------------|------------|
| RNF-001 | O sistema deve ser responsivo para dispositivos móveis                                     | ALTA       |
| RNF-002 | O tempo de resposta para busca deve ser inferior a 2s                                      | MÉDIA      |
| RNF-003 | A plataforma deve garantir segurança na autenticação dos usuários                          | ALTA       |
| RNF-004 | O sistema deve ser intuitivo e fácil de usar, garantindo uma boa experiência aos usuários. | MÉDIA      |

1. Crie casos de teste para cobrir todos os requisitos funcionais e não funcionais da aplicação.
2. Implemente testes unitários para testar unidades individuais de código, como funções e classes.
3. Realize testes de integração para verificar a interação correta entre os componentes da aplicação.
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

