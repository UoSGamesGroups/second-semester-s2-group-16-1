using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    float regularDrag;
    float regularMass;
    float regularBounciness;

	// Use this for initialization
	void Start ()
    {
        regularDrag = this.GetComponent<Rigidbody2D>().drag;
        regularMass = this.GetComponent<Rigidbody2D>().mass;
        regularBounciness = this.GetComponent<Rigidbody2D>().sharedMaterial.bounciness;

        UpdateFriction();
	}

    public void UpdateFriction()
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

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
}
