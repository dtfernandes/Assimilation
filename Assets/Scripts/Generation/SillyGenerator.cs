using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generator that only uses a MAze as a Template
/// Used when while the complete arquitecture with the WFC is 
/// not complete
/// </summary>
public class SillyGenerator : MonoBehaviour
{
    MazeGenerator _mazeGenerator;

    [Header("Dimensions")]
    [SerializeField] 
    private int _width;
    [SerializeField] 
    private int _height;
    
    public int Witdh => _width;
    public int Height => _height;

    [Header("Tiles")]
    [SerializeField]
    private WorldTile _templateTilePREFAB;

    private void Awake()
    {
        _mazeGenerator = new MazeGenerator();
        _mazeGenerator.AldousBroder(_width, _height);

        //Select Start 
        Tuple<int, int> startCoords =
            new Tuple<int, int>(UnityEngine.Random.Range(0,_width),
            UnityEngine.Random.Range(0, _height));


        //Select End

        Tuple<int, int> end = GetNewPosition(startCoords, 1);

        //Draw Map
        for (int i = 0; i < _mazeGenerator.Maze.GetLength(0); i++)
        {
            for (int j = 0; j < _mazeGenerator.Maze.GetLength(1); j++)
            {


                Vector2 position = new Vector2(i * 17.76f, j * -10);

                WorldTile tile =
                     Instantiate(_templateTilePREFAB, position,
                     Quaternion.identity, transform);

                tile.ConfigTile(_mazeGenerator.Maze[i, j]);

                //if is start
                if (startCoords.Item1 == i && startCoords.Item2 == j)
                {
                    tile.SetupAsStart();


                }

                //if is end
                if (end.Item1 == i && end.Item2 == j)
                {
                    tile.SetupAsEnd();
                }
            }

        }
    }


    /// <summary>
    /// Method that returns a new set of coordinates at a certain distance from 
    /// a slot
    /// </summary>
    /// <param name="startCoord">Initial Slot coordinates</param>
    /// <param name="distance">Distance from the slot</param>
    /// <returns></returns>
    public Tuple<int, int> GetNewPosition(Tuple<int, int> startCoord, int distance)
    {
       
        Tuple<int, int> bestCoord = null;
        int bestDistance = int.MaxValue;

        for (int x = 0; x < _mazeGenerator.Maze.GetLength(0); x++)
        {
            for (int y = 0; y < _mazeGenerator.Maze.GetLength(1); y++)
            {
                Tuple<int, int> currentTile = new Tuple<int, int>(x, y);
               
                int currentDistance = Math.Abs(currentTile.Item1 - startCoord.Item1) + Math.Abs(currentTile.Item2 - startCoord.Item2);

                if (currentDistance == distance && currentDistance < bestDistance)
                {
                    bestCoord = currentTile;
                    bestDistance = currentDistance;
                }
            }
        }

        return bestCoord;
    }
}
