Documentation du Design et du Déploiement de TeamService

1. Design du Service TeamService
1.1. Architecture
Le TeamService est conçu comme un microservice dans une architecture de microservices. Voici les principales composantes :

Microservice TeamService : Gère les équipes sportives. Communique avec PlayerService pour obtenir des informations sur les joueurs et avec MatchService pour les opérations liées aux matchs.
MongoDB : Base de données NoSQL utilisée pour stocker les informations sur les équipes.
RabbitMQ : Broker de messages utilisé pour la communication asynchrone entre les microservices.
Swagger : Outil de documentation API pour faciliter les tests et l'interaction avec l'API.
1.2. Conception des Endpoints API
Les points de terminaison de l'API sont conçus pour gérer les opérations courantes sur les équipes :

GET /api/team : Récupère toutes les équipes.

Réponse : Liste de toutes les équipes en JSON.
GET /api/team/{teamId} : Récupère une équipe par son ID.

Réponse : Détails de l'équipe demandée en JSON.
POST /api/team : Crée une nouvelle équipe.

Demande : Détails de l'équipe à créer en JSON.
Réponse : Confirmation de la création de l'équipe.
POST /api/team/{teamId}/add-player : Ajoute un joueur à une équipe.

Demande : ID du joueur à ajouter (en JSON).
Réponse : Confirmation de l'ajout du joueur.
1.3. Conception de la Base de Données
Collection Teams : Stocke les informations sur les équipes, y compris les ID des joueurs associés.

1.4. Conception de la Communication Asynchrone
RabbitMQ : Utilisé pour envoyer des messages entre les microservices lorsque des opérations critiques se produisent, comme l'ajout d'un joueur à une équipe.
Queue : playerTeamUpdates pour les mises à jour sur les joueurs ajoutés aux équipes.
2. Déploiement du Service TeamService
2.1. Prérequis
Avant le déploiement, assurez-vous que les éléments suivants sont en place :

MongoDB : Installez et configurez MongoDB pour le stockage des données.
RabbitMQ : Installez et configurez RabbitMQ pour la communication asynchrone.
Environnement d'exécution .NET : Assurez-vous que le runtime .NET 6.0 est installé sur le serveur de déploiement.
2.2. Déploiement Local
Cloner le Répertoire

Clonez le dépôt contenant le code source du service TeamService :

bash
Copy code
git clone https://github.com/your-repo/TeamService.git
cd TeamService
Restauration des Dépendances

Restaurez les dépendances NuGet :

bash
Copy code
dotnet restore
Configuration

Mettez à jour le fichier appsettings.json avec les informations de connexion appropriées :

json
Copy code
{
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017/yourdatabase",
    "RabbitMQ": "amqp://guest:guest@localhost:5672"
  }
}
Exécution

Exécutez le service en local :

bash
Copy code
dotnet run
Accès à Swagger

Swagger UI sera accessible à l'adresse suivante : https://localhost:5001/swagger si vous avez configuré Swagger comme indiqué.




Déploiement en Production
Préparer l'Environnement

Préparez l'environnement de production avec MongoDB et RabbitMQ. Assurez-vous que les services sont accessibles à partir de l'application.

Construire le Conteneur Docker

Si vous utilisez Docker, créez un Dockerfile pour le service TeamService :

dockerfile
Copy code
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TeamService/TeamService.csproj", "TeamService/"]
RUN dotnet restore "TeamService/TeamService.csproj"
COPY . .
WORKDIR "/src/TeamService"
RUN dotnet build "TeamService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeamService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamService.dll"]

Construisez l'image Docker : docker build -t teamsservice .

Exécutez le conteneur Docker : docker run -d -p 80:80 teamsservice

Déploiement sur le Serveur

Déployez le conteneur Docker ou le binaire compilé sur le serveur de production. Assurez-vous que MongoDB et RabbitMQ sont correctement configurés et accessibles.

Configuration de l'URL de Service

Assurez-vous que l'URL de PlayerService est correctement configurée dans les paramètres du service, pour permettre au TeamService de communiquer avec lui.

Conclusion
Cette documentation vous guide à travers les phases de design et de déploiement du TeamService. Suivez ces étapes pour configurer et déployer le service correctement, en veillant à ce que toutes les dépendances et configurations nécessaires soient en place.
