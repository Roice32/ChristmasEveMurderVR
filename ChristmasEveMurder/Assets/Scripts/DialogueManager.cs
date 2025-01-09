using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    private readonly List<string> SPEAKERS = new() { "Burke" };
    private readonly Vector3 SUSPECT_HEAD_POSITION = new Vector3(0.0f, 1.094f, 0.612f);

    [SerializeField] private GameObject DialogueUI;
    [SerializeField] private TMP_Text SpeakerText;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private GameObject Choice1;
    [SerializeField] private GameObject Choice2;
    [SerializeField] private GameObject ChoiceNext;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private GameObject PlayerCamera;
    [SerializeField] private GameObject Suspects;

    private TMP_Text Choice1Text;
    private TMP_Text Choice2Text;

    private Dictionary<string, Dialogue> Dialogues = new();
    private string CurrentSpeaker = null;
    private GameObject CurrentSuspect = null;

    private void Start()
    {
        PreloadDialogues();
        Choice1Text = Choice1.transform.Find("ChoiceText").GetComponent<TMP_Text>();
        Choice2Text = Choice2.transform.Find("ChoiceText").GetComponent<TMP_Text>();
        DialogueUI.SetActive(false);
        DeactivateAllSuspects();
    }

    private void PreloadDialogues()
    {
        foreach (string speaker in SPEAKERS)
        {
            TextAsset jsonText = Resources.Load<TextAsset>($"Dialogues/{speaker}");
            Dialogues[speaker] = JsonConvert.DeserializeObject<Dialogue>(jsonText.text);
        }
    }

    private void DeactivateAllSuspects()
    {
        foreach (Transform suspect in Suspects.transform)
        {
            suspect.gameObject.SetActive(false);
        }
    }

    public void ActivateSuspect(string suspectName)
    {
        Transform suspectTransform = Suspects.transform.Find(suspectName);
        if (suspectTransform != null)
        {
            if (CurrentSuspect != null && CurrentSuspect != suspectTransform.gameObject)
            {
                CurrentSuspect.SetActive(false);
            }

            suspectTransform.gameObject.SetActive(true);
            CurrentSuspect = suspectTransform.gameObject;
        }
    }

    public void EnterDialogue(string speakerName)
    {
        CurrentSpeaker = speakerName;
        SpeakerText.text = speakerName;
        Dialogues[CurrentSpeaker].IsInProgress = true;

        DialogueUI.SetActive(true);
        DisplayCurrentEntry();
    }

    public void EndDialogue()
    {
        DialogueUI.SetActive(false);
        CurrentSpeaker = null;
    }

    private void DisplayCurrentEntry()
    {
        var currentEntry = Dialogues[CurrentSpeaker].GetCurrentEntry();
        if (currentEntry == null)
        {
            EndDialogue();
            return;
        }

        DialogueText.text = currentEntry.DialogueLine;

        if (!string.IsNullOrEmpty(currentEntry.AudioFile))
        {
            AudioClip clip = Resources.Load<AudioClip>(currentEntry.AudioFile);
            if (clip != null)
            {
                AudioSource.clip = clip;
                AudioSource.Play();
            }
        }

        if (currentEntry.IsChoiceNode)
        {
            Choice1.SetActive(true);
            Choice1Text.text = currentEntry.Choice1.Text;
            Choice2.SetActive(true);
            Choice2Text.text = currentEntry.Choice2.Text;
            ChoiceNext.SetActive(false);
        }
        else
        {
            Choice1.SetActive(false);
            Choice2.SetActive(false);
            ChoiceNext.SetActive(true);
        }
    }

    public void AddProximityStressPoints()
    {
        Vector3 playerHeadPosition = PlayerCamera.transform.position;
        float distance = Vector3.Distance(playerHeadPosition, SUSPECT_HEAD_POSITION);
        int stressPoints = 0;
        if (distance >= 3.2f)
        {
            stressPoints = -1;
        }
        if (distance <= 3.0f && distance >= 2.9)
        {
            stressPoints = 1;
        }
        if (distance <= 2.8)
        {
            stressPoints = 2;
        }
        Dialogues[CurrentSpeaker].AddStressPoints(stressPoints);
    }

    public void GoToNextEntry(int choice)
    {
        AddProximityStressPoints();
        Dialogues[CurrentSpeaker].GoToNextEntry(choice);
        Debug.Log($"Stress level: {Dialogues[CurrentSpeaker].StressPoints}");
        DisplayCurrentEntry();
    }
}
