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
    <li><a href="#login-conclusao">Conclusão</a></li>
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

<!--#region Autenticação e Autorização em APIs  -->

<details id="conceitos-api"><summary>Autenticação e Autorização em APIs</summary>

<br/>

Como você já deve imaginar, tudo começa na API, visto que a segurança no lado do cliente é sempre fraca, todo processo deve rodar no servidor.

Armazenar um usuário e seus perfis é uma tarefa relativamente simples, incluindo ler estes dados e enviar para a tela, o problema está no armazenamento destes dados do outro lado.

Deixa eu te explicar melhor, em APIs **nós nunca ficamos autenticados ou autorizados**, a cada requisição este processo é feito.

<h3>Isto se repete para toda requisição</h3>

Existe um motivo para isto, e até um tempo atrás, utilizávamos **sessão** para manter estes dados em memória e o usuário permanecer conectado.

Com a distribuição das aplicações em diferentes servidores, manter o usuário conectado *não é algo viável*, pois os **servidores não compartilham memória**

Então imagina que você acessou o site do **balta.io** agora e fez o login, o servidor armazenou seus dados de login em **memória** e você está agora visualizando uma aula com **10 minutos de duração**.

Após terminar de ver a aula, você clica no botão concluir, porém, o servidor que você se autenticou previamente está **ocupado**.

**Neste momento entra em ação o Load Balancer ou balanceador de carga**

Ele rapidamente identifica que existe outro servidor do **balta.io** e que está **livre**, desocupado, então manda sua requisição para lá.

Como os servidores **não compartilham memória**, logo, você não está autenticado neste servidor e sua requisição falha com o erro **401 - Unauthorized**

Mudando o cenário para autenticação que temos hoje, onde a **cada requisição você precisa se autenticar**, este erro deixa de acontecer.

Neste modelo, geramos um Token de acesso, baseado em uma **chave privada que só o servidor tem** (ela tem que ser comum entre os servidores) e então a cada requisição, o **Frontend** envia este token.

Com o **Token** em mãos, como temos a chave privada, conseguimos **desencriptar** ele e obter os valores do usuário (e quaisquer outros valores que ele tenha)

Você pode também armazenar os **Tokens** para uma maior validação, mas isto implica em pelo menos **uma requisição no banco de dados** a cada requisição autenticada que sua API recebe.

</details>

<!--#endregion -->

<!--#region Onde armazenar o Token  -->

<details id="conceitos-token"><summary>Onde armazenar o Token</summary>

<br/>

<h3>Onde armazenar o Token?</h3>

**Não faz sentido!!!**

Foi a **primeira coisa que pensei** quando vi que os tokens devem ser armazenados pelo **Frontend** e enviados a cada requisição.

**mas deixa eu te explicar melhor este processo**

Se precisamos enviar o **token** a cada requisição, já que não ficamos autenticados nas APIs, precisamos armazená-los em algum lugar.

<h3>Mas onde???</h3>

<h3>Session Storage</h3>

```js
sessionStorage.setItem('chave','valor'); // salva um valor
sessionStorage.getItem('chave'); // lê um valor
sessionStorage.removeItem('chave'); // remove um item
sessionStorage.clear(); // limpa todos os dados
```

<h3>Local Storage</h3>

```js
localStorage.setItem('chave','valor'); // salva um valor
localStorage.getItem('chave'); // lê um valor
localStorage.removeItem('chave'); // remove um item
localStorage.clear(); // limpa todos os dados
```

<h3>Cookies</h3>

Os **Cookies** são **automaticamente anexados** nos cabeçalhos das requisições e alguns modelos de autenticação como as do ASP.NET MVC e ASP.NET Razor Pages **trabalham com Cookies**

É importante lembrar que ambos os modelos citados acima **são diferentes** do que estamos implementando aqui, por isto **Cookies** fazem sentido para o cenário.

---

De qualquer forma, tratar quando e como queremos compartilhar o **Token** de autenticação pelo **Local Storage** é a *melhor opção* para nosso cenário. 

<h3>Domínios e Sub Domínios</h3>

É importante lembrar também que o armazenamento local (**Session e Local**) são baseados nos **domínios** e/ou **sub domínios**, o que significa que informações persistidas nas sessões do site **balta.io** por exemplo, não serão visíveis nas sessões dentro do site **microsoft.com**

Desta forma, **não temos como compartilhar informações** entre **storages** de diferentes **domínios** ou **sub domínios**.

No caso dos **Cookies**, existem políticas que permitem estas trocas de informações, recursos como **Same Site** e troca de origem, desde que atribuídos de forma correta e consciente tornam o **Cookie** uma ótima opção para **Single Sign On** por exemplo. 

<h3>Banco de Dados</h3>

Sim, existe um banco de dados que roda dentro do *browser* chamado **IndexDb** e usando o **Blazor WASM** (WASM - Web Assembly) conseguimos até rodar o **SQLite** no *browser*.

De qualquer forma, as restrições são as mesmas do **Local Storage**, eles duram até serem removidos mas com a vantagem de armazenar mais informações (tamanho em disco)

Como no nosso caso, precisamos apenas de uma chave e valor e o **Local Storage** nos oferece até **200MB** (isto varia de acordo com o *browser*), podemos novamente ficar com o **Local Storage que é mais fácil, leve e simples**

</details>

<!--#endregion -->

<!--#region O que é JWT  -->

<details id="conceitos-jwt"><summary>O que é JWT</summary>

<br/>

<h3>O que é JWT?</h3>

Então você está me dizendo que eu vou armazenar um **Token** em um local onde o usuário ou outra pessoa **pode ir lá e visualizar**?

Isto mesmo, os **Tokens** são como **chaves de acesso** com informações e uma duração, ou seja, se alguém obter seu **Token**, ele pode **impersonar** ou fingir que é você.

Por isto a **segurança física** é a primeira e mais importante etapa que temos. 

Se alguém tem acesso ao seu **browser** fisicamente (ou remotamente) ele pode ver seu **Token**

Na verdade, este seria o **menor dos seus problemas**, já que os dados de navegação são armazenados localmente, ou seja, todas as suas sessões estão em um arquivo. 

Basta copiar este arquivo da sua máquina para a minha e pronto, **estarei logado com todas as suas sessões**

<h3>Por este motivo frequentemente somos recomendados a não clicar em links suspeitos, visto que uma simples cópia expõe todas as suas informações</h3>

Mas voltando aos **Tokens**, embora você possa armazenar uma chave/valor no **Local Storage**, é legal **encriptar** estas informações, correto?

Desta forma, se alguém roubar seu **Token** não verá nada além de uma **Hash* como esta abaixo:

```ps
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

Como este processo é algo **comum entre as aplicações e APIs**, criou-se um padrão chamado **JWT** (pronuncia-se JÓT), que é a sigla para **Json Web Token**

Ao desencriptar este **Token**, temos como resultado os seguintes JSON:

```json
{
    "alg": "HS256",
    "typ": "JWT"
},
{
    "sub": "1234567890",
    "name": "John Doe",
    "iat": 1516239022
}
```

Se você notar, o **Token** contém "." para segmentar suas regiões e o mesmo é dividido em três partes principais.

---

A **primeira parte** é chamada de **Header** ou cabeçalho, que define o **algoritmo utilizado** na encriptação e o **tipo do Token**, no nosso caso. JWT.

Quando **desencriptamos** ela, temos o seguinte JSON como resultado:

```json
{
    "alg": "HS256",
    "typ": "JWT"
}
```

Podemos **mudar estes valores**, incluindo o algoritmo e assim teremos valores diferentes. No caso, mudando de **HS256** para **HS384**, temos a seguinte **Hash** e **Header**:

```json
//eyJhbGciOiJIUzM4NCIsInR5cCI6IkpXVCJ9
{
    "alg": "HS384",
    "typ": "JWT"
}
```

<h3>O mais comum e recomendado até o momento da escrita deste artigo é o HS256, ele balanceia performance e segurança. Quanto mais alta a encriptação mais processamento ela requer</h3>

--- 

O **segundo item** é o **Payload** ou carga, que são informações que podemos incluir no **Token** com algumas ressalvas.

```json
{
    "sub": "1234567890",
    "name": "John Doe",
    "admin": true,
    "iat": 1516239022
}
```

Assim como nos **Headers** o **Payload** também varia de acordo com a quantidade de informações que colocamos nele. Ao incluirmos a informação **premium** temos um outro valor sendo gerado. 

```json
{
    "sub": "1234567890",
    "name": "John Doe",
    "admin": true,
    "premium": true,
    "iat": 1516239022
}
```

---

Por fim temos o **terceiro item** que representa a **assinatura** do **Token**, que só existe no lado do servidor.

Em resumo o que temos é uma encriptação **SHA256** de três itens convertidos para **Base64**, um código simples assim:

```js
HMACSHA256(
    base64UrlEncode(header) + "." +
    base64UrlEncode(payload),
    'MINHA CHAVE SECRETA'
)
```

---

<h3>Então quer dizer que, além de salvar o Token eu ainda posso visualizar ele? Sim, tem sites com o jwt.io que te permite visualizar tudo que um Token contém</h3>

Indo além disso, o **jwt.io** (você pode fazer isto manualmente também) te permite alterar o código de um **Token**, adicionando informações extras.

Isto significa que se eu pegar o **Token** abaixo, gerado para mim que diz que eu só tenho o perfil **student**:

```json
{
    "name":"André Baltieri",
    "roles": ["student"],
    "iat": 1516239022
}
```

<h3>E adicionar o perfil "admin":</h3>

```json
{
    "name":"André Baltieri",
    "roles": ["student", "admin"],
    "iat": 1516239022
}
```

Agora eu tenho um novo **Token**, com acesso de administrador:

```jwt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQW5kcsOpIEJhbHRpZXJpIiwicm9sZXMiOlsic3R1ZGVudCIsImFkbWluIl0sImlhdCI6MTUxNjIzOTAyMn0.gs12hMRKMhjboHF4Arw7R9r7MQUQTsKJNs6YxdDEC4w
```

</details>

<!--#endregion -->

<!--#region Entendendo Payload e assinatura do Token  -->

<details id="conceitos-payload"><summary>Entendendo Payload e assinatura do Token</summary>

<br/>

<h3>Payloads e Assinaturas</h3>

Muita calma nesta hora! **É possível sim mudar um Token**, mas para isto é necessário uma chave privada que somente o servidor deve conter.

<h3>Se o Token for gerado com qualquer outra chave, diferente da qual foi gerada, o mesmo é invalidado. Isto é o que torna os Tokens seguros em relações as mudanças</h3>

Então não importa se o **Token** foi alterado no cliente ou mesmo no **meio do caminho**, se ele não for regerado com a **chave privada** (que só o servidor deve conhecer) ele será invalidado. 

**MAN IN THE MIDDLE** existe um ataque comum chamado "Homem no meio" que basicamente intercepta a comunicação entre o cliente e o servidor e rouba informações ou modifica elas.

<h3>Não armazene valores sensíveis no Payload</h3>

Outro ponto importantíssimo é sobre o **uso do Payload**. Embora você possa adicionar qualquer informação que desejar nele, **não é recomendado trafegar informações sensíveis ali**

Cartão de crédito, telefone, endereço ou qualquer informação que comprometa os dados do seu usuário, **devem ser mantidos apenas no servidor**

Em suma, tendo o e-mail ou ID do usuário no **payload** já basta. Com estas informações você pode **consultar o que quiser sobre ele**

<h3>Tempo de Vida do JWT</h3>

Uma recomendação e padrão dos **JWT**s é conter no **Payload** a informação **iat** que significa **Issued At** ou **Gerado em**, que nada mais é do que um **timestamp** da data/hora que o **Token** foi gerado. 

Desta forma, podemos criar uma validação para o **Token**, dizendo que o mesmo **só pode existir por X tempo**. Assim, se um **Token** for roubado, ele só vai ser **útil durante XX dias ou horas**, passado isto ele se torna inválido.

```json
{
    "sub": "1234567890",
    "name": "John Doe",
    "iat": 1516239022
}
```

<h3>Dependendo do sistema este tempo de expiração pode variar, mas em geral, ele não deve ser tão curto a ponto de incomodar o usuário, que precisará se autenticar o tempo todo, nem tão longo que alguém possa roubar e usar por meses.</h3>

</details>

<!--#endregion -->

<!--#region Refresh Token  -->

<details id="conceitos-refresh"><summary>Refresh Token</summary>

<br/>

Caso opte pelo uso de um tempo reduzido no tempo de vida dos Tokens, uma ótima alternativa é o uso dos **Refresh Tokens**

Sempre que gerar um **Token** para o seu usuário, gere uma **chave aleatória junto**, encriptada e dado o **Token** anterior e mais esta nova chave, **um novo Token pode ser gerado**

No caso, é interessante gerar este novo **Token** em um **intervalo menor que a expiração do Token principal**, desta forma, o usuário sempre mantém a sessão ativa.

Em adicional, você pode optar por **não gerar um novo Token** caso o **anterior já tenha expirado**, isto varia bastante incluindo a base em relação tempo de vida do **Token**.

Em resumo, supondo que meu **Token** expira em 2 horas, como eu sei o quanto ele dura e quando foi emitido, **posso me prontificar e já gerar um novo Token** (refresh) desde que a chave do **Refresh Token** tenha sido enviada junto.

Caso meu **Token já tenha expirado**, mas há menos de 2 horas podemos manter o processo acima e também gerar o **Token** (Refresh)

Caso meu **Token** tenha **expirado há mais de 2 horas**, aí não tem jeito, o usuário precisa se autenticar novamente. 

</details>

<!--#endregion -->

<!--#endregion -->

<!--#region JWT e Bearer na Prática -->

<h2 id="jwt">JWT e Bearer na Prática</h2>

<!--#region Criando o projeto -->

<details id="jwt-bearer-projeto"><summary>Criando o projeto</summary>

<br/>

Criar o projeto:

```ps
dotnet new web -o JwtAspNet

