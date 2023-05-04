using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable: MonoBehaviour
{
    public const string TagName = "Interactable";

    [SerializeField]
    private GameObject _interactionAlert;

    private void Awake()
    {
        //Setup tag
        gameObject.tag = TagName;
    }

    public virtual void Enter()
    {
        _interactionAlert.SetActive(true);
    }

    public virtual void Exit()
    {
        _interactionAlert.SetActive(false);
    }

    public abstract void Interact();
}
