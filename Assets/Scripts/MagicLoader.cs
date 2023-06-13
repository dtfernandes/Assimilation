using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MagicLoader : MonoBehaviour
{

    [SerializeField]
    TypewriterEffect typewritter;

    // Start is called before the first frame update
    void Start()
    {
        GameState.Instance.IsWorldStopped = false;
        Time.timeScale = 1;
        StartCoroutine(Magic());

       
    }

    IEnumerator Magic()
    {

        while (typewritter.IsAnimating())
        {
            yield return null;
        }

        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
