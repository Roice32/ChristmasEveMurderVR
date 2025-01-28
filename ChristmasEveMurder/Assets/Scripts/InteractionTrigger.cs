using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionTrigger : MonoBehaviour
{
    public GameObject stobok; 
    public ParticleSystem sclipirici; 
    public float sparkleTime = 2.0f; 
    private bool interaction = false; 

    private XRGrabInteractable grabInteractable;
    private XRSimpleInteractable simpleInteractable;

    private void Start()
    {
        if (stobok != null) stobok.SetActive(false);
        if (sclipirici != null) sclipirici.gameObject.SetActive(false);

        grabInteractable = GetComponent<XRGrabInteractable>();
        simpleInteractable = GetComponent<XRSimpleInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.AddListener(EventStart);
            grabInteractable.onSelectExited.AddListener(StopEvent);   
        }

        else if (simpleInteractable != null)
        {
            simpleInteractable.onSelectEntered.AddListener(EventStart); 
            simpleInteractable.onSelectExited.AddListener(StopEvent);     
        }
    }

    public void MarkEvidenceFound(string name)
    {
        GameState.EvidenceFound.Add(name);
    }

    private void EventStart(XRBaseInteractor interactor)
    {
        if (stobok != null)
        {
            stobok.SetActive(true);
        }

        if (sclipirici != null)
        {
            sclipirici.gameObject.SetActive(true);
            sclipirici.Play();
            Invoke(nameof(StopSparkles), sparkleTime);
        }
    }

    private void StopEvent(XRBaseInteractor interactor)
    {

        if (sclipirici != null)
        {
            sclipirici.Stop();
            sclipirici.gameObject.SetActive(false);
        }
    }

    private void StopSparkles()
    {
        if (sclipirici != null)
        {
            sclipirici.Stop();
            sclipirici.gameObject.SetActive(false);
        }
    }
}
