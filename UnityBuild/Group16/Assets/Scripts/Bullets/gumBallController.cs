using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gumBallController : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 vel = rb.velocity;

        rb.velocity = new Vector2(vel.x * 0.2f, vel.y * 0.2f);
    }

}
