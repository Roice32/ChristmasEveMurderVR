using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DynamicKinematic : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        // Obține referințe la componente
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Abonează-te la evenimentele de selectare
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);

        // Asigură-te că obiectul este kinematic la început
        rb.isKinematic = true;
    }

    private void OnDestroy()
    {
        // Dezabonează-te de la evenimente când obiectul este distrus
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Dezactivează kinematic și activează gravitatea când obiectul este ridicat
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Asigură-te că gravitatea rămâne activată după eliberare
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
