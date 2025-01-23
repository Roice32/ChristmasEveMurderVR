using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class QuickTeleportManager : MonoBehaviour
{
    public InputActionReference quickTeleportAction;

    private string previouslyLoadedScene = "";

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        quickTeleportAction.action.Enable();
        quickTeleportAction.action.performed += HandleQuickTeleport;
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDestroy()
    {
        quickTeleportAction.action.Disable();
        quickTeleportAction.action.performed -= HandleQuickTeleport;
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void HandleQuickTeleport(InputAction.CallbackContext context)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName != "Interrogation")
        {
            previouslyLoadedScene = currentSceneName;
            SceneManager.LoadScene("Interrogation", LoadSceneMode.Single);
        }
        else if (!string.IsNullOrEmpty(previouslyLoadedScene))
        {
            SceneManager.LoadScene(previouslyLoadedScene, LoadSceneMode.Single);
        }
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                quickTeleportAction.action.Disable();
                quickTeleportAction.action.performed -= HandleQuickTeleport;
                break;
            case InputDeviceChange.Reconnected:
                quickTeleportAction.action.Enable();
                quickTeleportAction.action.performed += HandleQuickTeleport;
                break;
        }
    }
}
