using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{  
      public static GameManager Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // kill duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // persist across scenes
    }
    //logic script starts here
  // public LevelLoader levelLoader; 

    public void StartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Story()
    {
        SceneManager.LoadScene("Story");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MoveToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
