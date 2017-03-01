using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaitlinLongExtender : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(getBigger());
    }

    IEnumerator wait(bool a)
    {
        if (a)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(getBigger());
        }
        else
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(getSmaller());
        }
    }

    IEnumerator getBigger()
    {
        for (int i = 0; i < 50; i++)
        {
            this.transform.localScale = new Vector2(this.transform.localScale.x + 0.01f, this.transform.localScale.y);
            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(wait(false));
    }

    IEnumerator getSmaller()
    {
        for (int i = 0; i < 50; i++)
        {
            this.transform.localScale = new Vector2(this.transform.localScale.x - 0.01f, this.transform.localScale.y);
            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(wait(true));
    }
}
