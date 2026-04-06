using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textcomponent;
    public string[] lines;
    public float textspeed=0.01f;
    private int index;
    void Start()
    {
        textcomponent.text=string.Empty;
        StartDialogue();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (textcomponent.text == lines[index])
            {
                NextLine();

            }
            else
            {
                StopAllCoroutines();
                textcomponent.text=lines[index];
            }
        }
    }
    void StartDialogue()
    {
        index=0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {   yield return new WaitForSeconds(1.5f);
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

}
