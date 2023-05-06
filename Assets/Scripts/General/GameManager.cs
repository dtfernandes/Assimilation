using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the game's systems
/// </summary>
public class GameManager : MonoBehaviour
{

    private GameState _gameState;
    [SerializeField]
    private SkillSelection _skillSelection;


    private void Awake()
    {
        _gameState = GameState.Instance;
    }

    private void Start()
    {
        if (_gameState.Floor > 0)
        {
            //Start Degrade Selection
            _skillSelection.gameObject.SetActive(true);
            _skillSelection.SetupSelection(UpgradeType.Degrade);
        }
    }
}
