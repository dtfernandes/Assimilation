using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/RogueTile")]
public class RogueTile : ScriptableObject, ITile
{
    [SerializeField]
    private int _index;
    public int Index { get => _index; }

    [SerializeField]
    private Connections _connections;
    public Connections Connections => _connections;

    [field: SerializeField]
    public GameObject RoomPrefab { get; private set; }

}

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

    [SerializeField]
    private int up, down, left, right;

    public int Up => up;
    public int Down => down;
    public int Left => left;
    public int Right => right;
}