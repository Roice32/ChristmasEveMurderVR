using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    private readonly List<string> SPEAKERS = new() { "Burke" };

    [SerializeField] private GameObject DialogueUI;
    [SerializeField] private TMP_Text SpeakerText;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private GameObject Choice1;
    [SerializeField] private GameObject Choice2;
    [SerializeField] private GameObject ChoiceNext;
    [SerializeField] private AudioSource AudioSource;

    private TMP_Text Choice1Text;
    private TMP_Text Choice2Text;

    private Dictionary<string, Dialogue> Dialogues = new();
    private string CurrentSpeaker = null;

    private void Start()
    {
        PreloadDialogues();
        Choice1Text = Choice1.transform.Find("ChoiceText").GetComponent<TMP_Text>();
        Choice2Text = Choice2.transform.Find("ChoiceText").GetComponent<TMP_Text>();
        DialogueUI.SetActive(false);
    }

    private void PreloadDialogues()
    {
        foreach (string speaker in SPEAKERS)
        {
            TextAsset jsonText = Resources.Load<TextAsset>($"Dialogues/{speaker}");
            Dialogues[speaker] = JsonConvert.DeserializeObject<Dialogue>(jsonText.text);
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
            Choice1Text.text = currentEntry.Choice1Text;
            Choice2.SetActive(true);
            Choice2Text.text = currentEntry.Choice2Text;
            ChoiceNext.SetActive(false);
        }
        else
        {
            Choice1.SetActive(false);
            Choice2.SetActive(false);
            ChoiceNext.SetActive(true);
        }
    }

    public void GoToNextEntry(int choice)
    {
        Dialogues[CurrentSpeaker].GoToNextEntry(choice);
        DisplayCurrentEntry();
    }
}