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
    //public AudioSource preview2;
    //public AudioSource preview3;

    void Start()
    {   
        Instance = this;
        selector = Instantiate(selector);
        //preview1 = GetComponent<AudioSource>();
        //preview2 = GetComponent<AudioSource>();
        //preview3 = GetComponent<AudioSource>();

        phase = 1;
        selectedLevel = 1;
        difficulty = "normal";

        updateSelectorPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputUp)) {
            switch (phase) {
                case 1:
                    //todo
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
                    //todo
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
                    phase = 2;
                    break;
                case 2:
                    if (difficulty == "normal") {
                        SceneManager.LoadScene("DressUp");
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
                        preview1.Play();
                        break;
                    //case 2: todo when diff levels are avail.
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
}
