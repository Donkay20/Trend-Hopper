using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is attached to the rhythm-game-feedback prefab, destroying it after a short time.
May need to be reworked to destroy as soon as another one spawns.
*/

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField] private float secondsToDestroy = 0.7f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, secondsToDestroy);
    }
}
