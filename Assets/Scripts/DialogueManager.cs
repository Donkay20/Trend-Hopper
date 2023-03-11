using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TMPro.TextMeshPro nameText;
    public TMPro.TextMeshPro dialogueText;      //altering the textfields in-game.

    private bool conversationStarted = false;   //boolean trigger to see if conversation has started.
    private bool changedPrior = false;          //checks to see if name was changed earlier in the method.

    private Queue<string> sentences;
    private Queue<string> namesList;    //one queue for the text, another queue for the names of who's speaking

    private string status;              //main character(left side)'s emotional status

    public KeyCode nextButton;          //key for navigating through the dialogue

    public GameObject characterOnLeft;
    public GameObject characterOnRight; //the gameobjects control the images
    public Animator animateLeft;
    public Animator animateRight;       //the animators control the fade-in/fade-out
    public Animator nameTag;            //the animator for the name tag text box

    void Start()
    {   
        //Time.timeScale = 0.3f;
        status = "neutral";
        sentences = new Queue<string>();
        namesList = new Queue<string>();                        //initializes the queues. queues are FIFO
        animateLeft.SetBool("mainCharIsSpeaking", false);
        animateRight.SetBool("rightCharIsSpeaking", false);     //initializes both characters to not be speaking
        nameTag.SetBool("MCisSpeaking", true);
    }

    void Update()
    {
        if (Input.GetKeyDown(nextButton)) { //if conversation started, cycle through the sentences. if not, begin the dialogue.
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
        sentences.Clear();  //clears any existing dialogue and names
        namesList.Clear();

        foreach (string sentence in dialogue.sentences) //shoves all sentences in the queue
        {
            sentences.Enqueue(sentence);
        }

        foreach (string name in dialogue.names)         //shoves all names in the queue
        {
            namesList.Enqueue(name);
        }

        DisplayNextSentence();  //it only does this once. gets out of the startdialogue by initializing the dialogue stream.
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {     //if there are no sentences left, end the dialogue
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();  //offload the next sentences and name in the queue
        string name = namesList.Dequeue();      

        if (name.Contains("Jassmea")) {                //this logic is under the assumption that the main character is always on the left.
            nameTag.SetBool("MCisSpeaking", true);
            animateLeft.SetBool("mainCharIsSpeaking", true);    
            animateRight.SetBool("rightCharIsSpeaking", false);
            switch(name) {              //tldr: checks the mc for an emotion, if so, parses it out and applies the correct facial expression via animation. then updates the status.
                case "Jassmea_MAD":
                    switch(status) {
                        case "mad": break;
                        case "neutral":
                            animateLeft.SetBool("NeutralToMad", true);
                            animateLeft.SetBool("MadToNeutral", false); animateLeft.SetBool("HappyToNeutral", false);
                            nameText.text = "Jassmea"; changedPrior = true;
                            status = "mad";
                            break;
                        case "happy":
                            animateLeft.SetBool("HappyToMad", true);
                            animateLeft.SetBool("NeutralToHappy", false); animateLeft.SetBool("MadToHappy", false);
                            nameText.text = "Jassmea"; changedPrior = true;
                            status = "mad";
                            break;
                    }
                    break;
                case "Jassmea_NEUTRAL":
                    switch(status) {
                        case "neutral": break;
                        case "happy":
                            animateLeft.SetBool("HappyToNeutral", true);
                            animateLeft.SetBool("NeutralToHappy", false); animateLeft.SetBool("MadToHappy", false);
                            nameText.text = "Jassmea"; changedPrior = true;
                            status = "neutral";
                            break;
                        case "mad":
                            animateLeft.SetBool("MadToNeutral", true);
                            animateLeft.SetBool("NeutralToMad", false); animateLeft.SetBool("HappyToMad", false);
                            nameText.text = "Jassmea"; changedPrior = true;
                            status = "neutral";
                            break;
                    }
                    break;
                case "Jassmea_HAPPY":
                    switch(status) {
                        case "happy": break;
                        case "neutral":
                        animateLeft.SetBool("NeutralToHappy", true);
                        animateLeft.SetBool("HappyToNeutral", false); animateLeft.SetBool("MadToNeutral", false);
                        nameText.text = "Jassmea"; changedPrior = true;
                        status = "happy";
                        break;
                        case "mad":
                        animateLeft.SetBool("MadToHappy", true);
                        animateLeft.SetBool("HappyToMad", false); animateLeft.SetBool("NeutralToMad", false);
                        nameText.text = "Jassmea"; changedPrior = true;
                        status = "happy";
                        break;
                    }
                    break;
            }
        } else {                                //if the main char is speaking, highlight them. if not, make them out of focus. vice-versa for npcs.
            animateLeft.SetBool("mainCharIsSpeaking", false);   
            animateRight.SetBool("rightCharIsSpeaking", true);
            nameTag.SetBool("MCisSpeaking", false);
        }

        StopAllCoroutines();    //<- when the next sentence is loaded when the next one isn't finished yet.
        StartCoroutine(TypeSentence(sentence)); //to gradually load in the next sentence, letter by letter, check the TypeSentence IEnumerator.
        if (!changedPrior) {
                nameText.text = name;
        }

        changedPrior = false;
    }

    IEnumerator TypeSentence (string sentence) { //this is the thingy that makes the letters come one by one
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }

    void EndDialogue() {
        //todo, for scene transitions.
        Debug.Log("End of conversation.");
    }
}
