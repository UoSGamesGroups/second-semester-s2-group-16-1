using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour {

    public bool trigger;
    public bool canRotate;
    public bool upsideDown;
    public GameObject parent;

	// Use this for initialization
	void Start () { canRotate = true; }
    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            StartCoroutine(flip());
                
        }
    }

    public IEnumerator flip()
    {
        trigger = false;
        canRotate = false;
        for (int i = 0; i < 22; i++)
        {
            if (!upsideDown)
                this.transform.RotateAround(new Vector3(parent.transform.position.x - 1.2f, parent.transform.position.y, 0), new Vector3(0f, 0f, -1f), 2);
            else
            {
                this.transform.RotateAround(new Vector3(parent.transform.position.x + 1.2f, parent.transform.position.y, 0), new Vector3(0f, 0f, -1f), 2);
            }
            yield return new WaitForSeconds(0.001f);
        }

        yield return new WaitForSeconds(0.4f);

        for (int i = 0; i < 22; i++)
        {
            if (!upsideDown)
                this.transform.RotateAround(new Vector3(parent.transform.position.x - 1.2f, parent.transform.position.y, 0), new Vector3(0f, 0f, 1f), 2);
            else
            {
                this.transform.RotateAround(new Vector3(parent.transform.position.x + 1.2f, parent.transform.position.y, 0), new Vector3(0f, 0f, 1f), 2);
            }
            yield return new WaitForSeconds(0.001f);
        }
        canRotate = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball" || collision.gameObject.tag == "player1" || collision.gameObject.tag == "player2")
        {
            if (canRotate)
            {
                StartCoroutine(flip());
            }
        }
    }
}
