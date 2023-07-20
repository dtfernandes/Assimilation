using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Simple method for changing the scene
/// </summary>
public class ChangeSceneScript : MonoBehaviour
{
    //Name of the scene to change to
    [SerializeField]
    private string _sceneName;

    [SerializeField]
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(0.01f);
    }

    /// <summary>
    /// Method responsible for changing the current scene
    /// </summary>
    public void ChangeScene()
    {
        StartCoroutine("WaitChangeScene");
    }

    public IEnumerator WaitChangeScene()
    {
        yield return _waitForSeconds;
        SceneManager.LoadScene(_sceneName);
    }
}
