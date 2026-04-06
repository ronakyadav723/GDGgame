using UnityEngine;
using TMPro;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using Unity.Loading;


public class Dialogue : MonoBehaviour
{   public Transform cameratransform;
    public TextMeshProUGUI textcomponent;
    public string[] lines;
    public float textspeed=0.01f;
    private int index;
    private int i=0;
    public Vector2 goodboycamera;
   public Vector2 scene2_1= new Vector2(0, 0);
    public Vector2 scene2_2 = new Vector2(25, 0);
    public Vector2 scene2_3 = new Vector2(50, 0);
    public Vector2 scene2_4 = new Vector2(75, 0);
    public Vector2 scene2_5=new Vector2(100,0);
    void Start()
    {
        textcomponent.text=string.Empty;
        StartDialogue();
    }
    public void NextButton()
    {
        i++;
        switch (i)
        {
            
            case 0:
           goodboycamera =scene2_1;
            break;
            case 1:
            goodboycamera=scene2_2;
            break;
            case 2:
            goodboycamera=scene2_3;
            break;
            case 3:
           goodboycamera=scene2_4;
            break;
            case 4:
            goodboycamera=scene2_5;
            break;
            default :
            Debug.Log("default case.");
            break;
        }
        cameratransform.position = new Vector3(goodboycamera.x, goodboycamera.y, -10f);
            if (textcomponent.text == lines[index])
            {
                NextLine();
            
            }
            else
            {
                StopAllCoroutines();
                textcomponent.text=lines[index];
            }
        if (i > 5)
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        
    }
    void StartDialogue()
    {
        index=0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {   yield return new WaitForSeconds(1.2f);
        foreach( char c in lines[index].ToCharArray())
        {
            textcomponent.text+=c;
            yield return new WaitForSeconds(textspeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        { 
            index++;
            textcomponent.text=string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            
        }
    }
    public void StartWithText(string fullText)
 {
     lines = new string[] { fullText };
     index = 0;
     textcomponent.text = string.Empty;
     StopAllCoroutines();
    StartCoroutine(TypeLine());
 }

 public void SkipScene()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
