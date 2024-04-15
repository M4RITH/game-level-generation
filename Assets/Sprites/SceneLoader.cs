using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int sceneIndexToLoad; 

    public void LoadSceneByIndex()
    {
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}
