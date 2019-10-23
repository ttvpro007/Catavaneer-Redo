namespace ViTiet.UnityExtension.Math
{
    /// <summary>
    /// A collection of Unity Mathf extension
    /// </summary>
    public static class MathExtended
    {
        /// <summary>
        /// Return max float value in a 2D array
        /// </summary>
        public static float Max(float[,] values)
        {
            float maxValue = float.MinValue;

            for (int x = 0; x < values.GetLength(0); x++)
            {
                for (int y = 0; y < values.GetLength(1); y++)
                {
                    if (maxValue < values[x, y]) maxValue = values[x, y];
                }
            }

            return maxValue;
        }

        /// <summary>
        /// Return min float value in a 2D array
        /// </summary>
        public static float Min(float[,] values)
        {
            float minValue = float.MaxValue;

            for (int x = 0; x < values.GetLength(0); x++)
            {
                for (int y = 0; y < values.GetLength(1); y++)
                {
                    if (minValue > values[x, y]) minValue = values[x, y];
                }
            }

            return minValue;
        }

        /// <summary>
        /// Return max int value in a 2D array
        /// </summary>
        public static int Max(int[,] values)
        {
            int maxValue = int.MinValue;

            for (int x = 0; x < values.GetLength(0); x++)
            {
                for (int y = 0; y < values.GetLength(1); y++)
                {
                    if (maxValue < values[x, y]) maxValue = values[x, y];
                }
            }

            return maxValue;
        }

        /// <summary>
        /// Return min int value in a 2D array
        /// </summary>
        public static int Min(int[,] values)
        {
            int minValue = int.MaxValue;

            for (int x = 0; x < values.GetLength(0); x++)
            {
                for (int y = 0; y < values.GetLength(1); y++)
                {
                    if (minValue > values[x, y]) minValue = values[x, y];
                }
            }

            return minValue;
        }
    }
}