using System.Collections.Generic;
using DefaultNamespace;

namespace DefaultNamespace
{
    public static class ActionsQueueService
    {
        public static List<LastMoveData> ActionsQueue = new List<LastMoveData>();
        
        public static void SetLastMove(SolitaireCard2D card, SolitaireCard2D higherCard, EmptySlot emptySlot = null)
        {
            ActionsQueue.Add(new LastMoveData()
            {
                LastDraggedCard = card,
                HigherCard = higherCard,
                EmptySlot = emptySlot
            });
        }
    }
}


public class LastMoveData
{
    public SolitaireCard2D LastDraggedCard;
    public SolitaireCard2D HigherCard;
    public EmptySlot EmptySlot;
}