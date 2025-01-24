using System.Collections.Generic;

public static class GameState
{
    public static List<string> EvidenceFound { get; set; }
    public static int LiedCount { get; set; }

    static GameState()
    {
        EvidenceFound = new List<string> // Placeholder until actual evidence count is implemented
        {
            "RansomNote",
            "Body"
        };
        LiedCount = 0;
    }

    public static void AddEvidence(string evidence)
    {
        EvidenceFound.Add(evidence);
    }

    public static void AddLie()
    {
        LiedCount++;
    }
}
