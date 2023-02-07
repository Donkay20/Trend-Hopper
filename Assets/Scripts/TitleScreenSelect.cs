using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
This class manipulates the title screen. Pretty standard stuff tbh. Refer to the clothing selection script as it's implemented similarly.
*/

public class TitleScreenSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public static TitleScreenSelect Instance;
    public TMPro.TextMeshPro startText;
    public TMPro.TextMeshPro exitText;

    public KeyCode inputUp;
    public KeyCode inputDown;
    public KeyCode inputRight;
    public KeyCode inputLeft;

    private int selectedCategory;
    public GameObject selector;

    void Start()
    {
        Instance = this;
        selectedCategory = 1;
        startText.text = "> to Start!";
        exitText.text = "Exit!";
        selector = Instantiate(selector);
        updateCategoryIndicatorPosition(selectedCategory);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedCategory != 1) {
            startText.text = "Start!";
        }

        if (selectedCategory !=2) {
            exitText.text = "Exit!";
        }

        if (Input.GetKeyDown(inputUp)) {
            if (selectedCategory == 2) {
                selectedCategory = 1;
            } else {
                selectedCategory = 2;
            }
            updateCategoryIndicatorPosition(selectedCategory);
        }

        if (Input.GetKeyDown(inputDown)) {
            if (selectedCategory == 1) {
                selectedCategory = 2;
            } else {
                selectedCategory = 1;
            }
            updateCategoryIndicatorPosition(selectedCategory);
        }

        if (Input.GetKeyDown(inputRight)) {
            if (selectedCategory == 1) {
                SceneManager.LoadScene("DressUp");
            }
        }

        if (Input.GetKeyDown(inputLeft)) {
            if (selectedCategory == 2) {
                Application.Quit();
            }
            
        }
    }

    private void updateCategoryIndicatorPosition(int pos) {
        if (pos == 1) {
            selector.transform.position = new Vector3(-4.3f, -1.2f, 0f);
            startText.text = "> to Start!";
        }
        if (pos == 2) {
            selector.transform.position = new Vector3(-3.8f, -3.2f, 0f);
            exitText.text = "< to Exit!";
        }
    }
}
