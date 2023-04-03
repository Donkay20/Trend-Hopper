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
    public TMPro.TextMeshPro toolTipStart;
    public TMPro.TextMeshPro toolTipExit;
    //public TMPro.TextMeshPro startText;
    //public TMPro.TextMeshPro exitText;

    public Animator transition;
    public Animator startSplash;

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
        toolTipStart.text = "Press →";
        //startText.text = "> to Start!";
        //exitText.text = "Exit!";
        selector = Instantiate(selector);
        updateCategoryIndicatorPosition(selectedCategory);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedCategory != 1) {
            toolTipStart.text = "";
        }

        if (selectedCategory != 2) {
            toolTipExit.text = "";
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
                StartCoroutine(LoadLevel(1));
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
            selector.transform.position = new Vector3(0.96f, -1.65f, 0f);
            toolTipStart.text = "Press →";
        }
        if (pos == 2) {
            selector.transform.position = new Vector3(0.96f, -3.15f, 0f);
            toolTipExit.text = "Press ←";
        }
    }

    IEnumerator LoadLevel(int id) {
        transition.SetTrigger("trigger");
        startSplash.SetTrigger("trigger");
        yield return new WaitForSeconds(1);
        switch(id) {
            case 1:
                SceneManager.LoadScene("StageSelectV2");
                break; 
                
        }
        
    }
}
