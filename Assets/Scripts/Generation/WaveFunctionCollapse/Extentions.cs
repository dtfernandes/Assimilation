using System;
using System.Collections.Generic;

public static class Extentions
{
    public static List<T> GetNeighbours<T>(this T[,] map, int row, int col)
    {
        List<T> neighbors = new List<T>(4);

        // Check top neighbor
        if (row > 0)
        {
            T topNeighbor = map[row - 1, col];
            neighbors.Add(topNeighbor);
        }

        // Check bottom neighbor
        if (row < map.GetLength(0) - 1)
        {
            T bottomNeighbor = map[row + 1, col];
            neighbors.Add(bottomNeighbor);
        }

        // Check left neighbor
        if (col > 0)
        {
            T leftNeighbor = map[row, col - 1];
            neighbors.Add(leftNeighbor);
        }

        // Check right neighbor
        if (col < map.GetLength(1) - 1)
        {
            T rightNeighbor = map[row, col + 1];
            neighbors.Add(rightNeighbor);
        }

        return neighbors;
    }

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