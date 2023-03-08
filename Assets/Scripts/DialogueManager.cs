using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMPro.TextMeshPro nameText;
    public TMPro.TextMeshPro dialogueText;

    private bool conversationStarted = false;
    private Queue<string> sentences;
    public KeyCode nextButton;

    void Start()
    {   
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (Input.GetKeyDown(nextButton)) {
            switch (conversationStarted) {
                case false:
                    DialogueTrigger.Instance.TriggerDialogue();
                    conversationStarted = true;
                    break;
                case true:
                    DisplayNextSentence();
                    break;
            }
        }
    }

    public void StartDialogue(Dialogue dialogue) 
    {
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue() {
        Debug.Log("End of conversation.");
    }
}
