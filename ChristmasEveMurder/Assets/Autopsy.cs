using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionAudio : MonoBehaviour
{
    [SerializeField] private AudioClip interactionAudio; // Assign the audio clip in the Inspector
    private AudioSource audioSource;

    private XRGrabInteractable grabInteractable;
    private XRSimpleInteractable simpleInteractable;

    private void Start()
    {
        // Set up the AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = interactionAudio;
        audioSource.playOnAwake = false;

        // Get the interactable components
        grabInteractable = GetComponent<XRGrabInteractable>();
        simpleInteractable = GetComponent<XRSimpleInteractable>();

        // Attach interaction events
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.AddListener(PlayAudio); // On grab
        }
        else if (simpleInteractable != null)
        {
            simpleInteractable.onSelectEntered.AddListener(PlayAudio); // On simple interact
        }
    }

    private void PlayAudio(XRBaseInteractor interactor)
    {
        if (audioSource != null && interactionAudio != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
