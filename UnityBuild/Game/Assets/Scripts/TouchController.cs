using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    Rigidbody2D rb;
    public GameObject prefabBullet;

    GameObject child;

    float velocityScaleTimer;
    public float maxScale;
    public float scaleTimerDegrade;

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

    //Called every frame
    void mouseController()
    {
        //mouseDown holds whether mouse is down or not
        mouseDown = Input.GetMouseButton(0);

        //If mouse is down
        if (mouseDown)
        {
            //Reduce your velocity scale over time
            //(Holding longer = weaker bullet)
            if (velocityScaleTimer >= scaleTimerDegrade)
            {
                velocityScaleTimer -= scaleTimerDegrade;
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
            Vector2 playerPos = this.gameObject.transform.position;
            Vector2 mouseUpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //If our child isn't null (we already have an active bullet)
            if (child != null)
            {
                //Destroy it
                Destroy(child.gameObject);
            }

            //Spawn our child

            //Find the distance between the mouse upon release and the player
            float distance = Vector2.Distance(mouseUpPos, playerPos);

            //Figure our where abouts to spawn the ball around the circle
            Vector2 difference = new Vector2(playerPos.x - mouseUpPos.x, playerPos.y - mouseUpPos.y);

            float spawnDist = 0.5f;

            //Cap
            if (difference.x > spawnDist)
            {  difference = new Vector2(spawnDist, difference.y); }
            if (difference.x < -spawnDist)
            { difference = new Vector2(-spawnDist, difference.y); }
            if (difference.y > spawnDist)
            { difference = new Vector2(difference.x, spawnDist); }
            if (difference.y < -spawnDist)
            { difference = new Vector2(difference.x, -spawnDist); }

            //Spawn child bullet
            child = Instantiate(prefabBullet, new Vector2(playerPos.x - difference.x, playerPos.y - difference.y), Quaternion.identity);

            //Give it velocity relative to how quickly you released
            Vector2 ballVel = new Vector2(mouseUpPos.x - playerPos.x, mouseUpPos.y - playerPos.y).normalized;
            ballVel *= (velocityScaleTimer + distance);
            child.GetComponent<Rigidbody2D>().AddForce(ballVel, ForceMode2D.Impulse);

            //Reset the velocityScaleTimer
            velocityScaleTimer = maxScale;

        }




    }

    

    

    

}
