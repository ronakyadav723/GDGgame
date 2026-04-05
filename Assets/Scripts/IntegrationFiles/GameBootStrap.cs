using UnityEngine;

// ─────────────────────────────────────────────────────────────
//  GameBootstrap.cs  — NEW script
//  Attach to: your existing GameManager GameObject
//  (the one that already has GameManager.cs on it)
//
//  This calls APIManager.StartGame() once when the app launches.
//  It does NOT conflict with your existing GameManager.cs.
// ─────────────────────────────────────────────────────────────

public class GameBootstrap : MonoBehaviour
{
    void Start()
    {
        if (!SceneData.GameStarted)
        {
            Debug.Log("[GameBootstrap] Calling /start on backend...");
            APIManager.Instance.StartGame();
        }
    }
}
