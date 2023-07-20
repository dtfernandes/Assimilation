using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MagicLoader : MonoBehaviour
{

    [SerializeField]
    TypewriterEffect typewritter;
    [SerializeField]
    GameTimer timer;


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


        timer.StartTimer();

        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}

