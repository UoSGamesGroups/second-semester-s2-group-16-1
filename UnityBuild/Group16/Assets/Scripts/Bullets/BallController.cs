using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public enum ballType
    {
        none,
        balloon,
        steel,
        gum,
        slime,
        fire,
        smoke
    }

    float regularDrag;
    float regularMass;
    float regularBounciness;

    Rigidbody2D rb;

    // Use this for initialization
    void Start ()
    {
        rb = this.GetComponent<Rigidbody2D>();

        regularDrag = rb.drag;
        regularMass = rb.mass;
        regularBounciness = rb.sharedMaterial.bounciness;

        UpdateFriction();
	}

    public void UpdateFriction()
    {
        GameObject gamehandlerObj = GameObject.Find("gameHandler");
        gameHandler gh = gamehandlerObj.GetComponent<gameHandler>();

        switch (gh.gameTerrain)
        {
            case LevelController.LevelTerrain.terrain_ice:
                rb.drag = regularDrag * 0.5f;
                rb.mass = regularMass * 0.5f;
                break;
            case LevelController.LevelTerrain.terrain_sand:
                rb.drag = regularDrag * 2;
                rb.mass = regularMass * 2;
                break;
            case LevelController.LevelTerrain.terrain_rubber:
                rb.sharedMaterial.bounciness *= 1.1f; //10% increase
                break;
            case LevelController.LevelTerrain.terrain_no:
            default:
                rb.drag = regularDrag;
                rb.mass = regularMass;
                rb.sharedMaterial.bounciness = regularBounciness;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D tar)
    {
        if (tar.tag == "ballDeath")
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D tar)
    {
        if (tar.tag == "caitlinFanLeft")
        {
            Vector2 currentVel = rb.velocity;

            currentVel.x += 0.1f;

            rb.velocity = currentVel;
        }
        else if (tar.tag == "caitlinFanRight")
        {
            Vector2 currentVel = rb.velocity;

            currentVel.x -= 0.1f;

            rb.velocity = currentVel;
        }
    }
}
