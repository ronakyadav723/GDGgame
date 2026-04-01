using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator anim;
    public float transitionTime = 1f;

    void Awake()
    {
        // Ensure it's off when the Main Menu first loads [cite: 183]
        if(anim != null) anim.enabled = false;
    }

    public void StartTransition(int levelIndex)
    {
        // 1. Enable the animator ONLY when the transition begins
        anim.enabled = true; 
        
        StartCoroutine(LoadLevel(levelIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetBool("LoadStart",true);
        
        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(levelIndex);
    }
}