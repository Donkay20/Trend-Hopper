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

    public Animator arrow;
    public Animator transition;
    public AudioSource menuMusic;
    public ParticleSystem hopperSplash;
    [Space]
    public KeyCode inputUp;
    public KeyCode inputDown;
    public KeyCode inputRight;
    
    private int selectedCategory;

    void Start()
    {   
        StartCoroutine(playHopperSplashAfterDelay());
        Instance = this;
        menuMusic.Play();
        selectedCategory = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputUp)) {
            switch (selectedCategory) {
                case 1: break;
                case 2: 
                    arrow.SetBool("mid", false);
                    arrow.SetBool("up", true);
                    selectedCategory--;
                    break;
                case 3:
                    arrow.SetBool("bottom", false);
                    arrow.SetBool("mid", true);
                    selectedCategory--;
                    break;
            }
        }

        if (Input.GetKeyDown(inputDown)) {
            switch (selectedCategory) {
                case 1:
                    arrow.SetBool("up", false);
                    arrow.SetBool("mid", true);
                    selectedCategory++;
                    break;
                case 2: 
                    arrow.SetBool("mid", false);
                    arrow.SetBool("bottom", true);
                    selectedCategory++;
                    break;
                case 3: break;
            }
        }

        if (Input.GetKeyDown(inputRight)) {
            switch (selectedCategory) {
                case 1:
                    menuMusic.Stop();
                    if (Progress.introSeen) {
                        StartCoroutine(LoadLevel(1));
                    } else {
                        StartCoroutine(LoadLevel(3));
                    }
                    break;
                case 2:
                    menuMusic.Stop();
                    StartCoroutine(LoadLevel(2));
                    break;
                case 3:
                    menuMusic.Stop();
                    Application.Quit();
                    break;
            }
        }
    }

    IEnumerator playHopperSplashAfterDelay() {
        yield return new WaitForSeconds(2f);
        hopperSplash.Play();
    }
 
    IEnumerator LoadLevel(int id) {
        transition.SetBool("exit", true);
        yield return new WaitForSeconds(1f);
        switch(id) {
            case 1:
                SceneManager.LoadScene("StageSelectV2");
                break; 
            case 2:
                SceneManager.LoadScene("Credits");
                break;
            case 3:
                SceneManager.LoadScene("Intro");
                break;
        }
        
    }
}
