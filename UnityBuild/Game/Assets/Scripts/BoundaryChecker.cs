using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChecker : MonoBehaviour {

    GameObject levelController;
    LevelController lc;

    void Start()
    {
        levelController = GameObject.Find("LevelController");
        lc = levelController.GetComponent<LevelController>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "OutOfBounds")
        {
            if (this.gameObject.tag == "player1")
            {
                lc.mutatePlayerOneScore(1);
                
            }
            else if (this.gameObject.tag == "player2")
            {
                lc.mutatePlayerTwoScore(1);
            }
            StartCoroutine(waitToReset());
        }
    }

    IEnumerator waitToReset()
    {
        yield return new WaitForSeconds(2.0f);
        lc.ResetPlayers();
    }

}
