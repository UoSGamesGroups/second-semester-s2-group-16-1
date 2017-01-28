using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-4f, 0f), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D tar)
    {
        Destroy(this.gameObject);
    }
}
