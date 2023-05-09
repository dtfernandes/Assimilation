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
    private WorldTile[,] _map;


    private void Awake()
    {
        _mazeGenerator = new MazeGenerator();
        _mazeGenerator.AldousBroder(_width, _height);
        _map = new WorldTile[_width, _height];


        //Select Start 
        Tuple<int, int> startCoords =
            new Tuple<int, int>(UnityEngine.Random.Range(0,_width),
            UnityEngine.Random.Range(0, _height));

        int endDistance = GameState.Instance.GameValues.EndDistance.Value;

        //Make sure the distance is always possible
        if(endDistance > Witdh - 1)
        {
            endDistance = Witdh - 1;
        }

        //Select End
        Tuple<int, int> end = GetNewPosition(startCoords, endDistance);

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
               
                tile.X = i;
                tile.Y = j;
                _map[i, j] = tile;

            }

        }

        #region Setup Danger Levels
        _map[startCoords.Item1, startCoords.Item2].SetupAsStart();
        _map[end.Item1, end.Item2].SetupAsEnd();

        PropagateDangerValues(_map[end.Item1, end.Item2], DangerLevel.Impossible);
        PropagateDangerValues(_map[startCoords.Item1, startCoords.Item2], DangerLevel.Hard);
        _map[end.Item1, end.Item2].Danger = DangerLevel.Pacific;
        _map[startCoords.Item1, startCoords.Item2].Danger = DangerLevel.Pacific;

        for (int i = 0; i < _mazeGenerator.Maze.GetLength(0); i++)
        {
            for (int j = 0; j < _mazeGenerator.Maze.GetLength(1); j++)
            {
                _map[i, j].SpawnEnemies();
            }
        }

        #endregion
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

    public void PropagateDangerValues(WorldTile coord, DangerLevel dangerlevel, 
        DangerLevel forceLevel = DangerLevel.None)
    {
        int row = coord.X;
        int col = coord.Y;

        // Set the danger value of the initial tile
        if (forceLevel != DangerLevel.None)
        {
            _map[row, col].Danger = forceLevel;
        }
        else
        {
            _map[row, col].Danger = dangerlevel;
        }

        // Propagate the danger value to neighboring tiles
        Queue<WorldTile> queue = new Queue<WorldTile>();
        queue.Enqueue(coord);

        List<WorldTile> visited = new List<WorldTile> { };
        

        int iFP = 0;
        while (queue.Count > 0)
        {
            iFP++;
           
            WorldTile currentCoord = queue.Dequeue();
            visited.Add(currentCoord);

            int currentRow = currentCoord.X;
            int currentCol = currentCoord.Y;

            DangerLevel currentDanger = _map[currentRow, currentCol].Danger;

            if (iFP == 1)
                currentDanger = dangerlevel;

            List<WorldTile> neighbours = _map.GetNeighbours(currentRow, currentCol);
           
            // Propagate to neighboring tiles
            foreach (WorldTile neighbour in neighbours)
            {               
                int neighborRow = neighbour.X;
                int neighborCol = neighbour.Y;
                DangerLevel neighbourDanger = _map[neighborRow, neighborCol].Danger;

                if (currentDanger > neighbourDanger)
                {  
                    _map[neighborRow, neighborCol].Danger = currentDanger - 1; 
                    
                    if(!visited.Contains(neighbour))
                        queue.Enqueue(neighbour);
                }
            }

            if (iFP > 1000)
            { 
                Debug.LogError("Ifinite Loop");
                break;
            }
        }
    }
}
