Template WebApi with Swagger client.
Code example borrowed from several places;
https://dzone.com/articles/end-to-end-tutorial-for-developing-amp-deploying-a

*** FIND OTHER ARTICLES***


Uses EF Framework with Sqlite for local development and MS SQL DB when configured as explained below.

Swagger client is generated every build. 
(Client Factory must be maintained)
Client Project then contains a client and models and can be publised.

Includes integration tests using TestServer.

EF Migrations steps:
1) Delete existing Service\Migrations\*
2) dotnet ef migrations add InitialCreate

**** INSTRUCTIONS ON SETTING UP IN AZURE******