using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{

    public MazeSlot[,] Maze { get; private set; }

    public void AldousBroder(int width, int height)
    {
        // Get grid
        Maze = new MazeSlot[width, height];



        for (int i = 0; i < Maze.GetLength(0); i++)
        {
            // Iterating through columns
            for (int j = 0; j < Maze.GetLength(1); j++)
            {
                // Accessing each cell in the maze
                Maze[i, j].Default();
            }
        }

        int numbOfEmpty = Maze.Length - 1;

        //Get random neighbour
        int testX = UnityEngine.Random.Range(0, width);
        int testY = UnityEngine.Random.Range(0, height);

        int iLP1 = 0;
        Maze[testX, testY].Visited = true;

        //While unlinked cells exist
        while (numbOfEmpty > 0)
        {
            iLP1++;
            int infiniteLoopPrevention = 0;

            // Try to link
            while (true)
            {
                infiniteLoopPrevention++;

                Dir dir = (Dir)UnityEngine.Random.Range(0, 4);

                bool inBounds = CheckBounds(testX, testY, dir);

                if (inBounds)
                {
                    int newX = testX;
                    int newY = testY;

                    //Link
                    switch (dir)
                    {
                        case Dir.Bottom:
                            newY = testY + 1;
                            if (!Maze[testX, newY].Visited)
                            {
                                Maze[testX, testY].Down = Biome.Forest;
                                Maze[testX, newY].Up = Biome.Forest;
                            }
                            break;
                        case Dir.Top:
                            newY = testY - 1;
                            if (!Maze[testX, newY].Visited)
                            {
                                Maze[testX, testY].Up = Biome.Forest;
                                Maze[testX, newY].Down = Biome.Forest;
                            }
                            break;
                        case Dir.Left:
                            newX = testX - 1;
                            if (!Maze[newX, testY].Visited)
                            {
                                Maze[testX, testY].Left = Biome.Forest;
                                Maze[newX, testY].Right = Biome.Forest;
                            }
                            break;
                        case Dir.Right:
                            newX = testX + 1;
                            if (!Maze[newX, testY].Visited)
                            {
                                Maze[testX, testY].Right = Biome.Forest;
                                Maze[newX, testY].Left = Biome.Forest;
                            }
                            break;
                    }

                    testX = newX;
                    testY = newY;

                    if (!Maze[testX, testY].Visited)
                    {                                       
                        numbOfEmpty -= 1;
                        Maze[testX, testY].Visited = true;
                    }

                    break;
                }

                if (infiniteLoopPrevention == 100)
                {
                    Debug.LogError("Infinite Loop. Something Went Wrong");
                    break;
                }
            }

            if (iLP1 == 1000)
            {
                Debug.LogError("This Infinite Loop. Something Went Wrong");
                return;
            }
        }

        // Return Grid
        bool CheckBounds(int x, int y, Dir dir)
        {           
            bool inBounds = true;
            switch (dir)
            {
                case Dir.Bottom:
                    inBounds = !((y + 1) >= height);
                    break;
                case Dir.Top:
                    inBounds = !((y - 1) < 0);
                    break;
                case Dir.Left:
                    inBounds = !((x - 1) < 0);
                    break;
                case Dir.Right:
                    inBounds = !((x + 1) >= width);
                    break;                    
            }

            return inBounds;
        }
    }

    public void Wilson()
    {


    }

}
