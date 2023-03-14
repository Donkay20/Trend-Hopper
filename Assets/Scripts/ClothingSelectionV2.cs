using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class ClothingSelectionV2 : MonoBehaviour
{
    public static ClothingSelectionV2 Instance;

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
    [Space]
    public GameObject clothingSelector;
    public Animator leftTrigger;
    public Animator rightTrigger;   //for the moving selector
    [Space]
    public Animator[] hairAnimation = new Animator[6];
    public Animator[] topAnimation = new Animator[6];
    public Animator[] bottomAnimation = new Animator[6];
    public Animator[] shoeAnimation = new Animator[6];
    public Animator[] accessoryAnimation = new Animator[6];         //these will control the animations that go in and out
    [Space]
    public GameObject[] hairRows = new GameObject[2];
    public GameObject[] topRows = new GameObject[2];
    public GameObject[] bottomRows = new GameObject[2];
    public GameObject[] shoeRows = new GameObject[2];
    public GameObject[] accessoryRows = new GameObject[2];          //these should hold the rows of closet displays (right now 2, max 3 on completion.)
    [Space]
    public GameObject assignedHair;
    public GameObject assignedTop;
    public GameObject assignedBottom;
    public GameObject assignedShoe;
    public GameObject assignedAccessory;                             //these are the clothes that will show on the character.
    [Space]
    public Sprite[] appliedHairCatalog = new Sprite[6];
    public Sprite[] appliedTopCatalog = new Sprite[6];
    public Sprite[] appliedBottomCatalog = new Sprite[6];
    public Sprite[] appliedShoeCatalog = new Sprite[6];
    public Sprite[] appliedAccessoryCatalog = new Sprite[6];        //these are the clothes in storage that can be applied to the character. max is 6, end goal should be 9.

    private int hairRow;        private int hoverHair;              //row variable series should be 1 or 2 (1-3 later on) to determine what row to show.
    private int topRow;         private int hoverTop;               //hover variable series should determine what is being highlighted. goes from 0-5 (later should be 0-8)
    private int bottomRow;      private int hoverBottom;
    private int shoeRow;        private int hoverShoe;
    private int accessoryRow;   private int hoverAccessory;

    private string selectedCategory;                                //hair, top, bottom, shoe, accessory, check
    private int selectedHair;                                       //numbers to determine which outfit was chosen. plan is for 0-2 to be punk, 3-5 to be y2k, and 6-8 to be disco
    private int selectedTop;                                        //only goes from 0-5 for now, as disco isn't in yet.
    private int selectedBottom;
    private int selectedShoe;
    private int selectedAccessory;
    private static Random reroll = new Random();                    //this thing is to randomly assign clothing at the start of this godforsaken fucking game

    private bool hairOK; private bool topOK; private bool bottomOK; private bool shoeOK; private bool AccessoryOK; 
    private bool allOK;                                             //bools to check to see if each clothing category has been selected at least once; all OK if all of the have been checked once

    void Start()
    {
        Instance = this;            //initialize the instance
        selectedCategory = "hair";  //set the default position to hair.
        selectedHair = reroll.Next(0, 5); selectedTop = reroll.Next(0, 5); selectedBottom = reroll.Next(0, 5); selectedShoe = reroll.Next(0, 5); selectedAccessory = reroll.Next(0, 5);
        UpdateCategory(selectedCategory);
        UpdateOverlay();
        hoverHair = selectedHair; hoverTop = selectedTop; hoverBottom = selectedBottom; hoverShoe = selectedShoe; hoverAccessory = selectedAccessory;
        UpdateRow();                //initialize the row, dependent on the positions decided above.
        hairAnimation[hoverHair].SetBool("hair"+hoverHair, true);
        topAnimation[hoverTop].SetBool("top"+hoverTop, true);
        bottomAnimation[hoverBottom].SetBool("bottom"+hoverBottom, true);
        shoeAnimation[hoverShoe].SetBool("shoe"+hoverShoe, true);
        accessoryAnimation[hoverAccessory].SetBool("accessory"+hoverAccessory, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(hairOK && topOK && bottomOK && shoeOK && AccessoryOK) {
            allOK = true;
            checkmarkUI.SetBool("checkUnlocked", true);
        }

        if(Input.GetKeyDown(inputUp)) {
            upTrigger.SetTrigger("upTrigger");
            switch(selectedCategory) {
                case "hair":
                    hairUI.SetBool("hairIsSelected", false);
                    if (allOK) {
                        selectedCategory = "check";
                        checkmarkUI.SetBool("checkSelected", true);
                    } else {
                        selectedCategory = "accessory";
                        accessoryUI.SetBool("accIsSelected", true);
                    }
                    break;
                case "top":
                    selectedCategory = "hair";
                    topUI.SetBool("topIsSelected", false); hairUI.SetBool("hairIsSelected", true);
                    break;
                case "bottom":
                    selectedCategory = "top";
                    bottomUI.SetBool("bottomIsSelected", false); topUI.SetBool("topIsSelected", true);
                    break;
                case "shoe":
                    selectedCategory = "bottom";
                    shoeUI.SetBool("shoeIsSelected", false); bottomUI.SetBool("bottomIsSelected", true);
                    break;
                case "accessory":
                    selectedCategory = "shoe";
                    accessoryUI.SetBool("accIsSelected", false); shoeUI.SetBool("shoeIsSelected", true);
                    break;
                case "check":
                    selectedCategory = "accessory";
                    checkmarkUI.SetBool("checkSelected", false); accessoryUI.SetBool("accIsSelected", true);
                    break;
            }
            UpdateCategory(selectedCategory);
        }

        if(Input.GetKeyDown(inputDown)) {
            downTrigger.SetTrigger("downTrigger");
            switch(selectedCategory) {
                case "hair":
                    selectedCategory = "top";
                    hairUI.SetBool("hairIsSelected", false); topUI.SetBool("topIsSelected", true);
                    break;
                case "top":
                    selectedCategory = "bottom";
                    topUI.SetBool("topIsSelected", false); bottomUI.SetBool("bottomIsSelected", true);
                    break;
                case "bottom":
                    selectedCategory = "shoe";
                    bottomUI.SetBool("bottomIsSelected", false); shoeUI.SetBool("shoeIsSelected", true);
                    break;
                case "shoe":
                    selectedCategory = "accessory";
                    shoeUI.SetBool("shoeIsSelected", false); accessoryUI.SetBool("accIsSelected", true);
                    break;
                case "accessory":
                    accessoryUI.SetBool("accIsSelected", false);
                    if(allOK) {
                        selectedCategory = "check";
                        checkmarkUI.SetBool("checkSelected", true);
                    } else {
                        selectedCategory = "hair";
                        hairUI.SetBool("hairIsSelected", true);
                    }
                    break;
                case "check":
                    selectedCategory = "hair";
                    checkmarkUI.SetBool("checkSelected", false); hairUI.SetBool("hairIsSelected", true);
                    break;
            }
            UpdateCategory(selectedCategory);
        }

        if(Input.GetKeyDown(inputLeft)) {
            leftTrigger.SetTrigger("leftTrigger");
            switch(selectedCategory) {
                case "hair":
                    hairAnimation[hoverHair].SetBool("hair"+hoverHair, false);
                    switch(hoverHair) {
                        case 0:
                            hoverHair = 5;
                            break;
                        case >0:
                            hoverHair--;
                            break;
                    }
                    hairAnimation[hoverHair].SetBool("hair"+hoverHair, true);
                    break;

                case "top":
                    topAnimation[hoverTop].SetBool("top"+hoverTop, false);
                    switch(hoverTop) {
                        case 0:
                            hoverTop = 5;
                            break;
                        case >0:
                            hoverTop--;
                            break;
                    }
                    topAnimation[hoverTop].SetBool("top"+hoverTop, true);
                    break;

                case "bottom":
                    bottomAnimation[hoverBottom].SetBool("bottom"+hoverBottom, false);
                    switch(hoverBottom) {
                        case 0:
                            hoverBottom = 5;
                            break;
                        case >0:
                            hoverBottom--;
                            break;
                    }
                    bottomAnimation[hoverBottom].SetBool("bottom"+hoverBottom, true);
                    break;

                case "shoe":
                    shoeAnimation[hoverShoe].SetBool("shoe"+hoverShoe, false);
                    switch(hoverShoe) {
                        case 0:
                            hoverShoe = 5;
                            break;
                        case >0:
                            hoverShoe--;
                            break;
                    }
                    shoeAnimation[hoverShoe].SetBool("shoe"+hoverShoe, true);
                    break;

                case "accessory":
                    accessoryAnimation[hoverAccessory].SetBool("accessory"+hoverAccessory, false);
                    switch(hoverAccessory) {
                        case 0:
                            hoverAccessory = 5;
                            break;
                        case >0:
                            hoverAccessory--;
                            break;
                    }
                    accessoryAnimation[hoverAccessory].SetBool("accessory"+hoverAccessory, true);
                    break;
            }
            UpdateRow();
        }

        if(Input.GetKeyDown(inputRight)) {
            rightTrigger.SetTrigger("rightTrigger");
            switch(selectedCategory) {
                case "hair":
                    hairAnimation[hoverHair].SetBool("hair"+hoverHair, false);
                    switch(hoverHair) {
                        case 5:
                            hoverHair = 0;
                            break;
                        case <5:
                            hoverHair++;
                            break;
                    }
                    hairAnimation[hoverHair].SetBool("hair"+hoverHair, true);
                    break;

                case "top":
                    topAnimation[hoverTop].SetBool("top"+hoverTop, false);
                    switch(hoverTop) {
                        case 5:
                            hoverTop = 0;
                            break;
                        case <5:
                            hoverTop++;
                            break;
                    }
                    topAnimation[hoverTop].SetBool("top"+hoverTop, true);
                    break;

                case "bottom":
                    bottomAnimation[hoverBottom].SetBool("bottom"+hoverBottom, false);
                    switch(hoverBottom) {
                        case 5:
                            hoverBottom = 0;
                            break;
                        case <5:
                            hoverBottom++;
                            break;
                    }
                    bottomAnimation[hoverBottom].SetBool("bottom"+hoverBottom, true);
                    break;

                case "shoe":
                    shoeAnimation[hoverShoe].SetBool("shoe"+hoverShoe, false);
                    switch(hoverShoe) {
                        case 5:
                            hoverShoe = 0;
                            break;
                        case <5:
                            hoverShoe++;
                            break;
                    }
                    shoeAnimation[hoverShoe].SetBool("shoe"+hoverShoe, true);
                    break;

                case "accessory":
                    accessoryAnimation[hoverAccessory].SetBool("accessory"+hoverAccessory, false);
                    switch(hoverAccessory) {
                        case 5:
                            hoverAccessory = 0;
                            break;
                        case <5:
                            hoverAccessory++;
                            break;
                    }
                    accessoryAnimation[hoverAccessory].SetBool("accessory"+hoverAccessory, true);
                    break;
            }
            UpdateRow();
        }

        if(Input.GetKeyDown(inputSelect)) {
            if(selectedCategory == "check") {
                //apply the selected clothing to the universal checker
                Progress.chosenHair = selectedHair;
                Progress.chosenTop = selectedTop;
                Progress.chosenBottom = selectedBottom;
                Progress.chosenShoe = selectedShoe;
                Progress.chosenAccessory = selectedAccessory;

                //now figure out what level we're going to. this is determined by the identity set in the dialogue sessions.
                switch(Progress.lastLevel) {
                    //todo
                    case "dayOneIntro":
                        if (selectedHair < 3 && selectedTop < 3 && selectedBottom < 3 && selectedShoe < 3 && selectedAccessory < 3) {
                            SceneManager.LoadScene("Dialogue_Day1PassDressUp");
                            calculateBonus(1);
                        } else {
                            SceneManager.LoadScene("Dialogue_Day1FailDressUp");
                        }
                        break;
                    case "dayTwoIntro":
                        if (selectedHair > 2 && selectedTop > 2 && selectedBottom > 2 && selectedShoe > 2 && selectedAccessory > 2) { //this clause will need to be changed when lv3 stuff is here!
                            //load the day2 clear
                            calculateBonus(2);
                        } else {
                            //load the day2 fail
                        }
                        break;
                    case "dayThreeIntro":
                        break;
                }
            } else {
                switch(selectedCategory) {
                    case "hair":
                        selectedHair = hoverHair;
                        hairOK = true;
                        break;

                    case "top":
                        selectedTop = hoverTop;
                        topOK = true;
                        break;

                    case "bottom":
                        selectedBottom = hoverBottom;
                        bottomOK = true;
                        break;

                    case "shoe":
                        selectedShoe = hoverShoe;
                        shoeOK = true;
                        break;

                    case "accessory":
                        selectedAccessory = hoverAccessory;
                        AccessoryOK = true;
                        break;
                }
                UpdateOverlay();
            }
        }
    }

    private void UpdateCategory (string category) {
        switch (category) {
            case "hair":
            clothingSelector.transform.position = new Vector3(-0.5f, 3.4f, 0f);
            break;
            case "top":
            clothingSelector.transform.position = new Vector3(5.1f, 3f, 0f);
            break;
            case "bottom":
            clothingSelector.transform.position = new Vector3(5.1f, -1f, 0f);
            break;
            case "shoe":
            clothingSelector.transform.position = new Vector3(-0.5f, 1.1f, 0f);
            break;
            case "accessory":
            clothingSelector.transform.position = new Vector3(-0.5f, -1.6f, 0f);
            break;
            case "check":
            clothingSelector.transform.position = new Vector3(10f, 10f, 0f);
            break;
        }
    }

    private void UpdateOverlay() {
        assignedHair.GetComponent<SpriteRenderer>().sprite = appliedHairCatalog[selectedHair];
        assignedTop.GetComponent<SpriteRenderer>().sprite = appliedTopCatalog[selectedTop];
        assignedBottom.GetComponent<SpriteRenderer>().sprite = appliedBottomCatalog[selectedBottom];
        assignedShoe.GetComponent<SpriteRenderer>().sprite = appliedShoeCatalog[selectedShoe];
        assignedAccessory.GetComponent<SpriteRenderer>().sprite = appliedAccessoryCatalog[selectedAccessory];
    }

    private void UpdateRow() {
        /*  The modulo of the hover value will determine what row we're in. As there's only two rows, test against a modulo of 2 (essentially the same parity as odd/even).
            When the third row is added, we will test against a modulo of 3 instead. 
            The reason this needs to be done is 'cause the clothes alternate between styles. 0 is punk, 1 is y2k, 2 is punk, etc.
            When the disco style is added, 0 will be punk, 1 will be y2k, 2 will be disco, 3 will be punk, and so on and so forth. 
            Then, reveal the appropriate rows and hide the rest. The for-loop handles that below. It checks for two rows; it'll check for 3 once updated w/ disco.

            never mind fuck that, ignore whatever's above me cause that's impossible af
        */
        if (hoverHair > 2) {
            hairRow = 1;
        } else {
            hairRow = 0;
        }

        if (hoverTop > 2) {
            topRow = 1;
        } else {
            topRow = 0;
        }

        if (hoverBottom > 2) {
            bottomRow = 1;
        } else {
            bottomRow = 0;
        }

        if (hoverShoe > 2) {
            shoeRow = 1;
        } else {
            shoeRow = 0;
        }

        if (hoverAccessory > 2) {
            accessoryRow = 1;
        } else {
            accessoryRow = 0;
        }

        for (int i = 0; i < 2; i++) {
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

    private void calculateBonus(int day) {
        double allocateScore = 0.0; int allocateLeniency = 0; int allocateCoolness = 0;
        int[] clothingChecks = {selectedHair, selectedTop, selectedBottom, selectedShoe, selectedAccessory};

        switch(day) {
            case 1:
                for (int i = 0; i < 5; i++) {
                    switch(clothingChecks[i]) {
                        case 0:
                            allocateScore += 0.2; allocateLeniency += 10; allocateCoolness -= 5;
                            break;
                        case 1:
                            allocateScore += 0.1; allocateLeniency += 7; allocateCoolness -= 0;
                            break;
                        case 2:
                            allocateScore += 0.05; allocateLeniency += 5; allocateCoolness -= 0;
                            break;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < 5; i++) {
                    switch(clothingChecks[i]) {
                        case 3:
                            allocateScore += 0.2; allocateLeniency += 10; allocateCoolness -= 5;
                            break;
                        case 4:
                            allocateScore += 0.1; allocateLeniency += 7; allocateCoolness -= 0;
                            break;
                        case 5:
                            allocateScore += 0.05; allocateLeniency += 5; allocateCoolness -= 0;
                            break;
                    }
                }
                break;
            case 3:
                //todo
                break;
        }

        DressUpStatBonuses.scoreMultiplier = (1.0 + allocateScore);
        DressUpStatBonuses.leniency = (0.001*allocateLeniency);
        DressUpStatBonuses.scoreThreshold = (150 + allocateCoolness);
    }
}
