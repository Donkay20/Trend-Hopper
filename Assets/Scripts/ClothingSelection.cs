using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Whatever this class is attached to will be responsible for the management of the UI controls for the dress-up function of the game.
Controls are set up in such a way that it is possible to navigate this menu using only the arrow keys.
*/

public class ClothingSelection : MonoBehaviour
{
    public static ClothingSelection Instance;
    public TMPro.TextMeshPro startText;
    public TMPro.TextMeshPro scoreMultiplierText;
    public TMPro.TextMeshPro leniencyText;
    public TMPro.TextMeshPro coolnessText;

    public KeyCode inputUp;                 //controls
    public KeyCode inputDown;
    public KeyCode inputRight;
    public KeyCode inputLeft;

    private int selectedCategory;
    private int selectedClothingHair;
    private int selectedClothingAccessory;
    private int selectedClothingShoe;
    private int selectedClothingTop;
    private int selectedClothingBottom;     //variables set for the categories and clothings within said categories

    private int[] bonuses = new int[5];     //this array will be used to store the dress-up bonuses. 1 slot from 0-4 each for the category, and 1-3 within each slot for the type of bonus.
                                            //1 = score multiplier, 2 = leniency, 3 = coolness threshold

    public GameObject[] hairOverlay = new GameObject[3];
    public GameObject[] accessoryOverlay = new GameObject[3];
    public GameObject[] shoeOverlay = new GameObject[3];
    public GameObject[] topOverlay = new GameObject[3];
    public GameObject[] bottomOverlay = new GameObject[3];  //these are where the overlays are held

    public GameObject hairBox;
    public GameObject accessoryBox;
    public GameObject shoeBox;
    public GameObject topBox;
    public GameObject bottomBox;
    public GameObject categoryBox;          //this is where the prefabs go

    void Start()
    {        
        Instance = this;
        DressUpStatBonuses.scoreMultiplier = 1.0;
        DressUpStatBonuses.leniency = 0;
        DressUpStatBonuses.scoreThreshold = 120;
        selectedCategory = 1;           
        selectedClothingHair = 2;       //category 1
        selectedClothingAccessory = 2;  //category 2
        selectedClothingShoe = 2;       //category 3
        selectedClothingTop = 2;        //category 4
        selectedClothingBottom = 2;     //category 5
        startText.text = "Start!";      //category 6

        categoryBox = Instantiate(categoryBox);
        hairBox = Instantiate(hairBox);
        accessoryBox = Instantiate(accessoryBox);
        shoeBox = Instantiate(shoeBox);
        topBox = Instantiate(topBox);
        bottomBox = Instantiate(bottomBox);                     //add that stuff on the screen!

        UpdateCategoryIndicator(selectedCategory);
        UpdateHairIndicator(selectedClothingHair);
        UpdateAccessoryIndicator(selectedClothingAccessory);
        UpdateShoeIndicator(selectedClothingShoe);
        UpdateTopIndicator(selectedClothingTop);
        UpdateBottomIndicator(selectedClothingBottom);          //initializes all of the categories to 1, the first in each category. The category indicator is also initialized to 1, the hair category.
    }

