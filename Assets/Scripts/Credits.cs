using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public KeyCode exit;
    public Animator transition;

    void Update()
    {
        if (Input.GetKeyDown(exit)) {
            StartCoroutine(LoadLevel(1));
        }
    }

    IEnumerator LoadLevel(int id) {
        transition.SetBool("exit", true);
        yield return new WaitForSeconds(1f);
        switch(id) {
            case 1:
                SceneManager.LoadScene("TitleScreen");
                break; 
        }   
    }
}
