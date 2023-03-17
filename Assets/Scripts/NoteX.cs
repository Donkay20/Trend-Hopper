using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for each Note's behavior, such as when it should be destroyed (if not hit) and the movement down the screen.
*/

public class NoteX : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;
    public GameObject assignedLane;
    public Lane lane;

    void Start() //reference point to the song
    {
        //timeInstantiated = SongManager.GetAudioSourceTime();
        timeInstantiated = assignedTime - SongManager.Instance.noteTime;
        lane = assignedLane.GetComponent<Lane>();
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
            transform.localPosition = Vector3.Lerp(Vector3.right * lane.noteSpawn, Vector3.right * lane.noteDespawn, t);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
