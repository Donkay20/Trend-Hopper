using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothingSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public static ClothingSelection Instance;
    public TMPro.TextMeshPro bonusHairText;
    public TMPro.TextMeshPro bonusTopText;
    public TMPro.TextMeshPro bonusBottomText;
    public TMPro.TextMeshPro bonusShoeText;
    public TMPro.TextMeshPro startText;
//  public TMPro.TextMeshPro backText;

    public KeyCode inputUp; public KeyCode inputUpAlt;
    public KeyCode inputDown; public KeyCode inputDownAlt;
    public KeyCode inputRight; public KeyCode inputRightAlt;
    public KeyCode inputLeft; public KeyCode inputLeftAlt;

    private int selectedCategory;
    private int selectedClothingHair;
    private int selectedClothingTop;
    private int selectedClothingBottom;
    private int selectedClothingShoe;

    public GameObject hairBox;
    public GameObject topBox;
    public GameObject bottomBox;
    public GameObject shoeBox;
    public GameObject categoryBox;

    void Start()
    {
        Instance = this;
        selectedCategory = 1;
        selectedClothingHair = 1;   //category 1
        selectedClothingTop = 1;    //category 2
        selectedClothingBottom = 1; //category 3
        selectedClothingShoe = 1;   //category 4
        startText.text = "Start!";  //category 5
    //  backText.text = "Back!";    //category 6

        categoryBox = Instantiate(categoryBox);
        hairBox = Instantiate(hairBox);
        topBox = Instantiate(topBox);
        bottomBox = Instantiate(bottomBox);
        shoeBox = Instantiate(shoeBox);

        UpdateCategoryIndicator(selectedCategory);
        UpdateHairIndicator(selectedClothingHair);
        UpdateTopIndicator(selectedClothingTop);
        UpdateBottomIndicator(selectedClothingBottom);
        UpdateShoeIndicator(selectedClothingShoe);
    }

    // Update is called once per frame
    void Update()
    {   

        if (selectedCategory != 5) {
            startText.text = "Start!";
        }
        //if (selectedCategory != 6) {
        //  backText.text = "Back!";
        //}

        if (Input.GetKeyDown(inputUp) || Input.GetKeyDown(inputUpAlt)) {            //go up a category
            if (selectedCategory == 1) {
                selectedCategory = 5;
            } else {
                selectedCategory--;
            }
            UpdateCategoryIndicator(selectedCategory);
        }

         if (Input.GetKeyDown(inputDown)) {         //go down a category
            if (selectedCategory == 5) {
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
                if (selectedClothingTop == 3) {
                    selectedClothingTop = 1;
                } 
                else  {
                    selectedClothingTop++;
                }
                UpdateTopIndicator(selectedClothingTop);
            }

            if (selectedCategory == 3) {
                if (selectedClothingBottom == 3) {
                    selectedClothingBottom = 1;
                } 
                else  {
                    selectedClothingBottom++;
                }
                UpdateBottomIndicator(selectedClothingBottom);
            }

            if (selectedCategory == 4) {
                if (selectedClothingShoe == 3) {
                    selectedClothingShoe = 1;
                } 
                else  {
                    selectedClothingShoe++;
                }
                UpdateShoeIndicator(selectedClothingShoe);
            }

            if (selectedCategory == 5) {
                SceneManager.LoadScene("RhythmGame");
            }
        }

         if (Input.GetKeyDown(inputLeft)) {         //go left within the category. if at the return button, go back to ???
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
                if (selectedClothingTop == 1) {
                    selectedClothingTop = 3;
                } 
                else  {
                    selectedClothingTop--;
                }
                UpdateTopIndicator(selectedClothingTop);
            }
            if (selectedCategory == 3) {
                if (selectedClothingBottom == 1) {
                    selectedClothingBottom = 3;
                } 
                else  {
                    selectedClothingBottom--;
                }
                UpdateBottomIndicator(selectedClothingBottom);
            }
            if (selectedCategory == 4) {
                if (selectedClothingShoe == 1) {
                    selectedClothingShoe = 3;
                } 
                else  {
                    selectedClothingShoe--;
                }
                UpdateShoeIndicator(selectedClothingShoe);
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
            bonusHairText.text = "Hitting notes is a little easier.";
        }

        if (x == 2) {
            hairBox.transform.position = new Vector3(0.5f, 3.53f, 0f);
            bonusHairText.text = "Score increased by 20%.";
        }

        if (x == 3) {
            hairBox.transform.position = new Vector3(1.7f, 3.54f, 0f);
            bonusHairText.text = "Start with more coolness.";
        }
        
    }

    private void UpdateTopIndicator(int x) {
        if (x == 1) {
            topBox.transform.position = new Vector3(-0.88f, 1.39f, 0f);
            bonusTopText.text = "Hitting notes is a little easier.";
        }

        if (x == 2) {
            topBox.transform.position = new Vector3(0.39f, 1.36f, 0f);
            bonusTopText.text = "Score increased by 20%.";
        }

        if (x == 3) {
            topBox.transform.position = new Vector3(1.69f, 1.39f, 0f);
            bonusTopText.text = "Start with more coolness.";
        }
    }

    private void UpdateBottomIndicator(int x) {
        if (x == 1) {
            bottomBox.transform.position = new Vector3(3.6f, 3.03f, 0f);
            bonusBottomText.text = "Hitting notes is a little easier.";
        }

        if (x == 2) {
            bottomBox.transform.position = new Vector3(5.58f, 3f, 0f);
            bonusBottomText.text = "Score increased by 20%.";
        }

        if (x == 3) {
            bottomBox.transform.position = new Vector3(7.41f, 2.97f, 0f);
            bonusBottomText.text = "Start with more coolness.";
        }
    }

    private void UpdateShoeIndicator(int x) {
        if (x == 1) {
            shoeBox.transform.position = new Vector3(-0.83f, -0.77f, 0f);
            bonusShoeText.text = "Hitting notes is a little easier.";
        }

        if (x == 2) {
            shoeBox.transform.position = new Vector3(0.44f, -0.79f, 0f);
            bonusShoeText.text = "Score increased by 20%.";
        }

        if (x == 3) {
            shoeBox.transform.position = new Vector3(1.71f, -0.76f, 0f);
            bonusShoeText.text = "Start with more coolness.";
        }
    }

    private void UpdateCategoryIndicator(int x) {

        if (x == 1) {
            categoryBox.transform.position = new Vector3(0.5f, 3.53f, 0f);
        }

        if (x == 2) {
            categoryBox.transform.position = new Vector3(0.39f, 1.36f, 0f);
        }

        if (x == 3) {
            categoryBox.transform.position = new Vector3(5.58f, 3f, 0f);
        }

        if (x == 4) {
            categoryBox.transform.position = new Vector3(0.44f, -0.79f, 0f);
        }

        if (x == 5) {
            categoryBox.transform.position = new Vector3(4f, -3.8f, 0f);
            startText.text = "→ to start!";
        }
        
        //if (x == 6) {
        //  categoryBox.transform.position = new Vector3(1f, -3.8f, 0f);
        //  backText.text = "← to return!";
        //}
    }
}
