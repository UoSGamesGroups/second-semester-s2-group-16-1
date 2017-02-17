using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChecker : MonoBehaviour {

    GameObject levelController;
    LevelController lc;

    GameObject canvas;
    CanvasController cc;

    void Start()
    {
        levelController = GameObject.Find("LevelController");
        lc = levelController.GetComponent<LevelController>();

        canvas = GameObject.Find("Canvas");
        cc = canvas.GetComponent<CanvasController>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "OutOfBounds")
        {
            if (this.gameObject.tag == "player1")
            {
                lc.mutatePlayerTwoScore(1);
                
            }
            else if (this.gameObject.tag == "player2")
            {
                lc.mutatePlayerOneScore(1);
            }
            StartCoroutine(waitToReset());
        }

        cc.UpdatePlayerScore();
    }

    IEnumerator waitToReset()
    {
        yield return new WaitForSeconds(0.25f);
        lc.ResetPlayers();
    }

}
