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
