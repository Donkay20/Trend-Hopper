using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject readyUp;
    public KeyCode pause;
    public KeyCode up;
    public KeyCode down;
    public Animator pauseControl;

    private int selection;

    void Start() {
        selection = 0;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(pause))
        {
            if (GameIsPaused) {
                switch (selection) {
                    case 0:
                        Resume();
                        break;
                    case 1:
                        Restart();
                        break;
                    case 2:
                        MainMenu();
                        break;
                }
            } else {
                selection = 0;
                pauseControl.SetBool("mm", false);
                pauseControl.SetBool("restart", false);
                pauseControl.SetBool("resume", true);
                Pause();
            }
        }

        if (Input.GetKeyDown(down)) {
            switch(selection) {
                case 0:
                    pauseControl.SetBool("resume", false);
                    pauseControl.SetBool("restart", true);
                    selection = 1;
                    break;
                case 1:
                    pauseControl.SetBool("restart", false);
                    pauseControl.SetBool("mm", true);
                    selection = 2;
                    break;
                case 2:
                    //nothing
                    break;
            }
        }

        if (Input.GetKeyDown(up)) {
            switch(selection) {
                case 0:
                    //nothing
                    break;
                case 1:
                    pauseControl.SetBool("restart", false);
                    pauseControl.SetBool("resume", true);
                    selection = 0;
                    break;
                case 2:
                    pauseControl.SetBool("mm", false);
                    pauseControl.SetBool("restart", true);
                    selection = 1;
                    break;
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        StopAllCoroutines();
        StartCoroutine(GetReady());
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; AudioListener.pause = false; pauseMenuUI.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StageSelectV2");
        Time.timeScale = 1f; AudioListener.pause = false; pauseMenuUI.SetActive(false);
    }

    IEnumerator GetReady() {
        GameObject prefab = Instantiate(readyUp, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.0f);
        AudioListener.pause = false;
    }
}