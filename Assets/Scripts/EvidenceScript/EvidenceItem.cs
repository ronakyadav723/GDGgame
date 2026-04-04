using UnityEngine;

[CreateAssetMenu(fileName = "New Evidence", menuName = "Shimla Ledger/Evidence Item")]
public class EvidenceItem : ScriptableObject
{
    [Header("Identity")]
    public string evidenceID;
    public string displayName;
    public string targetSuspect; // Arjun, Bell, or Graves

    [Header("Classification")]
    public int suspicionScore;
    public string category; // Flavor, Major, or Critical

    [Header("Search Parameters")]
    public bool isAutoDiscovery; // True for Act 1 auto-clues
    public float searchTimeInSeconds; // e.g., 120 for 2 mins

    [Header("Officer Narration")]
    [TextArea(5, 10)]
    public string officerScript;
}
