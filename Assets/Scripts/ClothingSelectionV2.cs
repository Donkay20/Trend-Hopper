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
    public GameObject hairUI;
    public GameObject topUI;
    public GameObject bottomUI;
    public GameObject shoeUI;
    public GameObject accessoryUI; 
    public GameObject checkmarkUI;
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

    private int selectedHair;
    private int selectedTop;
    private int selectedBottom;
    private int selectedShoe;
    private int selectedAccessory;

    void Start()
    {
        Instance = this;
        selectedCategory = "hair";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(inputUp)) {
            switch(selectedCategory) {
                case "hair":
                    break;
                case "top":
                    break;
                case "bottom":
                    break;
                case "shoe":
                    break;
                case "check":
                    break;
            }
        }

        if(Input.GetKeyDown(inputDown)) {
            switch(selectedCategory) {
                case "hair":
                    break;
                case "top":
                    break;
                case "bottom":
                    break;
                case "shoe":
                    break;
                case "check":
                    break;
            }
        }

        if(Input.GetKeyDown(inputLeft)) {
            
        }

        if(Input.GetKeyDown(inputRight)) {
            
        }

        if(Input.GetKeyDown(inputSelect)) {
            if(selectedCategory == "check") {
                //load appropriate scene here.
            } else {
                //confirm the clothing and stuff
            }
        }
    }
}
