namespace ViTiet.Library.Generic
{
    public static class LoopLogic
    {
        public static int GetNextLoopIndex(int currentInt, int min, int max)
        {
            return (currentInt + 1 < max) ? currentInt + 1 : min;
        }
    }
}