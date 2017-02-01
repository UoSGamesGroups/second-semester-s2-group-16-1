using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Vector2 mousePos;
    public Camera camera;
    Rigidbody2D rb;

    public GameObject bullet;

    public float velocityScale;

    bool isPulling;

    bool mouseDown;
    bool mouseOver;
    bool clickOnBall;

    // Use this for initialization
    void Start()
    {
        isPulling = false;
        mouseDown = false;
        mouseOver = false;
        clickOnBall = false;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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

            GameObject child;

            
            //Charlie prototype
            if (this.gameObject.name == "player1")
            {
                child = Instantiate(bullet, new Vector2(ballPos.x + 0.5f, ballPos.y), Quaternion.identity);
            }
            else if (this.gameObject.name == "player2")
            {
                child = Instantiate(bullet, new Vector2(ballPos.x - 0.5f, ballPos.y), Quaternion.identity);
            }

            //Caitlin prototypes
            else if (this.gameObject.name == "p1")
            {
                child = Instantiate(bullet, new Vector2(ballPos.x, ballPos.y + 0.5f), Quaternion.identity);
            }
            else if (this.gameObject.name == "p2")
            {
                child = Instantiate(bullet, new Vector2(ballPos.x, ballPos.y - 0.5f), Quaternion.identity);
            }

            else
            {
                child = null;
                Debug.Log("Child not set");
            }

            child.GetComponent<Rigidbody2D>().AddForce(new Vector2((ballPos.x - mousePosUponRelease.x) * velocityScale, (ballPos.y - mousePosUponRelease.y) * velocityScale), ForceMode2D.Impulse);

        }
    }
}
