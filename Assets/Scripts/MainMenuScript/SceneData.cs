
using UnityEngine;
using System.Collections.Generic;

public static class SceneData
{
    // ── Original fields ───────────────────────────────────────
    public static Vector2 TargetCoordinates;
    public static int TotalSuspicionScore = 0;
    public static int ArjunSuspicion      = 0;
    public static int BellSuspicion       = 0;
    public static int GravesSuspicion     = 0;

    // ── API session ───────────────────────────────────────────
    public static string ThreadId         = "";
    public static bool   GameStarted      = false;

    // ── NPC dialogue ──────────────────────────────────────────
    public static string LastNpcReply     = "";
    public static string LastNpcName      = "";
    public static string ActiveNpc        = "officer";

    // ── Search ────────────────────────────────────────────────
    public static string LastSearchScript      = "";
    public static string LastLocationSearched  = "";

    // ── Locations — updated after every API call ──────────────
    // Contains only the names of currently UNLOCKED locations
    public static List<string> UnlockedLocations = new List<string>();

    // ── Accusation ────────────────────────────────────────────
    public static bool   AccusationAvailable = false;
    public static string AccusationResult    = "";
    public static bool   AccusationCorrect   = false;
}