    // Update is called once per frame
    void Update()
    {   
        if (selectedCategory != 6) {                //the text changes off "> to start!" when it isn't on the right category
            startText.text = "Start!";
        }

        if (Input.GetKeyDown(inputUp)) {            //go up a category
            if (selectedCategory == 1) {
                selectedCategory = 6;
            } else {
                selectedCategory--;
            }
            UpdateCategoryIndicator(selectedCategory);
        }

         if (Input.GetKeyDown(inputDown)) {         //go down a category
            if (selectedCategory == 6) {
                selectedCategory = 1;
            } else {
                selectedCategory++;
            }
            UpdateCategoryIndicator(selectedCategory);
        }

         if (Input.GetKeyDown(inputRight)) {        //go right in the category. if at the start button, start the game
            if (selectedCategory == 1) {
                if (selectedClothingHair == 3) {
                    selectedClothingHair = 1;
                } 
                else  {
                    selectedClothingHair++;
                }
                UpdateHairIndicator(selectedClothingHair);
            }

            if (selectedCategory == 2) {
                if (selectedClothingAccessory == 3) {
                    selectedClothingAccessory = 1;
                } 
                else  {
                    selectedClothingAccessory++;
                }
                UpdateAccessoryIndicator(selectedClothingAccessory);
            }

            if (selectedCategory == 3) {
                if (selectedClothingShoe == 3) {
                    selectedClothingShoe = 1;
                } 
                else  {
                    selectedClothingShoe++;
                }
                UpdateShoeIndicator(selectedClothingShoe);
            }

            if (selectedCategory == 4) {
                if (selectedClothingTop == 3) {
                    selectedClothingTop = 1;
                } 
                else  {
                    selectedClothingTop++;
                }
                UpdateTopIndicator(selectedClothingTop);
            }

            if (selectedCategory == 5) {
                if (selectedClothingBottom == 3) {
                    selectedClothingBottom = 1;
                } 
                else  {
                    selectedClothingBottom++;
                }
                UpdateBottomIndicator(selectedClothingBottom);
            }

            if (selectedCategory == 6) {
                SceneManager.LoadScene("RhythmGame");
            }
        }

         if (Input.GetKeyDown(inputLeft)) {         //go left within the category.
            if (selectedCategory == 1) {
                if (selectedClothingHair == 1) {
                    selectedClothingHair = 3;
                } 
                else  {
                    selectedClothingHair--;
                }
                UpdateHairIndicator(selectedClothingHair);
            }
            if (selectedCategory == 2) {
                if (selectedClothingAccessory == 1) {
                    selectedClothingAccessory = 3;
                } 
                else  {
                    selectedClothingAccessory--;
                }
                UpdateAccessoryIndicator(selectedClothingAccessory);
            }
            if (selectedCategory == 3) {
                if (selectedClothingShoe == 1) {
                    selectedClothingShoe = 3;
                } 
                else  {
                    selectedClothingShoe--;
                }
                UpdateShoeIndicator(selectedClothingShoe);
            }
            if (selectedCategory == 4) {
                if (selectedClothingTop == 1) {
                    selectedClothingTop = 3;
                } 
                else  {
                    selectedClothingTop--;
                }
                UpdateTopIndicator(selectedClothingTop);
            }

            if (selectedCategory == 5) {
                if (selectedClothingBottom == 1) {
                    selectedClothingBottom = 3;
                } 
                else  {
                    selectedClothingBottom--;
                }
                UpdateBottomIndicator(selectedClothingBottom);
            }
        }
    }

    //manipulates indicator positions for all clothing, as well as the category position. Vectors are hardcoded.

    private void UpdateHairIndicator(int x) {
        if (x == 1) {
            hairBox.transform.position = new Vector3(-2.1f, 3.5f, 0f);
            hairOverlay[0].SetActive(true); hairOverlay[1].SetActive(false); hairOverlay[2].SetActive(false);
            bonuses[0] = 1;
        }

        if (x == 2) {
            hairBox.transform.position = new Vector3(-0.6f, 3.5f, 0f);
            hairOverlay[0].SetActive(false); hairOverlay[1].SetActive(true); hairOverlay[2].SetActive(false);
            bonuses[0] = 2;
        }

        if (x == 3) {
            hairBox.transform.position = new Vector3(0.7f, 3.5f, 0f);
            hairOverlay[0].SetActive(false); hairOverlay[1].SetActive(false); hairOverlay[2].SetActive(true);
            bonuses[0] = 3;
        }
        UpdateBonusTooltip();
    }

    private void UpdateAccessoryIndicator(int x) {
        if (x == 1) {
            accessoryBox.transform.position = new Vector3(-2f, 1.1f, 0f);
            accessoryOverlay[0].SetActive(true); accessoryOverlay[1].SetActive(false); accessoryOverlay[2].SetActive(false);
            bonuses[1] = 1; 
        }

        if (x == 2) {
            accessoryBox.transform.position = new Vector3(-0.6f, 1.1f, 0f);
            accessoryOverlay[0].SetActive(false); accessoryOverlay[1].SetActive(true); accessoryOverlay[2].SetActive(false);
            bonuses[1] = 2;  
        }

        if (x == 3) {
            accessoryBox.transform.position = new Vector3(0.8f, 1.1f, 0f);
            accessoryOverlay[0].SetActive(false); accessoryOverlay[1].SetActive(false); accessoryOverlay[2].SetActive(true);
            bonuses[1] = 3;  
        }
        UpdateBonusTooltip();
    }

