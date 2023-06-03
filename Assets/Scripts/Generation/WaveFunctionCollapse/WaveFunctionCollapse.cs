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
            //Id there's no neighbour at this (s) direction
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
                maze[i, j].Left = Biome.Cavern;
            }
        }

        return maze;
    }
   
}
