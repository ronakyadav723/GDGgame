using UnityEngine;
using TMPro;
using UnityEngine.UI;

// ─────────────────────────────────────────────────────────────
//  InvestigationHUD.cs  — NEW script
//  Attach to: Panel (in your Investigation HUD scene)
//
//  This replaces OfficerSearchManager for the NPC dialogue scene.
//  It wires your existing PlayerInput + ASK button to the API.
//
//  Your existing Dialogue.cs typewriter still works — this script
//  feeds it the API reply text instead of hardcoded lines[].
// ─────────────────────────────────────────────────────────────

public class InvestigationHUD : MonoBehaviour
{
    [Header("Drag from Hierarchy")]
    public TextMeshProUGUI dialogueText;   // Text (TMP) inside Panel
    public TMP_InputField  playerInput;    // PlayerInput → TMP_InputField
    public Button          askButton;      // ASK button
    // public Button          continueButton; // CONTINUE button (goes back to MainMenu)
    //public TextMeshProUGUI npcNameLabel;   // optional — shows who you're talking to

   // [Header("Loading feedback")]
   // public GameObject loadingIndicator;    // optional spinner/text

    // Which NPC is active — set by NavigationManager via SceneData
    string _activeNpc = "officer";

    void OnEnable()
    {
        APIManager.OnNpcReply += ShowReply;
        APIManager.OnError    += ShowError;
    }

    void OnDisable()
    {
        APIManager.OnNpcReply -= ShowReply;
        APIManager.OnError    -= ShowError;
    }

    void Start()
    {
        // Read which NPC was selected on the MainMenu screen
        _activeNpc = SceneData.ActiveNpc;

        //if (npcNameLabel)
        //   npcNameLabel.text = Capitalize(_activeNpc);

        // Show last reply if re-entering the scene
        if (!string.IsNullOrEmpty(SceneData.LastNpcReply))
            dialogueText.text = SceneData.LastNpcReply;
        else
            dialogueText.text = $"You are speaking with {Capitalize(_activeNpc)}.";

        // Wire ASK button
        askButton.onClick.AddListener(OnAskClicked);

        // CONTINUE just goes back to MainMenu (your existing NavigationManager
        // already handles this — no change needed)
    }

    // ── Called when player clicks ASK ────────────────────────

    public void OnAskClicked()
    {
        string text = playerInput.text.Trim();
        if (string.IsNullOrEmpty(text)) return;

        SetBusy(true);
        playerInput.text = "";

        APIManager.Instance.TalkToNPC(_activeNpc, text);
    }

    // ── Called when API returns ───────────────────────────────

    void ShowReply(string reply)
    {
        SetBusy(false);
        dialogueText.text = reply;
    

        // If you want the typewriter effect, call your Dialogue.cs here:
         GetComponent<Dialogue>().StartWithText(reply);
        // (requires adding a public StartWithText method to Dialogue.cs — see note)
    }

    void ShowError(string error)
    {
        SetBusy(false);
        dialogueText.text = "Connection error. Please try again.";
        Debug.LogError("[InvestigationHUD] " + error);
    }

    // ── Helpers ───────────────────────────────────────────────

    void SetBusy(bool busy)
    {
        askButton.interactable      = !busy;
        playerInput.interactable    = !busy;
        //if (loadingIndicator) loadingIndicator.SetActive(busy);
    }

    static string Capitalize(string s) =>
        string.IsNullOrEmpty(s) ? s : char.ToUpper(s[0]) + s[1..];
}

// ─────────────────────────────────────────────────────────────
//  OPTIONAL: Add this method to your existing Dialogue.cs to
//  enable the typewriter effect with API text:
//
//  public void StartWithText(string fullText)
//  {
//      lines = new string[] { fullText };
//      index = 0;
//      textcomponent.text = string.Empty;
//      StopAllCoroutines();
//      StartCoroutine(TypeLine());
//  }
// ─────────────────────────────────────────────────────────────