O modelo "ASP.NET Core Vazio" foi criado com êxito.

Processando ações pós-criação...
Restaurando F:\marcelo\Educação\Segurança em APIs ASP.NET com JWT e Bearer Authentication (Balta.io)\JwtAspNet\JwtAspNet.csproj:
  Determinando os projetos a serem restaurados...
  F:\marcelo\Educação\Segurança em APIs ASP.NET com JWT e Bearer Authentication (Balta.io)\JwtAspNet\JwtAspNet.csproj r
  estaurado (em 167 ms).
A restauração foi bem-sucedida.
```

Acessar a pasta do projeto:

```ps
cd .\JwtAspNet\
```

Instalar o pacote **JwtBearer**:

https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/7.0.14

```ps
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 7.0.14

  Determinando os projetos a serem restaurados...
  Writing C:\Users\marce\AppData\Local\Temp\tmp68A8.tmp
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adicionando PackageReference do pacote 'Microsoft.AspNetCore.Authentication.JwtBearer' ao projeto 'F:\marcelo\Educação\Segurança em APIs ASP.NET com JWT e Bearer Authentication (Balta.io)\JwtAspNet\JwtAspNet.csproj'.
info : Restaurando pacotes para F:\marcelo\Educação\Segurança em APIs ASP.NET com JWT e Bearer Authentication (Balta.io)\JwtAspNet\JwtAspNet.csproj...
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.aspnetcore.authentication.jwtbearer/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.aspnetcore.authentication.jwtbearer/7.0.14/microsoft.aspnetcore.authentication.jwtbearer.7.0.14.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.aspnetcore.authentication.jwtbearer/7.0.14/microsoft.aspnetcore.authentication.jwtbearer.7.0.14.nupkg 500 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols.openidconnect/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols.openidconnect/index.json 623 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols.openidconnect/6.15.1/microsoft.identitymodel.protocols.openidconnect.6.15.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols.openidconnect/6.15.1/microsoft.identitymodel.protocols.openidconnect.6.15.1.nupkg 62 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols/index.json 610 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols/6.15.1/microsoft.identitymodel.protocols.6.15.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.protocols/6.15.1/microsoft.identitymodel.protocols.6.15.1.nupkg 64 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/index.json 745 ms
info :   GET https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/6.15.1/system.identitymodel.tokens.jwt.6.15.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/system.identitymodel.tokens.jwt/6.15.1/system.identitymodel.tokens.jwt.6.15.1.nupkg 200 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/index.json 620 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/6.15.1/microsoft.identitymodel.logging.6.15.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.logging/6.15.1/microsoft.identitymodel.logging.6.15.1.nupkg 62 ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/index.json 698 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/6.15.1/microsoft.identitymodel.tokens.6.15.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.tokens/6.15.1/microsoft.identitymodel.tokens.6.15.1.nupkg 62 ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/index.json 677 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/6.15.1/microsoft.identitymodel.jsonwebtokens.6.15.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.identitymodel.jsonwebtokens/6.15.1/microsoft.identitymodel.jsonwebtokens.6.15.1.nupkg 79 ms
info : Microsoft.AspNetCore.Authentication.JwtBearer 7.0.14 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo 640Jjm2NnpQYHew0I2RyP2PjBKfB1ItSDYHX1TD+ooJov+b+ELPuo/GgrJCNJr3ZI1YmplmsQv88d8JRojZ+fA==.
info : Microsoft.IdentityModel.Protocols 6.15.1 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo 6nHr+4yE8vj620Vy4L0pl7kmkvWc06wBrJ+AOo/gjqzu/UD/MYgySUqRGlZYrvvNmKkUWMw4hdn78MPCb4bstA==.
info : System.IdentityModel.Tokens.Jwt 6.15.1 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo q3ZLyWmpX4+jW4XITf7Axd+9sC6w2NrQaKcQQT9A8waoknHgaNwSeookpUmPMQDqS0afT9Lh0JYub196vzuzbA==.
info : Microsoft.IdentityModel.JsonWebTokens 6.15.1 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo X5K/Pt02agb1V+khh5u7Q8hg02IVTshxV5owpR7UdQ9zfs0+A6qzca0F9jyv3o8SlOjEFHBabs+5cp7Noofzvg==.
info : Microsoft.IdentityModel.Protocols.OpenIdConnect 6.15.1 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo WwecgT/PNrytLNUWjkYtnnG2LXMAzkINSaZM+8dPPiEpOGz1bQDBWAenTSurYICxGoA1sOPriFXk+ocnQyprKw==.
info : Microsoft.IdentityModel.Logging 6.15.1 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo PpZHL/Bt/8vQ8g/6LxweuI1EusV0ogUBYnGM+bPeL/SG89gx2n05xKNE/U5JNEkLFLL+sk7O8T7c/PXhFtUtUg==.
info : Microsoft.IdentityModel.Tokens 6.15.1 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo 0bd0ocKuNai0/GdhboIW37R6z8I0vFqlmiPeG055SJxPPJ7dfBo2tjJ3bPV9vjFCRDuusj24dldOsg4hWui6iw==.
info : O pacote 'Microsoft.AspNetCore.Authentication.JwtBearer' é compatível com todas as estruturas especificadas no projeto 'F:\marcelo\Educação\Segurança em APIs ASP.NET com JWT e Bearer Authentication (Balta.io)\JwtAspNet\JwtAspNet.csproj'.
info : PackageReference do pacote 'Microsoft.AspNetCore.Authentication.JwtBearer' versão '7.0.14' adicionada ao arquivo 'F:\marcelo\Educação\Segurança em APIs ASP.NET com JWT e Bearer Authentication (Balta.io)\JwtAspNet\JwtAspNet.csproj'.
info : Gravando o arquivo de ativos no disco. Caminho: F:\marcelo\Educação\Segurança em APIs ASP.NET com JWT e Bearer Authentication (Balta.io)\JwtAspNet\obj\project.assets.json
log  : F:\marcelo\Educação\Segurança em APIs ASP.NET com JWT e Bearer Authentication (Balta.io)\JwtAspNet\JwtAspNet.csproj restaurado (em 4,33 sec).
```

</details>

<!--#endregion -->

<!--#region Criando o arquivo de configuração -->

<details id="jwt-bearer-configuracao"><summary>Criando o arquivo de configuração</summary>

<br/>

Definir uma chave privada para criptografar o **Token** na API.

Cria uma classe estática **Configuration** e inserir inicialmente a chave *hardcoded*. Posteriormente será utilizado o **dotnet user secrets**

**Configuration.cs**:

```c#
namespace JwtAspNet;

public static class Configuration
{
    public static string PrivateKey { get; set; } = "5+IV)E2glD3xCH2rNTElZ_at9(TbG1N(E=´H)29*";
}
```

</details>

<!--#endregion -->

<!--#region Criando a classe de usuário -->

<details id=""><summary>Criando a classe de usuário</summary>

<br/>

Criar o registro **Models/User** que servirá como base para gerar um **Token**:

```c#
namespace JwtAspNet.Models;

public record User(
    int Id,
    string Email,
    string Password,
    string[] Roles)
{

}
```

Criar o serviço **Service/TokenService.cs**:

```c#
namespace JwtAspNet.Services;

public class TokenService
{
    public string Create()
    {

    }
}
```

Dado um objeto, será gerado um **Token**

</details>

<!--#endregion -->

<!--#region Iniciando o Token Service -->

<details id="jwt-bearer-tokenservice"><summary>Iniciando o Token Service</summary>

<br/>

3 passos simples para criação do **Token**:

1. Criar o **handler** que será o responsável por gerar o **Token** disponibilizado pelo pacote instalado anteriormente **Microsoft.AspNetCore.Authentication.JwtBearer**.

```c#
var handler = new JwtSecurityTokenHandler();
```

2. Gerar o **Token** 

```c#
var token = handler.CreateToken();
```

3. Retornar o **Token**

```c#
return handler.WriteToken(token);
```
---

Implementação dos 3 passos no **TokenService.cs**:

```c#
using System.IdentityModel.Tokens.Jwt;

namespace JwtAspNet.Services;

public class TokenService
{
    public string Create()
    {
        var handler = new JwtSecurityTokenHandler();

        var token = handler.CreateToken();
        return handler.WriteToken(token);
    }
}
```

</details>

<!--#endregion -->

<!--#region Assinando o Token -->

<details id="jwt-bearer-assinatura"><summary>Assinando o Token</summary>

<br/>

Gerar as informações para o **Token** e assiná-lo.

Para a assinatura será utilizada a chave **PrivateKey** disponibilizada anteriormente no **Configuration**. 

Esta chave será utilizada na geração e na leitura do **Token**

Para assinatura utilizar **SigningCredentials** que pede:
1. A chave simétrica **SecurityKey** disponibilizada pela classe **SymmetricSecurityKey** que espera receber um **array de bytes**. Para isso a chave **PrivateKey** precisa ser convertida.
2. O algoritmo disponibilizados dentro da classe **SecurityAlgorithms**.

Resultado da implementação:

```c#
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtAspNet.Services;

public class TokenService
{
    public string Create()
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);
        new SigningCredentials(
            key: new SymmetricSecurityKey(key),
            algorithm: SecurityAlgorithms.HmacSha256);

        var token = handler.CreateToken();
        return handler.WriteToken(token);
    }
}

```

</details>

<!--#endregion -->

<!--#region Gerando o Token -->

<details id="jwt-bearer-geracao"><summary>Gerando o Token</summary>

<br/>

Criar as variáveis
1. **credentials** para receber a implementação de **new SigningCredentials** realizada anteriormente, e
2. **tokenDescriptor** que será implementado conforme abaixo.

```c#
        var credentials = new SigningCredentials(
            key: new SymmetricSecurityKey(key),
            algorithm: SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor();        
```

O **tokenDescriptor** conterá as informações do **Token**. Preencher as informações do **tokenDescriptor**:
1. **SigningCredentials** que receberá a variável **credentials**;
2. **Expires** que define o tempo de expiração;
3. **Payload**.

Código sem **Payload**:

```c#
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2)
        };
```

---

Teste do funcionamento antes de implementar o Payload.

```c#
builder.Services.AddTransient<TokenService>();
```

```c#
app.MapGet(
    pattern: "/", 
    handler:(TokenService service) 
        => service.Create());
```

**Program.cs**:

```c#
using JwtAspNet.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapGet(
    pattern: "/",
    handler: (TokenService service)
        => service.Create());

app.Run();
```

```ps
dotnet run

Compilando...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5051
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: F:\marcelo\Educação\Segurança em APIs ASP.NET com JWT e Bearer Authentication (Balta.io)\JwtAspNet
```

[http:\\localhost:5051](http:\\localhost:5051)

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE3MDE2MTQxNDgsImV4cCI6MTcwMTYyMTM0OCwiaWF0IjoxNzAxNjE0MTQ4fQ.mMP5wbNETs5fDDhPd_W1r7P5kJZi_k-IHSjyS1AALao
```

[jwt.io](jwt.io)

![Conceitos](./Assets/Images/conceitos-02.png)

--- 

Passar o **tokenDescriptor** para o **handler.CreateToken**.

```c#
        var token = handler.CreateToken(tokenDescriptor);
```

</details>

<!--#endregion -->

<!--#region Entendendo os Claims -->

<details id="jwt-bearer-claims"><summary>Entendendo os Claims</summary>

<br/>

Para prosseguir na criação do **Payload** é preciso entender o conceito de **Claims** no .NET

**Claim** nada mais é do que uma **chave** e **valor**:

```c#
new Claim(type:"", value: "")
```

O tipo pode se repetir.

Alguns **Claims** no .NET são diferenciados, padrões do .NET:
- **Claim Type: Name** que pode ser recuperado utilizando **User.Identity.Name**
- **Claim Type: Role** que possibilita utilizar **User.IsInRole**

```c#
        new Claim(ClaimTypes.Name, "");
        new Claim(ClaimTypes.Email, "");
        new Claim(ClaimTypes.GivenName, "");
        new Claim(ClaimTypes.Role, "");
```

A classe **ClaimTypes** possui vários itens padrões.

Preferir utilizar **ClaimTypes**

> A tradução para **Claim** é **Afirmação**. 
> O **Claim** nada mais é do que uma **afirmação**

**TokenService.cs**:

```c#
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAspNet.Services;

public class TokenService
{
    public string Create()
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);
        var credentials = new SigningCredentials(
            key: new SymmetricSecurityKey(key),
            algorithm: SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2)
        };

        new Claim(ClaimTypes.Name, "");
        new Claim(ClaimTypes.Email, "");
        new Claim(ClaimTypes.GivenName, "");
        new Claim(ClaimTypes.Role, "");

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
}
```

