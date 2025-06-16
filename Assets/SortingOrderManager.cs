public static class SortingOrderManager
{
    private static int currentOrder = 0;

    public static int GetNextOrder()
    {
        return ++currentOrder;
    }
}