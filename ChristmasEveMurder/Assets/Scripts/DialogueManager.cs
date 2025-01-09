using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject DialogueUI;
    [SerializeField] private TMP_Text SpeakerText;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private GameObject Choice1;
    [SerializeField] private GameObject Choice2;
    [SerializeField] private GameObject ChoiceNext;
    [SerializeField] private AudioSource AudioSource;

    private TMP_Text Choice1Text;
    private TMP_Text Choice2Text;

    private Dialogue dialogue;
    public DialogueEntry CurrentEntry { get; private set; }

    private void Start()
    {
        Choice1Text = Choice1.transform.Find("ChoiceText").GetComponent<TMP_Text>();
        Choice2Text = Choice2.transform.Find("ChoiceText").GetComponent<TMP_Text>();

        DialogueUI.SetActive(false);
    }

    private void LoadDialogue(string speakerName)
    {
        TextAsset jsonText = Resources.Load<TextAsset>($"Dialogues/{speakerName}");
        dialogue = JsonConvert.DeserializeObject<Dialogue>(jsonText.text);
        SpeakerText.text = dialogue.SpeakerName;
        CurrentEntry = dialogue.Entries["Start"];
    }

    public void StartDialogue(string speakerName)
    {
        LoadDialogue(speakerName);
        DialogueUI.SetActive(true);
        DisplayEntry();
    }

    public void EndDialogue()
    {
        DialogueUI.SetActive(false);
        CurrentEntry = null;
    }

    private void DisplayEntry()
    {
        if (CurrentEntry == null)
        {
            EndDialogue();
            return;
        }

        DialogueText.text = CurrentEntry.DialogueLine;

        if (!string.IsNullOrEmpty(CurrentEntry.AudioFile))
        {
            AudioClip clip = Resources.Load<AudioClip>(CurrentEntry.AudioFile);
            if (clip != null)
            {
                AudioSource.clip = clip;
                AudioSource.Play();
            }
        }

        if (CurrentEntry.IsChoiceNode)
        {
            Choice1.SetActive(true);
            Choice1Text.text = CurrentEntry.Choice1Text;
            Choice2.SetActive(true);
            Choice2Text.text = CurrentEntry.Choice2Text;
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
        string nextEntryKey = null;
        switch (choice)
        {
            case 0:
                nextEntryKey = CurrentEntry.NextEntry;
                break;
            case 1:
                nextEntryKey = CurrentEntry.Choice1Next;
                break;
            case 2:
                nextEntryKey = CurrentEntry.Choice2Next;
                break;
        }

        if (!string.IsNullOrEmpty(nextEntryKey) && dialogue.Entries.ContainsKey(nextEntryKey))
        {
            CurrentEntry = dialogue.Entries[nextEntryKey];
        }
        else
        {
            CurrentEntry = null;
        }

        DisplayEntry();
    }
}