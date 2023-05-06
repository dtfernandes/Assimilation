using UnityEngine.SceneManagement;

public class EndInteractable : Interactable
{
    public override void Interact()
    {

        GameState gState = GameState.Instance;
        gState.Floor++;
        
        if (gState.Floor == 1)
        {
            SceneManager.LoadScene("GameWin");
        }
        else
        {          
            SceneManager.LoadScene("LoadingScreen");
        }
    }
}
