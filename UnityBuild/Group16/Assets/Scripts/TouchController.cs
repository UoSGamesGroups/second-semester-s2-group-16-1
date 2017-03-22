using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchController : MonoBehaviour
{

    GameObject gameController;
    gameHandler gc;

    GameObject levelController;
    LevelController lc;

    int currentBall = 1;

    int currentTouch;

    float velocityScaleTimer;
    public float maxScale; //7
    public float scaleTimerDegrade; //0.15
    float touchActivationDistance = 2f;
    bool touchOnBall;

    

    // Use this for initialization
    void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("levelController");
        lc = levelController.GetComponent<LevelController>();

        gameController = GameObject.Find("gameHandler");
        gc = gameController.GetComponent<gameHandler>();

        Input.multiTouchEnabled = true;
        touchOnBall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gc.gameOver)
        {
            touchController();
        }
    }

    void touchController()
    {
        //If someone is touching
        if (Input.touchCount > 0)
        {
            //Hold playerpos and touch pos
            Vector2 PlayerPos = this.gameObject.transform.position;
            Vector2 touchPos = new Vector2(0, 0);


            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                    if (Vector2.Distance(touchPos, PlayerPos) < 2f)
                    {
                        currentTouch = touch.fingerId;
                        touchOnBall = true;
                        //break;
                    }
                }
            }

            if (touchOnBall)
            {
                //Hold
                if (Input.GetTouch(currentTouch).phase == TouchPhase.Stationary || Input.GetTouch(currentTouch).phase == TouchPhase.Moved)
                {
                    if (velocityScaleTimer >= 1)
                    {
                        velocityScaleTimer -= scaleTimerDegrade;
                    }
                    touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(currentTouch).position);
                }
                //Release
                else if (Input.GetTouch(currentTouch).phase == TouchPhase.Ended)
                {
                    touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(currentTouch).position);

                    //If the finger moves enough...
                    if (Vector2.Distance(PlayerPos, touchPos) > touchActivationDistance)
                    {
                        shoot(touchPos);
                        touchOnBall = false;
                        velocityScaleTimer = maxScale;
                    }
                    //Else if it's just a tap
                    else
                    {
                        touchOnBall = false;
                    }
                }
            }
        }
    }

    public void selectBall(int i)
    {
        currentBall = i;
        print(this.gameObject.tag + " currentBall: " + currentBall);
    }

    public void shoot(Vector2 touchReleasePos)
    {
        //Get the position of this object
        Vector2 playerPos = this.gameObject.transform.position;
        //Vector2 mouseUpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Spawn our child
        GameObject child = null;

        //Find the distance between the mouse upon release and the player
        float distance = Vector2.Distance(touchReleasePos, playerPos);

        //Figure our where abouts to spawn the ball around the circle
        Vector2 difference = new Vector2(playerPos.x - touchReleasePos.x, playerPos.y - touchReleasePos.y);

        float spawnDist = 0.4f;

        //Cap
        if (difference.x > spawnDist)
        { difference = new Vector2(spawnDist, difference.y); }
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
                child = Instantiate(gc.prefab_balloonBall, new Vector2(playerPos.x - difference.x, playerPos.y - difference.y), Quaternion.identity);
                break;
            case 2:
                child = Instantiate(gc.prefab_gumBall, new Vector2(playerPos.x - difference.x, playerPos.y - difference.y), Quaternion.identity);
                break;
            case 3:
                child = Instantiate(gc.prefab_slimeBall, new Vector2(playerPos.x - difference.x, playerPos.y - difference.y), Quaternion.identity);
                break;
            case 4:
                child = Instantiate(gc.prefab_steelBall, new Vector2(playerPos.x - difference.x, playerPos.y - difference.y), Quaternion.identity);
                break;
            case 0:
            default:
                child = Instantiate(gc.prefab_regularBall, new Vector2(playerPos.x - difference.x, playerPos.y - difference.y), Quaternion.identity);
                break;
        }

        //Give it velocity relative to how quickly you released
        Vector2 ballVel = new Vector2(touchReleasePos.x - playerPos.x, touchReleasePos.y - playerPos.y).normalized;
        ballVel *= (velocityScaleTimer + distance);
        child.GetComponent<Rigidbody2D>().AddForce(ballVel, ForceMode2D.Impulse);

        //Reset the velocityScaleTimer
        velocityScaleTimer = maxScale;

        //Add this ball to the level controllers list of balls
        lc.currentBalls.Add(child);
        //Remove oldest ball if we have reached limit
        if (lc.currentBalls.Count >= 20)
        {
            Destroy(lc.currentBalls[0].gameObject);
            lc.currentBalls.RemoveAt(0);
        }
    }

    //void OnGUI()
    //{
    //    Vector2 playeronepos = new Vector2(0, 0);
    //    Vector2 playertwopos = new Vector2(0, 0);

    //    if (this.gameObject.tag == "player1")
    //    {
    //        playeronepos = this.gameObject.transform.position;
    //        GUI.Label(new Rect(20, 20, 100, 20), "p1: " + playeronepos.x + " " + playeronepos.y);

    //        GUI.Label(new Rect(150, 50, 100, 20), "p1 touchId: " + currentTouch);
            
    //        GUI.Label(new Rect(300, 50, 150, 20), "p1: " + touchOnBall);
    //    }
            
    //    if (this.gameObject.tag == "player2")
    //    {
    //        playertwopos = this.gameObject.transform.position;
    //        GUI.Label(new Rect(20, 50, 100, 20), "p2: " + playertwopos.x + " " + playertwopos.y);

    //        GUI.Label(new Rect(150, 80, 100, 20), "p2 touchId: " + currentTouch);

    //        GUI.Label(new Rect(300, 80, 150, 20), "p2: " + touchOnBall);
    //    }


    //    if (Input.touchCount > 0)
    //    {
    //        for (int i = 0; i < Input.touchCount; i++)
    //        {
    //            Vector2 tempTouchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

    //            string toPrint = "touch ";
    //            toPrint += i;
    //            toPrint += " ";
    //            toPrint += System.Math.Round(tempTouchPos.x, 2);
    //            toPrint += " , ";
    //            toPrint += System.Math.Round(tempTouchPos.y, 2);

    //            GUI.Label(new Rect(20, (50 * (i + 2)), 500, 20), toPrint );

    //        }
    //    }
    //}
}
