public class DialogueEntry
{
    public string DialogueLine { get; set; }
    public string AudioFile { get; set; } = null;
    public bool IsChoiceNode { get; set; } = false;
    public Choice Choice1 { get; set; } = null;
    public Choice Choice2 { get; set; } = null;
    public Choice ChoiceNext { get; set; } = null;
}
