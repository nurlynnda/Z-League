using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButton : MonoBehaviour
{
    public string nextSceneName;  // The name of the next scene to load

    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
