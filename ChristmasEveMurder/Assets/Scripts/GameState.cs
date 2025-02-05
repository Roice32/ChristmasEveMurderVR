using System.Collections.Generic;

public static class GameState
{
    public static List<string> EvidenceFound { get; set; }
    public static int TOTAL_EVIDENCE_COUNT = 7;
    public static Dictionary<string, int> LiedCount { get; set; }

    static GameState()
    {
        EvidenceFound = new List<string>();
        LiedCount = new Dictionary<string, int>
        {
            { "Burke", 0 },
            { "Patsy", 0 },
            { "John", 0 }
        };
    }

    public static void AddEvidence(string evidence)
    {
        if (!EvidenceFound.Contains(evidence))
            EvidenceFound.Add(evidence);
    }

    public static void AddLie(string suspectName)
    {
        LiedCount[suspectName]++;
    }
}
