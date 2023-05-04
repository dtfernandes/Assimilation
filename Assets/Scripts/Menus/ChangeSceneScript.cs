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

    /// <summary>
    /// Method responsible for changing the current scene
    /// </summary>
    public void ChangeScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
