using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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
            if (s == null)
            {
                int o = 0;

                if (index == 0) o = 1;
                if (index == 1) o = 0;
                if (index == 2) o = 3;
                if (index == 3) o = 2;

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
   
}

public interface IMap
{
    public int Witdh  { get; }
    public int Height { get; }

    public Slot[,] Slots { get; }

    public void GenerateMap();
    public Slot[] GetNeighbours(Slot slot);
}

public class Slot
{
    public Slot(IList<ITile> possibleTiles)
    {
        List<ITile> newList = new List<ITile> { };
        newList.AddRange(possibleTiles);
        PossibleTiles = newList;

        IsCollapsed = false;
    }

    /// <summary>
    /// The number of tiles that can possibly fit in this slot
    /// </summary>
    public int Entropy => PossibleTiles.Count;

    /// <summary>
    /// The list of tiles that can fit in the slot
    /// </summary>
    public IList<ITile> PossibleTiles { get; private set; }

    /// <summary>
    /// Flag indicating whether the slot is currently collapsed or not.
    /// </summary>
    public bool IsCollapsed { get; set; }

    /// <summary>
    /// Tile selected for this slot. 
    /// Is null if the slot is not collapsed yet
    /// </summary>
    public ITile Tile { get; set; }

    public void Collapse(int index, Connections connections)
    {
        int connection = 0;
        switch (index)
        {
            case 0:
                connection = connections.Left;
                break;
            case 1:
                connection = connections.Right;
                break;
            case 2:
                connection = connections.Up;
                break;
            case 3:
                connection = connections.Down;
                break;
        }

        IList<ITile> newPossibleTiles = new List<ITile> { };
        foreach(ITile tile in PossibleTiles)
        {
            int otherCon = 0;
            switch (index)
            {
                case 0:
                    otherCon = tile.Connections.Right;
                    break;
                case 1:
                    otherCon = tile.Connections.Left;
                    break;
                case 2:
                    otherCon = tile.Connections.Down;
                    break;
                case 3:
                    otherCon = tile.Connections.Up;
                    break;
            }
            if (otherCon == connection)
                newPossibleTiles.Add(tile);
        }

        PossibleTiles = newPossibleTiles;
    }

    public void Collapse(int index, IList<ITile> possible)
    {
        IList<ITile> newPossibleTiles = new List<ITile> { };
       
        foreach (ITile t in possible)
        {
            int connection = 0;
            switch (index)
            {
                case 0:
                    connection = t.Connections.Left;
                    break;
                case 1:
                    connection = t.Connections.Right;
                    break;
                case 2:
                    connection = t.Connections.Up;
                    break;
                case 3:
                    connection = t.Connections.Down;
                    break;
            }
          
            foreach (ITile tile in PossibleTiles)
            {
                int otherCon = 0;
                switch (index)
                {
                    case 0:
                        otherCon = tile.Connections.Right;
                        break;
                    case 1:
                        otherCon = tile.Connections.Left;
                        break;
                    case 2:
                        otherCon = tile.Connections.Down;
                        break;
                    case 3:
                        otherCon = tile.Connections.Up;
                        break;
                }
                if (otherCon == connection)
                    if (!newPossibleTiles.Contains(tile))
                    {
                        newPossibleTiles.Add(tile);
                    }
            }
        }

        PossibleTiles = newPossibleTiles;
    }
}

public interface ITile
{
    public int Index { get; }
    public Connections Connections { get; }
}

public static class Extentions
{
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