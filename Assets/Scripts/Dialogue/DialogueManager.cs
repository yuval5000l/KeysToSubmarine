using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager: MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    private Queue<string> dialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogue = new Queue<string>();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(Dialogue diag)
    {
        nameText.text = diag.name;
        foreach( string sentence in diag.sentences)
        {
            dialogue.Enqueue(sentence);
            Debug.Log(sentence);
        }
    }

    public void DisplayNextSentence()
    {
        if(dialogue.Count == 0)
        {
            return;
        }
        dialogueText.text = dialogue.Dequeue();
        
    }
}
