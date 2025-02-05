using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light bulbLight; 
    public Material bulbOnMaterial; 
    public Material bulbOffMaterial; 
    public MeshRenderer bulbRenderer; 
    public float minInterval = 0.1f; 
    public float maxInterval = 2f; 

    private bool isOn = false;

    void Start()
    {
        if (bulbLight == null)
        {
            bulbLight = GetComponent<Light>();
        }
        StartCoroutine(FlickerLight()); 
    }

    IEnumerator FlickerLight()
    {
        while (true)
        {
            isOn = !isOn;

            bulbLight.enabled = isOn;

            if (bulbRenderer != null)
            {
                bulbRenderer.material = isOn ? bulbOnMaterial : bulbOffMaterial;
            }

            float randomInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
