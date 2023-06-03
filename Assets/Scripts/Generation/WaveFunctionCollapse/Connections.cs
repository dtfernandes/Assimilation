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


    public Connections(int up, int down, int left, int right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    [SerializeField]
    private int up, down, left, right;

    public int Up => up;
    public int Down => down;
    public int Left => left;
    public int Right => right;
}