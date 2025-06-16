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
            var lastMove = ActionsQueue.Pop();
            
            foreach (var card in _cards)
            {
                if (card.Rank == lastMove.LastDraggedCard.Rank && card.Suit == lastMove.LastDraggedCard.Suit)
                {
                    if (card.PreviousHigherCard != null)
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
    public Vector3 InitialPosition;
}