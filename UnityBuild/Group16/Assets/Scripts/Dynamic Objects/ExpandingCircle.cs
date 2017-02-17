using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandingCircle : MonoBehaviour {

    bool gettingBigger;
    bool stayBigger;
    bool gettingSmaller;
    bool staySmaller;

	// Use this for initialization
	void Start ()
    {
        gettingBigger = false;
        stayBigger = true;
        gettingSmaller = false;
        staySmaller = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gettingBigger)
        {
            gettingBigger = false;
            StartCoroutine(waitGettingBigger());
        }
        else if (stayBigger)
        {
            stayBigger = false;
            StartCoroutine(waitStayBigger());
        }
        else if (gettingSmaller)
        {
            gettingSmaller = false;
            StartCoroutine(waitGettingSmaller());
        }
        else if (staySmaller)
        {
            staySmaller = false;
            StartCoroutine(waitStaySmaller());
        }
		
	}

    IEnumerator waitGettingBigger()
    {
        float xScale = transform.localScale.x;
        float yScale = transform.localScale.y;
        for (int i = 0; i < 100; i++)
        {
            xScale += 0.002f;
            yScale += 0.002f;
            transform.localScale = new Vector3(xScale, yScale);
            yield return new WaitForSeconds(0.01f);
        }
        stayBigger = true;
    }

    IEnumerator waitStayBigger()
    {
        yield return new WaitForSeconds(1.0f);
        gettingSmaller = true;
    }

    IEnumerator waitGettingSmaller()
    {
        float xScale = transform.localScale.x;
        float yScale = transform.localScale.y;
        for (int i = 0; i < 100; i++)
        {
            xScale -= 0.002f;
            yScale -= 0.002f;
            transform.localScale = new Vector3(xScale, yScale);
            yield return new WaitForSeconds(0.01f);
        }
        staySmaller = true;
    }

    IEnumerator waitStaySmaller()
    {
        yield return new WaitForSeconds(1.0f);
        gettingBigger = true;
    }

}
