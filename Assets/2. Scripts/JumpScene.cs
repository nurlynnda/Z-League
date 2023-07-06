using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpScene : MonoBehaviour
{
    public string change;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(change);
    }

    public void LoadNextSceneString(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}