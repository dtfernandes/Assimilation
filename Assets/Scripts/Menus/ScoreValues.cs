using UnityEngine;

[System.Serializable]
public struct ScoreValues
{
    public ScoreValues(int floor, string name, float time)
    {
        Floor = floor;
        Name = name;
        Time = time;
    }

    [field:SerializeField]
    public int Floor { get; private set; }

    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public float Time { get; private set; }


}