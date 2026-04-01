using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    
   public LevelLoader levelLoader; 

    public void StartGame()
    {
        int firstLevelIndex = 1; // Usually the scene after the Main Menu
        levelLoader.StartTransition(firstLevelIndex);
    }
    public void Story()
    {
        SceneManager.LoadScene("Story");
    }
    public void Quit()
    {
        Application.Quit();
    }
    
}
