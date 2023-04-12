# Exercice Console C# .NET 6.0, BDD DAO
### Contexte du projet
Premier Repository/DAO pour le chien
1. Créer une classe Dog dans son propre fichier qui contiendra toutes les propriétés correspondante à la table chien (un id en int32, un name en string, une breed en string et une birthdate en datetime)
2. Créer ensuite une classe DogRepository avec une méthode findAll qui renverra une List<Dog>
3. Au début de la méthode on crée une variable avec une liste et qu'on retourne à la fin et faire une nouvelle instance de la classe Dog et l'ajouter à l'intérieur de la liste
4. On met le result.Read() dans un while et voilà on a une méthode qui récupère tous les chiens et les transformes en instances de Dog
5. Bonus rajouté des arguments optionnels au findAll pour faire de la pagination et récupérer les chiens 15 par 15 (ou 10 par 10 ou autres)

Processus inverse, Dog To SQL
1. Créer une nouvelle méthode save(Dog dog) dans le repository et cette fois ci au lieu de faire un SELECT faire un INSERT INTO pour faire persister un nouveau chien en base de données
2. Dans cette requête, faire en sorte d'injecter les données de notre Dog dans les VALUES de la requête avant de l'exécuter
3. Trouver une manière de récupérer l'id auto incrémenté pou l'assigner à l'instance de dog
4. Bonus faire en sorte d'externaliser la connexion à la base de données et aussi d'empêcher les injections SQL