using System.Collections.Generic;

public class Dialogue
{
    public string SpeakerName { get; set; }
    public Dictionary<string, DialogueEntry> Entries { get; set; }
}
