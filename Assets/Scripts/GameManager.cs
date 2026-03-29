using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
