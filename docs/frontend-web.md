# Front-end Web

Este projeto visa desenvolver a interface web de uma aplicação utilizando React Native e AsyncStorage, com o objetivo de
fornecer uma experiência de usuário fluida e eficiente, especialmente em navegação entre páginas, login e gerenciamento
de serviços.

## Projeto da Interface Web

A interface da aplicação será projetada para fornecer uma navegação simples e intuitiva, com foco na interação fácil do
usuário. O layout será responsivo e otimizado para dispositivos móveis, considerando as práticas recomendadas para o
design mobile-first.

### Wireframes

https://docs.google.com/document/d/1gEkfZ9e2zNkm8sC_IsA62jaynjO5qNZ_jN8aYcJD_l0/edit?usp=sharing

### Apresentação

https://drive.google.com/file/d/1GKYx_yH6TpsnP12C9Djp0y924X88h_GN/view?usp=sharing

### Design Visual

O design visual seguirá uma paleta de cores sóbria e moderna, utilizando tons de roxo e branco para dar destaque às
ações mais importantes da interface, como botões e texto. A tipografia será limpa e legível, com o uso de fontes
sans-serif. Ícones minimalistas serão usados para representar as ações e funcionalidades da aplicação.

## Fluxo de Dados

A aplicação gerencia o estado do usuário com o uso de `AsyncStorage`, permitindo persistir dados de login entre as
sessões. O fluxo de dados se dá da seguinte forma:

1. **Login:** O usuário insere as credenciais e, ao ser autenticado, os dados são armazenados no `AsyncStorage` e o
   usuário é redirecionado para a tela principal.
2. **Navegação:** O estado de login é verificado sempre que a aplicação é carregada, e o estado de `userData` é
   utilizado para renderizar as páginas.
3. **Logout:** Ao realizar o logout, o `AsyncStorage` é limpo e o estado de `userData` é resetado.

## Tecnologias Utilizadas

- **Expo**
    - **React Native:** Framework utilizado para o desenvolvimento da interface móvel.
- **TypeScript:** Utilizado para garantir tipagem estática e maior segurança no desenvolvimento.

**Bibliotecas**

- **AsyncStorage:** Armazenamento de dados local no dispositivo para persistir o estado de login e outros dados
  essenciais.
- **React Navigation:** Biblioteca para gerenciar a navegação entre as telas.
- **NativeWind** Biblioteca de estilização CSS

## Considerações de Segurança

- **Autenticação:** A autenticação será realizada com base no armazenamento de dados do usuário no `AsyncStorage`.
  Sensibilidade ao manter informações como nome e outros dados do usuário será crucial.
- **Armazenamento Seguro:** Embora o `AsyncStorage` seja útil para persistir dados entre sessões, deve-se garantir que
  informações sensíveis (como senhas) sejam armazenadas de maneira segura.

## Implantação

1. **Requisitos de Hardware e Software:** A aplicação será implantada em um ambiente de servidor com suporte a Node.js
   para o backend e React Native para o frontend. O sistema operacional pode ser Linux ou Windows.
2. **Plataforma de Hospedagem:** Utilizar serviços como Hostinger, Locaweb.
3. **Configuração do Ambiente de Implantação:**
    - Instalar Node.js e o gerenciador de pacotes.
    - Instalar pacotes com 'npm install'
    - Rodar projeto com 'npx expo start'

## Testes

https://drive.google.com/file/d/1GKYx_yH6TpsnP12C9Djp0y924X88h_GN/view?usp=sharing

1. **Casos de Teste:**
    - Teste de apis
    - Teste nos formulários
    - Teste de respostas para o usuário

# Referências

- [React Navigation Documentation](https://reactnavigation.org/)
- [AsyncStorage Documentation](https://react-native-async-storage.github.io/async-storage/)
- [React Native Docs](https://reactnative.dev/docs/getting-started)
- [Jest Testing Framework](https://jestjs.io/docs/getting-started)
- [Cypress Testing Framework](https://www.cypress.io/)
