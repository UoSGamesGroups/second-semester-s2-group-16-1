using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaitlinElectric : MonoBehaviour {

    public GameObject top;
    public GameObject bot;

    bool goingUp;
    bool stayUp;
    bool goingDown;
    bool stayDown;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(waitGoingUp());
    }

    void Update()
    {
        if (goingUp)
        {
            goingUp = false;
            StartCoroutine(waitGoingUp());
        }
        else if (stayUp)
        {
            stayUp = false;
            StartCoroutine(waitStayUp());
        }
        else if (goingDown)
        {
            goingDown = false;
            StartCoroutine(waitGoingDown());
        }
        else if (stayDown)
        {
            stayDown = false;
            StartCoroutine(waitStayDown());
        }
    }

    IEnumerator waitGoingUp()
    {
        for (int i = 0; i < 100; i++)
        {
            top.transform.position = new Vector2(top.transform.position.x, top.transform.position.y + 0.01f);
            bot.transform.position = new Vector2(bot.transform.position.x, bot.transform.position.y - 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        stayUp = true;
    }

    IEnumerator waitStayUp()
    {
        yield return new WaitForSeconds(2.0f);
        goingDown = true;
    }

    IEnumerator waitGoingDown()
    {
        for (int i = 0; i < 100; i++)
        {
            top.transform.position = new Vector2(top.transform.position.x, top.transform.position.y - 0.01f);
            bot.transform.position = new Vector2(bot.transform.position.x, bot.transform.position.y + 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        stayDown = true;
    }

    IEnumerator waitStayDown()
    {
        yield return new WaitForSeconds(2.0f);
        goingUp = true;
    }

}
