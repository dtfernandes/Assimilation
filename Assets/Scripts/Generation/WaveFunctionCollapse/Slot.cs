using System.Collections.Generic;

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


    /// <summary>
    /// Collapse the possible tiles into a new set of tiles
    /// </summary>
    /// <param name="index">Direction of the conneciton</param>
    /// <param name="connections">Type of connection</param>
    public void Collapse(int index, Connections connections)
    {
        Biome connection = 0;
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
            Biome otherCon = 0;
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
            if (otherCon == connection || connection == 0)
                newPossibleTiles.Add(tile);
        }

        PossibleTiles = newPossibleTiles;
    }

    public void Collapse(int index, IList<ITile> possible)
    {
        IList<ITile> newPossibleTiles = new List<ITile> { };
       
        foreach (ITile t in possible)
        {
            Biome connection = 0;
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
                Biome otherCon = 0;
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
