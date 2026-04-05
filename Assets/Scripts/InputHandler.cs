using UnityEngine;
using TMPro; // Make sure you are using TextMesh Pro!

public class InputHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    private string savedInput;

    // This function will be called when you press Enter
    public void ProcessInput(string input)
    {
        // 1. Store the input in your string variable
        savedInput = input;
        Debug.Log("Data Stored: " + savedInput);

        // 2. Reset the input field so it's empty for next time
        inputField.text = "";

        // 3. Optional: Refocus the field so the player can type again immediately
        inputField.ActivateInputField();
    }
}