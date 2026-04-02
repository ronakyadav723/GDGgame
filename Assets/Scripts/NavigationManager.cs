using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapNavigation : MonoBehaviour
{
    // Drag your "Continue" button into this slot in the Inspector
    public Button continueButton;

    public Vector2 bell = new Vector2(0, 0);
    public Vector2 graves = new Vector2(25, 0);
    public Vector2 arjun = new Vector2(50, 0);
    public Vector2 officer = new Vector2(75, 0);

    void Start()
    {
        // Ensure the button starts disabled if "None" is the default dropdown choice
        continueButton.interactable = false;
    }

    public void GoToBackground(int locationIndex)
    {
        switch (locationIndex)
        {
            case 0: // "None" selected
                continueButton.interactable = false;
                break;

            case 1: 
                SceneData.TargetCoordinates = bell; 
                continueButton.interactable = true;
                break;

            case 2: 
                SceneData.TargetCoordinates = graves; 
                continueButton.interactable = true;
                break;

            case 3: 
                SceneData.TargetCoordinates = arjun; 
                continueButton.interactable = true;
                break;

            case 4: 
                SceneData.TargetCoordinates = officer; 
                continueButton.interactable = true;
                break;

            default:
                continueButton.interactable = false;
                break;
        }
    }

    public void LoadInvestigationScene()
    {
        // Double check for safety before loading
        if (continueButton.interactable)
        {
            SceneManager.LoadScene("Investigation HUD"); 
        }
    }
}