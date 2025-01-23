using UnityEngine;
using UnityEngine.UI;

public class NarrationSubtitles : MonoBehaviour
{
    public Text subtitleText; 
    public AudioSource narrationAudio; 

    private float[] subtitleTimes = { 0f, 5f, 10f, 20f }; 
    private string[] subtitles = {
        "At 1:00 p.m., Detective Arndt requested John Ramsey and Fleet White, a family friend, to search the house and report anything unusual.",
        "They began their search in the basement.",
        "John opened a latched door that had been overlooked by Officer French.",
        "Inside, the lifeless body of his daughter lay in one of the rooms."
    };

    private int currentIndex = 0;

    void Start()
    {
        subtitleText.text = ""; 
        narrationAudio.Play(); 
    }

    void Update()
    {
        if (currentIndex < subtitles.Length && narrationAudio.time >= subtitleTimes[currentIndex])
        {
            subtitleText.text = subtitles[currentIndex];
            currentIndex++;
        }

        if (currentIndex >= subtitles.Length && !narrationAudio.isPlaying)
        {
            subtitleText.text = "";
        }
    }
}
