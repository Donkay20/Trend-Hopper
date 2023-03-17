using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for each Note's behavior, such as when it should be destroyed (if not hit) and the movement down the screen.
*/

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;

    void Start() //reference point to the song
    {
        //timeInstantiated = SongManager.GetAudioSourceTime();
        timeInstantiated = assignedTime - SongManager.Instance.noteTime;
    }

    // Update is called once per frame
    void Update() //relies on the timing of the song to know when it should be destroyed
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));

        if (t > 1) //if it's past the hit window:
        {
            Destroy(gameObject);
        }
        else    //move it along the line
        {
            transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
