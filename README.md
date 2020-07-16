# Desafio prático do AceleraDev C# - Codenation 2020
Este projeto consite numa WebApi escrita em C# com .Net Core 3.0, afim de cumprir o desafio "Central de Erros" descrito abaixo.

### Objetivo
Em projetos modernos é cada vez mais comum o uso de arquiteturas baseadas em serviços ou microsserviços. Nestes ambientes complexos, erros podem surgir em diferentes camadas da aplicação (backend, frontend, mobile, desktop) e mesmo em serviços distintos. Desta forma, é muito importante que os desenvolvedores possam centralizar todos os registros de erros em um local, de onde podem monitorar e tomar decisões mais acertadas. Neste projeto vamos implementar um sistema para centralizar registros de erros de aplicações.

**Backend - API**
* criar endpoints para serem usados pelo frontend da aplicação
* criar um endpoint que será usado para gravar os logs de erro em um banco de dados relacional
* a API deve ser segura, permitindo acesso apenas com um token de autenticação válido

### Wireframes
Os seguintes wireframes foram disponibilizados para basear a aplicação que devia ser desenvolvida.
<img src="/Assets/Images/1-cadastro.png" width="514" height="380">
<img src="/Assets/Images/2-login.png" width="514" height="380">
<img src="/Assets/Images/3-dashboard.png" width="514" height="380">
<img src="/Assets/Images/7-detalhes.png" width="514" height="380">

### Tecnologias utilizadas
* .Net Core 3.0
* Entity Framework Core 3.1.5
* Microsoft SQL Server LocalDB
* JWT Bearer

### Banco de dados
O banco de dados utilizado é **mssqllocaldb**, cujo backup está em **/Database/CodenationProjetoPratico.bak** e pode ser restaurado através do seguinte comando:
```
RESTORE DATABASE MyDatabase
FROM DISK = ‘<path>\CodenationProjetoPratico.bak’
WITH MOVE ‘CodenationProjetoPratico’ TO ‘<path>\CodenationProjetoPratico.mdf’,
MOVE ‘CodenationProjetoPratico_log’ TO ‘<path>\CodenationProjetoPratico.ldf’,
REPLACE;
```
Alternativamente é possível criar manualmente um banco de dados vazio e executar a **Migration 'Inicial'** disponível no projeto.
Entetanto, nesta alternativa dados de exemplo não estarão presentes.

O ERD abaixo representa a modelagem do banco de dados.
![](/Assets/Images/ERD.png)

### API
Ao executar o projeto usando Kestrel, a API estará disponível pela URL **http://localhost:5000**.
Os endpoints disponíveis são os seguintes:

| Endpoint | Métodos |
|---|---|
| /api/ambiente | GET, POST, PUT |
| /api/ambiente/{id} | GET, DELETE |
| /api/log | GET, POST |
| /api/log/{id} | GET, PUT, DELETE |
| /api/tipolog | GET, POST, PUT |
| /api/tipolog/{id} | GET, DELETE |
| /api/usuario | GET, POST, PUT |
| /api/usuario/{id} | GET, DELETE |
| /api/usuario/login | POST |

A documentação completa da API está acessível através da URL **http://localhost:5000/swagger/index.html**.
