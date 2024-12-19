using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;  

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animation doorAnim;
    [SerializeField] private string openAnimationName = "Door_Open"; 
    [SerializeField] private string closeAnimationName = "Door_Close";
    [SerializeField] private string sceneToLoad; 
    private bool playerInZone = false; 
    private bool isDoorOpen = false; 
    private bool isLoading = false; 

    private void Start()
    {
        if (doorAnim == null)
        {
            doorAnim = GetComponent<Animation>(); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    private void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.E) && !isLoading) 
        {
            ToggleDoor(); 
        }
    }

    private void ToggleDoor()
    {
        if (isDoorOpen)
        {
            if (doorAnim != null && doorAnim.isPlaying == false)
            {
                doorAnim.Play(closeAnimationName); 
                isDoorOpen = false;
            }
        }
        else
        {
            if (doorAnim != null && doorAnim.isPlaying == false)
            {
                doorAnim.Play(openAnimationName); 
                isDoorOpen = true;

                StartCoroutine(WaitForAnimationAndLoadScene());
            }
        }
    }

    private IEnumerator WaitForAnimationAndLoadScene()
    {
        isLoading = true;

        while (doorAnim.isPlaying)
        {
            yield return null;
        }

        LoadScene();
    }

    private void LoadScene()
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
