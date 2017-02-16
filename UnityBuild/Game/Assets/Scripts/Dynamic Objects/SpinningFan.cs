using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningFan : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.gameObject.transform.Rotate(Vector3.forward, -1, Space.Self);
	}
}
