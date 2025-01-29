using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ShowMenuManager : MonoBehaviour
{
    public InputActionReference showMenuAction;
    [SerializeField] private GameObject menu;

    private void Awake()
    {
        menu.SetActive(false);
        showMenuAction.action.Enable();
        showMenuAction.action.performed += HandleShowMenu;
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDestroy()
    {
        showMenuAction.action.Disable();
        showMenuAction.action.performed -= HandleShowMenu;
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void HandleShowMenu(InputAction.CallbackContext context)
    {
        menu.SetActive(!menu.activeSelf);
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                showMenuAction.action.Disable();
                showMenuAction.action.performed -= HandleShowMenu;
                break;
            case InputDeviceChange.Reconnected:
                showMenuAction.action.Enable();
                showMenuAction.action.performed += HandleShowMenu;
                break;
        }
    }
}
