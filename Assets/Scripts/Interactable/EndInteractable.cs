using UnityEngine.SceneManagement;
using UnityEngine;

public class EndInteractable : Interactable
{

    private void Update()
    {
        //Cheat
        if (Input.GetKeyDown(KeyCode.K))
        {
            Player p = FindObjectOfType<Player>();
            p.transform.position = transform.position;
        }

    }

    public override void Interact()
    {
        
        
        GameState gState = GameState.Instance;
        gState.Floor++;
        
        if (gState.Floor == 5)
        {
            GameState.Instance.GameResult = GameResult.Win;
            SceneManager.LoadScene("GameLose");
        }
        else
        {          
            SceneManager.LoadScene("LoadingScreen");
        }
    }
}
