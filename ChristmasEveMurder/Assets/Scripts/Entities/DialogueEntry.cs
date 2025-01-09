public class DialogueEntry
{
    public string DialogueLine { get; set; }
    public string AudioFile { get; set; }
    public bool IsChoiceNode { get; set; }
    public string Choice1Text { get; set; }
    public string Choice1Next { get; set; }
    public string Choice2Text { get; set; }
    public string Choice2Next { get; set; }
    public string NextEntry { get; set; }
}
