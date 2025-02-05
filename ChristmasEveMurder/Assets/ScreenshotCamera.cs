using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenshotCamera : MonoBehaviour
{
    public Camera screenshotCamera; // Camera secundară pentru captură
    public Camera mainCamera; // Camera principală
    public Canvas cameraFrameCanvas; // Canvas pentru frame
    public GameObject cameraFrame; // Frame-ul camerei
    public Image flashEffect; // Efect de flash
    public KeyCode takeScreenshotKey = KeyCode.P; // Tasta pentru captură
    public string screenshotFolder = "E:/GitHub/ChristmasEveMurderVR/ChristmasEveMurder/Screenshots"; // Director pentru capturi
    public float flashDuration = 0.2f; // Durata efectului flash

    private bool isTakingScreenshot = false;

    void Start()
    {
       // Debug.Log("Scriptul ScreenshotCamera a pornit.");

        // Creează folderul pentru capturi dacă nu există
        if (!System.IO.Directory.Exists(screenshotFolder))
        {
            System.IO.Directory.CreateDirectory(screenshotFolder);
           // Debug.Log($"Folder creat: {screenshotFolder}");
        }
        else
        {
           // Debug.Log($"Folder deja există: {screenshotFolder}");
        }

        // Dezactivează efectele la start
        flashEffect?.gameObject.SetActive(false);
    }

    void Update()
    {
        // Sincronizează rama cu Main Camera în timpul jocului
        SyncCameraFrameToMainCamera();

        // Verifică dacă tasta pentru captură este apăsată
        if (!isTakingScreenshot && Input.GetKeyDown(takeScreenshotKey))
        {
            StartCoroutine(TakeScreenshot());
        }
    }

    private void SyncScreenshotCameraWithMainCamera()
    {
        if (mainCamera != null && screenshotCamera != null)
        {
            // Sincronizează poziția și rotația
            screenshotCamera.transform.position = mainCamera.transform.position;
            screenshotCamera.transform.rotation = mainCamera.transform.rotation;

            // Sincronizează setările camerei
            screenshotCamera.fieldOfView = mainCamera.fieldOfView;
            screenshotCamera.nearClipPlane = mainCamera.nearClipPlane;
            screenshotCamera.farClipPlane = mainCamera.farClipPlane;
            screenshotCamera.clearFlags = mainCamera.clearFlags;
            screenshotCamera.backgroundColor = mainCamera.backgroundColor;
            screenshotCamera.cullingMask = mainCamera.cullingMask;
        }
    }

    private void SyncCameraFrameToMainCamera()
    {
        if (cameraFrame != null && mainCamera != null)
        {
            // Poziționează rama ușor în fața camerei principale
            float distanceInFront = 0.1f; // Ajustează distanța
            cameraFrame.transform.position = mainCamera.transform.position + mainCamera.transform.forward * distanceInFront;

            // Sincronizează orientarea ramei cu camera principală
            cameraFrame.transform.rotation = mainCamera.transform.rotation;
        }
    }

    private IEnumerator TakeScreenshot()
    {
        isTakingScreenshot = true;

        // Sincronizează camera de screenshot cu camera principală
        SyncScreenshotCameraWithMainCamera();

        // Activare efect flash
        if (flashEffect != null)
        {
            flashEffect.gameObject.SetActive(true);
            yield return new WaitForSeconds(flashDuration);
            flashEffect.gameObject.SetActive(false);
        }

        // Așteaptă până la finalul frame-ului pentru a captura imaginea corect
        yield return new WaitForEndOfFrame();

        // Configurare RenderTexture
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        screenshotCamera.targetTexture = renderTexture;

        // Creare textură pentru captură
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshotCamera.Render();

        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // Salvează fișierul
        byte[] bytes = screenshot.EncodeToPNG();
        string screenshotPath = $"{screenshotFolder}/Screenshot_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        System.IO.File.WriteAllBytes(screenshotPath, bytes);
        //Debug.Log($"Screenshot salvat la: {screenshotPath}");

        // Cleanup
        screenshotCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        isTakingScreenshot = false;
        //Debug.Log("Captura de ecran finalizată.");
    }

    void OnDisable()
    {
        /*Debug.LogWarning("Scriptul ScreenshotCamera a fost dezactivat!");*/
    }

    void OnEnable()
    {
        Debug.Log("Scriptul ScreenshotCamera a fost activat!");
    }
}
