using UnityEngine;

[System.Serializable]
public class Connections
{
    public Connections()
    {
        up = 0;
        down = 0;
        left = 0;
        right = 0;
    }


    public Connections(Biome up, Biome down, Biome left, Biome right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    [SerializeField]
    private Biome up, down, left, right;

    public Biome Up => up;
    public Biome Down => down;
    public Biome Left => left;
    public Biome Right => right;
}