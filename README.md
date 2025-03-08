## Sobre o projeto

Este é um projeto de **e-commerce** desenvolvido com **.NET 8**, seguindo os princípios da **Clean Architecture** para garantir uma organização modular e escalável do código. A estrutura em camadas separa as responsabilidades de domínio, aplicação, infraestrutura e interface, promovendo um código mais limpo, testável e de fácil manutenção.

A aplicação utiliza **Entity Framework Core** para o mapeamento e manipulação do banco de dados **MySQL**, garantindo eficiência e flexibilidade no acesso aos dados. Além disso, foram aplicadas as melhores práticas de **Clean Code**, visando legibilidade, reutilização e padronização do código.

Esse projeto foi construído com foco em qualidade e boas práticas, proporcionando uma base sólida para o desenvolvimento de soluções robustas no comércio digital. 🚀

### Features

- **Criação de Pedidos**: Permite que o usuário finalize a compra, validando estoque e disponibilidade dos produtos. O pedido é registrado, o estoque é atualizado e o carrinho é esvaziado após a conclusão.
- **Atualização do Status do Pedido**: Permite alterar o status do pedido (ex: Pendente, Aprovado, Enviado), garantindo o acompanhamento do processo de compra.
- **Processamento de Pagamentos**: Calcula automaticamente o valor total do pedido, registra o pagamento e atualiza seu status para "Aprovado", garantindo a finalização da compra.
- **Adição de Itens ao Carrinho**: Permite adicionar produtos ao carrinho de compras, validando sua existência e armazenando automaticamente o preço e a quantidade selecionada. O total do carrinho é atualizado em tempo real.

## Getting Started

Para obter uma cópia local funcionando, siga estes passos simples.

### Requisitos

- Visual Studio versão 2022+ ou Visual Studio Code
- Windows 10+ ou Linux/MacOS com [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) instalado
- MySql Server

### Instalação

1. Clone o repositório:
    ```sh
    git clone git@github.com:benicio227/Ecommerce-API.git
    ```

2. Preencha as informações no arquivo `appsettings.json`
3. Execute a API

### Construído com

![.NET Badge](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge)  ![badge-windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white) ![visual-studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white) ![badge-mysql](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)
![badge-swagger](http://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge)  
