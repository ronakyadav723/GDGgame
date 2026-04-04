using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{
    // Drag your "Continue" button into this slot in the Inspector
    public Button continueButton1;
    public Button continueButton2;
    public Button continueButton3;

    public Vector2 bell = new Vector2(0, 0);
    public Vector2 graves = new Vector2(25, 0);
    public Vector2 arjun = new Vector2(50, 0);
    public Vector2 officer = new Vector2(75, 0);
    
    public Vector2 thornes_study = new Vector2(0, 0);
    public Vector2 storage_room = new Vector2(25, 0);
    public Vector2 arjun_office= new Vector2(50, 0);
    public Vector2 reading_hall = new Vector2(75, 0);
    void Start()
    {
        // Ensure the button starts disabled if "None" is the default dropdown choice
        continueButton1.interactable = false;
        continueButton2.interactable = false;
        continueButton3.interactable = false;
        
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
                continueButton1.interactable = true;
                break;

            case 2: 
                SceneData.TargetCoordinates = graves; 
                continueButton1.interactable = true;
                break;

            case 3: 
                SceneData.TargetCoordinates = arjun; 
                continueButton1.interactable = true;
                break;

            case 4: 
                SceneData.TargetCoordinates = officer; 
                continueButton1.interactable = true;
                break;

            default:
                continueButton1.interactable = false;
                break;
        }
    }
     public void GoToInterrogate(int locationIndex)
    {
        switch (locationIndex)
        {
            case 0: // "None" selected
                continueButton3.interactable = false;
                break;

            case 1: 
                SceneData.TargetCoordinates = bell; 
                continueButton3.interactable = true;
                break;

            case 2: 
                SceneData.TargetCoordinates = graves; 
                continueButton3.interactable = true;
                break;

            case 3: 
                SceneData.TargetCoordinates = arjun; 
                continueButton3.interactable = true;
                break;

            case 4: 
                SceneData.TargetCoordinates = officer; 
                continueButton3.interactable = true;
                break;

            default:
                continueButton3.interactable = false;
                break;
        }
    }
    public void GoToSearch(int locationIndex)
    {
        switch (locationIndex)
        {
            case 0: // "None" selected
                continueButton2.interactable = false;
                break;

            case 1: 
                SceneData.TargetCoordinates = thornes_study; 
                continueButton2.interactable = true;
                break;

            case 2: 
                SceneData.TargetCoordinates = storage_room; 
                continueButton2.interactable = true;
                break;

            case 3: 
                SceneData.TargetCoordinates = arjun_office; 
                continueButton2.interactable = true;
                break;

            case 4: 
                SceneData.TargetCoordinates = reading_hall; 
                continueButton2.interactable = true;
                break;

            default:
                continueButton2.interactable = false;
                break;
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
        SceneManager.LoadScene("TalkOfficer");
    }
    public void Quit()
    {
        Application.Quit();
    }
}