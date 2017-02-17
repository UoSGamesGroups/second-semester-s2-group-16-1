using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchController : MonoBehaviour
{

    Rigidbody2D rb;
    public GameObject prefabRegularBall;
    public GameObject prefabBalloonBall;
    public GameObject prefabGumBall;
    public GameObject prefabSlimeBall;
    public GameObject prefabSteelBall;

    GameObject child;
    int currentBall = 1;

    int currentTouch;

    float velocityScaleTimer;
    public float maxScale;
    public float scaleTimerDegrade;

    bool isPulling;
    bool mouseDown;
    bool mouseOver;
    bool clickOnBall;

    // Use this for initialization
    void Start()
    {
        Input.multiTouchEnabled = true;
        isPulling = mouseDown = mouseOver = clickOnBall = false;
        child = null;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        touchController();
    }

    void touchController()
    {
        //If someone is touching
        if (Input.touchCount > 0)
        {
            //Hold playerpos and touch pos
            Vector2 PlayerPos = this.gameObject.transform.position;
            Vector2 touchPos = new Vector2(0, 0);

            if (clickOnBall)
            {
                touchPos = touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(currentTouch).position);
            }

            if (!clickOnBall)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {

                        touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                        if (Vector2.Distance(touchPos, PlayerPos) < 0.75f)
                        {
                            currentTouch = touch.fingerId;
                            clickOnBall = true;
                            break;
                        }
                    }
                }
            }


            //Hold
            if (Input.GetTouch(currentTouch).phase == TouchPhase.Stationary || Input.GetTouch(currentTouch).phase == TouchPhase.Moved)
            {

                if (clickOnBall)
                {
                    velocityScaleTimer -= scaleTimerDegrade;
                    touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(currentTouch).position);
                }
            }
            //Release
            if (Input.GetTouch(currentTouch).phase == TouchPhase.Ended)
            {

                //If the finger moves enough...
                if (Vector2.Distance(PlayerPos, touchPos) > 2)
                {
                    if (clickOnBall)
                    {
                        shoot(touchPos);
                        clickOnBall = false;
                    }
                    velocityScaleTimer = maxScale;
                }
                //Else if it's just a tap
                else if (Vector2.Distance(PlayerPos, touchPos) < 2)
                {
                    //and we're on the right scene
                    if (SceneManager.GetActiveScene().buildIndex == 3)
                    {
                        GameObject canvas = GameObject.Find("Canvas");
                        CanvasController cc = canvas.GetComponent<CanvasController>();

                        if (this.gameObject.tag == "player1")
                        {
                            cc.showPlayerOneBallGUI(true);
                        }
                        else if (this.gameObject.tag == "player2")
                        {
                            cc.showPlayerTwoBallGUI(true);
                        }
                       
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

    void shoot(Vector2 touchReleasePos)
    {
        //Get the position of this object
        Vector2 playerPos = this.gameObject.transform.position;
        //Vector2 mouseUpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //If our child isn't null (we already have an active bullet)
        if (child != null)
        {
            //Destroy it
            Destroy(child.gameObject);
        }

        //Spawn our child

        //Find the distance between the mouse upon release and the player
        float distance = Vector2.Distance(touchReleasePos, playerPos);

        //Figure our where abouts to spawn the ball around the circle
        Vector2 difference = new Vector2(playerPos.x - touchReleasePos.x, playerPos.y - touchReleasePos.y);

        float spawnDist = 0.5f;

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
        Vector2 ballVel = new Vector2(touchReleasePos.x - playerPos.x, touchReleasePos.y - playerPos.y).normalized;
        ballVel *= (velocityScaleTimer + distance);
        child.GetComponent<Rigidbody2D>().AddForce(ballVel, ForceMode2D.Impulse);

        //Reset the velocityScaleTimer
        velocityScaleTimer = maxScale;
    }

    void OnGUI()
    {
        Vector2 playeronepos = new Vector2(0, 0);
        Vector2 playertwopos = new Vector2(0, 0);

        if (this.gameObject.tag == "player1")
        {
            playeronepos = this.gameObject.transform.position;
            GUI.Label(new Rect(20, 20, 100, 20), "p1: " + playeronepos.x + " " + playeronepos.y);

            GUI.Label(new Rect(150, 50, 100, 20), "p1 touchId: " + currentTouch);
            
            GUI.Label(new Rect(300, 50, 150, 20), "p1: " + clickOnBall);
        }
            
        if (this.gameObject.tag == "player2")
        {
            playertwopos = this.gameObject.transform.position;
            GUI.Label(new Rect(20, 50, 100, 20), "p2: " + playertwopos.x + " " + playertwopos.y);

            GUI.Label(new Rect(150, 80, 100, 20), "p2 touchId: " + currentTouch);

            GUI.Label(new Rect(300, 80, 150, 20), "p2: " + clickOnBall);
        }


        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector2 tempTouchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

                string toPrint = "touch ";
                toPrint += i;
                toPrint += " ";
                toPrint += System.Math.Round(tempTouchPos.x, 2);
                toPrint += " , ";
                toPrint += System.Math.Round(tempTouchPos.y, 2);

                GUI.Label(new Rect(20, (50 * (i + 2)), 500, 20), toPrint );

            }
        }
    }
}
