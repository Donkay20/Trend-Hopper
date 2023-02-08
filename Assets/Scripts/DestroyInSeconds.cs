using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField] private float secondsToDestroy = 0.7f;
    private static GameObject currentFeedback;

    void Start()
    {
        if (currentFeedback != null)
        {
            Destroy(currentFeedback);
        }
        currentFeedback = gameObject;
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(gameObject);
    }
}