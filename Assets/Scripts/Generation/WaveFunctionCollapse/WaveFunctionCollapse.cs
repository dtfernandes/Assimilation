using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class WaveFunctionCollapse
{
    private List<Slot> _alreadyTested;
    private Queue<Slot> _testNeighbours;

    public void Collapse(IMap map)
    {
        //Get slot with less entropy
        Slot selectedSlot = default;
        int lowestEntropy = 999;       
        foreach(Slot s in map.Slots)
        {
            if (s.IsCollapsed) continue;

            if(s.Entropy < lowestEntropy)
            {
                lowestEntropy = s.Entropy;
                selectedSlot = s;
            }
        }

        //Collapse Slot
        int rnd = UnityEngine.Random.Range(0, selectedSlot.PossibleTiles.Count);
        selectedSlot.IsCollapsed = true;
        selectedSlot.Tile = selectedSlot.PossibleTiles[rnd];


        //Propagate action trhough all the slots
        _testNeighbours = new Queue<Slot> { };
        _alreadyTested = new List<Slot> { };
        _alreadyTested.Add(selectedSlot);
        PropagateReaction(selectedSlot, map);

        //Check if any slot's entropy is 1. 
        //If so collapse forther and propagate until there's no entropy of 1

        bool ended = true;
        foreach(Slot s in map.Slots)
        {
            if(!s.IsCollapsed)
            {
                ended = false;
            }
        }

        if (!ended)
        {
            Collapse(map);
        }
    }
    
    public void PropagateReaction(Slot slot, IMap map)
    {
        if (_alreadyTested == null) _alreadyTested = new List<Slot> { };
        if (_testNeighbours == null) _testNeighbours = new Queue<Slot> { };

        Slot[] neighbours = map.GetNeighbours(slot);


        //To remember: 0 - left | 1 - right | 2 - top | 3 - bottom
        int index = -1;

        //Test neighbours
        foreach (Slot s in neighbours)
        {
            index++;
            //Id there's no neighbour in this (s) direction
            if (s == null)
            {
                int o = 0;

                //Get what direction we are talking about
                if (index == 0) o = 1;
                if (index == 1) o = 0;
                if (index == 2) o = 3;
                if (index == 3) o = 2;

                //Change the possible tiles so that this slot knows there's 
                //no connetion in this direction
                slot.Collapse(o, new Connections());
                continue;
            }


            if (slot.IsCollapsed)
            {
                s.Collapse(index, slot.Tile.Connections);
            }
            else
            {
                s.Collapse(index, slot.PossibleTiles);
            }



            //Check if neighbour was already tested
            if (_alreadyTested.Contains(s))
            {
                continue;
            }

            _alreadyTested.Add(slot);
            _testNeighbours.Enqueue(s);
        }

        if (_testNeighbours.Count > 0)
        {
            PropagateReaction(_testNeighbours.Dequeue(), map);
        }
    }

    public MazeSlot[,] Collapse(MazeSlot[,] maze)
    {

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            // Iterating through columns
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                // Accessing each cell in the maze             
                maze[i, j].Left = maze[i, j].Left & ~Biome.Forest;
                maze[i, j].Right = maze[i, j].Right & ~Biome.Forest;
                maze[i, j].Up = maze[i, j].Up & ~Biome.Forest;
                maze[i, j].Down = maze[i, j].Down & ~Biome.Forest;

            }
        }

        //Create a map with this slots
        MazeMap map = new MazeMap(maze.GetLength(0),maze.GetLength(1),maze);
        map.GenerateMap();

        Collapse(map);

        return map.GetAsMaze();
    }
     
}

public class GenericTile : ITile
{
    private int index;
    public int Index => index;

    private Connections connections;

    public GenericTile(int index, Connections connections)
    {
        this.index = index;
        this.connections = connections;
    }

    public Connections Connections => connections;
}

public class MazeMap : IMap
{
    private Slot[,] _slots;
    public Slot[,] Slots => _slots;

  
    private int _width, _height;
    private MazeSlot[,] _maze;

    public int Witdh => _width;

    public int Height => _height;

    private GenericTile[] _tiles;

    public MazeMap(int width, int height, MazeSlot[,] maze)
    {
        _width = width;
        _height = height;
        this._maze = maze;
    }

