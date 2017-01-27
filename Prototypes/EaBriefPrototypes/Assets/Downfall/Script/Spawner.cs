using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject ball;
    public Camera cam;

	
	// Update is called once per frame
	void Update ()
    {
        mouseCon();
	}

    void mouseCon()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(ball, new Vector3(pos.x, pos.y), Quaternion.identity);
        }
    }
}
