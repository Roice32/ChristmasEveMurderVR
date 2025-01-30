using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EvidenceHintsManager : MonoBehaviour
{
    public InputActionReference toggleEvidenceHintsAction;
    

    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
        toggleEvidenceHintsAction.action.Enable();
        toggleEvidenceHintsAction.action.performed += HandleToggleEvidenceHints;
        InputSystem.onDeviceChange += OnDeviceChange;
        SceneManager.sceneLoaded += HideAllEvidenceHints;
    }

    private void OnDestroy()
    {
        toggleEvidenceHintsAction.action.Disable();
        toggleEvidenceHintsAction.action.performed -= HandleToggleEvidenceHints;
        InputSystem.onDeviceChange -= OnDeviceChange;
        SceneManager.sceneLoaded -= HideAllEvidenceHints;
    }

    private void HandleToggleEvidenceHints(InputAction.CallbackContext context)
    {
        GameObject[] evidenceHints = GameObject.FindGameObjectsWithTag("EvidenceHint");
        foreach (GameObject evidenceHint in evidenceHints)
        {
            MeshRenderer meshRenderer = evidenceHint.GetComponent<MeshRenderer>();
            meshRenderer.enabled = !meshRenderer.enabled;
        }
    }

    private void HideAllEvidenceHints(Scene scene, LoadSceneMode mode)
    {
        GameObject[] evidenceHints = GameObject.FindGameObjectsWithTag("EvidenceHint");
        foreach (GameObject evidenceHint in evidenceHints)
        {
            MeshRenderer meshRenderer = evidenceHint.GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
        }
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                toggleEvidenceHintsAction.action.Disable();
                toggleEvidenceHintsAction.action.performed -= HandleToggleEvidenceHints;
                break;
            case InputDeviceChange.Reconnected:
                toggleEvidenceHintsAction.action.Enable();
                toggleEvidenceHintsAction.action.performed += HandleToggleEvidenceHints;
                break;
        }
    }
}
