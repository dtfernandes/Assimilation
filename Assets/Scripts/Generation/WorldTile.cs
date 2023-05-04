using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{

    //Walls, floor and ceiling
    [Header("Endings")]
    [SerializeField]
    private GameObject _left;
    [SerializeField]
    private GameObject _right, _up, _down;

    [Header("Open Variants")]
    [SerializeField]
    private GameObject _openFloor;
    [SerializeField]
    private GameObject _openCeiling;

    [Header("Other Setups")]
    [SerializeField]
    private GameObject _start;
    [SerializeField]
    private GameObject _end;

    public DangerLevel Danger { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    /// <summary>
    /// Config this tile type
    /// </summary>
    /// <param name="slot">Data of the slot this tile will mimic</param>
    public void ConfigTile(MazeSlot slot)
    {
        if (slot.Down)
        {
            _down.SetActive(false);
            _openFloor.SetActive(true);
        }

        if (slot.Up)
        {
            _up.SetActive(false);
            _openCeiling.SetActive(true);
        }

        if (slot.Left)
        {
            _left.SetActive(false);
        }

        if (slot.Right)
        {
            _right.SetActive(false);
        }

    }

    /// <summary>
    /// Makes this tile a start Tile
    /// </summary>
    public void SetupAsStart()
    {
        _start.SetActive(true);    
    }

    /// <summary>
    /// Makes this tile an end Tile
    /// </summary>
    public void SetupAsEnd()
    {
        _end.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Danger.GetColor();
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

}
