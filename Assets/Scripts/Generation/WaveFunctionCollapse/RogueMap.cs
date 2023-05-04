using UnityEngine;
using System.Linq;

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



    public void GenerateMap()
    {
        //Create array of slots with the defined sizes
        _slots = new Slot[_width, _height];

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

        WaveFunctionCollapse wfc = new WaveFunctionCollapse();

        wfc.PropagateReaction(test, this);
        
        wfc.Collapse(this);

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

    public Slot[] GetNeighbours(Slot slot)
    {
        System.Tuple<int,int> coords = _slots.CoordinatesOf(slot);
        Slot[] returnArray = new Slot[4];



        //Get slot to the left
        if(coords.Item1 - 1 >= 0)
        {
            Slot left = _slots[coords.Item1 - 1, coords.Item2];
            returnArray[0] = left;
        }
        //Get slot to the right
        if (coords.Item1 + 1 < _width)
        {
            Slot right = _slots[coords.Item1 + 1, coords.Item2];
            returnArray[1] = right;
        }
        //Get slot to the top_tiles
        if (coords.Item2 - 1 >= 0)
        {
            Slot top = _slots[coords.Item1, coords.Item2 - 1];
            returnArray[2] = top;
        }
        //Get slot to the bottom
        if (coords.Item2 + 1 < _height)
        {
            Slot bottom = _slots[coords.Item1, coords.Item2 + 1];
            returnArray[3] = bottom;
        }

        return returnArray;
    }
}
