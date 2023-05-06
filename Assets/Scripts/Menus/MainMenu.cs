using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible for handling the main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Prepares the initial game state.
    /// Needs better placement
    /// </summary>
    public void ResetGameState()
    {
        GameState.Instance.Reset();
    }

    /// <summary>
    /// Ends the aplication
    /// </summary>
    public void EndApliccation()
    {
        Application.Quit();
    }

}
