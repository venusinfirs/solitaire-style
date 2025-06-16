using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ActionsQueueService : MonoBehaviour
    {
        public Stack<LastMoveData> ActionsQueue = new Stack<LastMoveData>();
        public Button _undoButton;

        private List<SolitaireCard2D> _cards = new List<SolitaireCard2D>();
        
        void Start()
        {
            if (_undoButton == null)
            {
                Debug.LogError("No button component found on UndoButton");
                return;
            }
        
            _cards = FindObjectsOfType<SolitaireCard2D>().ToList();

            foreach (var card in _cards)
            {
               card.OnCardPlaced += SetLastMove;
            }

            _undoButton.onClick.AddListener(UndoLastAction);
        }
        
        public void SetLastMove(LastMoveData lastMove)
        {
            ActionsQueue.Push(lastMove);
        }

        private void UndoLastAction()
        {
            if (ActionsQueue.Count == 0) return;
            
            var lastMove = ActionsQueue.Pop();
            
            foreach (var card in _cards)
            {
                if (card.Rank == lastMove.LastDraggedCard.Rank && card.Suit == lastMove.LastDraggedCard.Suit)
                {
                    if (lastMove.PreviousHigherCard != null)
                    {
                        card.transform.position = card.PreviousHigherCard.transform.position + card.glueOffset;
                    }
                    else
                    {
                        card.transform.position = lastMove.InitialPosition;
                    }
                }
            }
        }
    }
}


public class LastMoveData
{
    public SolitaireCard2D LastDraggedCard;
    public SolitaireCard2D PreviousHigherCard;
    public SolitaireCard2D CurrentHigherCard;
    public Vector3 InitialPosition;
}