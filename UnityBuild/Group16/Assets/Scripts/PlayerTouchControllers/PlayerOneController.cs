using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneController : MonoBehaviour {

    GameObject gameController;
    gameHandler gc;

    GameObject levelController;
    LevelController lc;

    int currentBall;
    float velocityScaleTimer;
    public float maxScale;
    public float scaleTimerDegrade;
    bool touchOnBall;
    float touchActivationDistance;
    Touch thistouch;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.Find("gameHandler");
        gc = gameController.GetComponent<gameHandler>();

        levelController = GameObject.FindGameObjectWithTag("levelController");
        lc = levelController.GetComponent<LevelController>();

        touchOnBall = false;
        touchActivationDistance = 2;
	}
	
	// Update is called once per frame
	void Update ()
    {
        touchController();
	}

    void touchController()
    {

        if (Input.touchCount > 0)
        {

            Vector2 playerPos = this.gameObject.transform.position;
            Vector2 touchPos;

            if (!touchOnBall)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                        if (Vector2.Distance(touchPos, playerPos) < touchActivationDistance)
                        {
                            thistouch = touch;
                            touchOnBall = true;
                            return;
                        }
                    }
                }
            }
            else
            {
                if (thistouch.phase == TouchPhase.Stationary || thistouch.phase == TouchPhase.Moved)
                {
                    if (velocityScaleTimer >= 1)
                    {
                        velocityScaleTimer -= scaleTimerDegrade;
                    }

                    touchPos = Camera.main.ScreenToWorldPoint(thistouch.position);
                }

                if (thistouch.phase == TouchPhase.Ended)
                {
                    touchPos = Camera.main.ScreenToWorldPoint(thistouch.position);

                    touchOnBall = false;

                    if (Vector2.Distance(touchPos, playerPos) > touchActivationDistance)
                    {
                        shoot(touchPos);
                    }
                }
            }
        }
    }

    public void selectBall(int i)
    {
        currentBall = i;
    }

    void shoot(Vector2 touchReleasePos)
    {
        //Get the position of this object
        Vector2 playerPos = this.gameController.transform.position;

        //Child
        GameObject child = null;

        //Find the distance between the mouse upon release and the player
        float distance = Vector2.Distance(touchReleasePos, playerPos);

        //Figure out where the spawn the ball around the circle
        Vector2 difference = new Vector2(playerPos.x - touchReleasePos.x, playerPos.y - touchReleasePos.y);

        //How far "away" from the player to spawn the ball
        float spawnDist = 0.4f;

        //Cap
        if (difference.x > spawnDist)
        {
            difference = new Vector2(spawnDist, difference.y);
        }
        if (difference.x < -spawnDist)
        {
            difference = new Vector2(-spawnDist, difference.y);
        }
        if (difference.y > spawnDist)
        {
            difference = new Vector2(difference.x, spawnDist);
        }
        if (difference.y < -spawnDist)
        {
            difference = new Vector2(difference.x, -spawnDist);
        }

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

        //Reset the velocity scale timer
        velocityScaleTimer = maxScale;

        //Add this ball to the level controllers list of balls
        lc.currentBalls.Add(child);
        //Remove oldest balls if we have reached limit
        if (lc.currentBalls.Count >= 20)
        {
            Destroy(lc.currentBalls[0].gameObject);
            lc.currentBalls.RemoveAt(0);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(300, 80, 150, 20), "p2: " + touchOnBall);
    }
}
