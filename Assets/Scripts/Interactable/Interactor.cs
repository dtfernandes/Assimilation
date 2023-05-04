using UnityEngine;

public class Interactor: MonoBehaviour
{

    private Interactable _currentInteractable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Interactable.TagName)
        {
            _currentInteractable = collision.gameObject.GetComponent<Interactable>();
            _currentInteractable.Enter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Interactable.TagName)
        {
            _currentInteractable.Exit();
            _currentInteractable = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(_currentInteractable != null)
                _currentInteractable.Interact();
        }   
    }

}