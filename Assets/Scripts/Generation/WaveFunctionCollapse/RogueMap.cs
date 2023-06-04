using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class RogueMap : MonoBehaviour, IMap
{
    private Slot[,] _slots;
    public Slot[,] Slots => _slots;

    [SerializeField]
    private int _width, _height;

    public int Witdh => _width;

    public int Height => _height;

    [SerializeField]
    private RogueTile[] _tiles;
    [SerializeField]
    private RogueTile _end, _start;
    MazeGenerator gen;


    public void GenerateMap()
    {
        //Create array of slots with the defined sizes
        _slots = new Slot[_width, _height];

        gen = new MazeGenerator();
        gen.AldousBroder(_width,_height);

        //Iterate trough the array and 
        for (int i = 0; i < Witdh; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                _slots[i, j] = new Slot(_tiles);
            }
        }
    }

    public void Awake()
    {
        GenerateMap();
        Slot test = _slots[2, 0];

        test.IsCollapsed = true;
        test.Tile = _start;

        //WaveFunctionCollapse wfc = new WaveFunctionCollapse();
        //wfc.PropagateReaction(test, this);       
        //wfc.Collapse(this);

        SillyMapCollapser();

        //Draw Map
        for (int i = 0; i < _slots.GetLength(0); i++)
        {
            for (int j = 0; j < _slots.GetLength(1); j++)
            {
                //Get index 
                int id = _slots[i,j].Tile.Index;

                //Get tile
                RogueTile tile = _slots[i, j].Tile as  RogueTile;
                Vector2 position = new Vector2(i * 17.76f, j * -10);

                Instantiate(tile.RoomPrefab, position, Quaternion.identity, transform);
            }
        }

        //Instatiate Player
        
        //Setup Camera
    }

    private void SillyMapCollapser()
    {

        for (int i = 0; i < _slots.GetLength(0); i++)
        {
            for (int j = 0; j < _slots.GetLength(1); j++)
            {
                MazeSlot current = gen.Maze[i, j];

                int down = current.Down == Biome.Forest ? 1: 0;
                int up = current.Up == Biome.Forest ? 1 : 0;
                int left = current.Left == Biome.Forest ? 1 : 0;
                int right = current.Right == Biome.Forest ? 1 : 0;

                List<ITile> possibles = new List<ITile> { };
                possibles.AddRange(_tiles);
                ITile tile = default;
                try
                {
                    tile = possibles.First(x =>
                    x.Connections.Down == (Biome)down &&
                     x.Connections.Right == (Biome)right &&
                      x.Connections.Left == (Biome)left &&
                       x.Connections.Up == (Biome)up);

                }
                catch
                {
                    Debug.LogError("Tile not found" + " D: " + down + " U: "+ up 
                        + " L: " + left + " R: " + right);
                }


                _slots[i, j].Tile = tile;
                _slots[i, j].IsCollapsed = true;
            }
        }
    }

    public Slot[] GetNeighbours(Slot slot)
    {
        System.Tuple<int,int> coords = _slots.CoordinatesOf(slot);
        Slot[] returnArray = new Slot[4];


        MazeSlot mSlot = 
            gen.Maze[coords.Item1, coords.Item2];

        //Get slot to the left
        if (mSlot.Left != Biome.Closed)
        {
            returnArray[0] = null;
        }
        else
        {
            if (coords.Item1 - 1 >= 0)
            {
                Slot left = _slots[coords.Item1 - 1, coords.Item2];
                returnArray[0] = left;
            }
        }

        if (mSlot.Right != Biome.Closed)
        {
            returnArray[1] = null;
        }
        else
        {
            //Get slot to the right
            if (coords.Item1 + 1 < _width)
            {

                Slot right = _slots[coords.Item1 + 1, coords.Item2];
                returnArray[1] = right;
            }
        }

        if (mSlot.Up != Biome.Closed)
        {
            returnArray[2] = null;
        }
        else
        {
            //Get slot to the top_tiles
            if (coords.Item2 - 1 >= 0)
            {

                Slot top = _slots[coords.Item1, coords.Item2 - 1];
                returnArray[2] = top;
            }
        }

        if (mSlot.Down != Biome.Closed)
        {
            returnArray[3] = null;
        }
        else
        {
            //Get slot to the bottom
            if (coords.Item2 + 1 < _height)
            {

                Slot bottom = _slots[coords.Item1, coords.Item2 + 1];
                returnArray[3] = bottom;
            }
        }

        return returnArray;
    }
}
