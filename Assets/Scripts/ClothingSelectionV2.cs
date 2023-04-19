using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothingSelectionV2 : MonoBehaviour
{
    public static ClothingSelectionV2 Instance;
    private Color unfocus = Color.grey;
    private Color focus = Color.white;

    public KeyCode inputUp;
    public KeyCode inputDown;
    public KeyCode inputRight;
    public KeyCode inputLeft;
    public KeyCode inputSelect;     //inputs
    [Space]
    public Animator upTrigger;
    public Animator hairUI;
    public Animator topUI;
    public Animator bottomUI;
    public Animator shoeUI;
    public Animator accessoryUI; 
    public Animator checkmarkUI;
    public Animator downTrigger;    //for the UI tab on the left-hand side
    public Animator hairBonusDisplay;
    public Animator topBonusDisplay;
    public Animator bottomBonusDisplay;
    public Animator shoeBonusDisplay;
    public Animator accessoryBonusDisplay;
    [Space]
    public Sprite meterTitlePunk; public Sprite meterTitleY2K; public Sprite meterTitleDisco; public Sprite empty;
    public GameObject meterDesc;
    public ParticleSystem fullBar;
    public Animator meter;
    public Animator transition;
    public Animator sparkleMovement;
    [Space]
    public GameObject clothingSelector;
    public Animator leftTrigger;
    public Animator rightTrigger;   //for the moving selector
    [Space]
    public Animator[] hairAnimation = new Animator[9];
    public Animator[] topAnimation = new Animator[9];
    public Animator[] bottomAnimation = new Animator[9];
    public Animator[] shoeAnimation = new Animator[9];
    public Animator[] accessoryAnimation = new Animator[9];         //these will control the animations that go in and out
    [Space]
    public GameObject[] hairRows = new GameObject[3];
    public GameObject[] topRows = new GameObject[3];
    public GameObject[] bottomRows = new GameObject[3];
    public GameObject[] shoeRows = new GameObject[3];
    public GameObject[] accessoryRows = new GameObject[3];          //these should hold the rows of closet displays (right now 2, max 3 on completion.)
    [Space]
    public GameObject[] hairRowsInner = new GameObject[9];
    public GameObject[] topRowsInner = new GameObject[9];
    public GameObject[] bottomRowsInner = new GameObject[9];
    public GameObject[] shoeRowsInner = new GameObject[9];
    public GameObject[] accessoryRowsInner = new GameObject[9];     //this is for the clothing stuff when it gets darker when you move off of it or focus it
    [Space]
    public GameObject assignedHair;
    public GameObject assignedTop;
    public GameObject assignedBottom;
    public GameObject assignedShoe;
    public GameObject assignedAccessory;                             //these are the clothes that will show on the character.
    [Space]
    public Sprite[] appliedHairCatalog = new Sprite[9];
    public Sprite[] appliedTopCatalog = new Sprite[9];
    public Sprite[] appliedBottomCatalog = new Sprite[9];
    public Sprite[] appliedShoeCatalog = new Sprite[9];
    public Sprite[] appliedAccessoryCatalog = new Sprite[9];        //these are the clothes in storage that can be applied to the character. max is 6, end goal should be 9.
    [Space]
    public Animator bonusBox;
    public TMPro.TextMeshPro multiplier;
    public TMPro.TextMeshPro leniencyP;
    public TMPro.TextMeshPro coolness;
    [Space]
    public AudioSource shiftClothing;
    public AudioSource shiftCategory;
    public AudioSource confirm;

    private int hairRow;                                            //row variable series should be 1 or 2 (1-3 later on) to determine what row to show.
    private int topRow;                                             //hover variable series should determine what is being highlighted. goes from 0-5 (later should be 0-8)
    private int bottomRow;      
    private int shoeRow;        
    private int accessoryRow;   

    private string selectedCategory;                                //hair, top, bottom, shoe, accessory, check
    private int selectedHair;                                       //numbers to determine which outfit was chosen. plan is for 0-2 to be punk, 3-5 to be y2k, and 6-8 to be disco
    private int selectedTop;                                        //only goes from 0-5 for now, as disco isn't in yet.
    private int selectedBottom;
    private int selectedShoe;
    private int selectedAccessory;

    private bool allOK;                                             //bools to check to see if each clothing category has been selected at least once; all OK if all of the have been checked once

    private int meterCounter;                                       //(range: 0-5) check to see if the clothing is matching the theme of the level
    private bool hairOK; private bool topOK; private bool bottomOK; private bool shoeOK; private bool accessoryOK;   //+1 to matching clothing, +0 if not.

    void Start() {
        if (Progress.lastLevel == null) {       //This should NEVER trigger unless starting from this scene in the editor. If this happens please tell me immediately!!!
            Progress.lastLevel = "dayOneIntro";
        }

        switch(Progress.lastLevel) {            //Dependent on the last level, changes the display of the required clothing
            case "dayOneIntro":
                meterDesc.GetComponent<SpriteRenderer>().sprite = meterTitlePunk;
                break;
            case "dayTwoIntro":
                meterDesc.GetComponent<SpriteRenderer>().sprite = meterTitleY2K;
                break;
            case "dayThreeIntro":
                meterDesc.GetComponent<SpriteRenderer>().sprite = meterTitleDisco;
                break;
            case "EndingIntro":
                meterDesc.GetComponent<SpriteRenderer>().sprite = empty;
                break;
        }
        Instance = this;            //initialize the instance
        selectedCategory = "hair";  //set the default position to hair.
        meterCounter = 0;           //initialize the meter to 0, unsure if needed, probably not
        UpdateCategory(selectedCategory);
        selectedHair = 1; selectedTop = 1; selectedBottom = 1; selectedShoe = 1; selectedAccessory = 1;
        UpdateRow();                //initialize the row, dependent on the positions decided above.
        selectedHair = -1; selectedTop = -1; selectedBottom = -1; selectedShoe = -1; selectedAccessory = -1; //set them to -1 to initialize so nothing is "chosen"
    }

    void Update() {
        if(selectedHair != -1 && selectedTop != -1 && selectedBottom != -1 && selectedShoe != -1 && selectedAccessory != -1) { //if all clothing has been chosen at least once
            allOK = true;
            checkmarkUI.SetBool("checkUnlocked", true);
        }

        if(Input.GetKeyDown(inputUp)) {                 //up trigger.
            shiftCategory.Play();
            upTrigger.SetTrigger("upTrigger");
            switch(selectedCategory) {
                case "hair":
                    sparkleMovement.SetBool("hair", false);
                    hairUI.SetBool("hairIsSelected", false);
                    if (allOK) {
                        sparkleMovement.SetBool("check", true);
                        selectedCategory = "check";
                        updateBonusBoxDisplay();
                        bonusBox.SetBool("appear", true);
                        checkmarkUI.SetBool("checkSelected", true);
                    } else {
                        sparkleMovement.SetBool("accessory", true);
                        selectedCategory = "accessory";
                        accessoryUI.SetBool("accIsSelected", true);
                    }
                    break;
                case "top":
                    sparkleMovement.SetBool("top", false); sparkleMovement.SetBool("hair", true);
                    selectedCategory = "hair";
                    topUI.SetBool("topIsSelected", false); hairUI.SetBool("hairIsSelected", true);
                    break;
                case "bottom":
                    sparkleMovement.SetBool("bottom", false); sparkleMovement.SetBool("top", true);
                    selectedCategory = "top";
                    bottomUI.SetBool("bottomIsSelected", false); topUI.SetBool("topIsSelected", true);
                    break;
                case "shoe":
                    sparkleMovement.SetBool("shoe", false); sparkleMovement.SetBool("bottom", true);
                    selectedCategory = "bottom";
                    shoeUI.SetBool("shoeIsSelected", false); bottomUI.SetBool("bottomIsSelected", true);
                    break;
                case "accessory":
                    sparkleMovement.SetBool("accessory", false); sparkleMovement.SetBool("shoe", true);
                    selectedCategory = "shoe";
                    accessoryUI.SetBool("accIsSelected", false); shoeUI.SetBool("shoeIsSelected", true);
                    break;
                case "check":
                    sparkleMovement.SetBool("check", false); sparkleMovement.SetBool("accessory", true);
                    selectedCategory = "accessory";
                    bonusBox.SetBool("appear", false);
                    checkmarkUI.SetBool("checkSelected", false); accessoryUI.SetBool("accIsSelected", true);
                    break;
            }
            UpdateCategory(selectedCategory);
        }

        if(Input.GetKeyDown(inputDown)) {               //down trigger.
            shiftCategory.Play();
            downTrigger.SetTrigger("downTrigger");
            switch(selectedCategory) {
                case "hair":
                    sparkleMovement.SetBool("hair", false); sparkleMovement.SetBool("top", true);
                    selectedCategory = "top";
                    hairUI.SetBool("hairIsSelected", false); topUI.SetBool("topIsSelected", true);
                    break;
                case "top":
                    sparkleMovement.SetBool("top", false); sparkleMovement.SetBool("bottom", true);
                    selectedCategory = "bottom";
                    topUI.SetBool("topIsSelected", false); bottomUI.SetBool("bottomIsSelected", true);
                    break;
                case "bottom":
                    sparkleMovement.SetBool("bottom", false); sparkleMovement.SetBool("shoe", true);
                    selectedCategory = "shoe";
                    bottomUI.SetBool("bottomIsSelected", false); shoeUI.SetBool("shoeIsSelected", true);
                    break;
                case "shoe":
                    sparkleMovement.SetBool("shoe", false); sparkleMovement.SetBool("accessory", true);
                    selectedCategory = "accessory";
                    shoeUI.SetBool("shoeIsSelected", false); accessoryUI.SetBool("accIsSelected", true);
                    break;
                case "accessory":
                    sparkleMovement.SetBool("accessory", false);
                    accessoryUI.SetBool("accIsSelected", false);
                    if(allOK) {
                        sparkleMovement.SetBool("check", true);
                        selectedCategory = "check";
                        updateBonusBoxDisplay();
                        bonusBox.SetBool("appear", true);
                        checkmarkUI.SetBool("checkSelected", true);
                    } else {
                        sparkleMovement.SetBool("accessory", false); sparkleMovement.SetBool("hair", true);
                        selectedCategory = "hair";
                        hairUI.SetBool("hairIsSelected", true);
                    }
                    break;
                case "check":
                    sparkleMovement.SetBool("check", false); sparkleMovement.SetBool("hair", true);
                    selectedCategory = "hair";
                    bonusBox.SetBool("appear", false);
                    checkmarkUI.SetBool("checkSelected", false); hairUI.SetBool("hairIsSelected", true);
                    break;
            }
            UpdateCategory(selectedCategory);
        }

        if(Input.GetKeyDown(inputLeft)) {               //left trigger.
            shiftClothing.Play();
            leftTrigger.SetTrigger("leftTrigger");
            switch(selectedCategory) {
                case "hair":
                    if (selectedHair != -1) {
                        hairAnimation[selectedHair].SetBool("hair"+selectedHair, false);
                    }
                    switch(selectedHair) {
                        case -1:
                            selectedHair = 0;
                            break;
                        case 0:
                            selectedHair = 8;
                            break;
                        case >0:
                            selectedHair--;
                            break;
                    }
                    if (selectedHair != -1) {
                        hairAnimation[selectedHair].SetBool("hair"+selectedHair, true);
                    }
                    break;

                case "top":
                    if (selectedTop != -1) {
                        topAnimation[selectedTop].SetBool("top"+selectedTop, false);
                    }
                    switch(selectedTop) {
                        case -1:
                            selectedTop = 0;
                            break;
                        case 0:
                            selectedTop = 8;
                            break;
                        case >0:
                            selectedTop--;
                            break;
                    }
                    if (selectedTop != -1) {
                        topAnimation[selectedTop].SetBool("top"+selectedTop, true);
                    }
                    break;

                case "bottom":
                    if (selectedBottom != -1) {
                        bottomAnimation[selectedBottom].SetBool("bottom"+selectedBottom, false);
                    }
                    
                    switch(selectedBottom) {
                        case -1:
                            selectedBottom = 0;
                            break;
                        case 0:
                            selectedBottom = 8;
                            break;
                        case >0:
                            selectedBottom--;
                            break;
                    }
                    if (selectedBottom != -1) {
                        bottomAnimation[selectedBottom].SetBool("bottom"+selectedBottom, true);
                    }
                    break;

                case "shoe":
                    if (selectedShoe != -1) {
                        shoeAnimation[selectedShoe].SetBool("shoe"+selectedShoe, false);
                    }
                    
                    switch(selectedShoe) {
                        case -1:
                            selectedShoe = 0;
                            break;
                        case 0:
                            selectedShoe = 8;
                            break;
                        case >0:
                            selectedShoe--;
                            break;
                    }
                    if (selectedShoe != -1) {
                        shoeAnimation[selectedShoe].SetBool("shoe"+selectedShoe, true);
                    }
                    break;

                case "accessory":
                    if (selectedAccessory != -1) {
                        accessoryAnimation[selectedAccessory].SetBool("accessory"+selectedAccessory, false);
                    }
                    
                    switch(selectedAccessory) {
                        case -1:
                            selectedAccessory = 0;
                            break;
                        case 0:
                            selectedAccessory = 8;
                            break;
                        case >0:
                            selectedAccessory--;
                            break;
                    }
                    if (selectedAccessory != -1) {
                        accessoryAnimation[selectedAccessory].SetBool("accessory"+selectedAccessory, true);
                    }
                    break;
            }
            UpdateRow(); UpdateOverlay(); checkIfOK(); updateCategoryBonusDisplay();
        }

        if(Input.GetKeyDown(inputRight)) {              //right trigger.
            shiftClothing.Play();
            rightTrigger.SetTrigger("rightTrigger");
            switch(selectedCategory) {
                case "hair":
                    if (selectedHair != -1) {
                        hairAnimation[selectedHair].SetBool("hair"+selectedHair, false);
                    }
                    switch(selectedHair) {
                        case -1:
                            selectedHair = 0;
                            break;
                        case 8:
                            selectedHair = 0;
                            break;
                        case <8:
                            selectedHair++;
                            break;
                    }
                    if (selectedHair != -1) {
                        hairAnimation[selectedHair].SetBool("hair"+selectedHair, true);
                    }
                    break;

                case "top":
                    if (selectedTop != -1) {
                        topAnimation[selectedTop].SetBool("top"+selectedTop, false);
                    }
                    switch(selectedTop) {
                        case -1:
                            selectedTop = 0;
                            break;
                        case 8:
                            selectedTop = 0;
                            break;
                        case <8:
                            selectedTop++;
                            break;
                    }
                    if (selectedTop != -1) {
                        topAnimation[selectedTop].SetBool("top"+selectedTop, true);
                    }
                    break;

                case "bottom":
                    if (selectedBottom != -1) {
                        bottomAnimation[selectedBottom].SetBool("bottom"+selectedBottom, false);
                    }
                    switch(selectedBottom) {
                        case -1:
                            selectedBottom = 0;
                            break;
                        case 8:
                            selectedBottom = 0;
                            break;
                        case <8:
                            selectedBottom++;
                            break;
                    }
                    if (selectedBottom != -1) {
                        bottomAnimation[selectedBottom].SetBool("bottom"+selectedBottom, true);
                    }
                    break;

                case "shoe":
                    if (selectedShoe != -1) {
                        shoeAnimation[selectedShoe].SetBool("shoe"+selectedShoe, false);
                    }
                    switch(selectedShoe) {
                        case -1:
                            selectedShoe = 0;
                            break;
                        case 8:
                            selectedShoe = 0;
                            break;
                        case <8:
                            selectedShoe++;
                            break;
                    }
                    if (selectedShoe != -1) {
                        shoeAnimation[selectedShoe].SetBool("shoe"+selectedShoe, true);
                    }
                    break;

                case "accessory":
                    if (selectedAccessory != -1) {
                        accessoryAnimation[selectedAccessory].SetBool("accessory"+selectedAccessory, false);
                    }
                    switch(selectedAccessory) {
                        case -1:
                            selectedAccessory = 0;
                            break;
                        case 8:
                            selectedAccessory = 0;
                            break;
                        case <8:
                            selectedAccessory++;
                            break;
                    }
                    if (selectedAccessory != -1) {
                        accessoryAnimation[selectedAccessory].SetBool("accessory"+selectedAccessory, true);
                    }
                    break;
            }
            UpdateRow(); UpdateOverlay(); checkIfOK(); updateCategoryBonusDisplay();
        }

        if(Input.GetKeyDown(inputSelect)) {             //select button trigger.
            if(selectedCategory == "check") {
                //apply the selected clothing to the universal checker
                Progress.chosenHair = selectedHair;
                Progress.chosenTop = selectedTop;
                Progress.chosenBottom = selectedBottom;
                Progress.chosenShoe = selectedShoe;
                Progress.chosenAccessory = selectedAccessory;
                confirm.Play();

                //now figure out what level we're going to. this is determined by the identity set in the dialogue sessions.
                switch(Progress.lastLevel) {
                    //todo
                    case "dayOneIntro":
                    
                        if (selectedHair < 3 && selectedTop < 3 && selectedBottom < 3 && selectedShoe < 3 && selectedAccessory < 3) {
                            calculateBonus(1);
                            StartCoroutine(LoadLevel(1));
                            //SceneManager.LoadScene("Dialogue_Day1PassDressUp");
                        } else {
                            StartCoroutine(LoadLevel(2));
                            //SceneManager.LoadScene("Dialogue_Day1FailDressUp");
                        }
                        break;
                    case "dayTwoIntro":

                        if (selectedHair > 2 && selectedHair < 6 && selectedTop > 2 && selectedTop < 6 && selectedBottom > 2 && selectedBottom < 6 && selectedShoe > 2 && selectedShoe < 6 && selectedAccessory > 2 && selectedAccessory < 6) {
                            calculateBonus(2);
                            StartCoroutine(LoadLevel(3));
                            //SceneManager.LoadScene("Dialogue_Day2PassDressUp");
                        } else {
                            StartCoroutine(LoadLevel(4));
                            //SceneManager.LoadScene("Dialogue_Day2FailDressUp");
                        }
                        break;
                    case "dayThreeIntro":
                        if (selectedHair > 5 && selectedTop > 5 && selectedBottom > 5 && selectedShoe > 5 && selectedAccessory > 5) {
                            calculateBonus(3);
                            StartCoroutine(LoadLevel(5));
                            //SceneManager.LoadScene("Dialogue_Day3PassDressUp");
                        } else {
                            StartCoroutine(LoadLevel(6));
                            //SceneManager.LoadScene("Dialogue_Day3FailDressUp");
                        }
                        break;
                    case "EndingIntro":
                        StartCoroutine(LoadLevel(7));
                        //SceneManager.LoadScene("EndingOutro");
                        break;
                }
            }
        }
    }

    private void UpdateCategory (string category) {     //This method makes the categories light up when they're selected, and darken when they aren't. Also updates the associated category bonus tooltips.
        switch (category) {
            case "hair":
            clothingSelector.transform.position = new Vector3(-0.5f, 3.4f, 0f);
            for (int i = 0; i < 9; i++) {
                hairRowsInner[i].GetComponent<SpriteRenderer>().color = focus;          hairBonusDisplay.GetComponent<SpriteRenderer>().color = focus;
                topRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;         topBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                bottomRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;      bottomBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                shoeRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;        shoeBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                accessoryRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;   accessoryBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
            }
            break;
            case "top":
            clothingSelector.transform.position = new Vector3(5.1f, 3f, 0f);
            for (int i = 0; i < 9; i++) {
                hairRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;        hairBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                topRowsInner[i].GetComponent<SpriteRenderer>().color = focus;           topBonusDisplay.GetComponent<SpriteRenderer>().color = focus;
                bottomRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;      bottomBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                shoeRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;        shoeBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                accessoryRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;   accessoryBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
            }
            break;
            case "bottom":
            clothingSelector.transform.position = new Vector3(5.1f, -1f, 0f);
            for (int i = 0; i < 9; i++) {
                hairRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;        hairBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                topRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;         topBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                bottomRowsInner[i].GetComponent<SpriteRenderer>().color = focus;        bottomBonusDisplay.GetComponent<SpriteRenderer>().color = focus;
                shoeRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;        shoeBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                accessoryRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;   accessoryBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
            }
            break;
            case "shoe":
            clothingSelector.transform.position = new Vector3(-0.5f, 1.1f, 0f);
            for (int i = 0; i < 9; i++) {
                hairRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;        hairBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                topRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;         topBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                bottomRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;      bottomBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                shoeRowsInner[i].GetComponent<SpriteRenderer>().color = focus;          shoeBonusDisplay.GetComponent<SpriteRenderer>().color = focus;
                accessoryRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;   accessoryBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
            }
            break;
            case "accessory":
            clothingSelector.transform.position = new Vector3(-0.5f, -1.6f, 0f);
            for (int i = 0; i < 9; i++) {
                hairRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;        hairBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                topRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;         topBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                bottomRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;      bottomBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                shoeRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;        shoeBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                accessoryRowsInner[i].GetComponent<SpriteRenderer>().color = focus;     accessoryBonusDisplay.GetComponent<SpriteRenderer>().color = focus;
            }
            break;
            case "check":
            clothingSelector.transform.position = new Vector3(10f, 10f, 0f);
            for (int i = 0; i < 9; i++) {
                hairRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;        hairBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                topRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;         topBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                bottomRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;      bottomBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                shoeRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;        shoeBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
                accessoryRowsInner[i].GetComponent<SpriteRenderer>().color = unfocus;   accessoryBonusDisplay.GetComponent<SpriteRenderer>().color = unfocus;
            }
            break;
        }
    }

    private void UpdateOverlay() {                      // Updates what clothing is displayed on the main character on the left.
        if(selectedHair != -1) {
            assignedHair.GetComponent<SpriteRenderer>().sprite = appliedHairCatalog[selectedHair];
        }

        if(selectedTop != -1) {
            assignedTop.GetComponent<SpriteRenderer>().sprite = appliedTopCatalog[selectedTop];
        }

        if(selectedBottom != -1) {

            if (selectedBottom == 0) {                                          //fishnet exception
                assignedBottom.GetComponent<SpriteRenderer>().sortingOrder = 0;
            } else {
                assignedBottom.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            assignedBottom.GetComponent<SpriteRenderer>().sprite = appliedBottomCatalog[selectedBottom];
        }

        if(selectedShoe != -1) {
            assignedShoe.GetComponent<SpriteRenderer>().sprite = appliedShoeCatalog[selectedShoe];
        }

        if(selectedAccessory != -1) {
            assignedAccessory.GetComponent<SpriteRenderer>().sprite = appliedAccessoryCatalog[selectedAccessory];
        }

        
        multiplier.text = DressUpStatBonuses.scoreMultiplier.ToString() + "x";
        leniencyP.text = "+" + DressUpStatBonuses.leniencyValue.ToString() + "ms";
        coolness.text = DressUpStatBonuses.scoreThreshold.ToString() + "required";
    }

    private void updateCategoryBonusDisplay() {         // Updates the tooltip that displays the bonus above every clothing category. 
        switch (selectedCategory) {
            case "hair":
                if (selectedHair == 0 || selectedHair == 3 || selectedHair == 6) {
                    hairBonusDisplay.SetBool("coolness", false); 
                    hairBonusDisplay.SetBool("leniency", false); 
                    hairBonusDisplay.SetBool("score", true);
                }
                if (selectedHair == 1 || selectedHair == 4 || selectedHair == 7) {
                    hairBonusDisplay.SetBool("score", false); 
                    hairBonusDisplay.SetBool("coolness", false);
                    hairBonusDisplay.SetBool("leniency", true);
                }
                if (selectedHair == 2 || selectedHair == 5 || selectedHair == 8) {
                    hairBonusDisplay.SetBool("score", false);
                    hairBonusDisplay.SetBool("leniency", false);
                    hairBonusDisplay.SetBool("coolness", true);
                }
                break;
            case "top":
                if (selectedTop == 0 || selectedTop == 3 || selectedTop == 6) {
                    topBonusDisplay.SetBool("coolness", false); 
                    topBonusDisplay.SetBool("leniency", false); 
                    topBonusDisplay.SetBool("score", true);
                }
                if (selectedTop == 1 || selectedTop == 4 || selectedTop == 7) {
                    topBonusDisplay.SetBool("score", false); 
                    topBonusDisplay.SetBool("coolness", false);
                    topBonusDisplay.SetBool("leniency", true);
                }
                if (selectedTop == 2 || selectedTop == 5 || selectedTop == 8) {
                    topBonusDisplay.SetBool("score", false);
                    topBonusDisplay.SetBool("leniency", false);
                    topBonusDisplay.SetBool("coolness", true);
                }
                break;
            case "bottom":
                if (selectedBottom == 0 || selectedBottom == 3 || selectedBottom == 6) {
                    bottomBonusDisplay.SetBool("coolness", false); 
                    bottomBonusDisplay.SetBool("leniency", false); 
                    bottomBonusDisplay.SetBool("score", true);
                }
                if (selectedBottom == 1 || selectedBottom == 4 || selectedBottom == 7) {
                    bottomBonusDisplay.SetBool("score", false); 
                    bottomBonusDisplay.SetBool("coolness", false);
                    bottomBonusDisplay.SetBool("leniency", true);
                }
                if (selectedBottom == 2 || selectedBottom == 5 || selectedBottom == 8) {
                    bottomBonusDisplay.SetBool("score", false);
                    bottomBonusDisplay.SetBool("leniency", false);
                    bottomBonusDisplay.SetBool("coolness", true);
                }
                break;
            case "shoe":
                if (selectedShoe == 0 || selectedShoe == 3 || selectedShoe == 6) {
                    shoeBonusDisplay.SetBool("coolness", false); 
                    shoeBonusDisplay.SetBool("leniency", false); 
                    shoeBonusDisplay.SetBool("score", true);
                }
                if (selectedShoe == 1 || selectedShoe == 4 || selectedShoe == 7) {
                    shoeBonusDisplay.SetBool("score", false); 
                    shoeBonusDisplay.SetBool("coolness", false);
                    shoeBonusDisplay.SetBool("leniency", true);
                }
                if (selectedShoe == 2 || selectedShoe == 5 || selectedShoe == 8) {
                    shoeBonusDisplay.SetBool("score", false);
                    shoeBonusDisplay.SetBool("leniency", false);
                    shoeBonusDisplay.SetBool("coolness", true);
                }
                break;
            case "accessory":
                if (selectedAccessory == 0 || selectedAccessory == 3 || selectedAccessory == 6) {
                    accessoryBonusDisplay.SetBool("coolness", false); 
                    accessoryBonusDisplay.SetBool("leniency", false); 
                    accessoryBonusDisplay.SetBool("score", true);
                }
                if (selectedAccessory == 1 || selectedAccessory == 4 || selectedAccessory == 7) {
                    accessoryBonusDisplay.SetBool("score", false); 
                    accessoryBonusDisplay.SetBool("coolness", false);
                    accessoryBonusDisplay.SetBool("leniency", true);
                }
                if (selectedAccessory == 2 || selectedAccessory == 5 || selectedAccessory == 8) {
                    accessoryBonusDisplay.SetBool("score", false);
                    accessoryBonusDisplay.SetBool("leniency", false);
                    accessoryBonusDisplay.SetBool("coolness", true);
                }
                break;
        }
    }

    private void UpdateRow() {                          // Updates which row of clothing for each clothing category will be displayed on-screen.
        /*  The modulo of the hover value will determine what row we're in. As there's only two rows, test against a modulo of 2 (essentially the same parity as odd/even).
            When the third row is added, we will test against a modulo of 3 instead. 
            The reason this needs to be done is 'cause the clothes alternate between styles. 0 is punk, 1 is y2k, 2 is punk, etc.
            When the disco style is added, 0 will be punk, 1 will be y2k, 2 will be disco, 3 will be punk, and so on and so forth. 
            Then, reveal the appropriate rows and hide the rest. The for-loop handles that below. It checks for two rows; it'll check for 3 once updated w/ disco.

            never mind fuck that, ignore whatever's above me cause that's impossible af
        */
        if (selectedHair == -1) {} if (selectedTop == -1) {} if (selectedBottom == -1) {} if (selectedShoe == -1) {} if (selectedAccessory == -1) {}

        if (selectedHair < 3) {
            hairRow = 0;
        } else if (selectedHair > 2 && selectedHair < 6){
            hairRow = 1;
        } else if (selectedHair > 5) {
            hairRow = 2;
        }

        if (selectedTop < 3) {
            topRow = 0;
        } else if (selectedTop > 2 && selectedTop < 6){
            topRow = 1;
        } else if (selectedTop > 5) {
            topRow = 2;
        }

        if (selectedBottom < 3) {
            bottomRow = 0;
        } else if (selectedBottom > 2 && selectedBottom < 6){
            bottomRow = 1;
        } else if (selectedBottom > 5) {
            bottomRow = 2;
        }

        if (selectedShoe < 3) {
            shoeRow = 0;
        } else if (selectedShoe > 2 && selectedShoe < 6){
            shoeRow = 1;
        } else if (selectedShoe > 5) {
            shoeRow = 2;
        }

        if (selectedAccessory < 3) {
            accessoryRow = 0;
        } else if (selectedAccessory > 2 && selectedAccessory < 6){
            accessoryRow = 1;
        } else if (selectedAccessory > 5) {
            accessoryRow = 2;
        }

        for (int i = 0; i < 3; i++) {
            if (i != hairRow) {
                hairRows[i].transform.position = new Vector3(10f, 10f, 0f);
            } else {
                hairRows[i].transform.position = new Vector3(-0.5f, 3.4f, 0f);
            }

            if (i != topRow) {
                topRows[i].transform.position = new Vector3(10f, 10f, 0f);
            } else {
                topRows[i].transform.position = new Vector3(5.1f, 3f, 0f);
            }

            if (i != bottomRow) {
                bottomRows[i].transform.position = new Vector3(10f, 10f, 0f);
            } else {
                bottomRows[i].transform.position = new Vector3(5.1f, -1f, 0f);
            }

            if (i != shoeRow) {
                shoeRows[i].transform.position = new Vector3(10f, 10f, 0f);
            } else {
                shoeRows[i].transform.position = new Vector3(-0.5f, 1.1f, 0f);
            }

            if (i != accessoryRow) {
                accessoryRows[i].transform.position = new Vector3(10f, 10f, 0f);
            } else {
                accessoryRows[i].transform.position = new Vector3(-0.5f, -1.6f, 0f);
            }
        }
    }

    private void calculateBonus(int day) {              // Used to calculate the bonus for the rhythm game level, depending on the clothing chosen on this screen.
        double allocateScore = 0.0; int allocateLeniency = 0; int allocateCoolness = 0;
        int[] clothingChecks = {selectedHair, selectedTop, selectedBottom, selectedShoe, selectedAccessory};

        switch(day) {
            case 1:
                for (int i = 0; i < 5; i++) {
                    switch(clothingChecks[i]) {
                        case 0:
                            allocateScore += 0.2;  
                            break;
                        case 1:
                            allocateLeniency += 10;
                            break;
                        case 2:
                            allocateCoolness -= 5;
                            break;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < 5; i++) {
                    switch(clothingChecks[i]) {
                        case 3:
                            allocateScore += 0.2;  
                            break;
                        case 4:
                            allocateLeniency += 10;
                            break;
                        case 5:
                            allocateCoolness -= 5;
                            break;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < 5; i++) {
                    switch(clothingChecks[i]) {
                        case 6:
                            allocateScore += 0.2;  
                            break;
                        case 7:
                            allocateLeniency += 10;
                            break;
                        case 8:
                            allocateCoolness -= 5;
                            break;
                    }
                }
                break;
        }
        DressUpStatBonuses.scoreMultiplier = (1.0 + allocateScore);
        DressUpStatBonuses.leniency = (0.001*allocateLeniency);
        DressUpStatBonuses.leniencyValue = allocateLeniency;
        DressUpStatBonuses.scoreThreshold = (150 + allocateCoolness);
    }

    private void checkIfOK() {                          // Updates the 'meter' to calculate the associated clothing level.
        switch(Progress.lastLevel) {
            case "dayOneIntro":
                if(selectedHair == 0 || selectedHair == 1 || selectedHair == 2) {
                    hairOK = true;
                } else {
                    hairOK = false;
                }
                break;
            case "dayTwoIntro":
                if(selectedHair == 3 || selectedHair == 4 || selectedHair == 5) {
                    hairOK = true;
                } else {
                    hairOK = false;
                }
                break;
            case "dayThreeIntro":
                if(selectedHair == 6 || selectedHair == 7 || selectedHair == 8) {
                    hairOK = true;
                } else {
                    hairOK = false;
                }
                break;
            case "EndingIntro":
                if(selectedHair != -1) {
                    hairOK = true;
                }
                break;
        }

        switch(Progress.lastLevel) {
            case "dayOneIntro":
                if(selectedTop == 0 || selectedTop == 1 || selectedTop == 2) {
                    topOK = true;
                } else {
                    topOK = false;
                }
                break;
            case "dayTwoIntro":
                if(selectedTop == 3 || selectedTop == 4 || selectedTop == 5) {
                    topOK = true;
                } else {
                    topOK = false;
                }
                break;
             case "dayThreeIntro":
                if(selectedTop == 6 || selectedTop == 7 || selectedTop == 8) {
                     topOK = true;
                } else {
                     topOK = false;
                }
                break;
            case "EndingIntro":
                if(selectedTop != -1) {
                    topOK = true;
                }
                break;
        }

        switch(Progress.lastLevel) {
            case "dayOneIntro":
                if(selectedBottom == 0 || selectedBottom == 1 || selectedBottom == 2) {
                    bottomOK = true;
                } else {
                    bottomOK = false;
                }
                break;
            case "dayTwoIntro":
                if(selectedBottom == 3 || selectedBottom == 4 || selectedBottom == 5) {
                    bottomOK = true;
                } else {
                    bottomOK = false;
                }
                break;
             case "dayThreeIntro":
                if(selectedBottom == 6 || selectedBottom == 7 || selectedBottom == 8) {
                     bottomOK = true;
                } else {
                     bottomOK = false;
                }
                break;
            case "EndingIntro":
                if(selectedBottom != -1) {
                    bottomOK = true;
                }
                break;
        }

        switch(Progress.lastLevel) {
            case "dayOneIntro":
                if(selectedShoe == 0 || selectedShoe == 1 || selectedShoe == 2) {
                    shoeOK = true;
                } else {
                    shoeOK = false;
                }
                break;
            case "dayTwoIntro":
                if(selectedShoe == 3 || selectedShoe == 4 || selectedShoe == 5) {
                    shoeOK = true;
                } else {
                    shoeOK = false;
                }
                break;
             case "dayThreeIntro":
                if(selectedShoe == 6 || selectedShoe == 7 || selectedShoe == 8) {
                     shoeOK = true;
                } else {
                     shoeOK = false;
                }
                break;
            case "EndingIntro":
                if(selectedShoe != -1) {
                    shoeOK = true;
                }
                break;
        }

        switch(Progress.lastLevel) {
            case "dayOneIntro":
                if(selectedAccessory == 0 || selectedAccessory == 1 || selectedAccessory == 2) {
                    accessoryOK = true;
                } else {
                    accessoryOK = false;
                }
                break;
            case "dayTwoIntro":
                if(selectedAccessory == 3 || selectedAccessory == 4 || selectedAccessory == 5) {
                    accessoryOK = true;
                } else {
                    accessoryOK = false;
                }
                break;
             case "dayThreeIntro":
                if(selectedAccessory == 6 || selectedAccessory == 7 || selectedAccessory == 8) {
                     accessoryOK = true;
                } else {
                     accessoryOK = false;
                }
                break;
            case "EndingIntro":
                if(selectedAccessory != -1) {
                    accessoryOK = true;
                }
                break;
        }

        meterCounter = 0;
        if (hairOK) {meterCounter++;}
        if (topOK) {meterCounter++;}
        if (bottomOK) {meterCounter++;}
        if (shoeOK) {meterCounter++;}
        if (accessoryOK) {meterCounter++;}

        if (meterCounter > 0) {
            meter.SetBool("to"+(meterCounter-1), false);
        }

        meter.SetBool("to"+meterCounter, true);

        if (meterCounter < 5) {
            meter.SetBool("to"+(meterCounter+1), false);
        }

        if (meterCounter == 5) {
            fullBar.Play();
        } else {
            fullBar.Stop();
        }
    }

    public void updateBonusBoxDisplay() {               // Updates the bonus summary box displayed when hovering over the checkmark/confirm button.
        double allocateScore = 0.0; int allocateLeniency = 0; int allocateCoolness = 0;
        int[] clothingChecks = {selectedHair, selectedTop, selectedBottom, selectedShoe, selectedAccessory};
                for (int i = 0; i < 5; i++) {
                    switch(clothingChecks[i]) {
                        case 0:
                            allocateScore += 0.2;  
                            break;
                        case 1:
                            allocateLeniency += 10;
                            break;
                        case 2:
                            allocateCoolness -= 5;
                            break;
                        case 3:
                            allocateScore += 0.2;  
                            break;
                        case 4:
                            allocateLeniency += 10;
                            break;
                        case 5:
                            allocateCoolness -= 5;
                            break;
                        case 6:
                            allocateScore += 0.2;  
                            break;
                        case 7:
                            allocateLeniency += 10;
                            break;
                        case 8:
                            allocateCoolness -= 5;
                            break;    
                    }
                }

        DressUpStatBonuses.scoreMultiplier = (1.0 + allocateScore);
        DressUpStatBonuses.leniency = (0.001*allocateLeniency);
        DressUpStatBonuses.leniencyValue = allocateLeniency;
        DressUpStatBonuses.scoreThreshold = (150 + allocateCoolness);

        multiplier.text = DressUpStatBonuses.scoreMultiplier.ToString() + "x";
        leniencyP.text = "+" + DressUpStatBonuses.leniencyValue.ToString() + "ms";
        coolness.text = DressUpStatBonuses.scoreThreshold.ToString() + "\nrequired";
    }

    IEnumerator LoadLevel(int id) {                     // Loads a new scene.
        transition.SetBool("exit", true);
        yield return new WaitForSeconds(1f);
        switch(id) {
            case 1:
                SceneManager.LoadScene("Dialogue_Day1PassDressUp");
                break;
            case 2:
                SceneManager.LoadScene("Dialogue_Day1FailDressUp");
                break;
            case 3:
                SceneManager.LoadScene("Dialogue_Day2PassDressUp");
                break;
            case 4:
                SceneManager.LoadScene("Dialogue_Day2FailDressUp");
                break;
            case 5:
                SceneManager.LoadScene("Dialogue_Day3PassDressUp");
                break;
            case 6:
                SceneManager.LoadScene("Dialogue_Day3FailDressUp");
                break;
            case 7:
                SceneManager.LoadScene("EndingOutro");
                break;
        }   
    }
}