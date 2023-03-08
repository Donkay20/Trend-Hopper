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
    private Queue<string> namesList;
    public KeyCode nextButton;

    public GameObject dialogueToTrigger;

    void Start()
    {   
        sentences = new Queue<string>();
        namesList = new Queue<string>();
    }

    void Update()
    {
        if (Input.GetKeyDown(nextButton)) {
            switch (conversationStarted) {
                case false:
                    DialogueTrigger.Instance.TriggerDialogue();
                    //dialogueToTrigger.GetComponent<DialogueTrigger>().Instance.TriggerDialogue();
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
        sentences.Clear();
        namesList.Clear();

        foreach (string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence);
        }

        foreach (string name in dialogue.names) 
        {
            namesList.Enqueue(name);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        string name = namesList.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        nameText.text = name;
    }

    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }

    void EndDialogue() {
        Debug.Log("End of conversation.");
    }
}
