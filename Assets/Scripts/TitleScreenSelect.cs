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
    public AudioSource menuMusic;
    [Space]
    public KeyCode inputUp;
    public KeyCode inputDown;
    public KeyCode inputRight;
    
    private int selectedCategory;

    void Start()
    {
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
                    SceneManager.LoadScene("StageSelectV2");
                    break;
                case 2:
                    menuMusic.Stop();
                    SceneManager.LoadScene("Credits");
                    break;
                case 3:
                    menuMusic.Stop();
                    Application.Quit();
                    break;
            }
        }
    }

    IEnumerator LoadLevel(int id) {
        yield return new WaitForSeconds(1);
        switch(id) {
            case 1:
                SceneManager.LoadScene("StageSelectV2");
                break; 
        }
        
    }
}
