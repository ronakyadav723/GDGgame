
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// No "Search for evidence" button — search fires automatically on enter.
// Busy state disables Back so player can't leave mid-request.

public class SearchHUD : MonoBehaviour
{
    [Header("Drag from Hierarchy")]
    public TextMeshProUGUI reportText;
    public TextMeshProUGUI locationLabel;
    public Button          backButton;

    void OnEnable()
    {
        APIManager.OnSearchResult += ShowResult;
        APIManager.OnError        += ShowError;
    }

    void OnDisable()
    {
        APIManager.OnSearchResult -= ShowResult;
        APIManager.OnError        -= ShowError;
    }

    void Start()
    {
        string loc = SceneData.LastLocationSearched;
        if (string.IsNullOrEmpty(loc)) loc = "Unknown location";
        if (locationLabel) locationLabel.text = loc;

        if (backButton) backButton.onClick.AddListener(() =>
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"));

        string target = SceneData.LastLocationSearched;
        if (!string.IsNullOrEmpty(target))
        {
            SetBusy(true);
            reportText.text = "Searching...";
            APIManager.Instance.SearchLocation(target);
        }
        else
        {
            reportText.text = "No location selected.";
        }
    }

    void ShowResult(string officerText)
    {
        SetBusy(false);
        reportText.text = officerText.Replace("\\n", "\n");
    }

    void ShowError(string error)
    {
        SetBusy(false);
        reportText.text = "Search failed. Please try again.";
        Debug.LogError("[SearchHUD] " + error);
    }

    void SetBusy(bool busy)
    {
        if (backButton) backButton.interactable = !busy;
    }
}