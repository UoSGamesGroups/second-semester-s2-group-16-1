using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastBallController : MonoBehaviour {

    Vector2 mousePos;
    public Camera camera;
    Rigidbody2D rb;



    bool isPulling;

    bool mouseDown;
    bool mouseOver;
    bool clickOnBall;

	// Use this for initialization
	void Start ()
    {
        isPulling = false;
        mouseDown = false;
        mouseOver = false;
        clickOnBall = false;

        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MouseCon();
	}

    void velocityController()
    {
        
    }

    void MouseCon()
    {
        mouseDown = Input.GetMouseButton(0);
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);


        if (mouseOver && mouseDown)
        {
            clickOnBall = true;
        }
    }

    void OnMouseOver()
    {
        mouseOver = true;
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    void OnMouseExit()
    {
        mouseOver = false;
    }

    void OnMouseUp()
    {
        if (clickOnBall)
        {
            Vector2 mousePosUponRelease = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 ballPos = this.gameObject.transform.position;

            float distance = Vector2.Distance(mousePosUponRelease, ballPos);

            GetComponent<Rigidbody2D>().AddForce(new Vector2((ballPos.x - mousePosUponRelease.x) * 2.0f, (ballPos.y - mousePosUponRelease.y) * 2.0f), ForceMode2D.Impulse);

        }
    }

}
