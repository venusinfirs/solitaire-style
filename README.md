# Main scene PlayingCards.unity

The project is quite simple and consists of three classes:

- SolitaireCard2D, which contains the drag-and-drop logic and data about adjacent cards, and parses Rank and Suit data from the prefab name;
- ActionsQueueService, a class that contains data about all the moves made during the game session;
- LastMoveData, an object that stores information about the last move;

AI was used for:

- A Python script to rename Unity Asset Store assets;
- The basic logic of SolitaireCard2D (everything related to drag-and-drop and parcing prefab name to get card rank and suit);
- Attempted to find solutions for the problem that, in the current implementation, makes it impossible to grab a card when another card is stacked on top of it.

What I would change:
In the current implementation it is impossible to move stacks of cards, which is really frustrating.
