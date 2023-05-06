using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/GameState")]
public class GameState : ScriptableSingletonObject<GameState>
{
    public void Reset()
    {
        Floor = 0;
    }

    public int Floor { get; set; }
}
