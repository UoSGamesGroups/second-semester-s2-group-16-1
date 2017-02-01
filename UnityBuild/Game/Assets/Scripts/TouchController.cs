using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    Rigidbody2D rb;
    public GameObject prefabBullet;

    GameObject child;

    float velocityScaleTimer;
    public float maxScale;

    bool isPulling;
    bool mouseDown;
    bool mouseOver;
    bool clickOnBall;

	// Use this for initialization
	void Start ()
    {
        isPulling = mouseDown = mouseOver = clickOnBall = false;
        child = null;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        mouseController();
	}

    void OnMouseOver() { mouseOver = true; }
    void OnMouseExit() { mouseOver = false; }
    void OnMouseDown()
    {
        velocityScaleTimer = maxScale;
    }

    void mouseController()
    {
        mouseDown = Input.GetMouseButton(0);

        if (mouseDown)
        {
            if (velocityScaleTimer >= 0.05f)
            {
                velocityScaleTimer -= 0.05f;
                Debug.Log(gameObject.tag + ": " + velocityScaleTimer);
            }   
        }

        if (mouseOver && mouseDown)
        {
            clickOnBall = true;
        }
    }

    void OnMouseUp()
    {

        if (clickOnBall)
        {
            //Get the position of this object
            Vector2 ballPos = this.gameObject.transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //If our child isn't null (we already have an active bullet)
            if (child != null)
            {
                //Destroy it
                Destroy(child.gameObject);
            }

            //Spawn our child
            if (this.gameObject.tag == "player1")
            {
                child = Instantiate(prefabBullet, new Vector2(ballPos.x + 0.5f, ballPos.y), Quaternion.identity);
            }
            else if (this.gameObject.tag == "player2")
            {
                child = Instantiate(prefabBullet, new Vector2(ballPos.x - 0.5f, ballPos.y), Quaternion.identity);
            }
            else { child = null; }

            //Give it velocity relative to how quickly you released
            Vector2 ballVel = new Vector2(mousePos.x - ballPos.x, mousePos.y - ballPos.y).normalized;
            ballVel *= velocityScaleTimer;
            child.GetComponent<Rigidbody2D>().AddForce(ballVel, ForceMode2D.Impulse);

            //Reset the velocityScaleTimer
            velocityScaleTimer = maxScale;

        }




    }

    

    

    

}
