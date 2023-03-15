using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static StageSelectManager Instance;

    private int phase;
    private int selectedLevel;
    private string difficulty;

    public KeyCode inputUp;
    public KeyCode inputDown;
    public KeyCode inputRight;
    public KeyCode inputLeft;

    public GameObject selector;
    public AudioSource preview1;
    public AudioSource preview2;
    //public AudioSource preview3;
    
    public GameObject levelOne;
    public GameObject levelTwo;
    public GameObject levelThree;

    public GameObject levelTwoLock;
    //public GameObject levelThreeLock;
    

    void Start()
    {   
        Instance = this;
        selector = Instantiate(selector);
        phase = 1;
        selectedLevel = 1;
        difficulty = "normal";
        updateSelectorPosition();
        if(Progress.levelOneCleared) {
            levelTwoLock.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputUp)) {
            switch (phase) {
                case 1:
                    switch(selectedLevel) {
                        case 1:
                            break;
                        case 2:
                            selectedLevel--;
                            break;
                        case 3:
                            selectedLevel--;
                            break;
                    }
                    updateLevelShown();
                    break;
                case 2:
                    switch (difficulty) {
                    case "easy":
                        difficulty = "hard";
                        break;
                    case "normal":
                        difficulty = "easy";
                        break;
                    case "hard":
                        difficulty = "normal";
                        break;
                }
                    break;
            }
            updateSelectorPosition();
        }

        if (Input.GetKeyDown(inputDown)) {
            switch (phase) {
                case 1:
                    switch(selectedLevel) {
                        case 1:
                            selectedLevel++;
                            break;
                        case 2:
                            selectedLevel++;
                            break;
                        case 3:
                            break;
                    }
                    updateLevelShown();
                    break;
                case 2:
                    switch (difficulty) {
                        case "easy":
                            difficulty = "normal";
                            break;
                        case "normal":
                            difficulty = "hard";
                            break;
                        case "hard":
                            difficulty = "easy";
                            break;
                }
                    break;
            }
            updateSelectorPosition();
        }

        if (Input.GetKeyDown(inputRight)) {
            switch(phase) {
                case 1:
                    switch(selectedLevel) {
                        case 1:
                            phase = 2;
                            break;
                        case 2:
                            if(Progress.levelOneCleared) {
                                phase = 2;
                            }
                            break;
                        case 3:
                            if(Progress.levelTwoCleared) {
                                phase = 2;
                            }
                            break;
                    }
                    break;
                case 2:
                    switch(selectedLevel) {
                        case 1:
                            if (difficulty == "normal") {
                                SceneManager.LoadScene("Dialogue_Day1");
                            }
                            break;
                        case 2:
                            if (difficulty == "normal") {
                                SceneManager.LoadScene("Dialogue_Day2");
                            }
                            break;
                        //case 3: todo
                    }
                    break;
            }
            updateSelectorPosition();
        }

        if (Input.GetKeyDown(inputLeft)) {    
            switch(phase) {
                case 1:
                    SceneManager.LoadScene("TitleScreen");
                    break;
                case 2:
                    phase = 1;
                    break;
            }
            updateSelectorPosition();
        }
    }

    private void updateSelectorPosition() {
        switch(phase) {
            case 1:
                switch(selectedLevel) {
                    case 1:
                        selector.transform.position = new Vector3(-0.75f, 0.4f, 0f);
                        if (preview1.isPlaying) {
                            //do nothing
                        } else {
                            preview1.Play();
                            preview2.Stop();
                        }
                        break;
                    case 2:
                        selector.transform.position = new Vector3(-0.75f, 0.4f, 0f);
                        if (preview2.isPlaying) {
                            //do nothing
                        } else {
                            preview2.Play();
                            preview1.Stop();
                        }
                        break;
                    case 3:
                        selector.transform.position = new Vector3(-0.75f, 0.4f, 0f);
                        preview2.Stop();
                        preview1.Stop();
                        break;
                }
                break;
            case 2:
                switch(difficulty) {
                    case "easy":
                        selector.transform.position = new Vector3(4f, -0.3f, 0f);
                        break;
                    case "normal":
                        selector.transform.position = new Vector3(4f, -1.8f, 0f);
                        break;
                    case "hard":
                        selector.transform.position = new Vector3(4f, -3.3f, 0f);
                        break;
                }
                break;
        }
    }

    private void updateLevelShown() {
        switch (selectedLevel) {
            case 1:
                levelOne.transform.position = new Vector3(0f, 0f, 0f);
                levelTwo.transform.position = new Vector3(0f, 10f, 0f);
                levelThree.transform.position = new Vector3(0f, 10f, 0f);
                break;
            case 2:
                levelOne.transform.position = new Vector3(0f, 10f, 0f);
                levelTwo.transform.position = new Vector3(0f, 0f, 0f);
                levelThree.transform.position = new Vector3(0f, 10f, 0f);
                break;
            case 3:
                levelOne.transform.position = new Vector3(0f, 10f, 0f);
                levelTwo.transform.position = new Vector3(0f, 10f, 0f);
                levelThree.transform.position = new Vector3(0f, 0f, 0f);
                break;
        }
    }
}
