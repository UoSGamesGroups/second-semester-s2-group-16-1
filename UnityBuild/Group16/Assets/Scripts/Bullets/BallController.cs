using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    float regularDrag;

	// Use this for initialization
	void Start ()
    {
        UpdateFriction();
        regularDrag = this.GetComponent<Rigidbody2D>().angularDrag;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateFriction()
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

        GameObject gamehandlerObj = GameObject.Find("gameHandler");
        gameHandler gh = gamehandlerObj.GetComponent<gameHandler>();

        switch (gh.gameTerrain)
        {
            case LevelController.LevelTerrain.terrain_ice:
                rb.angularDrag = regularDrag * 2;
                break;
            case LevelController.LevelTerrain.terrain_sand:
                rb.angularDrag = regularDrag * 0.5f;
                break;
            case LevelController.LevelTerrain.terrain_no:
            default:
                rb.angularDrag = regularDrag;
                break;
        }

    }
}