    public void GenerateMap()
    {
        _tiles = new GenericTile[15];


        //Forest Tiles
        _tiles[0] = new GenericTile(0, new Connections((int)Biome.Forest, (int)Biome.Forest, (int)Biome.Forest, (int)Biome.Forest));
        _tiles[1] = new GenericTile(1, new Connections((int)Biome.Forest, (int)Biome.Cavern, (int)Biome.Forest, (int)Biome.Forest));
        _tiles[2] = new GenericTile(2, new Connections((int)Biome.Forest, (int)Biome.Forest, (int)Biome.Beach, (int)Biome.Forest));
        _tiles[3] = new GenericTile(3, new Connections((int)Biome.Forest, (int)Biome.Forest, (int)Biome.Forest, (int)Biome.Building));

        _tiles[4] = new GenericTile(9, new Connections((int)Biome.Building, (int)Biome.Building, (int)Biome.Building, (int)Biome.Building));
        _tiles[5] = new GenericTile(10, new Connections((int)Biome.Building, (int)Biome.Cavern, (int)Biome.Building, (int)Biome.Building));
        _tiles[6] = new GenericTile(11, new Connections((int)Biome.Cavern, (int)Biome.Building, (int)Biome.Building, (int)Biome.Building));
        _tiles[7] = new GenericTile(12, new Connections((int)Biome.Building, (int)Biome.Cavern, (int)Biome.Building, (int)Biome.Building));
        _tiles[8] = new GenericTile(13, new Connections((int)Biome.Building, (int)Biome.Building, (int)Biome.Cavern, (int)Biome.Building));
        _tiles[9] = new GenericTile(14, new Connections((int)Biome.Building, (int)Biome.Building, (int)Biome.Building, (int)Biome.Cavern));

        _tiles[10] = new GenericTile(15, new Connections((int)Biome.Cavern, (int)Biome.Cavern, (int)Biome.Cavern, (int)Biome.Cavern));
        _tiles[11] = new GenericTile(16, new Connections((int)Biome.Cavern, (int)Biome.Cavern, (int)Biome.Alien, (int)Biome.Cavern));
        _tiles[12] = new GenericTile(17, new Connections((int)Biome.Cavern, (int)Biome.Cavern, (int)Biome.Cavern, (int)Biome.Alien));
        _tiles[13] = new GenericTile(18, new Connections((int)Biome.Cavern, (int)Biome.Alien, (int)Biome.Cavern, (int)Biome.Cavern));

        _tiles[14] = new GenericTile(19, new Connections((int)Biome.Alien, (int)Biome.Alien, (int)Biome.Alien, (int)Biome.Alien));

        //Create array of slots with the defined sizes
        _slots = new Slot[_width, _height];

        //Iterate trough the array and 
        for (int i = 0; i < Witdh; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                //Add possibiliies
                _slots[i, j] = new Slot(_tiles);
            }
        }
    }

    public Slot[] GetNeighbours(Slot slot)
    {
        System.Tuple<int, int> coords = _slots.CoordinatesOf(slot);
        Slot[] returnArray = new Slot[4];


        MazeSlot mSlot =
            _maze[coords.Item1, coords.Item2];

        //Get slot to the left
        if ((mSlot.Left & Biome.Closed) == Biome.Closed)
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

        if ((mSlot.Right & Biome.Closed) == Biome.Closed)
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

        if ((mSlot.Up & Biome.Closed) == Biome.Closed)
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

        if ((mSlot.Down & Biome.Closed) == Biome.Closed)
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

    public MazeSlot[,] GetAsMaze()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {              
                _maze[i, j].Left =
                    _maze[i, j].Left | (Biome)Slots[i,j].Tile.Connections.Left;
                _maze[i, j].Right = 
                    _maze[i, j].Right | (Biome)Slots[i, j].Tile.Connections.Right;
                _maze[i, j].Up = 
                    _maze[i, j].Up | (Biome)Slots[i, j].Tile.Connections.Up;
                _maze[i, j].Down =
                    _maze[i, j].Down | (Biome)Slots[i, j].Tile.Connections.Down;
            }
        }

        return _maze;
    }
}