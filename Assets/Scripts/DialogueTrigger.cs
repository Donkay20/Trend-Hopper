using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{  
    public static DialogueTrigger Instance;
    public Dialogue dialogue;

    void Start() {
        Instance = this;
    }

    public void TriggerDialogue() 
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
