using System.Collections.Generic;

public static class GameState
{
    public static List<string> EvidenceFound { get; set; }
    public static int TOTAL_EVIDENCE_COUNT = 2; // Modify this when final evidence count is determined
    public static Dictionary<string, int> LiedCount { get; set; }

    static GameState()
    {
        EvidenceFound = new List<string> // Placeholder until actual evidence count is implemented
        {
            "RansomNote",
            "Body"
        };
        LiedCount = new Dictionary<string, int> // Initialize this with 0 in final project
        {
            { "Burke", 3 },
            { "Patsy", 1 },
            { "John", 0 }
        };
    }

    public static void AddEvidence(string evidence)
    {
        EvidenceFound.Add(evidence);
    }

    public static void AddLie(string suspectName)
    {
        LiedCount[suspectName]++;
    }
}
