# GD2 Bres Quentin Unity Cours

Le projet original appartenait à Quentin, mais le renommer entraînait trop d’erreurs.



Read Me Final Ilana Linares Roll A Ball GD2



README – Projet Unity : Crystal Run



INFORMATIONS GÉNÉRALES



Nom du projet : Crystal Run

Version Unity : 6000.2 (Unity 6)

Pipeline de rendu : URP (Universal Render Pipeline)

Type de jeu : Plateforme / Collecte / Évasion



CONCEPT DU JEU



Le joueur contrôle une boule qui parcourt un environnement 3D composé de plateformes, de zones dangereuses et de cristaux à collecter.

L’objectif est de ramasser tous les cristaux avant la fin du temps imparti, tout en évitant les zones mortelles et en utilisant les plateformes mobiles pour atteindre la fin du niveau.

Le jeu repose sur la précision, la gestion du timing et l’observation.



SCRIPTS DU JOUEUR



PlayerMovement.cs

Objectif : Gérer les déplacements de la boule

Déplacement fluide avec les touches ZQSD

Détection du sol avec un rayon invisible vers le bas

Physique adaptée pour un mouvement rond  



PlayerHealth.cs

Objectif : Gérer la vie et la mort du joueur

Mort au contact d’un ennemi ou la fin du temps impartie

Respawn au point de départ

Réinitialise les cristaux collectés à chaque mort



Player\_Collect.cs

Objectif : Gérer la collecte des cristaux et des bonus

Compte le nombre de cristaux collectés

Met à jour le score affiché à l’écran

Vérifie si tous les cristaux ont été ramassés pour déclencher la victoire

C’est l’inventaire du joueur, il garde la trace de tout ce que vous ramassez.



SCRIPTS DE CAMÉRA

CameraFollow.cs

Objectif : Suivre la boule de manière fluide

Suit automatiquement le joueur avec un léger décalage

Mouvement lissé pour éviter les secousses

Position stable pour garder une bonne visibilité du terrain





SCRIPTS D’ENNEMIS



EnemyCube.cs

Objectif : Ennemi qui patrouille aléatoirement

Se déplace dans une direction aléatoire

Change de direction quand il touche un mur

Tue le joueur au contact



EnemySpawner.cs

Objectif : Créer des ennemis automatiquement

Fait apparaître des ennemis à intervalles réguliers

Zone de spawn configurable

Nombre maximum d’ennemis limité





SCRIPTS DE COLLECTIBLES



BaseCollectible.cs

Objectif : Modèle de base pour tous les objets à ramasser

Fait tourner et léviter les objets pour attirer l’attention

Sert de base à tous les autres collectibles



CrystalCollectible.cs

Objectif : Cristaux à collecter

Augmente le score

Ajoute du temps au chronomètre

Disparaît après collecte



TimeBonus.cs

Objectif : Bonus de temps

Ajoute +10 secondes au chronomètre

Apparence dorée et brillante pour être visible

Permet de prolonger la partie



CollectibleManager.cs

Objectif : Gérer tous les objets à ramasser

Enregistre tous les cristaux et bonus de la scène

Les fait réapparaître quand le joueur meurt



SCRIPTS DE TEMPS



GameTimer.cs

Objectif : Gérer le compte à rebours

Chronomètre de 60 secondes par défaut

Game Over si le temps atteint 0

Ajout de temps possible via les bonus

Met à jour l’affichage du timer à l’écran



SCRIPTS D’INTERFACE (UI)



UIController.cs

Objectif : Mettre à jour l’affichage du score et du temps

Gère les informations visibles à l’écran

Actualise les valeurs à chaque action du joueur



GameOverScreen.cs

Objectif : Écran de défaite

Apparaît quand le joueur meurt ou que le temps est écoulé

Met le jeu en pause

Boutons Retry et Quit



VictoryScreen.cs

Objectif : Écran de victoire

Apparaît quand tous les cristaux sont collectés

Met le jeu en pause

Message de félicitations





SCRIPTS D’ENVIRONNEMENT



MovingPlatform.cs

Objectif : Plateformes mobiles

Se déplacent verticalement entre deux points

Transportent la boule quand elle est dessus

Vitesse configurable



HazardZone.cs

Objectif : Zone de danger temporisée

Alterne entre deux états :

\-	5 secondes actives (mortelles)

\-	5 secondes inactives (sûres)

Change de couleur selon son état (rouge = danger, vert = sûr)

Tue instantanément le joueur quand elle est active



KillZoneTrigger.cs

Objectif : Zone de mort instantanée

Tue le joueur immédiatement s’il tombe dedans

Utilisée pour les trous ou les bords de carte



Door.cs

Objectif : Porte de fin de niveau

S’ouvre quand tous les cristaux ont été ramassés

Sert de sortie pour terminer le niveau





SCRIPTS DE GESTION



LevelManager.cs

Objectif : Gérer les transitions entre scènes

Recharge la scène avec la touche L

Passe au niveau suivant après la victoire



ScoreDatase.cs

Objectif : Sauvegarder les meilleurs scores

ScriptableObject qui garde les résultats

Persiste entre les scènes





INTERACTIONS ENTRE LES SCRIPTS



PlayerMovement, PlayerHealth et Player\_Collect fonctionnent ensemble :

Movement = mouvement de la boule

Health = vies et mort

Collect = inventaire et score

Les ennemis et les zones dangereuses interagissent avec PlayerHealth :

Ennemi ou zone mortelle → perte de vie → GameOverScreen

Les collectibles communiquent avec le timer et l’interface :

Cristal → ajoute du score et du temps → UI mise à jour

Les plateformes mobiles ajoutent un rythme et des défis de timing.





STRUCTURE DU PROJET



Assets : 

•	Scenes/ (MainMenu, Level01)

•	Scripts/ (Player, Enemies, Collectibles, Environment, UI, System)

•	Prefabs/

•	Materials/

•	Sound\_Design/

•	UI\_Menu/

Suite à un bug lié aux assets, l’organisation de mes scènes a été modifiée et ne correspond plus exactement à ma structure initiale, voici donc un schéma représentatif de la disposition que j’avais prévue



AMÉLIORATIONS POSSIBLES

\-	Ajouter des checkpoints dans les niveaux

\-	Créer des plateformes horizontales et diagonales

\-	Créer un vrai menu de paramètres (volume, qualité, touches)

\-	Un Level 3



