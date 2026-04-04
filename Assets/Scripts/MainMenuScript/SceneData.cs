using UnityEngine;

public static class SceneData
{
    // This variable stays in memory between scenes
    public static Vector2 TargetCoordinates;
    // The "Master" score for unlocking rooms
    public static int TotalSuspicionScore = 0;

    // Individual buckets for the NPCs
    public static int ArjunSuspicion = 0;
    public static int BellSuspicion = 0;
    public static int GravesSuspicion = 0;
}