No **Program.css** pode-se utilizar obtidos a partir dos **ClaimTypes** padrões:
- **user.Identity.Name**
- **user.Identity.IsAuthenticated**
- **user.IsInRole**

**Program.cs**:

```c#
... 
app.MapGet(
    pattern: "/",
    handler: (
        TokenService service,
        ClaimsPrincipal user)
        => new
        {
            Token = service.Create(),
            User = user.Identity.Name
            //User = user.Identity.IsAuthenticated
            //User = user.IsInRole
        });
...        
```

</details>

<!--#endregion -->

<!--#region Claims Identity -->

<details id="jwt-bearer-claimsidentity"><summary>Claims Identity</summary>

<br/>

Criação de um método privado **GenerateClaims()** para o serviço **TokenService** que retornará um objeto **ClaimsIdentity**, com o objetivo de não poluir muito o método **Create**.

**ClaimsIdentity** retornará uma lista de **Claims**  (tipos e valores) que o **Token** possuirá, automaticamente adicionado ao **Payload**. 

As informações do **Token** são passadas a partir dos **Claims**. 

Os **Claims** podem ser customizados. 

Criação de um objeto **ClaimsIdentity** e retorná-lo no método. Criação de **Claims** com **AddClaim** ou **AddClaims**.

```c#
    private ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim("id", user.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        ci.AddClaim(new Claim("image", user.Image));

        foreach (var role in user.Roles)        
            ci.AddClaim(new Claim(ClaimTypes.Role, role));

        return ci;
    }
```

</details>

<!--#endregion -->

<!--#region Payload -->

<details id="jwt-bearer-payload"><summary>Payload</summary>

<br/>

Tornar o método **GenerateClaims** estático para facilitar seu uso no serviço **TokenService**:

```c#
private static ClaimsIdentity GenerateClaims(User user)
```

Alterar a assinatura do método **Create** para receber o objeto **User**

Os **Claims** ficarão no **Subject** do **TokenDescriptor** com a chamada do **GenerateClaims** e passagem do objeto **user**. O **Subject** gerará o **Payload** do **Token**.

```c#
public string Create(User user)

...

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2),
            Subject = GenerateClaims(user)
        };
```

Ajuste do **Program.cs** para passar um usuário para o método **Create** do serviço **TokenService**:

```c#
...

app.MapGet(
    pattern: "/",
    handler: (TokenService service)
        => 
        {
            var user = new User(
                Id:1,
                Name:"André Baltieri",
                Email:"xyz@balta.io",
                Image:"https://balta.io/",
                Password:"xyz",
                Roles:new[] { "student", "premium" });

            return service.Create(user);
        });

```

```ps
dotnet run

Compilando...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5051
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: F:\marcelo\Educação\Segurança em APIs ASP.NET com JWT e Bearer Authentication (Balta.io)\JwtAspNet
```

