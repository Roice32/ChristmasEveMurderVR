using System.Collections;
using UnityEngine;

public class ScreenshotFrameController : MonoBehaviour
{
    
    public GameObject cameraFrame; 

    
    public float frameDisplayDuration = 0.5f;

    private void Start()
    {
        
        if (cameraFrame != null)
        {
            cameraFrame.SetActive(false);
        }
    }

    
    public void TakeScreenshot()
    {
        if (cameraFrame != null)
        {
            StartCoroutine(DisplayFrameAndTakeScreenshot());
        }
    }

    private IEnumerator DisplayFrameAndTakeScreenshot()
    {

        cameraFrame.SetActive(true);

        
        yield return new WaitForSeconds(frameDisplayDuration);

        
        string screenshotPath = $"{Application.dataPath}/Screenshots/screenshot_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        ScreenCapture.CaptureScreenshot(screenshotPath);
        Debug.Log($"Screenshot saved to: {screenshotPath}");


        yield return new WaitForEndOfFrame();
        cameraFrame.SetActive(false);
    }
}
