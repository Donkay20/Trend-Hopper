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

    private bool messaging;
    private string savedSentence;

    public string identity;             //determinant of what scene this is, saved to the global lastLevel variable
    [Space]
    public bool dressedUp;              //bool to determine if she needs to wear a custom outfit in this scene
    public GameObject appliedHair;
    public GameObject appliedTop;
    public GameObject appliedBottom;
    public GameObject appliedAccessory;
    [Space]
    public KeyCode nextButton;          //key for navigating through the dialogue
    [Space]
    public GameObject characterOnLeft;
    public GameObject characterOnRight; //the gameobjects control the images
    public GameObject background;
    public GameObject continuePrefab;   //the little icon thingy that shows when to continue
    public Animator animateLeft;
    public Animator animateRight;       //the animators control the fade-in/fade-out
    public Animator nameTag;            //the animator for the name tag text box
    [Space]
    public Sprite school_bg;
    public Sprite home_bg;
    [Space]
    public Sprite EMPTY;
    public Sprite Vicky; public Sprite Rocky; public Sprite Tyler;          //punk NPCs
    public Sprite Mackaylah; public Sprite Milgo; public Sprite meiLing;    //Y2K NPCs
    [Space]
    public Sprite mcDressedUpTemplate;              //I don't think I really need this tbh
    public Sprite[] hairCatalog = new Sprite[6];    //expand to 9 upon third set of clothing
    public Sprite[] topCatalog = new Sprite[6];
    public Sprite[] bottomCatalog = new Sprite[6];
    public Sprite[] accessoryCatalog = new Sprite[6];

    void Start()
    {   
        Progress.lastLevel = identity;
        continuePrefab = Instantiate(continuePrefab);
        continuePrefab.SetActive(false);
        status = "neutral";
        sentences = new Queue<string>();
        namesList = new Queue<string>();                        //initializes the queues. queues are FIFO
        animateLeft.SetBool("mainCharIsSpeaking", false);
        animateRight.SetBool("rightCharIsSpeaking", false);     //initializes both characters to not be speaking
        nameTag.SetBool("MCisSpeaking", true);

        if (dressedUp) {
            animateLeft.SetBool("mcIsDressed", true);
            status = "smug";
            characterOnLeft.GetComponent<Image>().sprite = mcDressedUpTemplate;
            appliedHair.GetComponent<Image>().sprite = hairCatalog[Progress.chosenHair];
            appliedTop.GetComponent<Image>().sprite = topCatalog[Progress.chosenTop];
            appliedBottom.GetComponent<Image>().sprite = bottomCatalog[Progress.chosenBottom];
            appliedAccessory.GetComponent<Image>().sprite = accessoryCatalog[Progress.chosenAccessory];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(nextButton)) { //if conversation started, cycle through the sentences. if not, begin the dialogue.
            switch(messaging) {
                case true:
                    dialogueText.text = savedSentence;
                    continuePrefab.SetActive(true);
                    StopAllCoroutines();
                    messaging = false;
                    break;
                case false:
                    switch (conversationStarted) {
                        case false:
                            DialogueTrigger.Instance.TriggerDialogue();
                            conversationStarted = true;
                            break;
                        case true:
                            DisplayNextSentence();
                            break;
                }
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
        continuePrefab.SetActive(false);
        if (sentences.Count == 0) {     //if there are no sentences left, end the dialogue
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();  //offload the next sentences and name in the queue
        savedSentence = sentence;
        string name = namesList.Dequeue();  

        if (name.Contains("Jassmea")) {                //this logic is under the assumption that the main character is always on the left.
            nameTag.SetBool("MCisSpeaking", true);
            animateLeft.SetBool("mainCharIsSpeaking", true);    
            animateRight.SetBool("rightCharIsSpeaking", false);
            if (dressedUp) {
                switch(name) {              //SMUG / CRYING / SURPRISED / WINKING
                    case "Jassmea_SMUG":
                        switch(status) {
                            case "smug": break;
                            case "crying":
                                animateLeft.SetBool("CryingToSmug", true);
                                animateLeft.SetBool("SmugToCrying", false); animateLeft.SetBool("SurprisedToCrying", false); animateLeft.SetBool("WinkingToCrying", false); 
                                nameText.text = "Jassmea"; changedPrior = true;
                                break;
                            case "surprised":
                                animateLeft.SetBool("SurprisedToSmug", true); 
                                animateLeft.SetBool("SmugToSurprised", false); animateLeft.SetBool("CryingToSurprised", false); animateLeft.SetBool("WinkingToSurprised", false); 
                                nameText.text = "Jassmea"; changedPrior = true;
                                break;
                            case "winking":
                                animateLeft.SetBool("WinkingToSmug", true);
                                animateLeft.SetBool("SmugToWinking", false); animateLeft.SetBool("CryingToWinking", false); animateLeft.SetBool("SurprisedToWinking", false); 
                                nameText.text = "Jassmea"; changedPrior = true;
                                break;
                        }
                        status = "smug";
                        break;
                    case "Jassmea_CRYING":
                        switch(status) {
                            case "crying": break;
                            case "smug":
                                animateLeft.SetBool("SmugToCrying", true);
                                animateLeft.SetBool("CryingToSmug", false); animateLeft.SetBool("SurprisedToSmug", false); animateLeft.SetBool("WinkingToSmug", false); 
                                nameText.text = "Jassmea"; changedPrior = true;
                                break;
                            case "surprised":
                                animateLeft.SetBool("SurprisedToCrying", true);
                                animateLeft.SetBool("CryingToSurprised", false); animateLeft.SetBool("SmugToSurprised", false); animateLeft.SetBool("WinkingToSurprised", false); 
                                nameText.text = "Jassmea"; changedPrior = true;
                                break;
                            case "winking":
                                animateLeft.SetBool("WinkingToCrying", true);
                                animateLeft.SetBool("CryingToWinking", false); animateLeft.SetBool("SmugToWinking", false); animateLeft.SetBool("SurprisedToWinking", false); 
                                nameText.text = "Jassmea"; changedPrior = true;
                                break;
                        }
                        status = "crying";
                        break;
                    case "Jassmea_SURPRISED":
                        switch(status) {
                            case "surprised": break;
                            case "smug":
                                animateLeft.SetBool("SmugToSurprised", true);
                                animateLeft.SetBool("SurprisedToSmug", false); animateLeft.SetBool("CryingToSmug", false); animateLeft.SetBool("WinkingToSmug", false); 
                                nameText.text = "Jassmea"; changedPrior = true;
                                break;
                            case "crying":
                                animateLeft.SetBool("CryingToSurprised", true);
                                animateLeft.SetBool("SurprisedToCrying", false); animateLeft.SetBool("SmugToCrying", false); animateLeft.SetBool("WinkingToCrying", false); 
                                nameText.text = "Jassmea"; changedPrior = true;
                                break;
                            case "winking":
                                animateLeft.SetBool("WinkingToSurprised", true);
                                animateLeft.SetBool("SurprisedToWinking", false); animateLeft.SetBool("SmugToWinking", false); animateLeft.SetBool("CryingToWinking", false); 
                                nameText.text = "Jassmea"; changedPrior = true;
                                break;
                        }
                        status = "surprised";
                        break;
                    case "Jassmea_WINKING":
                        switch(status) {
                            case "winking": break;
                            case "smug":
                                animateLeft.SetBool("SmugToWinking", true);
                                animateLeft.SetBool("WinkingToSmug", false); animateLeft.SetBool("CryingToSmug", false); animateLeft.SetBool("SurprisedToSmug", false);
                                nameText.text = "Jassmea"; changedPrior = true;
                                break;
                            case "crying":
                                animateLeft.SetBool("CryingToWinking", true);
                                animateLeft.SetBool("WinkingToCrying", false); animateLeft.SetBool("SmugToCrying", false); animateLeft.SetBool("SurprisedToCrying", false); 
                                nameText.text = "Jassmea"; changedPrior = true;
                                break;
                            case "surprised":
                                animateLeft.SetBool("SurprisedToWinking", true);
                                animateLeft.SetBool("WinkingToSurprised", false); animateLeft.SetBool("SmugToSurprised", false); animateLeft.SetBool("CryingToSurprised", false); 
                                nameText.text = "Jassmea"; changedPrior = true;
                                break;
                        }
                        status = "winking";
                        break;
                }
            } else {
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
            }
            
        } else if (name.Contains("TRANSITION")){                                //if the main char is speaking, highlight them. if not, make them out of focus. vice-versa for npcs.
            switch(name) {
                case "TRANSITION_SCHOOL":
                background.GetComponent<SpriteRenderer>().sprite = school_bg;
                nameText.text = "-"; changedPrior = true;
                break;
                case "TRANSITION_HOME":
                background.GetComponent<SpriteRenderer>().sprite = home_bg;
                nameText.text = "-"; changedPrior = true;
                break;
            }
        } else {
            switch(name) {
                case "EMPTY":
                characterOnRight.GetComponent<Image>().sprite = EMPTY;
                nameText.text = "-"; changedPrior = true;
                break;
                case "Vicky":
                characterOnRight.GetComponent<Image>().sprite = Vicky;
                break;
                case "Rocky":
                characterOnRight.GetComponent<Image>().sprite = Rocky;
                break;
                case "Tyler":
                characterOnRight.GetComponent<Image>().sprite = Tyler;
                break;
                case "Mackaylah":
                characterOnRight.GetComponent<Image>().sprite = Mackaylah;
                break;
                case "Milgo":
                characterOnRight.GetComponent<Image>().sprite = Milgo;
                break;
                case "Mei Ling":
                characterOnRight.GetComponent<Image>().sprite = meiLing;
                break;
            }
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
        messaging = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
        continuePrefab.SetActive(true);
        messaging = false;
    }

    void EndDialogue() {    //switches to different scenes depending on what dialogue is currently playing.
        switch(SceneManager.GetActiveScene().name) {
            //Day 1
            case "Dialogue_Day1":
                SceneManager.LoadScene("DressUpV2");
                break;
            case "Dialogue_Day1FailDressUp":
                SceneManager.LoadScene("StageSelect");
                break;
            case "Dialogue_Day1PassDressUp":
                SceneManager.LoadScene("RhythmGame");
                break;
            //Day 2
            case "Dialogue_Day2":
                SceneManager.LoadScene("DressUpV2");
                break;
            case "Dialogue_Day2FailDressUp":
                SceneManager.LoadScene("StageSelect");
                break;
            case "Dialogue_Day2PassDressUp":
                //load level 2;
                break;
            //Day 3
        }
    }
}
