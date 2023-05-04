using System;

public static class Extentions
{

    public static void Random(this Dir self)
    {
        self = (Dir)UnityEngine.Random.Range(0,4);
    }

    public static Tuple<int, int> CoordinatesOf<T>(this T[,] matrix, T value)
    {
        int w = matrix.GetLength(0); // width
        int h = matrix.GetLength(1); // height

        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                if (matrix[x, y].Equals(value))
                    return Tuple.Create(x, y);
            }
        }

        return Tuple.Create(-1, -1);
    }
}