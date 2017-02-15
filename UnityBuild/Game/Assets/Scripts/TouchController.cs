using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    Rigidbody2D rb;
    public GameObject prefabRegularBall;
    public GameObject prefabBalloonBall;
    public GameObject prefabGumBall;
    public GameObject prefabSlimeBall;
    public GameObject prefabSteelBall;

    GameObject child;
    int currentBall = 0;

    float velocityScaleTimer;
    public float maxScale;
    public float scaleTimerDegrade;

    bool isPulling;
    bool mouseDown;
    bool mouseOver;
    bool clickOnBall;

    //Temporary
    bool deletePreviousBall = false;

    void changeBallDeletion()
    { deletePreviousBall = !deletePreviousBall; }

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
        //mouseController();

        touchController();
        

        




	}

    void touchController()
    {
        //If someone is touching
        if (Input.touchCount > 0)
        {
            //For all touches
            foreach (Touch touch in Input.touches)
            {
                Vector2 playerPos = this.gameObject.transform.position;
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Began)
                { 
                    if (Physics.Raycast(touchPos, playerPos))
                    {
                        clickOnBall = true;
                    }
                }


                if (touch.phase == TouchPhase.Ended)
                {
                    if (clickOnBall)
                    {
                        OnMouseUp();
                    }
                }

            }


        }


    }

    void OnMouseOver() { mouseOver = true; }
    void OnMouseExit() { mouseOver = false; }
    void OnMouseDown()
    {
        velocityScaleTimer = maxScale;
    }

    public void selectBall(int i)
    {
        currentBall = i;
        print(this.gameObject.tag +  " currentBall: " + currentBall);
    }

    //Called every frame
    void mouseController()
    {
        //mouseDown holds whether mouse is down or not
        //mouseDown = Input.GetMouseButton(0);

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

        //if (mouseOver && mouseDown)
        //{
        //    clickOnBall = true;
        //}
    }

    void OnMouseUp()
    {

        if (clickOnBall)
        {
            //Get the position of this object
            Vector2 playerPos = this.gameObject.transform.position;
            Vector2 mouseUpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (deletePreviousBall)
            {
                //If our child isn't null (we already have an active bullet)
                if (child != null)
                {
                    //Destroy it
                    Destroy(child.gameObject);
                }
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
            switch (currentBall)
            {
                case 1:
                    child = Instantiate(prefabBalloonBall, new Vector2(playerPos.x - difference.x, playerPos.y - difference.y), Quaternion.identity);
                    break;
                case 2:
                    child = Instantiate(prefabGumBall, new Vector2(playerPos.x - difference.x, playerPos.y - difference.y), Quaternion.identity);
                    break;
                case 3:
                    child = Instantiate(prefabSlimeBall, new Vector2(playerPos.x - difference.x, playerPos.y - difference.y), Quaternion.identity);
                    break;
                case 4:
                    child = Instantiate(prefabSteelBall, new Vector2(playerPos.x - difference.x, playerPos.y - difference.y), Quaternion.identity);
                    break;
                case 0:
                default:
                    child = Instantiate(prefabRegularBall, new Vector2(playerPos.x - difference.x, playerPos.y - difference.y), Quaternion.identity);
                    break;
            }

            

            //Give it velocity relative to how quickly you released
            Vector2 ballVel = new Vector2(mouseUpPos.x - playerPos.x, mouseUpPos.y - playerPos.y).normalized;
            ballVel *= (velocityScaleTimer + distance);
            child.GetComponent<Rigidbody2D>().AddForce(ballVel, ForceMode2D.Impulse);

            //Reset the velocityScaleTimer
            velocityScaleTimer = maxScale;

        }




    }

    

    

    

}
