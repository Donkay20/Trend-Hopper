using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmFeedback : MonoBehaviour
{
    // Start is called before the first frame update
    public static RhythmFeedback Instance;
    public GameObject statTextPrefab;

    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showResult(string text) {
        if (statTextPrefab) 
        {
            GameObject prefab = Instantiate(statTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }
}
