<h1>Segurança em APIs ASP.NET com JWT e Bearer Authentication</h1>

Ref.: Balta.io

> O conteúdo também foi organizado nos **commits**

<!--#region Sumário -->

<!--#region Conceitos de Segurança -->

<details><summary>Conceitos de Segurança</summary>

<ul>
    <li><a href="#conceitos-apresentacao">Apresentação do Módulo</a></li>
    <li><a href="#conceitos-autenticacao">O que é autenticação</a></li>
    <li><a href="#conceitos-autorizacao">O que é autorização</a></li>
    <li><a href="#conceitos-api">Autenticação e Autorização em APIs</a></li>
    <li><a href="#conceitos-token">Onde armazenar o Token</a></li>
    <li><a href="#conceitos-jwt">O que é JWT</a></li>
    <li><a href="#conceitos-payload">Entendendo Payload e assinatura do Token</a></li>
    <li><a href="#conceitos-refresh">Refresh Token</a></li>
</ul>

</details>

<!--#endregion -->

<!--#region JWT e Bearer na Prática -->

<details><summary>JWT e Bearer na Prática</summary>

<ul>
    <li><a href="#jwt-bearer-projeto">Criando o projeto</a></li>
    <li><a href="#jwt-bearer-configuracao">Criando o arquivo de configuração</a></li>
    <li><a href="#jwt-bearer-usuario">Criando a classe de usuário</a></li>
    <li><a href="#jwt-bearer-tokenservice">Iniciando o Token Service</a></li>
    <li><a href="#jwt-bearer-assinatura">Assinando o Token</a></li>
    <li><a href="#jwt-bearer-geracao">Gerando o Token</a></li>
    <li><a href="#jwt-bearer-claims">Entendendo os Claims</a></li>
    <li><a href="#jwt-bearer-claimsidentity">Claims Identity</a></li>
    <li><a href="#jwt-bearer-payload">Payload</a></li>
    <li><a href="#jwt-bearer-adic-autenticacao">Adicionando autenticação</a></li>
    <li><a href="#jwt-bearer-config-autenticacao">Configurando a autenticação</a></li>
    <li><a href="#jwt-bearer-testes">Testando o Token</a></li>
    <li><a href="#jwt-bearer-policies">Policies</a></li>
    <li><a href="#jwt-bearer-hack">Hackeando o Token</a></li>
    <li><a href="#jwt-bearer-get-claims">Obtendo Claims do Token</a></li>
    <li><a href="#jwt-bearer-claimsidentityextension">Claims Identity Extension</a></li>
</ul>

</details>

<!--#endregion -->

<!--#region Criando um Sistema de Login -->

<details><summary>Criando um Sistema de Login</summary>

<ul>
    <li><a href="#login-projeto">Criando o projeto</a></li>
    <li><a href="#login-entity-vo">Entity e Value Object</a></li>
    <li><a href="#login-string-extension">String Extension</a></li>
    <li><a href="#login-vo-email">Value Object de Email</a></li>
    <li><a href="#login-vo-email-verif">Value Object de Verificação de E-mail</a></li>
    <li><a href="#login-vo-senha">Value Object de Senha</a></li>
    <li><a href="#login-senha-aleat">Gerando senhas aleatórias</a></li>
    <li><a href="#login-password">Password Hashing</a></li>
    <li><a href="#login-hashes">Comparando Hashes</a></li>
    <li><a href="#login-user-entity">Finalizando a entidade User</a></li>
    <li><a href="#login-user-map">Mapeando o User</a></li>
    <li><a href="#login-datacontext">Criando o DataContext</a></li>
    <li><a href="#login-api-config">Configurando a API</a></li>
    <li><a href="#login-api-organizer">Organizando a API</a></li>
    <li><a href="#login-bdados">Gerando o banco de dados</a></li>
    <li><a href="#login-usecases">Use Cases</a></li>
    <li><a href="#login-response">Response</a></li>
    <li><a href="#login-response-comp">Compondo a resposta</a></li>
    <li><a href="#login-specification">Specification</a></li>
    <li><a href="#login-repo-services">Repositórios e Serviços</a></li>
    <li><a href="#login-req-valid">Validando a requisição</a></li>
    <li><a href="#login-entity-vo-ger">Gerando entidades de value objects</a></li>
    <li><a href="#login-repo-interag">Interagindo com repositórios</a></li>
    <li><a href="#login-handler">Finalizando o handler</a></li>
    <li><a href="#login-repo-impl">Implementando o repositório</a></li>
    <li><a href="#login-service-impl">Implementando o serviço</a></li>
    <li><a href="#login-api-config-2">Configurando a API</a></li>
    <li><a href="#login-depend-reg">Registrando as dependências</a></li>
    <li><a href="#login-mediator">Adicionando suporte ao Mediator</a></li>
    <li><a href="#login-met-post">Criando o método POST</a></li>
    <li><a href="#login-user-secrets">dotnet user secrets</a></li>
    <li><a href="#login-api-test">Testando a API</a></li>
    <li><a href="#login-auth-usecase">Authenticate Use Case</a></li>
    <li><a href="#login-auth-handlers">Authenticate Handler</a></li>
    <li><a href="#login-auth-repo">Authenticate Repository</a></li>
    <li><a href="#login-jwt-service">JWT Service</a></li>
    <li><a href="#login-token-ret">Retornando o Token</a></li>
    <li><a href="#login-roles">Criando Roles</a></li>
    <li><a href="#login-roles-map">Mapeando Roles</a></li>
    <li><a href="#login-roles-get">Recuperando os Roles</a></li>
    <li><a href="#login-roles-add">Adicionando Roles ao Token</a></li>    
    <li><a href="#login-roles-util">Utilizando os Roles</a></li>
    <li><a href="#login-conclusao">Conclusao</a></li>
