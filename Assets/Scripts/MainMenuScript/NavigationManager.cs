using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NavigationManager : MonoBehaviour
{
    // Drag your "Continue" button into this slot in the Inspector
    public Button continueButton1;
    public Button continueButton2;


    public TMP_Dropdown dropdownSearch;

    public Vector2 bell = new Vector2(0, 0);
    public Vector2 graves = new Vector2(25, 0);
    public Vector2 arjun = new Vector2(50, 0);
    public Vector2 officer = new Vector2(75, 0);
    

    void Start()
    {
        // Ensure the button starts disabled if "None" is the default dropdown choice
        continueButton1.interactable = false;
        continueButton2.interactable = false;
        
    }

    public void GoToNPC(int locationIndex)
    {
        switch (locationIndex)
        {
            case 0: // "None" selected
                continueButton1.interactable = false;
                break;

            case 1: 
                SceneData.TargetCoordinates = bell; 
                SceneData.ActiveNpc="bell";
                continueButton1.interactable = true;
                break;

            case 2: 
                SceneData.TargetCoordinates = graves; 
                 SceneData.ActiveNpc="graves";
                continueButton1.interactable = true;
                break;

            case 3: 
                SceneData.TargetCoordinates = arjun; 
                 SceneData.ActiveNpc="arjun";
                continueButton1.interactable = true;
                break;

            case 4: 
                SceneData.TargetCoordinates = officer; 
                 SceneData.ActiveNpc="officer";
                continueButton1.interactable = true;
                break;

            default:
                continueButton1.interactable = false;
                break;
        }
    }
     
    public void GoToSearch(int locationIndex)
{
    if (locationIndex == 0 || dropdownSearch == null)
    {
        continueButton2.interactable = false;
        return;
    }

    if (locationIndex >= dropdownSearch.options.Count)
    {
        continueButton2.interactable = false;
        return;
    }

    string selectedLocation = dropdownSearch.options[locationIndex].text;
    SceneData.LastLocationSearched = selectedLocation;
    SceneData.LastSearchScript     = "";

    // Move camera to correct background for this location
    SceneData.TargetCoordinates = GetLocationCoordinates(selectedLocation);

    continueButton2.interactable = true;
}

Vector2 GetLocationCoordinates(string locationName)
{
    switch (locationName)
    {
        case "Thorne's study":  return new Vector2(0, 0);
        case "Storage room":    return new Vector2(25, 0);
        case "Arjun's office":  return new Vector2(50, 0);
        case "Reading hall":    return new Vector2(75, 0);
        case "Interrogation":   return new Vector2(100, 0); // add your coordinate
        case "Admin office":    return new Vector2(125, 0); // add your coordinate
        case "Pantry":          return new Vector2(150, 0); // add your coordinate
        default:                return Vector2.zero;
    }
}
    public void LoadInvestigationScene()
    {
        // Double check for safety before loading
            SceneManager.LoadScene("Investigation HUD"); 
        
    }
    public void LoadSearchScene()
    {
         // Double check for safety before loading
        if (continueButton2.interactable)
        {
            SceneManager.LoadScene("SearchEvidence"); 
        }
    }
    public void AccuseScene()
    {
        SceneManager.LoadScene("CaseFile");
    }
    public void CurrentStoryScene()
    {
        SceneManager.LoadScene("CurrentStory");
    }
    public void TalkTOOfficer()
    {
        //SceneManager.LoadScene("TalkOfficer");
        LoadInvestigationScene();
        SceneData.TargetCoordinates = officer; 
        SceneData.ActiveNpc="officer";
        Debug.Log("FUCK YOU");
    }
    public void Quit()
    {
        Application.Quit();
    }
}