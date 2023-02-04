using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothingSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public static ClothingSelection Instance;
//  public TMPro.TextMeshPro bonusHairText;
//  public TMPro.TextMeshPro bonusTopText;
//  public TMPro.TextMeshPro bonusBottomText;
//  public TMPro.TextMeshPro bonusShoeText;
    public TMPro.TextMeshPro startText;
//  public TMPro.TextMeshPro backText;

    public KeyCode inputUp;
    public KeyCode inputDown;
    public KeyCode inputRight;
    public KeyCode inputLeft;

    private int selectedCategory;
    private int selectedClothingHair;
    private int selectedClothingAccessory;
    private int selectedClothingShoe;
    private int selectedClothingTop;
    private int selectedClothingBottom;

    public GameObject hairBox;
    public GameObject accessoryBox;
    public GameObject shoeBox;
    public GameObject topBox;
    public GameObject bottomBox;
    public GameObject categoryBox;

    void Start()
    {
        Instance = this;
        selectedCategory = 1;           
        selectedClothingHair = 1;       //category 1
        selectedClothingAccessory = 1;  //category 2
        selectedClothingShoe = 1;       //category 3
        selectedClothingTop = 1;        //category 4
        selectedClothingBottom = 1;     //category 5
        startText.text = "Start!";      //category 6
    //  backText.text = "Back!";

        categoryBox = Instantiate(categoryBox);
        hairBox = Instantiate(hairBox);
        accessoryBox = Instantiate(accessoryBox);
        shoeBox = Instantiate(shoeBox);
        topBox = Instantiate(topBox);
        bottomBox = Instantiate(bottomBox);

        UpdateCategoryIndicator(selectedCategory);
        UpdateHairIndicator(selectedClothingHair);
        UpdateAccessoryIndicator(selectedClothingAccessory);
        UpdateShoeIndicator(selectedClothingShoe);
        UpdateTopIndicator(selectedClothingTop);
        UpdateBottomIndicator(selectedClothingBottom);
    }

    // Update is called once per frame
    void Update()
    {   

        if (selectedCategory != 6) {
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
            
            //if (selectedCategory == 6) {
                //TODO
                //go back to world hub?? screen
                //defunct
            //}
        }
    }

    private void UpdateHairIndicator(int x) {
        if (x == 1) {
            hairBox.transform.position = new Vector3(-0.76f, 3.53f, 0f);
        }

        if (x == 2) {
            hairBox.transform.position = new Vector3(0.5f, 3.53f, 0f);
        }

        if (x == 3) {
            hairBox.transform.position = new Vector3(1.7f, 3.54f, 0f);
        }
        
    }

    private void UpdateAccessoryIndicator(int x) {
        if (x == 1) {
            accessoryBox.transform.position = new Vector3(-0.82f, 1.44f, 0f);
        }

        if (x == 2) {
            accessoryBox.transform.position = new Vector3(0.53f, 1.42f, 0f);
        }

        if (x == 3) {
            accessoryBox.transform.position = new Vector3(1.84f, 1.42f, 0f);
        }
        
    }

    private void UpdateShoeIndicator(int x) {
        if (x == 1) {
            shoeBox.transform.position = new Vector3(-0.83f, -0.77f, 0f);
        }

        if (x == 2) {
            shoeBox.transform.position = new Vector3(0.44f, -0.79f, 0f);
        }

        if (x == 3) {
            shoeBox.transform.position = new Vector3(1.71f, -0.76f, 0f);
        }
    }

    private void UpdateTopIndicator(int x) {
        if (x == 1) {
            topBox.transform.position = new Vector3(3.8f, 3f, 0f);
        }

        if (x == 2) {
            topBox.transform.position = new Vector3(5.52f, 3f, 0f);
        }

        if (x == 3) {
            topBox.transform.position = new Vector3(7.28f, 3f, 0f);
        }
    }

    private void UpdateBottomIndicator(int x) {
        if (x == 1) {
            bottomBox.transform.position = new Vector3(3.71f, 0f, 0f);
        }

        if (x == 2) {
            bottomBox.transform.position = new Vector3(5.62f, 0f, 0f);
        }

        if (x == 3) {
            bottomBox.transform.position = new Vector3(7.41f, 0f, 0f);
        }
    }

    private void UpdateCategoryIndicator(int x) {

        if (x == 1) {   //hair
            categoryBox.transform.position = new Vector3(0.5f, 3.53f, 0f);
        }

        if (x == 2) {   //accessory
            categoryBox.transform.position = new Vector3(0.53f, 1.42f, 0f);
        }

        if (x == 3) {   //shoe
            categoryBox.transform.position = new Vector3(0.44f, -0.79f, 0f);
        }

        if (x == 4) {   //top
            categoryBox.transform.position = new Vector3(5.52f, 3f, 0f);
        }

        if (x == 5) {   //bottom
            categoryBox.transform.position = new Vector3(5.62f, 0f, 0f);
        }

        if (x == 6) {   //start btn
            categoryBox.transform.position = new Vector3(4f, -3.8f, 0f);
            startText.text = "→ to start!";
        }
        
        //if (x == 6) {
        //  categoryBox.transform.position = new Vector3(1f, -3.8f, 0f);
        //  backText.text = "← to return!";
        //}
    }
}
