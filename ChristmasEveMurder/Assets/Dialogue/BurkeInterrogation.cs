public static class BurkeInterrogation
{
    public static string SpeakerName = "Burke";

    public static DialogueEntry StartEntry;
    public static DialogueEntry EmpathyEntry1;
    public static DialogueEntry EmpathyEntry2;
    public static DialogueEntry InfoEntry1;
    public static DialogueEntry DefensiveEndEntry1;
    public static DialogueEntry DefensiveEndEntry2;

    static BurkeInterrogation()
    {
        DefensiveEndEntry2 = new DialogueEntry(
            dialogueLine: "Oh, is that how you wanna play? I ain't sayin' another word without my lawyer.",
            audioFile: "Voicelines/BurkeDefensiveEnd2",
            isChoiceNode: false
        );

        DefensiveEndEntry1 = new DialogueEntry(
            dialogueLine: "Uh... well... I assumed so...",
            audioFile: "Voicelines/BurkeDefensiveEnd1",
            isChoiceNode: false
        );

        InfoEntry1 = new DialogueEntry(
            dialogueLine: "I... I don't know. She was a good person. I can't think of anyone who would want to hurt her.",
            audioFile: "Voicelines/BurkeInfo", 
            isChoiceNode: false
        );

        EmpathyEntry2 = new DialogueEntry(
            dialogueLine: "Let's... just get on with this. How can I help you solve her murder?",
            audioFile: "Voicelines/BurkeEmpathy2",
            isChoiceNode: true,
            choice1Text: "Do you know anyone who had a grudge against her?",
            choice1Next: InfoEntry1,
            choice2Text: "I never mentioned she was murdered...",
            choice2Next: DefensiveEndEntry1
        );

        EmpathyEntry1 = new DialogueEntry(
            dialogueLine: "Well... It's hard. I appreciate you asking, but I-- I-- Sorry.",
            audioFile: "Voicelines/BurkeEmpathy1",
            isChoiceNode: false,
            nextEntry: EmpathyEntry2
        );

        StartEntry = new DialogueEntry(
            dialogueLine: "So, detective, w-what is it you wanted to talk about?",
            audioFile: "Voicelines/BurkeInterrogationStart",
            isChoiceNode: true,
            choice1Text: "How are you holdin' up?",
            choice1Next: EmpathyEntry1,
            choice2Text: "Why did you kill her? You sick bastard.",
            choice2Next: DefensiveEndEntry2
        );
    }
}
