public static class BurkeInterrogation
{
    public static string SpeakerName = "Burke";

    public static DialogueEntry StartEntry;
    public static DialogueEntry StoryEntry;
    public static DialogueEntry EndEntry;

    static BurkeInterrogation()
    {
        // Initialize entries in the correct order
        EndEntry = new DialogueEntry(
            dialogueLine: "Goodbye, adventurer!",
            isChoiceNode: false
        );

        StoryEntry = new DialogueEntry(
            dialogueLine: "Once upon a time in a faraway land...",
            isChoiceNode: false,
            nextEntry: EndEntry
        );

        StartEntry = new DialogueEntry(
            dialogueLine: "Hello there! Would you like to hear a story?",
            isChoiceNode: true,
            choice1Text: "Yes, please!",
            choice1Next: StoryEntry,
            choice2Text: "No, thank you.",
            choice2Next: EndEntry
        );
    }
}
