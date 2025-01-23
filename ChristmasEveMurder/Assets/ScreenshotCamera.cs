using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenshotCamera : MonoBehaviour
{
    public Camera screenshotCamera; // Camera secundară pentru captură
    public Canvas cameraFrameCanvas; // Canvas pentru frame
    public Image flashEffect; // Efect de flash
    public KeyCode takeScreenshotKey = KeyCode.P; // Tasta pentru captură
    public string screenshotFolder = "E:/GitHub/ChristmasEveMurderVR/ChristmasEveMurder/Screenshots"; // Director pentru capturi
    public float flashDuration = 0.2f; // Durata efectului flash

    private bool isTakingScreenshot = false;

    void Start()
    {
        Debug.Log("Scriptul ScreenshotCamera a pornit.");

        // Creează folderul pentru capturi dacă nu există
        if (!System.IO.Directory.Exists(screenshotFolder))
        {
            System.IO.Directory.CreateDirectory(screenshotFolder);
            Debug.Log($"Folder creat: {screenshotFolder}");
        }
        else
        {
            Debug.Log($"Folder deja există: {screenshotFolder}");
        }

        // Dezactivează elementele la start
        cameraFrameCanvas?.gameObject.SetActive(false);
        screenshotCamera?.gameObject.SetActive(false);
        flashEffect?.gameObject.SetActive(false);
    }

    void Update()
    {
        Debug.Log("Update() este apelată.");

        // Verifică dacă tasta pentru captură este apăsată
        if (!isTakingScreenshot && Input.GetKeyDown(takeScreenshotKey))
        {
            Debug.Log($"Tasta {takeScreenshotKey} a fost apăsată. Începem captura de ecran.");
            StartCoroutine(TakeScreenshot());
        }
    }

    private IEnumerator TakeScreenshot()
    {
        Debug.Log("Începerea capturii de ecran.");
        isTakingScreenshot = true;

        // Activare frame și cameră
        if (cameraFrameCanvas != null)
        {
            cameraFrameCanvas.gameObject.SetActive(true);
            Debug.Log("Canvas activat.");
        }

        if (screenshotCamera != null)
        {
            screenshotCamera.gameObject.SetActive(true);
            Debug.Log("Camera de screenshot activată.");
        }

        // Activare efect flash
        if (flashEffect != null)
        {
            flashEffect.gameObject.SetActive(true);
            Debug.Log("Efectul de flash activat.");
            yield return new WaitForSeconds(flashDuration);
            flashEffect.gameObject.SetActive(false);
            Debug.Log("Efectul de flash dezactivat.");
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
        Debug.Log($"Screenshot salvat la: {screenshotPath}");

        // Cleanup
        screenshotCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // Dezactivare frame și cameră
        if (cameraFrameCanvas != null)
        {
            cameraFrameCanvas.gameObject.SetActive(false);
            Debug.Log("Canvas dezactivat.");
        }

        if (screenshotCamera != null)
        {
            screenshotCamera.gameObject.SetActive(false);
            Debug.Log("Camera de screenshot dezactivată.");
        }

        isTakingScreenshot = false;
        Debug.Log("Captura de ecran finalizată.");
    }

    void OnDisable()
    {
        Debug.LogWarning("Scriptul ScreenshotCamera a fost dezactivat!");
    }

    void OnEnable()
    {
        Debug.Log("Scriptul ScreenshotCamera a fost activat!");
    }
}
