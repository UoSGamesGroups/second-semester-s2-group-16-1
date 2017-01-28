using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    int hitCount;

	// Use this for initialization
	void Start ()
    {
        hitCount = 0;
	}
	
    void OnCollisionEnter2D(Collision2D target)
    {

        if (target.gameObject.tag == "caitobj")
        {
            Destroy(this.gameObject);
        }

        hitCount++;

        if (target.gameObject.name == "player1" || target.gameObject.name == "player2")
        {
            Debug.Log("Hit player!");
            Destroy(this.gameObject);
        }

        if (hitCount >= 5)
        {
            Destroy(this.gameObject);
        }
    }

}
