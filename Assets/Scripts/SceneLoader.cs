using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName) {
        Debug.Log("Loading scene");
        SceneManager.LoadScene(sceneName);
    }
}