    private void UpdateShoeIndicator(int x) {
        if (x == 1) {
            shoeBox.transform.position = new Vector3(-2.2f, -1.4f, 0f);
            shoeOverlay[0].SetActive(true); shoeOverlay[1].SetActive(false); shoeOverlay[2].SetActive(false);
            bonuses[2] = 1; 
        }

        if (x == 2) {
            shoeBox.transform.position = new Vector3(-0.75f, -1.4f, 0f);
            shoeOverlay[0].SetActive(false); shoeOverlay[1].SetActive(true); shoeOverlay[2].SetActive(false); 
            bonuses[2] = 2; 
        }

        if (x == 3) {
            shoeBox.transform.position = new Vector3(0.8f, -1.4f, 0f);
            shoeOverlay[0].SetActive(false); shoeOverlay[1].SetActive(false); shoeOverlay[2].SetActive(true); 
            bonuses[2] = 3; 
        }
        UpdateBonusTooltip();
    }

    private void UpdateTopIndicator(int x) {
        if (x == 1) {
            topBox.transform.position = new Vector3(2.9f, 2.8f, 0f);
            topOverlay[0].SetActive(true); topOverlay[1].SetActive(false); topOverlay[2].SetActive(false); 
            bonuses[3] = 1; 
        }

        if (x == 2) {
            topBox.transform.position = new Vector3(4.9f, 2.8f, 0f);
            topOverlay[0].SetActive(false); topOverlay[1].SetActive(true); topOverlay[2].SetActive(false); 
            bonuses[3] = 2; 
        }

        if (x == 3) {
            topBox.transform.position = new Vector3(7f, 2.8f, 0f);
            topOverlay[0].SetActive(false); topOverlay[1].SetActive(false); topOverlay[2].SetActive(true); 
            bonuses[3] = 3; 
        }
        UpdateBonusTooltip();
    }

    private void UpdateBottomIndicator(int x) {
        if (x == 1) {
            bottomBox.transform.position = new Vector3(2.8f, -0.3f, 0f);
            bottomOverlay[0].SetActive(true); bottomOverlay[1].SetActive(false); bottomOverlay[2].SetActive(false); 
            bonuses[4] = 1; 
        }

        if (x == 2) {
            bottomBox.transform.position = new Vector3(4.9f, -0.65f, 0f);
            bottomOverlay[0].SetActive(false); bottomOverlay[1].SetActive(true); bottomOverlay[2].SetActive(false); 
            bonuses[4] = 2; 
        }

        if (x == 3) {
            bottomBox.transform.position = new Vector3(7f, -0.35f, 0f);
            bottomOverlay[0].SetActive(false); bottomOverlay[1].SetActive(false); bottomOverlay[2].SetActive(true); 
            bonuses[4] = 3; 
        }
        UpdateBonusTooltip();
    }

    private void UpdateCategoryIndicator(int x) {

        if (x == 1) {   //hair
            categoryBox.transform.position = new Vector3(-0.6f, 3.5f, 0f);
        }

        if (x == 2) {   //accessory
            categoryBox.transform.position = new Vector3(-0.6f, 1.1f, 0f);
        }

        if (x == 3) {   //shoe
            categoryBox.transform.position = new Vector3(-0.75f, -1.4f, 0f);
        }

        if (x == 4) {   //top
            categoryBox.transform.position = new Vector3(4.9f, 2.8f, 0f);
        }

        if (x == 5) {   //bottom
            categoryBox.transform.position = new Vector3(4.9f, -0.65f, 0f);
        }

        if (x == 6) {   //start btn
            categoryBox.transform.position = new Vector3(7f, -3.7f, 0f);
            startText.text = "→ to start!";
        }
    }

    private void UpdateBonusTooltip() {
        double allocateScore = 0.0; int allocateLeniency = 0; int allocateCoolness = 0;

        for (int i = 0; i < 5; i++) {
            if (bonuses[i] == 1) {
                allocateScore += 0.2;
            }
            if (bonuses[i] == 2) {
                allocateLeniency += 10;
            }
            if (bonuses[i] == 3) {
                allocateCoolness -= 5;
            }
        }
        scoreMultiplierText.text = (1.0 + allocateScore).ToString(); DressUpStatBonuses.scoreMultiplier = (1.0 + allocateScore);
        leniencyText.text = (100 + allocateLeniency).ToString(); DressUpStatBonuses.leniency = (100 + allocateLeniency);
        coolnessText.text = (150 + allocateCoolness).ToString(); DressUpStatBonuses.scoreThreshold = (150 + allocateCoolness);
    }
}
