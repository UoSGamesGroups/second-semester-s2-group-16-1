using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public bool movingUp;
    bool canMove;
    static float speed = 0.025f;
    float end = 3.4f;
    
    void Start()
    {
        canMove = true;
    }

	// Update is called once per frame
	void Update ()
    {
		if (movingUp && canMove)
        {
            moveUp();
        }
        else if (!movingUp && canMove)
        {
            moveDown();
        }

        check();
	}

    void check()
    {
        if (movingUp)
        {
            if (this.transform.position.y == end)
            {
                movingUp = false;
                StartCoroutine(wait());
            }
        }
        else if (!movingUp)
        {
            if (this.transform.position.y == -end)
            {
                movingUp = true;
                StartCoroutine(wait());
            }
        }
    }

    IEnumerator wait()
    {
        canMove = false;
        yield return new WaitForSeconds(1.0f);
        canMove = true;
    }

    void moveDown()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, -end), speed);
    }

    void moveUp()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, end), speed);
    }

}
