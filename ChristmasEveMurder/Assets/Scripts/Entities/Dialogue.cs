using System.Collections.Generic;

public class Dialogue
{
    public string SpeakerName { get; set; }
    public Dictionary<string, DialogueEntry> Entries { get; set; }
    public string CurrentEntry { get; set; }
    public bool IsInProgress { get; set; }
    public int StressPoints { get; set; }

    public DialogueEntry GetCurrentEntry()
    {
        if (string.IsNullOrEmpty(CurrentEntry))
        {
            if (IsInProgress)
            {
                return Entries["ExhaustedDialogue"];
            }
            return null;
        }
        return Entries[CurrentEntry];
    }

    public void GoToNextEntry(int choice)
    {
        string nextEntryKey = null;
        switch (choice)
        {
            case 0:
                nextEntryKey = GetCurrentEntry().NextEntry;
                break;
            case 1:
                nextEntryKey = GetCurrentEntry().Choice1Next;
                break;
            case 2:
                nextEntryKey = GetCurrentEntry().Choice2Next;
                break;
        }
        if (!string.IsNullOrEmpty(nextEntryKey) && Entries.ContainsKey(nextEntryKey))
        {
            CurrentEntry = nextEntryKey;
        }
        else
        {
            CurrentEntry = null;
            IsInProgress = false;
        }
    }
}
