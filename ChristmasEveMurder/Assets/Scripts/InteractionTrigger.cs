using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionTrigger : MonoBehaviour
{
    public GameObject placuta; // Plăcuța care apare
    public ParticleSystem sclipici; // Sistemul de particule pentru sclipici
    public AudioClip naratiuneAudio; // Fișierul audio pentru narativă
    public float durataSclipici = 2.0f; // Durata în secunde pentru sclipici
    public float durataNaratiune; // Durata narativului (calculată automat)

    private AudioSource audioSource;
    private bool interacțiuneDeclanșată = false; // Pentru a preveni re-declanșarea

    // Adăugăm referință la XRGrabInteractable
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        // Asigură-te că plăcuța și sclipicii sunt ascunse la început
        if (placuta != null) placuta.SetActive(false);
        if (sclipici != null) sclipici.gameObject.SetActive(false);

        // Adaugă un AudioSource component dacă nu există deja
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Setează durata narativului dacă audio este prezent
        if (naratiuneAudio != null)
        {
            durataNaratiune = naratiuneAudio.length;
        }

        // Ia referință la componenta XRGrabInteractable
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Asigură-te că sunt adăugate evenimentele
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.AddListener(DeclanseazaEveniment); // La apucare
            grabInteractable.onSelectExited.AddListener(OpresteEveniment);    // La eliberare (opțional)
        }
    }

    private void DeclanseazaEveniment(XRBaseInteractor interactor)
    {
        // Afișează plăcuța permanent
        if (placuta != null)
        {
            placuta.SetActive(true);
        }

        // Activează și pornește sclipicii temporar
        if (sclipici != null)
        {
            sclipici.gameObject.SetActive(true);
            sclipici.Play();
            Invoke(nameof(OpresteSclipicii), durataSclipici);
        }

        // Pornește narativul audio
        if (naratiuneAudio != null && audioSource != null)
        {
            audioSource.clip = naratiuneAudio;
            audioSource.Play();
        }
    }

    private void OpresteEveniment(XRBaseInteractor interactor)
    {
        // Oprește narativul audio (opțional)
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Poți decide să oprești efectul de sclipici la eliberare (opțional)
        // Oprește sistemul de particule și ascunde-l
        if (sclipici != null)
        {
            sclipici.Stop();
            sclipici.gameObject.SetActive(false);
        }
    }

    private void OpresteSclipicii()
    {
        if (sclipici != null)
        {
            sclipici.Stop();
            sclipici.gameObject.SetActive(false);
        }
    }
}
