using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MagicLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameState.Instance.IsWorldStopped = false;
        Time.timeScale = 1;
        StartCoroutine(Magic());
    }

    IEnumerator Magic()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
