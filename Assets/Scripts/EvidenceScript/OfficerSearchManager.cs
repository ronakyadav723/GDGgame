using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OfficerSearchManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject reportPanel;
    public TextMeshProUGUI reportText;
    public TextMeshProUGUI scoreText; 
    public Button searchButton;
    public Button nextButton; 

    [Header("Current Room Data")]
    // We now use a direct list so any room can pass its evidence here
    public List<EvidenceItem> currentRoomEvidence = new List<EvidenceItem>();

    private List<EvidenceItem> discoveryQueue = new List<EvidenceItem>();

    void Start()
    {
        reportPanel.SetActive(false);
        UpdateScoreUI();
        TriggerAutoItems();
    }

    public void SwitchLocation(List<EvidenceItem> newRoomEvidence)
    {
        discoveryQueue.Clear();
        reportPanel.SetActive(false); // Close any open reports from the old room
        
        currentRoomEvidence = newRoomEvidence; 
        
        TriggerAutoItems();
    }

    void TriggerAutoItems()
    {
        foreach (EvidenceItem item in currentRoomEvidence)
        {
            if (item.isAutoDiscovery) 
            {
                discoveryQueue.Add(item);
            }
        }

        if (discoveryQueue.Count > 0) ShowNextDiscovery();
    }

    public void ShowNextDiscovery()
    {
        if (discoveryQueue.Count > 0)
        {
            reportPanel.SetActive(true);
            EvidenceItem current = discoveryQueue[0];
            reportText.text = current.officerScript;
            
            AddScores(current);
            discoveryQueue.RemoveAt(0);

            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = 
                (discoveryQueue.Count == 0) ? "Close" : "Next Item";
        }
        else
        {
            reportPanel.SetActive(false);
        }
    }

    private void AddScores(EvidenceItem item)
    {
        SceneData.TotalSuspicionScore += item.suspicionScore;

        if (item.targetSuspect == "Arjun") SceneData.ArjunSuspicion += item.suspicionScore;
        else if (item.targetSuspect == "Bell") SceneData.BellSuspicion += item.suspicionScore;
        else if (item.targetSuspect == "Graves") SceneData.GravesSuspicion += item.suspicionScore;

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = "Total Suspicion: " + SceneData.TotalSuspicionScore;
    }

    public void OnSearchButtonClicked()
    {
        // Searches for the first item not yet found that isn't Auto-Discovery
        foreach (EvidenceItem item in currentRoomEvidence)
        {
            if (!item.isAutoDiscovery)
            {
                StartCoroutine(SearchRoutine(item));
                break; 
            }
        }
    }

    IEnumerator SearchRoutine(EvidenceItem item)
    {
        searchButton.interactable = false;
        yield return new WaitForSeconds(item.searchTimeInSeconds); 
        
        discoveryQueue.Add(item);
        ShowNextDiscovery();
        searchButton.interactable = true;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}