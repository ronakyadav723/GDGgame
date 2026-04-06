

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public static APIManager Instance { get; private set; }

    [Header("Backend URL (no trailing slash)")]
    public string baseUrl = "https://gameaitesting-production.up.railway.app/docs#/";
    public float timeoutSeconds = 20f;

    // Events
    public static event Action<string>       OnNpcReply;
    public static event Action<string>       OnSearchResult;
    public static event Action<bool>         OnAccusationDone;
    public static event Action               OnGameStarted;
    public static event Action               OnLocationsUpdated;  // fires after every call
    public static event Action<string>       OnError;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ── Public API ────────────────────────────────────────────

    public void StartGame()
    {
        StartCoroutine(GetRequest("/start", OnStartSuccess));
    }

    public void TalkToNPC(string npcName, string playerText)
    {
        Debug.Log($"[APIManager] Sending to /talk — npc:{npcName} input:{playerText}");
        var body = JsonUtility.ToJson(new TalkBody
        {
            npc_name     = npcName.ToLower().Trim(),
            player_input = playerText.Trim()
        });
        StartCoroutine(PostRequest("/talk", body, OnTalkSuccess));
    }

    public void SearchLocation(string locationName)
    {
        var body = JsonUtility.ToJson(new SearchBody { location = locationName });
        StartCoroutine(PostRequest("/search", body, OnSearchSuccess));
    }

    public void Accuse(string suspect)
    {
        var body = JsonUtility.ToJson(new AccuseBody { suspect = suspect.ToLower() });
        StartCoroutine(PostRequest("/accuse", body, OnAccuseSuccess));
    }

    // ── Response handlers ─────────────────────────────────────

    void OnStartSuccess(string json)
    {
        var r = ParseResponse(json);
        SceneData.ThreadId    = r.thread_id ?? "";
        SceneData.GameStarted = true;
        ApplySharedState(r, json);
        OnGameStarted?.Invoke();
        OnLocationsUpdated?.Invoke();
        Debug.Log("[APIManager] Game started. Thread: " + SceneData.ThreadId);
    }

    void OnTalkSuccess(string json)
    {
        var r = ParseResponse(json);
        SceneData.LastNpcReply = r.npc_reply ?? r.officer_output ?? "";
        SceneData.LastNpcName  = SceneData.ActiveNpc;
        ApplySharedState(r, json);
        OnNpcReply?.Invoke(SceneData.LastNpcReply);
        OnLocationsUpdated?.Invoke();
    }

    void OnSearchSuccess(string json)
    {
        var r = ParseResponse(json);
        SceneData.LastSearchScript     = r.search_result ?? r.script ?? r.officer_output ?? "";
        SceneData.LastLocationSearched = r.location_searched ?? "";
        ApplySharedState(r, json);
        OnSearchResult?.Invoke(SceneData.LastSearchScript);
        OnLocationsUpdated?.Invoke();
    }

    void OnAccuseSuccess(string json)
    {
        var r = ParseResponse(json);
        SceneData.AccusationResult  = r.result ?? "";
        SceneData.AccusationCorrect = r.correct;
        ApplySharedState(r, json);
        OnAccusationDone?.Invoke(r.correct);
        OnLocationsUpdated?.Invoke();
    }

    // ── Shared state update ───────────────────────────────────

    void ApplySharedState(APIResponse r, string rawJson)
    {
        // Suspicion scores
        SceneData.ArjunSuspicion  = (int)r.scores_arjun;
        SceneData.GravesSuspicion = (int)r.scores_graves;
        SceneData.BellSuspicion   = (int)r.scores_bell;
        SceneData.TotalSuspicionScore = SceneData.ArjunSuspicion +
                                        SceneData.GravesSuspicion +
                                        SceneData.BellSuspicion;

        SceneData.AccusationAvailable = r.accusation_available;

        // Parse locations_unlocked manually — JsonUtility can't handle Dictionary
        SceneData.UnlockedLocations = ParseLocations(rawJson);
    }

    // ── Custom location parser ────────────────────────────────
    // Parses: "locations_unlocked":{"Thorne's study":true,"Storage room":false,...}

    static List<string> ParseLocations(string json)
    {
        var unlocked = new List<string>();

        int start = json.IndexOf("\"locations_unlocked\"");
        if (start < 0) return unlocked;

        int brace = json.IndexOf('{', start);
        int end   = json.IndexOf('}', brace);
        if (brace < 0 || end < 0) return unlocked;

        string block = json.Substring(brace + 1, end - brace - 1);
        // block looks like: "Thorne's study":true,"Arjun's office":true,...

        string[] pairs = block.Split(',');
        foreach (string pair in pairs)
        {
            int colon = pair.LastIndexOf(':');
            if (colon < 0) continue;

            string key   = pair.Substring(0, colon).Trim().Trim('"');
            string value = pair.Substring(colon + 1).Trim().ToLower();

            if (value == "true")
                unlocked.Add(key);
        }

        return unlocked;
    }

    // ── HTTP helpers ──────────────────────────────────────────

    IEnumerator GetRequest(string path, Action<string> onSuccess)
    {
        using var req = UnityWebRequest.Get(baseUrl + path);
        req.timeout = (int)timeoutSeconds;
        yield return req.SendWebRequest();
        HandleResult(req, onSuccess);
    }

    IEnumerator PostRequest(string path, string json, Action<string> onSuccess)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        using var req = new UnityWebRequest(baseUrl + path, "POST");
        req.uploadHandler   = new UploadHandlerRaw(bytes);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        req.timeout = (int)timeoutSeconds;
        yield return req.SendWebRequest();
        HandleResult(req, onSuccess);
    }

    void HandleResult(UnityWebRequest req, Action<string> onSuccess)
    {
        if (req.result != UnityWebRequest.Result.Success)
        {
            string err = $"HTTP {req.responseCode}: {req.error}";
            Debug.LogError("[APIManager] " + err);
            OnError?.Invoke(err);
            return;
        }
        string raw = req.downloadHandler.text;
        Debug.Log("[APIManager] Response: " + raw);
        onSuccess(raw);
    }

    // ── JSON structs ──────────────────────────────────────────

    APIResponse ParseResponse(string json) =>
        JsonUtility.FromJson<APIResponse>(json);

    [Serializable] class TalkBody   { public string npc_name; public string player_input; }
    [Serializable] class SearchBody { public string location; }
    [Serializable] class AccuseBody { public string suspect; }

    [Serializable]
    class APIResponse
    {
        public string npc_reply;
        public string officer_output;
        public string script;
        public string search_result;
        public string location_searched;
        public string result;
        public bool   correct;
        public string thread_id;
        public bool   accusation_available;
        public string error;
        public string message;

        // Scores — parsed via nested class
        public ScoresBlock scores;
        public float total_suspicion;

        // Convenience getters used by ApplySharedState
        public float scores_arjun  => scores?.arjun  ?? 0f;
        public float scores_graves => scores?.graves ?? 0f;
        public float scores_bell   => scores?.bell   ?? 0f;

        [Serializable]
        public class ScoresBlock
        {
            public float arjun;
            public float bell;
            public float graves;
            public float officer;
        }
    }
}