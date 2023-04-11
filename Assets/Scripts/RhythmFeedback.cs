using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Whatever this class is attached to will have the feedback rhythm images sent from it when the user hits/misses a note during the rhythm game.
Unless changes are made to change the text to an asset, probably nothing needs changes here.
*/
public class RhythmFeedback : MonoBehaviour
{
    // Start is called before the first frame update
    public static RhythmFeedback Instance;
    public GameObject basedPrefab;
    public GameObject midPrefab;
    public GameObject cringePrefab;

    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showResult(string text) {   //accepts the text to insert in the prefab as an argument
        switch (text) {
            case "based":
                GameObject prefab = Instantiate(basedPrefab, transform.position, Quaternion.identity);   //idk what this is doing here tbh
                break;
            case "mid":
                GameObject prefab2 = Instantiate(midPrefab, transform.position, Quaternion.identity);
                break;
            case "cringe":
                GameObject prefab3 = Instantiate(cringePrefab, transform.position, Quaternion.identity);
                break;
        }
    }
}
