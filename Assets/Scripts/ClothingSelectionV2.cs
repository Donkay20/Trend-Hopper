using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject leftArrow;
    public GameObject rightArrow;
    public Animator leftTrigger;
    public Animator rightTrigger;   //for the moving selector
    [Space]
    public GameObject[] hairOnDisplay = new GameObject[3];
    public GameObject[] topOnDisplay = new GameObject[3];
    public GameObject[] bottomOnDisplay = new GameObject[3];
    public GameObject[] shoeOnDisplay = new GameObject[3];
    public GameObject[] accessoryOnDisplay = new GameObject[3]; //these are the ones that'll be on-screen in the closet
    [Space]
    public Sprite[] hairCatalog = new Sprite[6];
    public Sprite[] topCatalog = new Sprite[6];
    public Sprite[] bottomCatalog = new Sprite[6];
    public Sprite[] shoeCatalog = new Sprite[6];
    public Sprite[] accessoryCatalog = new Sprite[6];           //these are the in-closet things that are stored in these arrays
    [Space]
    public Sprite appliedHair;
    public Sprite appliedTop;
    public Sprite appliedBottom;
    public Sprite appliedShoe;
    public Sprite appliedAccessory;                             //these are the clothes that will show on the character.
    [Space]
    public Sprite[] appliedHairCatalog = new Sprite[6];
    public Sprite[] appliedTopCatalog = new Sprite[6];
    public Sprite[] appliedBottomCatalog = new Sprite[6];
    public Sprite[] appliedShoeCatalog = new Sprite[6];
    public Sprite[] appliedAccessoryCatalog = new Sprite[6];    //these are the clothes in storage that can be applied to the character.

    private string selectedCategory;                            //hair, top, bottom, shoe, accessory, confirm

    private int selectedHair;                                   //numbers to determine which outfit was chosen
    private int selectedTop;
    private int selectedBottom;
    private int selectedShoe;
    private int selectedAccessory;

    private bool hairOK; private bool topOK; private bool bottomOK; private bool shoeOK; private bool AccessoryOK; 
    private bool allOK; //bools to check to see if each clothing category has been selected at least once; all OK if all of the have been checked once

    void Start()
    {
        Instance = this;
        selectedCategory = "hair";
        selectedHair = 1;
        selectedTop = 2;
        selectedBottom = 3;
        selectedShoe = 2;
        selectedAccessory = 1;
    }

    // Update is called once per frame
    void Update()
    {
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
        }

        if(Input.GetKeyDown(inputLeft)) {
            leftTrigger.SetTrigger("leftTrigger");
        }

        if(Input.GetKeyDown(inputRight)) {
            rightTrigger.SetTrigger("rightTrigger");
        }

        if(Input.GetKeyDown(inputSelect)) {
            if(selectedCategory == "check") {
                //load appropriate scene here.
            } else {
                //confirm the clothing and stuff
            }
        }
    }

    private void UpdateCategory (int x) {

    }
}
