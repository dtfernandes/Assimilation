using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    private GameState _gameState;
    [SerializeField]
    private GameObject _menu;

    private void Awake()
    {
        _gameState = GameState.Instance;
    }

    private void Update()
    {
      

        //Cheat 
        if (Input.GetKeyDown(KeyCode.J))
        {
            _gameState.Reset();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_gameState.IsWorldStopped)
            {
                _menu.SetActive(true);

                _gameState.IsWorldStopped = true;
            }
            else if (_menu.activeSelf)
            {
                LeaveMenu();
            }
        }
    }

    public void LeaveMenu()
    {
        _menu.SetActive(false);

        _gameState.IsWorldStopped = false;
    }

}
