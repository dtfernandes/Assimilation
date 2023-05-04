public interface IMap
{
    public int Witdh  { get; }
    public int Height { get; }

    public Slot[,] Slots { get; }

    public void GenerateMap();
    public Slot[] GetNeighbours(Slot slot);
}