</ul>

</details>

<!--#endregion -->

<!--#endregion -->

<!--#region Conceitos de Segurança -->

<h2 id="conceitos">Conceitos de Segurança</h2>

<!--#region Apresentação do Módulo  -->

<details id="conceitos-apresentacao"><summary>Apresentação do Módulo</summary>

<br/>

- Neste módulo vamos entender os principais conceitos de **autenticação** e **autorização**
- O que são Tokens, como eles são gerados, como inspecionamos eles e quais padrões, como **JWT** temos disponíveis
- Ao término deste módulo, você terá uma **visão completa** de como funciona a autenticação em APIs e Poderá utilizar estes conceitos para implementar estes **princípios em qualquer tecnologia**

</details>

<!--#endregion -->

<!--#region O que é autenticação  -->

<details id="conceitos-autenticacao"><summary>O que é autenticação?</summary>

<br/>

<h3>O que é autenticação?</h3>

Autenticação é o processo que **diz quem você é**

Por exemplo, em um processo de autenticação interno ou externo, eu estou garantindo que sou o **André Baltieri**, através do e-mail **xyz@balta.io**

Este processo pode ser feito de **diferentes maneiras**:

- usuário e senha
- e-mail e senha
- redes sociais, os famosos **Login com Google**, **Login com Facebook**

De qualquer forma, não importa como. 

<h3>O processo sempre é o mesmo!</h3>

Garantir que quem está dizendo que é **xyz@balta.io** é realmente o **André Baltieri**

<h3>Mas como garantir que eu sou eu?</h3>

O primeiro passo que precisamos é garantir que uma pessoa está ligada a um **e-mail**, **telefone** ou um **nome de usuário**. 

Este processo é relativamente simples.

No caso da garantia de um e-mail ser de quem ele realmente disse que é, basta no processo de registro do usuário, enviar um e-mail com um código a ele.

![Conceitos](./Assets/Images/conceitos-01.png)

<h3>Autenticação Externa</h3>

Neste modelo, você **delega a responsabilidade** para outro servidor, o que pode ser uma boa já que o processo de verificação do **Google** ou **Microsoft** por exemplo são bem mais **complexos** e **completos** do que possivelmente o seu será.

Neste formato, o que fazemos é no login, gerar um token de ativação e redirecionar o usuário para uma plataforma externa.

Após autenticado, o usuário retorna para nossa plataforma com um token e assim damos andamento na requisição.

Em suma, qualquer pessoa pode fornecer este serviço, basta realizar a implementação do **OIDC (Open Id Connect)**, um protocolo aberto de autenticação.

Existem servidores **OIDC** prontos como o **Identity Server** ou **Keycloak**, ambos fornecem uma ótima implementação e são completos em recursos.

Resumindo, se o **balta.io** tivesse uma implementação **OIDC**, você poderia adicionar um botão **Login com balta** em seu site.

Como o custo e risco de manter um **OIDC** próprio são altos, a recomendação é sempre começar do mais básico, implementando autenticação **oAuth** com **JWT**.

</details>

<!--#endregion -->

<!--#region O que é autorização  -->

<details id="conceitos-autorizacao"><summary>O que é autorização?</summary>

<br/>

<h3>O que é autorização?</h3>

Se *autenticação diz quem você é*, **autorização diz o que você pode fazer**

São os famosos **Roles** ou Perfis, e que no ASP.NET se estendem para políticas (**Policies**) e afirmações (**Claims**).

Enquanto a autenticação segue em diversas vezes uma **padronização**

**A autorização não tem necessariamente uma regra**

Eu mesmo já fiz sistemas onde ao invés de **Roles** utilizávamos **Tags**

<h3>De qualquer forma, a ideia aqui é, sabendo que "xyz@balta.io" é o "Andre Baltieri" o que ele pode fazer dentro deste sistema?</h3>

Note que estamos falando **DESTE** sistema, pois a autorização varia muito, inclusive entre módulos, páginas e até mesmo botões.

Podem haver páginas no sistema que eu posso ver, mas não posso editar e a autorização precisa tratar tudo isto.

Na maioria dos casos também, a autorização é **CUMULATIVA** ou seja, eu posso ter vários perfis como "admin", "employee", "sales" e cada um deles ter funções distintas que são acumuladas.

</details>

<!--#endregion -->

<!--#endregion -->

<!--#region JWT e Bearer na Prática -->

<h2 id="jwt">JWT e Bearer na Prática</h2>

<!--#region  -->

<details id=""><summary></summary>

<br/>



</details>

<!--#endregion -->

<!--#endregion -->

<!--#region Criando um Sistema de Login -->

<h2 id="login">Criando um Sistema de Login</h2>

<!--#region  -->

<details id=""><summary></summary>

<br/>



</details>

<!--#endregion -->

<!--#endregion -->