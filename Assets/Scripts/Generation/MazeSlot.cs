using UnityEngine;

[System.Serializable]
public struct MazeSlot
{
    public bool Visited { get; set; }
    [field:SerializeField]
    public Biome Left { get; set; }
    [field:SerializeField]
    public Biome Right { get; set; }
    [field:SerializeField]
    public Biome Up { get; set; }
    [field:SerializeField]
    public Biome Down { get; set; }

    /// <summary>
    /// Get what is the main biome of this slot.
    /// </summary>
    /// <returns>Main Biome of the slot</returns>
    public Biome GetBiome()
    {
        //Create a list of all the connections in order of priority
        Biome[] connections = new Biome[]{Left, Right, Up, Down};

        //Iterate until it finds a "Open" connection
        foreach (Biome b in connections)
        {
            Biome biomeTemp = b & ~Biome.Closed;

            if (biomeTemp == 0)
                continue;

            return biomeTemp;
        }

        return Biome.Closed;

    }

    public void Default()
    {
        Left = Biome.Closed;
        Right = Biome.Closed;
        Up = Biome.Closed;
        Down = Biome.Closed;
    }
}