[http://localhost:5051/](http://localhost:5051/)

```ps
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJwcmVtaXVtIl0sIm5iZiI6MTcwMTYyNzA5NiwiZXhwIjoxNzAxNjM0Mjk2LCJpYXQiOjE3MDE2MjcwOTZ9.cMkAFPwjHDGkczS0FV19DbA3ANHZ6Trx1MS_QJlY6Jk
```

![jwt.io](./Assets/Images/conceitos-03.png)

Observar:
1. **ClaimTypes.Name** foi trocado para **unique_name**;
2. **ClaimTypes.Email** foi trocado para **email**;
3. **ClaimTypes.GivenName** foi trocado para **given_name**;

Siglas da tradução do **Token**:
- **alg**: Signature or encryption algorithm
- **typ**: Type of token
- **nbf**: No valid before
- **exp**: Expiration time
- **iat**: Issued at

</details>

<!--#endregion -->

<!--#region Adicionando autenticação -->

<details id="jwt-bearer-adic-autenticacao"><summary>Adicionando autenticação</summary>

<br/>

Precisamos implementar a autenticação. 

Até então não está sendo feita a autenticação, somente a geração do **Token**


No **Program** implementar o **middleware** de autenticação que é executado no meio da execução, quando a execução é interrompida para checar a autenticação:

```c#
...

builder.Services.AddAuthentication();

var app = builder.Build();
app.UseAuthentication();

... 
```

**app.UseAuthentication()** deve ser implementado antes das rotas **app.MagGet**

Da mesma forma, implementar o **middleware** de autorização. Este **middleware** deve ser implementado após o **middleware** de autenticação, pois é necessário saber quem o usuário é para depois definir o que ele pode fazer:

```c#
...

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

... 
```

Foi dito à aplicação que existirá autenticação, mas não o que a aplicação deve fazer (checar o token, de onde o token veio, qual o formato de autenticação etc.)

Existem várias formas de autenticação:

- No Auth
- API Key
- Bearer Token
- JWT Bearer
- Basic Auth
- Digest Auth
- OAuth 1.0
- OAuth 2.0
- Hawk Authentication
- AWS Signature
- NTML Authentication
- Akamai EdgeGrid
- Etc.

Trabalharemos com **Bearer Token**.

</details>

<!--#endregion -->

<!--#region Configurando a autenticação -->

<details id="jwt-bearer-config-autenticacao"><summary>Configurando a autenticação</summary>

<br/>

Dentro do **AddAuthentication** no **Program** serão configuradas as opções de autenticação. Serão definidos:
1. O esquema de autenticação padrão para os padrões **JwtBearer** (**options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;**);
2. Uma configuração extra que é a forma como a requisição será interrogada para saber onde está o **Token** e como lidar com esse tipo de autenticação (**options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;**).

```c#
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});
```

Com isso está sendo configurada a utilização de **Bearer** e que a requisição deverá ser tratada como tal.

Por enquanto não está sendo dito como e onde achar, e descriptografar o **Token**. 

Para isso deve ser utilizado **AddJwtBearer()** e configurada suas opções que estão dentro dos parâmetros de validação do **Token** **TokenValidationParameters**. **Issuer SigningKey** define a **PrivateKey** que será utilizada durante a validação da assinatura da requisição.

Os demais parâmetros são recomendações por não estar utilizando validação de domínio HTTP/S.

```c#
...
 
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

...

```

</details>

<!--#endregion -->

<!--#region Testando o Token -->

<details id="jwt-bearer-testes"><summary>Testando o Token</summary>

<br/>

Ajustar as rotas no **Program**

```c#
app.MapGet(
    pattern: "/login",
    handler: (TokenService service)
        => 
        {
            var user = new User(
                Id:1,
                Name:"André Baltieri",
                Email:"xyz@balta.io",
                Image:"https://balta.io/",
                Password:"xyz",
                Roles:new[] { "student", "premium" });

            return service.Create(user);
        });

app
    .MapGet(
        pattern: "/restrito",
        handler: () => "Você tem acesso!")
    .RequireAuthorization();
```

```ps
dotnet run
```

> Postman

Requisição: **GET** http://localhost:5051/login

Resposta:

```txt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJwcmVtaXVtIl0sIm5iZiI6MTcwMTY4NTQ5OSwiZXhwIjoxNzAxNjkyNjk5LCJpYXQiOjE3MDE2ODU0OTl9.Ji75wFHoqafW91g-EYDtmcMZmRQY9AVdyndEh_jgcgs
```

---

Requisição: **GET** http://localhost:5051/restrito

Autorização: **Bearer Token**

Token:

```txt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJwcmVtaXVtIl0sIm5iZiI6MTcwMTY4NTQ5OSwiZXhwIjoxNzAxNjkyNjk5LCJpYXQiOjE3MDE2ODU0OTl9.Ji75wFHoqafW91g-EYDtmcMZmRQY9AVdyndEh_jgcgs
```

Resposta:

```txt
Você tem acesso!
```

cURL:

```txt
curl --location --request GET 'http://localhost:5051/restrito' \
--header 'Content-Type: application/json' \
--header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJwcmVtaXVtIl0sIm5iZiI6MTcwMTY4NTQ5OSwiZXhwIjoxNzAxNjkyNjk5LCJpYXQiOjE3MDE2ODU0OTl9.Ji75wFHoqafW91g-EYDtmcMZmRQY9AVdyndEh_jgcgs' \
```

</details>

<!--#endregion -->

<!--#region Policies -->

<details id="jwt-bearer-policies"><summary>Policies</summary>

<br/>

Criação de uma nova rota no **Program**:

```c#

    .MapGet(
        pattern: "/admin",
        handler: () => "Você tem acesso!")
    .RequireAuthorization("admin");
```

> Postman

Requisição: **GET** http://localhost:5051/login

Resposta:

```txt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJwcmVtaXVtIl0sIm5iZiI6MTcwMTY4NTk3NCwiZXhwIjoxNzAxNjkzMTc0LCJpYXQiOjE3MDE2ODU5NzR9.lcygOtq-jnbJVWFYylbNXWNTB12g3E3nuak78jXK5Gs
```

---

Ao avaliar o **Token** é possível verificar os roles do usuário.

**jwt.io**

![](./Assets/Images/conceitos-04.png)

---

Requisição: **GET** http://localhost:5051/admin

Autorização: **Bearer Token**

Token:

```txt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJwcmVtaXVtIl0sIm5iZiI6MTcwMTY4NTQ5OSwiZXhwIjoxNzAxNjkyNjk5LCJpYXQiOjE3MDE2ODU0OTl9.Ji75wFHoqafW91g-EYDtmcMZmRQY9AVdyndEh_jgcgs
```

Resposta:

```txt
System.InvalidOperationException: The AuthorizationPolicy named: 'admin' was not found.
   at Microsoft.AspNetCore.Authorization.AuthorizationPolicy.CombineAsync(IAuthorizationPolicyProvider policyProvider, IEnumerable`1 authorizeData, IEnumerable`1 policies)
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)

HEADERS
=======
Accept: */*
Connection: keep-alive
Host: localhost:5051
User-Agent: PostmanRuntime/7.35.0
Accept-Encoding: gzip, deflate, br
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJwcmVtaXVtIl0sIm5iZiI6MTcwMTY4NTk3NCwiZXhwIjoxNzAxNjkzMTc0LCJpYXQiOjE3MDE2ODU5NzR9.lcygOtq-jnbJVWFYylbNXWNTB12g3E3nuak78jXK5Gs
Content-Type: application/json
Content-Length: 18954
Postman-Token: 2f4c4c02-66f3-47de-b546-69f7e4a7324f
```

--- 

Não existe uma política de autorização chamada **admin**

**RequiredAuthorization** busca por políticas. Uma política pode ser a composição de um ou mais **roles**. A política é um pouco maior que os **roles**.

No **Program** adicionar um **AuthorizationPolicyBuilder** **Require Role** nas opções de autorização:

```c#
builder.Services
    .AddAuthorization(options =>
    {
        options.AddPolicy("Admin", p => p.RequireRole("admin"));
    });

app
    .MapGet(
        pattern: "/admin",
        handler: () => "Você tem acesso!")
    .RequireAuthorization("Admin");    
```

---

> Postman


Requisição: **GET** http://localhost:5051/admin

Autorização: **Bearer Token**

Token:

```txt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJwcmVtaXVtIl0sIm5iZiI6MTcwMTY4NjgxOCwiZXhwIjoxNzAxNjk0MDE4LCJpYXQiOjE3MDE2ODY4MTh9.NlPca0lMSHlOU2gkZxaWbiR0clrXGEct2_MqZTC3ehs
```

Resposta: **403 - Forbidden** (o usuário não possui o **role** admin para acessar essa página)

Neste cenário além de autenticar conseguimos autorizar.

</details>

<!--#endregion -->

<!--#region Hackeando o Token -->

<details id="jwt-bearer-hack"><summary>Hackeando o Token</summary>

<br/>

**jwt.io**

Token:

```txt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJwcmVtaXVtIl0sIm5iZiI6MTcwMTY4NzY0MiwiZXhwIjoxNzAxNjk0ODQyLCJpYXQiOjE3MDE2ODc2NDJ9.yOq3xIwJpn3blFus5yICXMG5CMJb6qJom11yBmP7cdI
```

<h3>Alterar o role **premium** para **admin** e obter um novo **Token** sem a chave privada.</h3>

![](./Assets/Images/conceitos-05.png)

Novo Token:

```txt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJhZG1pbiJdLCJuYmYiOjE3MDE2ODc2NDIsImV4cCI6MTcwMTY5NDg0MiwiaWF0IjoxNzAxNjg3NjQyfQ.iUEoYJgc5DI-El2DlUhMJnEJMjlS8rxg3l1F8usq7k4
```

> Postman


Requisição: **GET** http://localhost:5051/admin

Autorização: **Bearer Token**

Token:

```txt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJhZG1pbiJdLCJuYmYiOjE3MDE2ODc2NDIsImV4cCI6MTcwMTY5NDg0MiwiaWF0IjoxNzAxNjg3NjQyfQ.iUEoYJgc5DI-El2DlUhMJnEJMjlS8rxg3l1F8usq7k4
```

Resposta: **401 - Unauthorized** 

---

<h3>Alterar o role **premium** para **admin** e obter um novo **Token** com a chave privada.</h3>

![](./Assets/Images/conceitos-06.png)

Novo Token:

```txt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJhZG1pbiJdLCJuYmYiOjE3MDE2ODY4MTgsImV4cCI6MTcwMTY5NDAxOCwiaWF0IjoxNzAxNjg2ODE4fQ.cXKqMltWn0CKKRqQH2KjyNcHwnRlKhDlQhNUrCGM1qA
```

> Postman


Requisição: **GET** http://localhost:5051/admin

Autorização: **Bearer Token**

Token:

```txt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJhZG1pbiJdLCJuYmYiOjE3MDE2ODY4MTgsImV4cCI6MTcwMTY5NDAxOCwiaWF0IjoxNzAxNjg2ODE4fQ.X-L72LTyxMm3ykCXgxfKvBa5orItmCAIzFOscM21oVo
```

Resposta: **200 - OK** 

</details>

<!--#endregion -->

<!--#region Obtendo Claims do Token -->

<details id="jwt-bearer-get-claims"><summary>Obtendo Claims do Token</summary>

<br/>

Além do **RequiredAuthorization** existe a opção de expor e trabalhar com as informações dos **Claims**. Isso tem que estar dentro de uma Url autenticada.

Na rota pode-se esperar **ClaimsIdentity** aqui chamado de **user**

**Program.cs**:

```c#
app
    .MapGet(
        pattern: "/restrito",
        handler: (ClaimsPrincipal user) =>
            new
            {
                id = user.Claims.FirstOrDefault(x=> x.Type == "id").Value,
                name = user.Claims.FirstOrDefault(x=> x.Type == ClaimTypes.Name).Value,
                email = user.Claims.FirstOrDefault(x=> x.Type == ClaimTypes.Email).Value,
                givenName = user.Claims.FirstOrDefault(x=> x.Type == ClaimTypes.GivenName).Value,
                image = user.Claims.FirstOrDefault(x=> x.Type == "image").Value
            })
    .RequireAuthorization();
```

> Postman

Requisição: **GET** http://localhost:5051/login

Resposta: **200 - OK** 

```txt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1bmlxdWVfbmFtZSI6Inh5ekBiYWx0YS5pbyIsImVtYWlsIjoieHl6QGJhbHRhLmlvIiwiZ2l2ZW5fbmFtZSI6IkFuZHLDqSBCYWx0aWVyaSIsImltYWdlIjoiaHR0cHM6Ly9iYWx0YS5pby8iLCJyb2xlIjpbInN0dWRlbnQiLCJwcmVtaXVtIl0sIm5iZiI6MTcwMTY4OTQxMywiZXhwIjoxNzAxNjk2NjEzLCJpYXQiOjE3MDE2ODk0MTN9.DA__d-7v_mbueKvzFr_7zAY7Xln_E20NE2l777S61Qo
```

---

Requisição: **GET** http://localhost:5051/restrito

Autorização: Bearer Token

Resposta: **200 - OK** 

```txt
{
    "id": "1",
    "name": "xyz@balta.io",
    "email": "xyz@balta.io",
    "givenName": "André Baltieri",
    "image": "https://balta.io/"
}
```


</details>

<!--#endregion -->

<!--#region Claims Identity Extension -->

<details id="jwt-bearer-claimsidentityextension"><summary>Claims Identity Extension</summary>

<br/>

Criar uma classe **Extensions/ClaimTypesExtension.cs** para tratar a possibilidade de nulos:

```c#
using System.Security.Claims;

namespace JwtAspNet.Extensions;

public static class ClaimTypesExtension
{
    public static int Id(this ClaimsPrincipal user)
    {
        try
        {
            var id= user.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "0";
            return int.Parse(id);
        }
        catch
        {
            return 0;
        }
    }
    
    public static string Name(this ClaimsPrincipal user)
    {
        try
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
    
    public static string GivenName(this ClaimsPrincipal user)
    {
        try
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string Email(this ClaimsPrincipal user)
    {
        try
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
    
    public static string Image(this ClaimsPrincipal user)
    {
        try
        {
            return user.Claims.FirstOrDefault(c => c.Type == "image")?.Value ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
}
```

Alterar o **Program**:

```c#
app
    .MapGet(
        pattern: "/restrito",
        handler: (ClaimsPrincipal user) =>
            new
            {
                id = user.Id(),
                name = user.Name(),
                email = user.Email(),
                givenName = user.GivenName(),
                image = user.Image()
            })
    .RequireAuthorization();
```

</details>

<!--#endregion -->

<!--#endregion -->

<!--#region Criando um Sistema de Login -->

<h2 id="login">Criando um Sistema de Login</h2>

<!--#region Criando o projeto -->

<details id="login-projeto"><summary>Criando o projeto</summary>

<br/>

```ps
mkdir JwtStore
cd .\JwtStore\

dotnet new sln
dotnet new classlib -o JwtStore.Core
dotnet new classlib -o JwtStore.Infra
dotnet new web -o JwtStore.Api

dotnet sln add .\JwtStore.Api\
dotnet sln add .\JwtStore.Core\
dotnet sln add .\JwtStore.Infra\
```

A proposta deste módulo é a criação de um modelo de arquitetura mais vertical do que horizontal.

</details>

<!--#endregion -->

<!--#region Entity e Value Object -->

<details id="login-entity-vo"><summary>Entity e Value Object</summary>

<br/>

Começando pelo projeto **JwtStore.Core**.

Criação dos diretórios:
- **AccountContext** (contexto específico de contas)
- **SharedContext** (contexto compartilhado)

---

Dentro de **SharedContext** criar uma classe base **Entities/Entity.cs** para as entidade sendo abstrata para permitir somente ser herdada e não ser instanciada. 

Essa classe também  implementará a interface **IEquatable<Guid>** para possibilitar a sua comparação com outros objetos. 

> **IEquatable** define um método generalizado que um tipo de valor ou classe implementa para criar um método de tipo específico para determinar igualdade de instâncias.

A entidade base **Entity** terá:
- Uma propriedade pública **Id** do tipo **Guid** associado no construtor **protected** com **Guid.NewGuid()**. Ou seja, dada uma instância dela o **Id** será gerado;
- Dois métodos sendo **Equals** e **GetHashCode** que são a implementação da interface **IEquatable** utilizado para comparar a entidade com outra a partir do **Id**, e se precisar do **HashCode** será utilizado o método **GetHashCode** contido dentro do **Guid**.

**SharedContext/Entities/Entity.cs**:

```cs
namespace JwtStore.Core.SharedContext.Entities;

public abstract class Entity : IEquatable<Guid>
{
    protected Entity() 
        => Id = Guid.NewGuid();

    public Guid Id { get; }

    public bool Equals(Guid id) 
        => Id == id;

    public override int GetHashCode() 
        => Id.GetHashCode();
}

```

---

Ainda dentro de **SharedContext** criar uma classe base **ValueObjects/ValueObject.cs** para os objetos de valor sendo abstrata para permitir somente ser herdada e não ser instanciada. 

**SharedContext/ValueObjects/ValueObject.cs**:

```c#
namespace JwtStore.Core.SharedContext.ValueObjects;

public abstract class ValueObject
{

}
```

</details>

<!--#endregion -->

<!--#region String Extension -->

<details id="login-string-extension"><summary>String Extension</summary>

<br/>

Dentro de **AccountContext** criar uma classe **Entities/User.cs** que herdará de **Entity**.

```c#
using JwtStore.Core.SharedContext.Entities;

namespace JwtStore.Core.AccountContext.Entities;

public class User : Entity
{

}
```

---

Para evitar trabalhar com tipos primitivos e evitar repetição de código será trabalhado com **ValueObjects**.

Ainda dentro de **AccountContext** criar uma class **ValueObjects/Email.cs** que herdará de **ValueObject** que receberá a implementação de um e-mail conforme abaixo:

- Será trabalhado com uma expressão regular dentro de uma constante **Pattern**;
- Possuirá duas propriedades imutáveis **Address** e **Hash**. O **Hash** será a base64 de **Address**.

A conversão para base64 é algo comum é sempre criada dentro de **SharedContext**.

---

Dentro de **SharedContext** será criada uma classe estática de extensão **Extensions/StringExtension.cs** que conterá um método para conversão para base64.

```c#
using System.Text;

namespace JwtStore.Core.SharedContext.Extensions;

public static class StringExtension
{
    public static string ToBase64(this string text)
        => Convert.ToBase64String(Encoding.ASCII.GetBytes(text));
}
```

---

Voltando ao **AccountContext** a propriedade **Hash** será inicializada com **Address.ToBase64()**

```c#
using JwtStore.Core.SharedContext.Extensions;
using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AccountContext.ValueObjects;

public class Email : ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    public string Address { get; }
    public string Hash => Address.ToBase64();
}
```

> gravatar.com

</details>

<!--#endregion -->

<!--#region Value Object de Email -->

<details id="login-vo-email"><summary>Value Object de Email</summary>

<br/>

Voltando para o **ValueObject** **Email** dentro de **AccountContext** teremos a verificação de uma **expressão regular** através do **source generator** que também requererá que a classe **Email** seja **partial**.

A validação é simples. Basta utilizar o atributo **GenerateRegex** e seu padrão **Pattern** e criar o método **partial** **Regex**.

Com isso, no construtor da classe **Email** sempre será recebido um endereço **address**. Ou seja, toda vez que for criado um e-mail deverá ser informado um endereço.

Serão inseridas algumas verificações para o endereço informado:
- se nulo ou vazio;
- tamanho menor que 5;
- verificar o endereço com o padrão da expressão regular.

Com isso já teremos o endereço e-mail validado e seu **Hash**.

---

Também serão inseridos três métodos. Dois para a conversão implícita **Implicit Operators** para conversão de e-mail para string e vice-versa, e outro para sobrescrever o métoto **ToString**.

```c#
    public static implicit operator string(Email email) => email.ToString();
    public static implicit operator Email(string address) => new Email(address);

    public override string ToString() => Address;
```

Os **implicit operators** dirão para o compilador como converter uma string em e-mail, e vice-versa.

---

**AccountContext/ValueObjects/Email.cs**:

```c#
using JwtStore.Core.SharedContext.Extensions;
using JwtStore.Core.SharedContext.ValueObjects;
using System.Text.RegularExpressions;

namespace JwtStore.Core.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    public Email(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new Exception("E-mail inválido");

        Address = address.Trim().ToLower();

        if (Address.Length < 5)
            throw new Exception("E-mail inválido");

        if (!EmailRegex().IsMatch(Address))
            throw new Exception("E-mail inválido");
    }

    public string Address { get; }
    public string Hash => Address.ToBase64();

    public static implicit operator string(Email email) => email.ToString();
    public static implicit operator Email(string address) => new Email(address);

    public override string ToString() => Address;

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
}
```

</details>

<!--#endregion -->

<!--#region Value Object de Verificação de E-mail -->

<details id="login-vo-email-verif"><summary>Value Object de Verificação de E-mail</summary>

<br/>

Verificação de e-mail para validação ativa. Para cada e-mail gerado, deve ser gerado um código de verificação.

Será criado uma classe **ValueObjects/Verification** dentro de **AccountContext** que poderá ser reutilizada para outras verificações. Está classe também herdará de **ValueObject**.

Declarar as propriedades
- **Code**, propriedade imutável para o código de verificação;
- **ExpiresAt**, propriedade para armazenar a data e hora da expiração do código de verificação com prazo de 5 minutos;
- **VerifiedAt**, propriedade para armazenar a data e hora de quando o código foi verificado.

```c#
    public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;
```

> **ToString("N")** retira os traços do **Guid**
> **[..6]** recupera somente os seis primeiros caracteres
> Sempre utilizar data e hora **UTC** para independer de **TimeZone**

Também a criação de uma propriedade **IsActive** para retornar se o código de verificação está ativo ou não.

```c#
    public bool IsActive
        => VerifiedAt != null && ExpiresAt == null;
```

---

Criação do método **Verify** para verificação do código.

```c#
    public void Verify (string code)
    {
        if (IsActive)
            throw new Exception("Este item já foi ativado");

        if (ExpiresAt < DateTime.UtcNow)
            throw new Exception("Este código já expirou");

        if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de verificação inválido");
    }
```

---

**AccountContext/ValueObjects/Verification.cs**:

```c#
using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AccountContext.ValueObjects;

public class Verification : ValueObject
{
    public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;

    public bool IsActive
        => VerifiedAt != null && ExpiresAt == null;

    public void Verify (string code)
    {
        if (IsActive)
            throw new Exception("Este item já foi ativado");

        if (ExpiresAt < DateTime.UtcNow)
            throw new Exception("Este código já expirou");

        if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de verificação inválido");

        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }
}
```

</details>

<!--#endregion -->

<!--#region Value Object de Senha -->

<details id="login-vo-senha"><summary>Value Object de Senha</summary>

<br/>

Voltando ao **ValueObject** **Email.cs** teremos agora uma propriedade **Verification**:

```c#
public Verification Verification { get; private set; }
```

Além disso será criado um método **ResendVerification** para criar um novo código de verificação:

```c#
    public void ResendVerification()
        => Verification = new Verification();
```

E com isso, fechamos a implementação do **ValueObject** **Email**:

**AccountContext/ValueObjects/Email.cs**:

```c#
using JwtStore.Core.SharedContext.Extensions;
using JwtStore.Core.SharedContext.ValueObjects;
using System.Text.RegularExpressions;

namespace JwtStore.Core.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    public Email(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new Exception("E-mail inválido");

        Address = address.Trim().ToLower();

        if (Address.Length < 5)
            throw new Exception("E-mail inválido");

        if (!EmailRegex().IsMatch(Address))
            throw new Exception("E-mail inválido");
    }

    public string Address { get; }
    public string Hash => Address.ToBase64();
    public Verification Verification { get; private set; } = new();

    public void ResendVerification()
        => Verification = new Verification();

    public static implicit operator string(Email email) => email.ToString();
    public static implicit operator Email(string address) => new Email(address);

    public override string ToString() => Address;

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
}

```

---

Vamos voltar para a classe **User** será feito o uso do e-mail:

```c#
using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.SharedContext.Entities;

namespace JwtStore.Core.AccountContext.Entities;

public class User : Entity
{
    public string Nome { get; private set; }
    public Email Email { get; private set; }
}
```

---

Dentro de **AccountContext** vamos trabalhar com outro **ValueObject** que será chamado de **Password** que também herdará de **ValueObject**.

Cenários:
- Geração automática de senha
- Senha informada

Trabalharemos com duas constantes para os caracteres alfanuméricos e os especiais.

```c#
    private const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    private const string Special = "!@#$%ˆ&*(){}[];";
```

Criar propriedades:
- **Hash**;
- **ResetCode** que é o código para resetar a senha que será enviado para o usuário caso ele precise resetar a senha

---

**AccountContext/ValueObjects/Password.cs**:

```c#
using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AccountContext.ValueObjects;

public class Password : ValueObject
{
    private const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    private const string Special = "!@#$%ˆ&*(){}[];";

    public string Hash { get; } = String.Empty;
    public string ResetCode { get; } = Guid.NewGuid().ToString("N")[..8].ToUpper();
}
```

</details>

<!--#endregion -->

<!--#region Gerando senhas aleatórias -->

<details id="login-senha-aleat"><summary>Gerando senhas aleatórias</summary>

<br/>

A implementação da senha ficará um pouco complexa.

Referências:
- https://github.com/andrebaltieri/SecureIdentity
- https://www.nuget.org/packages/SecureIdentity

Criar um primeiro método **Generate** para gerar a senha:

```c#
    private static string Generate(
        short length = 16,
        bool includeSpecialChars = true,
        bool upperCase = false)
    {
        var chars = includeSpecialChars ? (Valid + Special) : Valid;
        var startRandom = upperCase ? 26 : 0;
        var index = 0;
        var res = new char[length];
        var rnd = new Random();

        while (index < length)
            res[index++] = chars[rnd.Next(startRandom, chars.Length)];

        return new string(res);
    }
```

Criar outro método para **Hash** da senha:

```c#
    private static string Hashing(
        string password,
        short saltSize = 16,
        short keySize = 32,
        int iterations = 10000,
        char splitChar = '.')
    {
        if (string.IsNullOrEmpty(password))
            throw new Exception("Password should not be null or empty");

        password += Configuration.Secrets.PasswordSaltKey;

        using var algorithm = new Rfc2898DeriveBytes(
            password,
            saltSize,
            iterations,
            HashAlgorithmName.SHA256);
        var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{iterations}{splitChar}{salt}{splitChar}{key}";
    }
```

---

**AccountContext/ValueObjects/Password.cs**:

```c#
using JwtStore.Core.SharedContext.ValueObjects;
using System.Security.Cryptography;

namespace JwtStore.Core.AccountContext.ValueObjects;

public class Password : ValueObject
{
    private const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    private const string Special = "!@#$%ˆ&*(){}[];";

    public string Hash { get; } = String.Empty;
    public string ResetCode { get; } = Guid.NewGuid().ToString("N")[..8].ToUpper();

    private static string Generate(
        short length = 16,
        bool includeSpecialChars = true,
        bool upperCase = false)
    {
        var chars = includeSpecialChars ? (Valid + Special) : Valid;
        var startRandom = upperCase ? 26 : 0;
        var index = 0;
        var res = new char[length];
        var rnd = new Random();

        while (index < length)
            res[index++] = chars[rnd.Next(startRandom, chars.Length)];

        return new string(res);
    }

    private static string Hashing(
        string password,
        short saltSize = 16,
        short keySize = 32,
        int iterations = 10000,
        char splitChar = '.')
    {
        if (string.IsNullOrEmpty(password))
            throw new Exception("Password should not be null or empty");

        password += Configuration.Secrets.PasswordSaltKey;

        using var algorithm = new Rfc2898DeriveBytes(
            password,
            saltSize,
            iterations,
            HashAlgorithmName.SHA256);
        var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{iterations}{splitChar}{salt}{splitChar}{key}";
    }
}

```

</details>

<!--#endregion -->

<!--#region Password Hashing -->

<details id="login-password"><summary>Password Hashing</summary>

<br/>

Concatenar a senha do usuário com uma outra senha segura.

Para isso criar uma nova classe estática **Configuration.cs** dentro na raiz projeto **JwtStore.Core**, aninhar uma nova classe **SecretsConfiguration** com as propriedade abaixo e expô-la publicamente:
- **ApiKey**
- **JwtPrivateKey**
- **PasswordSaltKey**

```c#
using System.Net.NetworkInformation;

namespace JwtStore.Core;

public static class Configuration
{
    public static SecretsConfiguration Secrets { get; set; } = new();

    public class SecretsConfiguration
    {
        public string ApiKey { get; set; } = string.Empty;
        public string JwtPrivateKey { get; set; } = string.Empty;
        public string PasswordSaltKey { get; set; } = string.Empty;
    }
}
```

Com isso o **password** de 16 bytes informado pelo usuário será concatenado com **PasswordSaltKey** e tornará a senha muito mais segura.

```c#
...

public class Password : ValueObject
{
    ...

    private static string Hashing(
        string password,
        short saltSize = 16,
        short keySize = 32,
        int iterations = 10000,
        char splitChar = '.')
    {
        ...

        password += Configuration.Secrets.PasswordSaltKey;

        using var algorithm = new Rfc2898DeriveBytes(
            password,
            saltSize,
            iterations,
            HashAlgorithmName.SHA256);
        var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{iterations}{splitChar}{salt}{splitChar}{key}";
    }
}    
```

Também foi especificado o algoritmo **Rfc2898DeriveBytes** que será utilizado para a criptografia.

> O melhor cenário é não ter que lidar com senha

</details>

<!--#endregion -->

<!--#region Comparando Hashes -->

<details id="login-hashes"><summary>Comparando Hashes</summary>

<br/>

Para finalizar teremos um método chamado **Verify** dentro da classe **Password**, pois após criptografar as senhas não temos mais acesso a seu conteúdo original. Não é possível descriptografá-la.

> Password One Way Only

```c#
    private static bool Verify(
        string hash,
        string password,
        short keySize = 32,
        int iterations = 10000,
        char splitChar = '.')
    {
        password += Configuration.Secrets.PasswordSaltKey;

        var parts = hash.Split(splitChar, 3);
        if (parts.Length != 3)
            return false;

        var hashIterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        if (hashIterations != iterations)
            return false;

        using var algorithm = new Rfc2898DeriveBytes(
            password,
            salt,
            iterations,
            HashAlgorithmName.SHA256);
        var keyToCheck = algorithm.GetBytes(keySize);

        return keyToCheck.SequenceEqual(key);
    }
```

</details>

<!--#endregion -->

<!--#region Finalizando a entidade User -->

<details id="login-user-entity"><summary>Finalizando a entidade User</summary>

<br/>

Criar as propriedades abaixo na entidade **User**:
- **Password**
- **Image**

Criar um construtor para receber:
- **Email**
- **Password**

Criar outro construtor **protected** para ser utilizado pelo **EntityFramework**

Criar os métodos:
- **UpdatePassword** (para resetar a senha, recebe o código enviado para o e-mail, por exemplo)
- **UpdateEmail** (atualiza o e-mail)
- **ChangePassword** (altera a senha)

> **plainTextPassword** está explícito para não confundir com senha hasheada

**AccountContext/Entities/User.cs**:

```c#
using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.SharedContext.Entities;

namespace JwtStore.Core.AccountContext.Entities;

public class User : Entity
{
    protected User()
    {
        
    }

    public User(string email, string? password = null)
    {
        Email = email;
        Password = new Password(password);
    }

    public string Nome { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public string Image { get; private set; } = string.Empty;

    public void UpdatePassword(string plainTextPassword, string code)
    {
        if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de restauração inválido");

        var password = new Password(plainTextPassword);
        Password = password;
    }

    public void UpdateEmail(Email email)
    {
        Email = email;
    }

    public void ChangePassword(string plainTextPassword)
    {
        var password = new Password(plainTextPassword);
        Password = password;
    }
}
```

---

Faz-se necessário criar o construtor da classe **Password** também:

```c#
    protected Password()
    {
    }

    public Password(string? text = null)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            text = Generate();

        Hash = Hashing(text);
    }
```

O construtor **protected Password()** é para ser utilizado pelo **EntityFramework**.

A criação deste construtor **public/protected** foi replicado para as demais classes **ValueObjects**:
- **Email**
- **Verification**

</details>

<!--#endregion -->

<!--#region Mapeando o User -->

<details id="login-user-map"><summary>Mapeando o User</summary>

<br/>

Projeto **JwtStore.Infra** para mapear os objetos para o banco de dados.

```ps
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.14
dotnet add reference ..\JwtStore.Core\
```

Para mapear objeto de valor será utilizado **OwnsOne**

Criar nova classe **AccountContext/Mappings/UserMap**

```c#
using JwtStore.Core.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JwtStore.Infra.AccountContext.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired(true);

        builder.Property(x => x.Image)
            .HasColumnName("Image")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired(true);

        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .IsRequired(true);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.Code)
            .HasColumnName("EmailVerificationCode")
            .IsRequired(true);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.ExpiresAt)
            .HasColumnName("EmailVerificationExpiresAt")
            .IsRequired(false);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.VerifiedAt)
            .HasColumnName("EmailVerificationVerifiedAt")
            .IsRequired(false);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Ignore(x => x.IsActive);

        builder.OwnsOne(x => x.Password)
            .Property(x => x.Hash)
            .HasColumnName("PasswordHash")
            .IsRequired();

        builder.OwnsOne(x => x.Password)
            .Property(x => x.ResetCode)
            .HasColumnName("PasswordResetCode")
            .IsRequired();
    }
}

```

</details>

<!--#endregion -->

<!--#region Criando o DataContext -->

<details id="login-datacontext"><summary>Criando o DataContext</summary>

<br/>

No projeto **JwtStore.Core** criar uma pasta **Contexts** e mover **AccountContext** e **SharedContext** para ela.

No projeto **JwtStore.Infra** criar uma pasta **Contexts** e mover **AccountContext** para ela.

> Recurso da IDE: **Sincronizar namespaces**

---

Voltando no projeto **JwtStore.Infra** criar uma classe **Data/AppDbContext.cs** que será o **DataContext**

```c#
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Infra.Contexts.AccountContext.Mappings;
using Microsoft.EntityFrameworkCore;

namespace JwtStore.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}
```

</details>

<!--#endregion -->

<!--#region Configurando a API -->

<details id="login-api-config"><summary>Configurando a API</summary>

<br/>

No projeto **JwtStore.Api** serão criadas as extensões para organizar a utilização das **MinimalAPIs** que tende a ficar desorganizado e inserido suporte a criação dos **Migrations** para criar o banco de dados.

No **AppSettings** definir a **ConnectionString** e os **Secrets**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=jwt-store;User ID=sa;Password=sa;Trusted_Connection=False; TrustServerCertificate=True;"
  },
  "Secrets": {
    "ApiKey": "wN~EQ!Pe46xw23026YiM",
    "JwtPrivateKey": "CK54q}V6r]3$7oaa/*b",
    "PasswordSaltKey": "Cm20]N5?4pb9%R+k8[L"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

> Recomendação: Utilizar o **dotnet secrets**

Adicionar uma referência ao projeto:

```ps
dotnet add reference ..\JwtStore.Core\
dotnet add reference ..\JwtStore.Infra\
```

Criar a classe **Extensions/BuilderExtensions.cs** e o método **AddConfiguration**:

```c#
    ...

    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.Database.ConnectionString =
            builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

        Configuration.Secrets.ApiKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey") ?? string.Empty;
        Configuration.Secrets.JwtPrivateKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;
        Configuration.Secrets.PasswordSaltKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("PasswordSaltKey") ?? string.Empty;
    }

    ...
```

Criar a configuração do **Database** em **Configuration.cs**:

```c#
using System.Net.NetworkInformation;

namespace JwtStore.Core;

public static class Configuration
{
    ...

    public static DatabaseConfiguration Database { get; set; } = new();

    ...

    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
    }
}
 
```

---

Ainda na classe **Extensions/BuilderExtensions.cs** criar o método **AddDatabase**:

```c#
    ...

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(x =>
            x.UseSqlServer(
                Configuration.Database.ConnectionString,
                b => b.MigrationsAssembly("JwtStore.Api")));
    }

    ...
```

Foi utilizado **MigrationsAssembly** pois o projeto de Infra é diferente do projeto de API. Com isso será possível utilizar **Migrations**

</details>

<!--#endregion -->

<!--#region Organizando a API -->

<details id="login-api-organizer"><summary>Organizando a API</summary>

<br/>

```ps
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 7.0.14
```

Criar método **AddJwtAuthentication**

```c#
    ...

    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        builder.Services.AddAuthorization();
    }

    ...
```

No **Program.cs** chamar os métodos de extensão criados:

```c#
...

builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();

...
```

</details>

<!--#endregion -->

<!--#region Gerando o banco de dados -->

<details id="login-bdados"><summary>Gerando o banco de dados</summary>

<br/>

JwtStore.Api/Program.cs

```c#
...
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
...
```

**Migrations**

> Sugestão para utilizar SQL Docker em https://balta.io/blog/sql-server-docker

```ps
pwd
JwtStore\JwtStore.Api>
```

```ps
dotnet ef migrations add v1
Build started...
Build succeeded.
Your startup project 'JwtStore.Api' doesn't reference Microsoft.EntityFrameworkCore.Design. This package is required for the Entity Framework Core Tools to work. Ensure your startup project is correct, install the package, and try again.

```

```ps
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.16
  Determinando os projetos a serem restaurados...
  Writing C:\Users\marce\AppData\Local\Temp\tmpqe2qk2.tmp
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adicionando PackageReference do pacote 'Microsoft.EntityFrameworkCore.Design' ao projeto 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Api\JwtStore.Api.csproj'.
info : Restaurando pacotes para C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Api\JwtStore.Api.csproj...
info :   CACHE https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.design/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.design/7.0.16/microsoft.entityframeworkcore.design.7.0.16.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.design/7.0.16/microsoft.entityframeworkcore.design.7.0.16.nupkg 232 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.relational/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.relational/index.json 571 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.relational/7.0.16/microsoft.entityframeworkcore.relational.7.0.16.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.relational/7.0.16/microsoft.entityframeworkcore.relational.7.0.16.nupkg 15 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore/index.json 556 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore/7.0.16/microsoft.entityframeworkcore.7.0.16.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore/7.0.16/microsoft.entityframeworkcore.7.0.16.nupkg 14 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.abstractions/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.analyzers/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.abstractions/index.json 150 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.abstractions/7.0.16/microsoft.entityframeworkcore.abstractions.7.0.16.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.abstractions/7.0.16/microsoft.entityframeworkcore.abstractions.7.0.16.nupkg 15 ms
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.analyzers/index.json 201 ms
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.analyzers/7.0.16/microsoft.entityframeworkcore.analyzers.7.0.16.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore.analyzers/7.0.16/microsoft.entityframeworkcore.analyzers.7.0.16.nupkg 22 ms
info : Microsoft.EntityFrameworkCore.Analyzers 7.0.16 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo C:\Users\marce\.nuget\packages\microsoft.entityframeworkcore.analyzers\7.0.16.
info : Microsoft.EntityFrameworkCore.Design 7.0.16 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo C:\Users\marce\.nuget\packages\microsoft.entityframeworkcore.design\7.0.16.
info : Microsoft.EntityFrameworkCore.Abstractions 7.0.16 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo C:\Users\marce\.nuget\packages\microsoft.entityframeworkcore.abstractions\7.0.16.
info : Microsoft.EntityFrameworkCore.Relational 7.0.16 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo C:\Users\marce\.nuget\packages\microsoft.entityframeworkcore.relational\7.0.16.
info : Microsoft.EntityFrameworkCore 7.0.16 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo C:\Users\marce\.nuget\packages\microsoft.entityframeworkcore\7.0.16.
info :   CACHE https://api.nuget.org/v3/vulnerabilities/index.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2024.02.21.05.25.41/vulnerability.base.json
info :   CACHE https://api.nuget.org/v3-vulnerabilities/2024.02.21.05.25.41/2024.02.24.11.26.11/vulnerability.update.json
info : O pacote 'Microsoft.EntityFrameworkCore.Design' é compatível com todas as estruturas especificadas no projeto 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Api\JwtStore.Api.csproj'.
info : PackageReference do pacote 'Microsoft.EntityFrameworkCore.Design' versão '7.0.16' adicionada ao arquivo 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Api\JwtStore.Api.csproj'.
info : Gerando arquivo do MSBuild C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Api\obj\JwtStore.Api.csproj.nuget.g.props.
info : Gerando arquivo do MSBuild C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Api\obj\JwtStore.Api.csproj.nuget.g.targets.
info : Gravando o arquivo de ativos no disco. Caminho: C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Api\obj\project.assets.json
log  : C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Api\JwtStore.Api.csproj restaurado (em 3,03 sec).
```

```ps
dotnet ef migrations add v1
Build started...
Build succeeded.
The Entity Framework tools version '7.0.13' is older than that of the runtime '7.0.16'. Update the tools for the latest features and bug fixes. See https://aka.ms/AAc1fbw for more information.
Done. To undo this action, use 'ef migrations remove'
```

```ps
dotnet ef database update
Build started...
Build succeeded.
The Entity Framework tools version '7.0.13' is older than that of the runtime '7.0.16'. Update the tools for the latest features and bug fixes. See https://aka.ms/AAc1fbw for more information.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (582ms) [Parameters=[], CommandType='Text', CommandTimeout='60']
      CREATE DATABASE [jwt-store];
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (212ms) [Parameters=[], CommandType='Text', CommandTimeout='60']
      IF SERVERPROPERTY('EngineEdition') <> 5
      BEGIN
          ALTER DATABASE [jwt-store] SET READ_COMMITTED_SNAPSHOT ON;
      END;
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (16ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (15ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [__EFMigrationsHistory] (
          [MigrationId] nvarchar(150) NOT NULL,
          [ProductVersion] nvarchar(32) NOT NULL,
          CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (22ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT OBJECT_ID(N'[__EFMigrationsHistory]');
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (25ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT [MigrationId], [ProductVersion]
      FROM [__EFMigrationsHistory]
      ORDER BY [MigrationId];
info: Microsoft.EntityFrameworkCore.Migrations[20402]
      Applying migration '20240224192248_v1'.
Applying migration '20240224192248_v1'.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (7ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [User] (
          [Id] uniqueidentifier NOT NULL,
          [Name] NVARCHAR(120) NOT NULL,
          [Email] nvarchar(max) NOT NULL,
          [EmailVerificationCode] nvarchar(max) NOT NULL,
          [EmailVerificationExpiresAt] datetime2 NULL,
          [EmailVerificationVerifiedAt] datetime2 NULL,
          [PasswordHash] nvarchar(max) NOT NULL,
          [PasswordResetCode] nvarchar(max) NOT NULL,
          [Image] VARCHAR(255) NOT NULL,
          CONSTRAINT [PK_User] PRIMARY KEY ([Id])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (17ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
      VALUES (N'20240224192248_v1', N'7.0.16');
Done.
```

![](./Assets/Images/login-bdados-gerando-01.png)


</details>

<!--#endregion -->

<!--#region Use Cases -->

<details id="login-usecases"><summary>Use Cases</summary>

<br/>

Criar diretório: 
**JwtStore.Core/AccountContext/UseCases/Create**

> UseCases - Ações dentro da aplicação

Criar as classes:
- Handler.cs
- Request.cs
- Response.cs
- Specification.cs

</details>

<!--#endregion -->

<!--#region Response -->

<details id="login-response"><summary>Response</summary>

<br/>

Adicionar o **Flunt** para as validações:

```ps
cd .\JwtStore.Core\
dotnet add package Flunt --version 2.0.5

  Determinando os projetos a serem restaurados...
  Writing C:\Users\marce\AppData\Local\Temp\tmpno4f4a.tmp
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adicionando PackageReference do pacote 'Flunt' ao projeto 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Core\JwtStore.Core.csproj'.
info : Restaurando pacotes para C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Core\JwtStore.Core.csproj...
info :   GET https://api.nuget.org/v3/vulnerabilities/index.json
info :   OK https://api.nuget.org/v3/vulnerabilities/index.json 22 ms
info :   GET https://api.nuget.org/v3-vulnerabilities/2024.02.21.05.25.41/vulnerability.base.json
info :   GET https://api.nuget.org/v3-vulnerabilities/2024.02.21.05.25.41/2024.02.26.05.26.29/vulnerability.update.json
info :   OK https://api.nuget.org/v3-vulnerabilities/2024.02.21.05.25.41/vulnerability.base.json 248 ms
info :   OK https://api.nuget.org/v3-vulnerabilities/2024.02.21.05.25.41/2024.02.26.05.26.29/vulnerability.update.json 272 ms
info : O pacote 'Flunt' é compatível com todas as estruturas especificadas no projeto 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Core\JwtStore.Core.csproj'.
info : PackageReference do pacote 'Flunt' versão '2.0.5' adicionada ao arquivo 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Core\JwtStore.Core.csproj'.
info : Gravando o arquivo de ativos no disco. Caminho: C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Core\obj\project.assets.json
log  : C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Core\JwtStore.Core.csproj restaurado (em 1,66 sec).
```

Criar diretório e classe de resposta padrão **JwtStore.Core/Context/SharedContext/UseCases/Response.cs**:

```c#
using Flunt.Notifications;

namespace JwtStore.Core.Contexts.SharedContext.UseCases;

public abstract class Response
{
    public string Message { get; set; } = string.Empty;
    public int Status { get; set; } = 400;
    public IEnumerable<Notification>? Notifications { get; set; }

    public bool IsSucess => Status is >= 200 and <= 299;
}
```

</details>

<!--#endregion -->

<!--#region Compondo a resposta -->

<details id="login-response-comp"><summary>Compondo a resposta</summary>

<br/>

**JwtStore.Core/Contexts/AccountContext/UseCases/Create/Response.cs:**

```c#
using Flunt.Notifications;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Response : SharedContext.UseCases.Response
{
    protected Response()
    {

    }

    public Response(
        string message, 
        int status, 
        IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }

    public Response(
        string message, 
        ResponseData data)
    {
        Message = message;
        Status = 201;
        Notifications = null;
        Data = data;
    }

    public ResponseData? Data { get; set; }
}

public record ResponseData(Guid Id, string Name, string Email);
```

</details>

<!--#endregion -->

<!--#region Specification -->

<details id="login-specification"><summary>Specification</summary>

<br/>

**JwtStore.Core/Contexts/AccountContext/UseCases/Create/Request.cs:**

```c#
namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public record Request(
    string Name,
    string Email,
    string Password
);

```

Como será trata a especificação externamente o **Request** pode ser um **record**

**JwtStore.Core/Contexts/AccountContext/UseCases/Create/Specification.cs:**

```c#
using Flunt.Notifications;
using Flunt.Validations;
using System.Diagnostics.Contracts;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Name.Length, 160, "Name","O nome deve conter menos que 160 caracteres")
            .IsGreaterThan(request.Name.Length, 3, "Name","O nome deve conter mais que 3 caracteres")
            .IsLowerThan(request.Password.Length, 40, "Password","A senha deve conter menos que 40 caracteres")
            .IsGreaterThan(request.Password.Length, 8, "Password","A senha deve conter mais que 8 caracteres")
            .IsEmail(request.Email, "Email", "E-mail inválido");
}

```

Uma especificação para cada **Use Case**. O **Request** será validado com base na especificação.

</details>

<!--#endregion -->

<!--#region Repositórios e Serviços -->

<details id="login-repo-services"><summary>Repositórios e Serviços</summary>

<br/>

**JwtStore.Core/Contexts/AccountContext/UseCases/Create/**

Criar diretório **Contracts** e classes **IRepository** e **IService**.

IRepository.cs:

```c#
namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IRepository
{

}
```

IService.cs:

```c#
namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IService
{

}
```

**JwtStore.Core/Contexts/AccountContext/UseCases/Create/Handler.cs:**

```c#
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Handler
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(
        IRepository repository,
        IService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Response> Handle(
        Request request, 
        CancellationToken cancellationToken)
    {

    }
}
```

> **Repositório** trata o que é referente à base de dados, e **serviço** ao que é externo (i.e., envio de e-mails).

</details>

<!--#endregion -->

<!--#region Validando a requisição -->

<details id="login-req-valid"><summary>Validando a requisição</summary>

<br/>

**JwtStore.Core/Context/AccountContext/UseCases/Create/Handler.cs**:

```c#
...
public async Task<Response> Handle(
    Request request, 
    CancellationToken cancellationToken)
{
    #region 01. Valida a requisição

    try
    {
        var res = Specification.Ensure(request);
        if (!res.IsValid)
            return new Response("Requisição inválida",400,res.Notifications);
    }
    catch 
    {
        return new Response("Não foi possível validar sua requisição", 500);
    }

    #endregion
    
    // 02 - Gerar os objetos
    // 03 - Verificar se o usuário existe
    // 04 - Persistir os dados
    // 05 - Enviar e-mail de ativação
}
...
```


</details>

<!--#endregion -->

<!--#region Gerando entidades de value objects -->

<details id="login-entity-vo-ger"><summary>Gerando entidades de value objects</summary>

<br/>


Criação de um novo construtor para **User** (JwtStore.Core/Contexts/AccountContext/Entities/):

```c#
    ...
    public User(string name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    ...
```

Passo 2 do método **Handle** (JwtStore.Core/Contexts/AccountContext/UseCases/Create/):

```c#
...
        #region 02. Gerar os objetos

        Email email;
        Password password;
        User user;

        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(request.Name,email,password);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);                        
        }

        #endregion
...
```


</details>

<!--#endregion -->

<!--#region Interagindo com repositórios -->

<details id="login-repo-interag"><summary>Interagindo com repositórios</summary>

<br/>

Criar a assinatura do método **AnyAsync** dentro de **IRepository** (JwtStore.Core/Contexts/AccountContext/UseCases/Create/Contracts/):

```c#
namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyAsync(string email, CancellationToken cancellationToken);
}
```

Passo 2 do método **Handle** (JwtStore.Core/Contexts/AccountContext/UseCases/Create/):

```c#
...
        #region 03. Verifica se o usuário existe

        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response("Este e-mail já está em uso", 400);
        }
        catch 
        {
            return new Response("Falha ao verificar e-mail cadastrado", 500);
        }

        #endregion
...
```

</details>

<!--#endregion -->

<!--#region Finalizando o handler -->

<details id="login-handler"><summary>Finalizando o handler</summary>

<br/>

Criar a assinatura do método **SaveAsync** dentro de **IRepository** (JwtStore.Core/Contexts/AccountContext/UseCases/Create/Contracts/):

```c#
using JwtStore.Core.Contexts.AccountContext.Entities;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyAsync(string email, CancellationToken cancellationToken);
    Task SaveAsync(User user, CancellationToken cancellationToken);
}
```

Criar a assinatura do método **SendVerificationEmailAsync** dentro de **IService** (JwtStore.Core/Contexts/AccountContext/UseCases/Create/Contracts/):

```c#
using JwtStore.Core.Contexts.AccountContext.Entities;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IService
{
    Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
}
```

Passos 4 e 5, e retorno com sucesso do método **Handle** (JwtStore.Core/Contexts/AccountContext/UseCases/Create/):

```c#
...
        #region 04. Persiste os dados

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir dados",500);
        }

        #endregion

        #region 05. Envia e-mail de ativação

        try
        {
            await _service.SendVerificationEmailAsync(user,cancellationToken);
        }
        catch
        {
            // do nothing
        }

        #endregion

        return new Response("Conta criada", new ResponseData(user.Id, user.Name, user.Email));
...
```

</details>

<!--#endregion -->

<!--#region Implementando o repositório -->

<details id="login-repo-impl"><summary>Implementando o repositório</summary>

<br/>

No projeto **JwtStore.Infra** criar as classes **Repository** e **Service** seguindo a mesma estrutura de diretórios do projeto **JwtStore.Core**.

Repository.cs

```c#
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Create;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) 
        => _context = context;

    public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken)
        => await _context
            .Users
            .AsNoTracking()
            .AnyAsync(x => x.Email.Address == email,cancellationToken);

    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

```

Service.cs

```c
namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Create;

public class Service
{
}

```

</details>

<!--#endregion -->

<!--#region Implementando o serviço -->

<details id="login-service-impl"><summary>Implementando o serviço</summary>

<br/>

```ps
cd .\JwtStore.Infra\
dotnet add package SendGrid

  Determinando os projetos a serem restaurados...
  Writing C:\Users\marce\AppData\Local\Temp\tmpubgp0p.tmp
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adicionando PackageReference do pacote 'SendGrid' ao projeto 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Infra\JwtStore.Infra.csproj'.
info :   GET https://api.nuget.org/v3/registration5-gz-semver2/sendgrid/index.json
info :   OK https://api.nuget.org/v3/registration5-gz-semver2/sendgrid/index.json 210 ms
info : Restaurando pacotes para C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Infra\JwtStore.Infra.csproj...
info :   GET https://api.nuget.org/v3-flatcontainer/sendgrid/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/sendgrid/index.json 622 ms
info :   GET https://api.nuget.org/v3-flatcontainer/sendgrid/9.29.2/sendgrid.9.29.2.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/sendgrid/9.29.2/sendgrid.9.29.2.nupkg 64 ms
info :   GET https://api.nuget.org/v3-flatcontainer/starkbank-ecdsa/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/starkbank-ecdsa/index.json 612 ms
info :   GET https://api.nuget.org/v3-flatcontainer/starkbank-ecdsa/1.3.3/starkbank-ecdsa.1.3.3.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/starkbank-ecdsa/1.3.3/starkbank-ecdsa.1.3.3.nupkg 68 ms
info : SendGrid 9.29.2 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo C:\Users\marce\.nuget\packages\sendgrid\9.29.2.
info : starkbank-ecdsa 1.3.3 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo C:\Users\marce\.nuget\packages\starkbank-ecdsa\1.3.3.
info :   GET https://api.nuget.org/v3/vulnerabilities/index.json
info :   OK https://api.nuget.org/v3/vulnerabilities/index.json 60 ms
info :   GET https://api.nuget.org/v3-vulnerabilities/2024.02.29.05.28.27/vulnerability.base.json
info :   GET https://api.nuget.org/v3-vulnerabilities/2024.02.29.05.28.27/2024.03.06.11.29.28/vulnerability.update.json
info :   OK https://api.nuget.org/v3-vulnerabilities/2024.02.29.05.28.27/vulnerability.base.json 64 ms
info :   OK https://api.nuget.org/v3-vulnerabilities/2024.02.29.05.28.27/2024.03.06.11.29.28/vulnerability.update.json 129 ms
info : O pacote 'SendGrid' é compatível com todas as estruturas especificadas no projeto 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Infra\JwtStore.Infra.csproj'.
info : PackageReference do pacote 'SendGrid' versão '9.29.2' adicionada ao arquivo 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Infra\JwtStore.Infra.csproj'.
info : Gravando o arquivo de ativos no disco. Caminho: C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Infra\obj\project.assets.json
log  : C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Infra\JwtStore.Infra.csproj restaurado (em 4,72 sec).
```

Implementar **Service.cs** (JwtStore.Infra/Contexts/AccountContext/UseCases/Create/)

Service.cs:

```c#
using JwtStore.Core;
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Create;

public class Service : IService
{
    public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
    {
        var client = new SendGridClient(Configuration.SendGrid.ApiKey);
        var from = new EmailAddress(Configuration.Email.DefaultFromEmail, Configuration.Email.DefaultFromName);
        const string subject = "Verifique sua conta";
        var to = new EmailAddress(user.Email, user.Name);
        var content = $"Código {user.Email.Verification.Code}";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
        await client.SendEmailAsync(msg, cancellationToken);
    }
}

```

Ajustar as configurações (JwtStore.Core.Configuration.cs):

```c#
...
    public static EmailConfiguration Email { get; set; } = new();
    public static SendGridConfiguration SendGrid { get; set; } = new();
...
    public class EmailConfiguration
    {
        public string DefaultFromEmail { get; set; } = "test@barbieri.dev.br";
        public string DefaultFromName { get; set; } = "barbieri.dev.br";
    }

    public class SendGridConfiguration
    {
        public string ApiKey { get; set; } = string.Empty;
    }
...
```

</details>

<!--#endregion -->

<!--#region Configurando a API -->

<details id="login-api-config-2"><summary>Configurando a API</summary>

<br/>

Sequência:
1. Core
2. Infra
3. API

Na API criar uma **extension** para cada contexto (i.e., **AccountContextExtension.cs**) e criar 2 métodos principais:
1. AddAccountContext
2. MapAccountEndpoints

AccountContextExtension.cs (JwtStore.Api/Extensions/):
```c#
namespace JwtStore.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {

    }

    public static void MapAccountEndpoints(this WebApplication app)
    {

    }
}
```

Program.cs
```c
using JwtStore.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();

builder.AddAccountContext();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAccountEndpoints();

app.Run();
```

</details>

<!--#endregion -->

<!--#region Registrando as dependências -->

<details id="login-depend-reg"><summary>Registrando as dependências</summary>

<br/>

Na implementação do **AddAccountContext**:

Um **region** para cada **UseCase** para mapear os serviços existentes.

- Interface
- Implementação

```c#
namespace JwtStore.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository, // Interface
            JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Repository>(); // Implementação

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts.IService, // Interface
            JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Service>(); // Implementação

        #endregion
    }

    public static void MapAccountEndpoints(this WebApplication app)
    {

    }
}
```

</details>

<!--#endregion -->

<!--#region Adicionando suporte ao Mediator -->

<details id="login-mediator"><summary>Adicionando suporte ao Mediator</summary>

<br/>

Na implementação do método **MapAccountEndpoints**

Criar **#region** por **UseCase**

Existe uma forma mais fácil de mapear os **Handlers** com o **pattern** **Mediator** que intermedeia as comunicações (i.e., usa essa requisição, acha o **handler** dela e gera a resposta)

Será necessário informar ao **Handler** e **Request** (JwtStore.Core/Contexts/AccountContext/UseCases/Create/) que será utilizado o **Mediator**

```ps
cd .\JwtStore.Core\
dotnet add package MediatR --version 12.1.1

  Determinando os projetos a serem restaurados...
  Writing C:\Users\marce\AppData\Local\Temp\tmpjhxzje.tmp
info : X.509 certificate chain validation will use the default trust store selected by .NET for code signing.
info : X.509 certificate chain validation will use the default trust store selected by .NET for timestamping.
info : Adicionando PackageReference do pacote 'MediatR' ao projeto 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Core\JwtStore.Core.csproj'.
info : Restaurando pacotes para C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Core\JwtStore.Core.csproj...
info :   GET https://api.nuget.org/v3-flatcontainer/mediatr/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/mediatr/index.json 646 ms
info :   GET https://api.nuget.org/v3-flatcontainer/mediatr/12.1.1/mediatr.12.1.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/mediatr/12.1.1/mediatr.12.1.1.nupkg 210 ms
info :   GET https://api.nuget.org/v3-flatcontainer/mediatr.contracts/index.json
info :   OK https://api.nuget.org/v3-flatcontainer/mediatr.contracts/index.json 214 ms
info :   GET https://api.nuget.org/v3-flatcontainer/mediatr.contracts/2.0.1/mediatr.contracts.2.0.1.nupkg
info :   OK https://api.nuget.org/v3-flatcontainer/mediatr.contracts/2.0.1/mediatr.contracts.2.0.1.nupkg 65 ms
info : MediatR.Contracts 2.0.1 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo C:\Users\marce\.nuget\packages\mediatr.contracts\2.0.1.
info : MediatR 12.1.1 instalado de https://api.nuget.org/v3/index.json com o hash de conteúdo C:\Users\marce\.nuget\packages\mediatr\12.1.1.
info :   GET https://api.nuget.org/v3/vulnerabilities/index.json
info :   OK https://api.nuget.org/v3/vulnerabilities/index.json 60 ms
info :   GET https://api.nuget.org/v3-vulnerabilities/2024.02.29.05.28.27/vulnerability.base.json
info :   GET https://api.nuget.org/v3-vulnerabilities/2024.02.29.05.28.27/2024.03.06.17.29.31/vulnerability.update.json
info :   OK https://api.nuget.org/v3-vulnerabilities/2024.02.29.05.28.27/vulnerability.base.json 63 ms
info :   OK https://api.nuget.org/v3-vulnerabilities/2024.02.29.05.28.27/2024.03.06.17.29.31/vulnerability.update.json 181 ms
info : O pacote 'MediatR' é compatível com todas as estruturas especificadas no projeto 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Core\JwtStore.Core.csproj'.
info : PackageReference do pacote 'MediatR' versão '12.1.1' adicionada ao arquivo 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Core\JwtStore.Core.csproj'.
info : Gravando o arquivo de ativos no disco. Caminho: C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Core\obj\project.assets.json
log  : C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Core\JwtStore.Core.csproj restaurado (em 3,18 sec).
```

Request.cs

```c#
...

public record Request(
    string Name,
    string Email,
    string Password
) : IRequest<Response>;

```

Handler.cs

```c#
...
public class Handler : IRequestHandler<Request, Response>
...
```

> **IRequestHandler** exige a implementação do método **Handle** anteriormente implementado

Nas extensões da API, criar um método de extensão para adicionar o **MediatR**, e nele informar onde serão encontrados os **handlers**.

BuilderExtension.cs:

```c#
...
    public static void AddMediatR(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(
            x => x.RegisterServicesFromAssembly(typeof(Configuration).Assembly)); // type(Configuration) = JWtStore.Core
    }
    ...
```

Program.cs:

```c#
...
builder.AddMediatR();
...
```

</details>

<!--#endregion -->

<!--#region Criando o método POST -->

<details id="login-met-post"><summary>Criando o método POST</summary>

<br/>

Implementar o mapeamento (JwtStore.Api/Extensions)

AccountContextExtension.cs

```c#
...
    public static void MapAccountEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost("api/v1/users",async (
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Request request,
            IRequestHandler<
                JwtStore.Core.Contexts.AccountContext.UseCases.Create.Request,
                JwtStore.Core.Contexts.AccountContext.UseCases.Create.Response> handler) => 
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSucess
                ? Results.Created("",result)
                : Results.Json(result, statusCode: result.Status);
        });

        #endregion
    }
...
```

</details>

<!--#endregion -->

<!--#region dotnet user secrets -->

<details id="login-user-secrets"><summary>dotnet user secrets</summary>

<br/>

Adicionar as configurações para **SendGrid** e **E-mail** (JwtStore.Api/Extensions/)

BuilderExtension.cs:

```c#
...
        Configuration.SendGrid.ApiKey =
            builder.Configuration.GetSection("SendGrid").GetValue<string>("ApiKey") ?? string.Empty;

        Configuration.Email.DefaultFromName =
            builder.Configuration.GetSection("Email").GetValue<string>("DefaultFromName") ?? string.Empty;
        Configuration.Email.DefaultFromEmail =
            builder.Configuration.GetSection("Email").GetValue<string>("DefaultFromEmail") ?? string.Empty;
...
```

AppSettings.json

```json
...
  "SendGrid": {
    "ApiKey": ""
  },
  "Email": {
    "DefaultFromName": "barbieri.dev",
    "DefaultFromEmail":  "marcelo@barbieri.dev"
  },
...
```

```ps
cd .\JwtStore.Api\
dotnet user-secrets init

Set UserSecretsId to '4f0c595c-3627-4a6a-977d-37611f61281c' for MSBuild project 'C:\DEV\clone\backend-dotnet-api-security-baltaio\JwtStore\JwtStore.Api\JwtStore.Api.csproj'.
```

```ps
dotnet user-secrets set "SendGrid:ApiKey" "TOKEN_SENDGRID"

Successfully saved SendGrid:ApiKey = TOKEN_SENDGRID to the secret store.
```

```ps
dotnet user-secrets set "Email:DefaultFromName" "barbieri.dev"

Successfully saved Email:DefaultFromName = barbieri.dev to the secret store.
```

```ps
dotnet user-secrets set "Email:DefaultFromEmail" "marcelo@barbieri.dev"

Successfully saved Email:DefaultFromEmail = marcelo@barbieri.dev to the secret store.
```

Limpar tudo:

**dotnet user-secrets clear** 

</details>

<!--#endregion -->

<!--#region Testando a API -->

<details id="login-api-test"><summary>Testando a API</summary>

<br/>

![](./Assets/Images/login-testando-api-01.png)    
![](./Assets/Images/login-testando-api-02.png)    
![](./Assets/Images/login-testando-api-03.png)    

</details>

<!--#endregion -->

<!--#region Authenticate Use Case -->

<details id="login-auth-usecase"><summary>Authenticate Use Case</summary>

<br/>

Criação o caso de uso **Authenticate** com a mesma estrutura do **Create** dentro de **JwtStore.Core/Contexts/AccountContext/UseCases/Authenticate**:

- Contracts/
    - IRepository.cs
- Handler.cs
- Request.cs
- Response.cs
- Specification.cs

Request.cs:

```c#
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public record Request(
    string Name,
    string Email,
    string Password
) : IRequest<Response>;

```

Response.cs:

```c#
using Flunt.Notifications;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Response : SharedContext.UseCases.Response
{
    protected Response()
    {

    }

    public Response(
        string message,
        int status,
        IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }

    public Response(
        string message,
        ResponseData data)
    {
        Message = message;
        Status = 201;
        Notifications = null;
        Data = data;
    }

    public ResponseData? Data { get; set; }
}

public class ResponseData
{
    public string Token { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
}

```

Specification.cs:

```c#
using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Password.Length, 40, "Password", "A senha deve conter menos que 40 caracteres")
            .IsGreaterThan(request.Password.Length, 8, "Password", "A senha deve conter mais que 8 caracteres")
            .IsEmail(request.Email, "Email", "E-mail inválido");
}

```

</details>

<!--#endregion -->

<!--#region Authenticate Handler -->

<details id="login-auth-handlers"><summary>Authenticate Handler</summary>

<br/>

Password.cs (JwtStore.Core/Contexts/AccountContext/ValueObjects/):

```c#
...
    public bool Challenge(string plainTextPassword)
        => Verify(Hash, plainTextPassword);
...
```

IRepository.cs (JwtStore.Core/Contexts/AccountContext/Authenticate/Contracts):

```c#
...
Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
...
```

Handler.cs (JwtStore.Core/Contexts/AccountContext/Authenticate/)

```c#
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Handler : IRequestHandler<Request, Response>    
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
        => _repository = repository;    

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Valida a requisição

        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição inválida", 400, res.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }

        #endregion

        #region 02. Recupera o perfil

        User? user;
        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user is null)
                return new Response("Perfil não encontrado", 404);
        }
        catch (Exception e)
        {
            return new Response("Não foi possível recuperar seu perfil", 500);
        }

        #endregion

        #region 03. Checa se a senha é válida

        if (!user.Password.Challenge(request.Password))
            return new Response("Usuário ou senha inválidos", 400);

        #endregion

        #region 04. Checa se a conta está verificada

        try
        {
            if (!user.Email.Verification.IsActive)
                return new Response("Conta inativa", 400);
        }
        catch
        {
            return new Response("Não foi possível verificar seu perfil", 500);
        }

        #endregion

        #region 05. Retorna os dados

        try
        {
            var data = new ResponseData
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Roles = Array.Empty<string>()
            };

            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Não foi possível obter os dados do perfil", 500);
        }

        #endregion        
    }
}

```

</details>

<!--#endregion -->

<!--#region Authenticate Repository -->

<details id="login-auth-repo"><summary>Authenticate Repository</summary>

<br/>

Implementação do repositório no projeto de infraestrutura.

Repository.cs (JwtStore.Infra/Contexts/AccountContext/UseCases/):

```c#
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using JwtStore.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Authenticate;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
        => _context = context;    

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email,cancellationToken);
}

```

Ajustar a extensão.

AccountContextExtension.cs (JwtStore.Api/Extensions/):

```c#
...
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        ...

        #region Authenticate

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository, // Interface
            JwtStore.Infra.Contexts.AccountContext.UseCases.Authenticate.Repository>(); // Implementação

        #endregion
    }

...

    public static void MapAccountEndpoints(this WebApplication app)
    {
        ...

        #region Authenticate

        app.MapPost("api/v1/authenticate", async (
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Request request,
            IRequestHandler<
                JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Request,
                JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler) =>
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSucess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        });

        #endregion
    }
}
```

![](./Assets/Images/login-authenticate-repository-01.png)

![](./Assets/Images/login-authenticate-repository-02.png)

![](./Assets/Images/login-authenticate-repository-03.png)

</details>

<!--#endregion -->

<!--#region JWT Service -->

<details id="login-jwt-service"><summary>JWT Service</summary>

<br/>

Implementação da autenticação e autorização. O **token** é algo exclusivo e de responsabilidade da API.

Criação do **JwtExtension** (JwtStore.Api/Extensions/) para gerar um **token**.

JwtExtension.cs:

```c#
using JwtStore.Core;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtStore.Api.Extensions;

public static class JwtExtension
{
    public static string Generate(ResponseData data)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(data),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = credentials,
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(ResponseData user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim("Id", user.Id));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Name));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        foreach (var role in user.Roles)
            ci.AddClaim(new Claim(ClaimTypes.Role, role));

        return ci;
    }
}
```

Criação de outra extensão **ClaimsPrincipalExtension**:

```c#
using System.Security.Claims;

namespace JwtStore.Api.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string Id(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value ?? string.Empty;

    public static string Name(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;

    public static string Email(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
}

```

</details>

<!--#endregion -->

<!--#region Retornando o Token -->

<details id="login-token-ret"><summary>Retornando o Token</summary>

<br/>

No método de extensão **MapAccountEndpoints** de **AccountContextExtension** faz-se necessário para o **Authenticate** retornar o **token**.

```c#
...

public static void MapAccountEndpoints(this WebApplication app)
{

    ...

    #region Authenticate

    app.MapPost("api/v1/authenticate", async (
        JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Request request,
        IRequestHandler<
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Request,
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler) =>
    {
        var result = await handler.Handle(request, new CancellationToken());
        if (!result.IsSucess)
            return Results.Json(result, statusCode: result.Status);

        if (result.Data is null)
            return Results.Json(result, statusCode: 500);

        result.Data.Token = JwtExtension.Generate(result.Data);

        return Results.Ok(result);
    });

    #endregion

...
```

![](./Assets/Images/login-authenticate-ret-token.png)

</details>

<!--#endregion -->

<!--#region Criando Roles -->

<details id="login-roles"><summary>Criando Roles</summary>

<br/>

A autorização dependerá dos **roles**

Dentro de **AccountContext/Entities** (JwtStore.Core/Contexts/) criar a classe **Role.cs**:

```c#
using JwtStore.Core.Contexts.SharedContext.Entities;

namespace JwtStore.Core.Contexts.AccountContext.Entities;

public class Role : Entity
{
    public string Name { get; set; } = string.Empty;
}
```

Ajustar o **AppDbContext** (JwtStore.Infra/Data/):

```c#
...
public class AppDbContext : DbContext
{
    ...

    public DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ...
        modelBuilder.ApplyConfiguration(new RoleMap());
    }
...
```

Criar um novo mapeamento **RoleMap.cs** (JwtStore.Infra/Contexts/AccountContext/Mappings/):

```c#
using JwtStore.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JwtStore.Infra.Contexts.AccountContext.Mappings;

public class RoleMap : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired(true);
    }
}

```

Ajustar as entidades pois um usuário pode ter vários perfis.

User.cs (JwtStore.Core/Contexts/AccountContext/Entities/):

```c#
...
public IEnumerable<Role> Roles { get; set; } = Enumerable.Empty<Role>();
...
```

</details>

<!--#endregion -->

<!--#region Mapeando Roles -->

<details id="login-roles-map"><summary>Mapeando Roles</summary>

<br/>

Ajustar a entidade **Role.cs** para conter uma propriedade com os usuários.

Role.cs (JwtStore.Core/Contexts/AccountContext/Entities/):

```c#
...
public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();
...
```

Fazer o mapeamento de **Role** em **UserMap**.

UserMap.cs (JwtStore.Infra/Contexts/AccountContext/Mappings/):

```c#
...
        builder
            .HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                role => role
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                User => User
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade));
...
```

Executar o **Migration** novamente:

```ps
cd .\JwtStore.Api\
```

```ps
dotnet ef migrations add v2

Build started...
Build succeeded.
Done. To undo this action, use 'ef migrations remove'
```

```ps
dotnet ef database update

Build started...
Build succeeded.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (51ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (55ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT OBJECT_ID(N'[__EFMigrationsHistory]');
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT OBJECT_ID(N'[__EFMigrationsHistory]');
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (92ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT [MigrationId], [ProductVersion]
      FROM [__EFMigrationsHistory]
      ORDER BY [MigrationId];
info: Microsoft.EntityFrameworkCore.Migrations[20402]
      Applying migration '20240308235544_v2'.
Applying migration '20240308235544_v2'.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (154ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Role] (
          [Id] uniqueidentifier NOT NULL,
          [Name] NVARCHAR(120) NOT NULL,
          CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (124ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [UserRole] (
          [RoleId] uniqueidentifier NOT NULL,
          [UserId] uniqueidentifier NOT NULL,
          CONSTRAINT [PK_UserRole] PRIMARY KEY ([RoleId], [UserId]),
          CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE,
          CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (24ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_UserRole_UserId] ON [UserRole] ([UserId]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (135ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
      VALUES (N'20240308235544_v2', N'7.0.16');
Done.
```

![](./Assets/Images/login-map-roles-01.png)

![](./Assets/Images/login-map-roles-02.png)

</details>

<!--#endregion -->

<!--#region Recuperando os Roles -->

<details id="login-roles-get"><summary>Recuperando os Roles</summary>

<br/>

![](./Assets/Images/login-criando-role-01.png)    

Alterar as entidades **Role** e **User** de **IEnumerable** para **List**

Role.cs (/JwtStore.Core/Context/AccountContext/Entities/):

```c#
...
public List<User> Users { get; set; } = new();
...
```

User.cs (/JwtStore.Core/Context/AccountContext/Entities/):

```c#
...
public List<Role> Roles { get; set; } = new();
...
```

</details>

<!--#endregion -->

<!--#region Adicionando Roles ao Token -->

<details id="login-roles-add"><summary>Adicionando Roles ao Token</summary>

<br/>

Ajustar o passo 5 do **Handler** (JwtStore.Core/Contexts/AccountContext/UseCases/Authenticate/) para retornar os **Roles**:

```c#
...
        Roles = user.Roles.Select(x => x.Name).ToArray()
...
```

![](./Assets/Images/login-util-roles-01.png)

![](./Assets/Images/login-util-roles-02.png)

</details>

<!--#endregion -->

<!--#region Utilizando os Roles -->

<details id="login-roles-util"><summary>Utilizando os Roles</summary>

<br/>

Utilizar 

```c#
        .RequireAuthorization("Admin,Student");
```

ou

```c$
        .AllowAnonymous();
```        

no mapeamento dos endpoints dos referidos contextos.

</details>

<!--#endregion -->

<!--#region Conclusão -->

<details id="login-conclusao"><summary>Conclusão</summary>

<br/>

![](./Assets/Images/login-conclusao-01.png)

</details>

<!--#endregion -->

<!--#endregion -->
