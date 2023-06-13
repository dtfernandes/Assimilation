using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ButtonSounds : MonoBehaviour
{
    private AudioSource _audio;
    private Button button;

    [SerializeField]
    private AudioClip _clickSound, _highlightSound;

    void Start()
    {
        button = GetComponent<Button>();
        _audio = GetComponent<AudioSource>();

        button.onClick.AddListener(OnButtonClick);

        var eventTrigger = button.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = button.gameObject.AddComponent<EventTrigger>();
        }

        var pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((data) => { OnButtonHighlight(); });
        eventTrigger.triggers.Add(pointerEnter);
    }
    void OnButtonClick()
    {
        if (_clickSound != null)
        {
            _audio.clip = _clickSound;
            _audio.Play();
        }
    }

    void OnButtonHighlight()
    {
        _audio.clip = _highlightSound;
        _audio.Play();                   
    }
}
