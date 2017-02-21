using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regBulletController : MonoBehaviour
{
    public float secondsToLive;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(deathTimer());
    }

    IEnumerator deathTimer()
    {
        yield return new WaitForSeconds(secondsToLive);
        Destroy(this.gameObject);
    }

}
