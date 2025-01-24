using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class VerdictManager : MonoBehaviour
{
    private float DELAY = 1.0f;

    [SerializeField] private GameObject Suspects;
    [SerializeField] private GameObject EndScreenLights;
    [SerializeField] private GameObject EndScreenStats;
    [SerializeField] private GameObject EndScreenButtons;

    private string CurrentlyAccused;

    private void Start()
    {
        CurrentlyAccused = null;
        Suspects.SetActive(true);
        DeactivateEndScreen();
        PlayAudio("GeneralAudio", "Narrations/EnterVerdictRoom");
    }

    private void DeactivateEndScreen()
    {
        foreach (Transform light in EndScreenLights.transform)
        {
            light.gameObject.SetActive(false);
        }
        foreach (Transform stat in EndScreenStats.transform)
        {
            stat.gameObject.SetActive(false);
        }
        EndScreenButtons.SetActive(false);
    }

    public void Accuse(string suspectName)
    {
        if (CurrentlyAccused != suspectName)
        {
            CurrentlyAccused = suspectName;
            PlayAudio(suspectName, $"Voicelines/Verdict/{suspectName}");
        }
        else
        {
            new WaitForSeconds(DELAY);
            PlayAudio("GeneralAudio", "EnvSounds/GavelStrike");
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        HideOtherSuspects();
        yield return StartCoroutine(TurnOnLights());
        yield return StartCoroutine(ShowStats());
        yield return new WaitForSeconds(DELAY);
        EndScreenButtons.SetActive(true);
    }

    private void HideOtherSuspects()
    {
        foreach (Transform suspect in Suspects.transform)
        {
            if (suspect.name != CurrentlyAccused)
            {
                suspect.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator TurnOnLights()
    {
        yield return new WaitForSeconds(DELAY);
        EndScreenLights.transform.Find("DirectionalLight").gameObject.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(DELAY);
            PlayAudio("GeneralAudio", "EnvSounds/LightSwitch");
            EndScreenLights.transform.Find($"SpotLight{i}").gameObject.SetActive(true);
        }
    }

    IEnumerator ShowStats()
    {
        yield return new WaitForSeconds(2*DELAY);
        PlayAudio("GeneralAudio", "EnvSounds/Typewriter");
        SetStatText("EvidenceCount", $"You found {GameState.EvidenceFound.Count}/{GameState.TOTAL_EVIDENCE_COUNT} pieces of evidence.");

        yield return new WaitForSeconds(2*DELAY);
        PlayAudio("GeneralAudio", "EnvSounds/Typewriter");
        SetStatText("LiedToCount", $"You have been lied to {GameState.LiedCount.Values.Sum()} times.");

        yield return new WaitForSeconds(2*DELAY);
        PlayAudio("GeneralAudio", "EnvSounds/Typewriter");
        SetStatText("Accused", $"You think {CurrentlyAccused} is the killer...");

        PlayAudio("GeneralAudio", "EnvSounds/DrumRoll");
        yield return new WaitForSeconds(3*DELAY);
        PlayAudio("GeneralAudio", "EnvSounds/DrumRoll");
        SetStatText("FoundTheMurderer", CurrentlyAccused == "Burke" ? "... and you are CORRECT!" : "... and you are WRONG!");

        yield return new WaitForSeconds(2*DELAY);
        PlayAudio("GeneralAudio", "EnvSounds/Typewriter");
        ShowHint();
    }

    private void SetStatText(string statName, string statValue)
    {
        Transform stat = EndScreenStats.transform.Find(statName);
        TMP_Text statText = stat.GetComponent<TMP_Text>();
        statText.text = $"{statValue}";
        stat.gameObject.SetActive(true);
    }

    private void ShowHint()
    {
        if (GameState.EvidenceFound.Count < GameState.TOTAL_EVIDENCE_COUNT)
        {
            SetStatText("Hint", "Hint: You can hold 'A' to show all evidence in a room."); // Modify this when hint button is set
            return;
        }

        string bestLiar = GameState.LiedCount.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        if (GameState.LiedCount[bestLiar] == 0)
        {
            SetStatText("Hint", "Hint: You had all the pieces of the puzzle.");
            return;
        }

        switch (bestLiar)
        {
            case "Burke":
                SetStatText("Hint", "Hint: Burke might fumble his words under a lot of stress.");
                break;
            case "Patsy":
                SetStatText("Hint", "Hint: Patsy doesn't trust you unless you are really serious.");
                break;
            case "John":
                SetStatText("Hint", "Hint: John is old. Even he doesn't know when he's lying to you.");
                break;
        }
    }

    private void PlayAudio(string audioSourceName, string audioFilePath)
    {
        AudioSource audioSource = GameObject.Find(audioSourceName).GetComponentInChildren<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>(audioFilePath);
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
