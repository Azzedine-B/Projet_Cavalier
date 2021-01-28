# Projet_Cavalier

Le but de cette application graphique WinForms (C#), est de faire parcourir à un cavalier
l'ensemble d'un échiquier sans passer deux fois sur la même case. On rappelle la technique
de déplacement d'un cavalier sur un échiquier : à partir d'une case X, un cavalier peut se
déplacer sur l'une des 8 cases suivantes

 La méthode à utiliser, basée sur une heuristique due à Euler, consiste à choisir comme
case de fuite, en partant de l'étape N, la case de l'étape N+1 qui, à l'étape N+2, présente le
MINIMUM de case de fuites possibles. Si l'on applique cette méthode dès le départ, cela
revient à choisir n’importe quelle case comme case de départ. Avec cette méthode, on est sûr
(selon Euler !) de parcourir l'ensemble de l'échiquier. Néanmoins, votre programme doit
prévoir le cas où le cavalier tombe dans une impasse (ce qui ne devrait pas arriver
normalement).
 Au départ, le programme affiche un échiquier « vierge », puis l’effigie du cavalier à son
point de départ puis, pas à pas (prévoir une pause de 1s entre chaque déplacement), on
indiquera les cases successivement occupées en y affichant le numéro de l'étape à laquelle le
cavalier s'y est posé.

Les coordonnées de la case de départ du cavalier doivent, soit pouvoir être choisies par
l’utilisateur, soit être tirées au sort sur ordre de l’utilisateur. Prévoir 2 boutons.
