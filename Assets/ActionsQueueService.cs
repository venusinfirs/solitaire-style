using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ActionsQueueService : MonoBehaviour
    {
        public Stack<LastMoveData> ActionsQueue = new Stack<LastMoveData>();
        public Button _undoButton;
        
        void Start()
        {
            if (_undoButton == null)
            {
                Debug.LogError("No button component found on UndoButton");
                return;
            }
        
            _undoButton.onClick.AddListener(UndoLastAction);
        }
        
        public void SetLastMove(SolitaireCard2D card, SolitaireCard2D peviousHigherCard, Vector3 previousPos)
        {
            ActionsQueue.Push(new LastMoveData()
            {
                LastDraggedCard = card,
                PreviousHigherCard = peviousHigherCard,
                PreviousPosition = previousPos
            });
        }

        private void UndoLastAction()
        {
            var lastMove = ActionsQueue.Pop();
            
            var cardsOnScene = FindObjectsOfType<SolitaireCard2D>();
            
            foreach (var card in cardsOnScene)
            {
                if (card.Rank == lastMove.LastDraggedCard.Rank && card.Suit == lastMove.LastDraggedCard.Suit)
                {
                   
                }
            }
        }
    }
}


public class LastMoveData
{
    public SolitaireCard2D LastDraggedCard;
    public SolitaireCard2D PreviousHigherCard;
    public Vector3 PreviousPosition;
}