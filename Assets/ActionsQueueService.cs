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
               ActionsQueue.Push(new LastMoveData()
               {
                   LastDraggedCard = card,
                   PreviousHigherCard = card.HigherCard,
                   PreviousPosition = card.transform.position
               });
            }

            _undoButton.onClick.AddListener(UndoLastAction);
        }
        
        public void SetLastMove(SolitaireCard2D card, SolitaireCard2D previousHigherCard = null, Vector3 previousPos = default)
        {
            ActionsQueue.Push(new LastMoveData()
            {
                LastDraggedCard = card,
                PreviousHigherCard = previousHigherCard,
                PreviousPosition = previousPos
            });
        }

        private void UndoLastAction()
        {
            var lastMove = ActionsQueue.Pop();
            
            foreach (var card in _cards)
            {
                if (card.Rank == lastMove.LastDraggedCard.Rank && card.Suit == lastMove.LastDraggedCard.Suit)
                {
                   card.transform.position = lastMove.PreviousPosition;
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