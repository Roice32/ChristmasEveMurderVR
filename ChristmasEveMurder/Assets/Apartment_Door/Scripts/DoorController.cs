using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorController : MonoBehaviour
{
    
    [SerializeField] private string sceneToLoad; // Scene to load after opening the door
    
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is not assigned!");
        }
    }
}
