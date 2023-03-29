using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManagerV2 : MonoBehaviour
{
    // Start is called before the first frame update

    private int phase;
    private int selectedLevel;
    private string difficulty;

    public KeyCode inputUp;
    public KeyCode inputDown;
    public KeyCode inputRight;
    public KeyCode inputLeft;
    public KeyCode cheatButton;

    public AudioSource preview1;
    public AudioSource preview2;
    //public AudioSource preview3;

    void Start()
    {   
        phase = 1;
        selectedLevel = 1;
        difficulty = "normal";
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
        }

        if (Input.GetKeyDown(cheatButton)) {
            Progress.levelOneCleared = true;
        }
    }
}

