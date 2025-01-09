using System.Collections.Generic;
using UnityEngine;

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

    public void AddStressPoints(int points)
    {
        StressPoints += points;
        if (StressPoints < 0)
        {
            StressPoints = 0;
        }
        if (StressPoints > 5)
        {
            StressPoints = 5;
        }
    }

    private bool IsGoingToLie()
    {
        return StressPoints > 3;
    }

    public void GoToNextEntry(int choice)
    {
        Choice selectedChoice = null;
        switch (choice)
        {
            case 0:
                selectedChoice = GetCurrentEntry().ChoiceNext;
                break;
            case 1:
                selectedChoice = GetCurrentEntry().Choice1;
                break;
            case 2:
                selectedChoice = GetCurrentEntry().Choice2;
                break;
        }

        if (selectedChoice == null)
        {
            CurrentEntry = null;
            IsInProgress = false;
            return;
        }

        AddStressPoints(selectedChoice.StressPoints);

        string nextEntryKey = null;
        if (IsGoingToLie())
        {
            nextEntryKey = selectedChoice.NextLie;
        }
        else
        {
            nextEntryKey = selectedChoice.NextTruth;
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
