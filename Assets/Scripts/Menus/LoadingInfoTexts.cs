using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class responsible for handling all info ui during the loading screen
/// </summary>
public class LoadingInfoTexts : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _floorText;

    [SerializeField]
    TypewriterEffect typewriter;

    private void Start()
    {
        if(GameState.Instance.Floor != 0)
        {
            typewriter.Begin("Floor " + GameState.Instance.Floor + " ");
            _floorText.fontSize = 62.5f;  
        }
        else{
            typewriter.Begin("Mission ID: X2AX01 \n Agent number : 20300:001 \n A strange signal has been detected "
            + "in the area \n Your mission is to find it, secure it and eliminate if necessary. \n Entering Floor 0. . . . . . .");  

            _floorText.fontSize = 30;  
        }
    }
}
