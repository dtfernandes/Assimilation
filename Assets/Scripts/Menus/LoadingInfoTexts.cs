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

    private void Awake()
    {
        _floorText.text = "Floor " + GameState.Instance.Floor;
    }
}
