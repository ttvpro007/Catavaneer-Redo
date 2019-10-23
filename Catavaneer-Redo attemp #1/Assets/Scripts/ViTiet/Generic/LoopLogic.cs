namespace ViTiet.Generic
{
    /// <summary>
    /// Logic related to loop
    /// </summary>
    public static class LoopLogic
    {
        /// <summary>
        /// Returns the next index in a loop
        /// </summary>
        public static int GetNextLoopIndex(int currentInt, int min, int max)
        {
            return (currentInt + 1 < max) ? currentInt + 1 : min;
        }
    }
}