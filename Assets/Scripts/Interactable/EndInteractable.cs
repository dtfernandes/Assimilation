using UnityEngine.SceneManagement;

public class EndInteractable : Interactable
{
    public override void Interact()
    {
        //Silly temp method
        //Just resets the game 
        SceneManager.LoadScene("Game");
    }
}
