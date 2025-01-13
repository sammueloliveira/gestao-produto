# gestao-produto
Este repositório contém um sistema de Cadastro de Produtos desenvolvido com Angular no front-end e .NET Core no back-end. O objetivo do sistema é permitir o cadastro, edição, e listagem de produtos, com suporte à gestão de departamentos.

Tecnologias Utilizadas
Front-end (Angular):

Angular: Framework para a construção da aplicação.
Angular Material: Utilizado para componentes de UI, como tabelas, formulários, e seleção de dados.
RxJS: Para gerenciar fluxos assíncronos e chamadas HTTP.
FormsModule: Para validação de formulários no Angular.
Routing: Implementação de navegação entre páginas.
HTTPClient: Para comunicação com a API back-end.
Utilizei as melhores práticas e todas as tecnologias disponíveis do Angular para otimizar a experiência do usuário e garantir a responsividade e interação em tempo real da aplicação.

Back-end (.NET Core):

.NET Core 8.0+: Framework para criação da API.
PostgreSQL: Banco de dados utilizado, com consultas feitas de forma manual (queries escritas à mão, sem uso de frameworks como Entity Framework).
Swagger: Para documentação e validação da API.
C#: Linguagem principal para o desenvolvimento da API.
Detalhes Técnicos
Consultas Manuais: No back-end, todas as queries foram feitas manualmente sem o uso de ORM (como o Entity Framework), garantindo controle total sobre a execução das consultas e desempenho da aplicação.
Cadastro de Produtos: O sistema permite o cadastro, edição e listagem de produtos, com suporte à seleção de departamentos específicos como “Bebidas”, “Congelados”, “Laticínios” e “Vegetais”.
Cadastro de Departamentos: Os departamentos são listados diretamente no front-end, com cada produto podendo ser associado a um ou mais departamentos.
