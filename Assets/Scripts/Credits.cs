using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public KeyCode exit;

    void Update()
    {
        if (Input.GetKeyDown(exit)) {
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
