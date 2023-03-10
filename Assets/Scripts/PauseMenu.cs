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

    void Start() {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(pause))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
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
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator GetReady() {
        GameObject prefab = Instantiate(readyUp, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.0f);
        AudioListener.pause = false;
    }
}