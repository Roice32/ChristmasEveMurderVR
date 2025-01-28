using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NoteInteraction : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 offset = new Vector3(0, 0, 0.5f);
    private AudioSource audioSource;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not attached to the note!");
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void OnSelectEnter()
    {
        if (cameraTransform != null)
        {
            transform.position = cameraTransform.position + cameraTransform.forward * offset.z +
                                 cameraTransform.up * offset.y +
                                 cameraTransform.right * offset.x;
            transform.rotation = cameraTransform.rotation;
        }

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void OnSelectExit()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
