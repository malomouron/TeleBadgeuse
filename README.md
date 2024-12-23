# TéléBadgeuse

Application de bureau Windows permettant d’enregistrer ses débuts et fins de journée, de consulter ses pointages et de suivre son temps de travail.

## Fonctionnalités

- Enregistrer un début ou une fin de journée avec confirmation.
- Consulter les pointages et les heures travaillées, restantes ou supplémentaires.
- Parcourir l’historique par jour, mois ou année, en affichage liste ou grille.
- Choisir le thème clair ou sombre et l’interface française ou anglaise.
- Préparer un récapitulatif quotidien dans le client de messagerie par défaut.

## Prérequis

- Windows, requis par WPF.
- .NET 8 SDK.

## Construire et lancer

Depuis la racine du dépôt, dans PowerShell :

```powershell
dotnet restore .\TeleBadgeuse.sln
dotnet build .\TeleBadgeuse.sln --configuration Release
dotnet run --project .\TeleBadgeuse\TeleBadgeuse.csproj
```

## Tests

Exécuter les tests unitaires avec :

```powershell
dotnet test .\TeleBadgeuseTestUnit\TeleBadgeuseTestUnit.csproj --configuration Release
```

## Données locales et configuration e-mail

L’application stocke les pointages et les préférences dans une base SQLite `data.db`, créée dans le répertoire de travail de l’application. Cette base peut contenir des horaires personnels : conservez-en une sauvegarde et ne la publiez pas.

Le partage ouvre un nouveau message dans le client de messagerie par défaut ; l’application ne l’envoie pas automatiquement. Pour configurer le destinataire et le contenu du message, copiez `TeleBadgeuse/config.example.json` vers `TeleBadgeuse/config.json`, puis renseignez les champs `mail.name`, `mail.nameTo`, `mail.to` et `mail.cc`. Le fichier personnel `config.json` est ignoré par Git ; lorsqu’il est absent, le modèle est copié dans le dossier de sortie.

## Licence et contributeurs

Le projet est distribué sous licence [MIT](LICENSE.txt). Voir [CONTRIBUTORS.md](CONTRIBUTORS.md) pour les contributeurs et les mentions d’assistance.
