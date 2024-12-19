public class DialogueEntry
{
    public string DialogueLine { get; set; }
    public string AudioFile { get; set; }
    public bool IsChoiceNode { get; set; }
    public string Choice1Text { get; set; }
    public DialogueEntry Choice1Next { get; set; }
    public string Choice2Text { get; set; }
    public DialogueEntry Choice2Next { get; set; }
    public DialogueEntry NextEntry { get; set; } // For linear progression if no choices

    public DialogueEntry(
        string dialogueLine,
        string audioFile = null,
        bool isChoiceNode = false,
        string choice1Text = null,
        DialogueEntry choice1Next = null,
        string choice2Text = null,
        DialogueEntry choice2Next = null,
        DialogueEntry nextEntry = null)
    {
        DialogueLine = dialogueLine;
        AudioFile = audioFile;
        IsChoiceNode = isChoiceNode;
        Choice1Text = choice1Text;
        Choice1Next = choice1Next;
        Choice2Text = choice2Text;
        Choice2Next = choice2Next;
        NextEntry = nextEntry;
    }
}
