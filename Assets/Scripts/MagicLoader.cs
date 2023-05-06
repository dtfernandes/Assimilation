using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MagicLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Magic());
    }

    IEnumerator Magic()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
