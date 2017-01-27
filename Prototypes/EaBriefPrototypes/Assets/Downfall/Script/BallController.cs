using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.name == "Floor" || target.gameObject.name == "squRed" || target.gameObject.name == "squRedRight")
        {
            Destroy(this.gameObject);
        }
    }
}
