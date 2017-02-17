using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public Text playerOneScore;
    public Text playerTwoScore;

    GameObject levelController;
    LevelController lc;

	// Use this for initialization
	void Start ()
    {
        levelController = GameObject.Find("LevelController");
        lc = levelController.GetComponent<LevelController>();
	}

    public void UpdatePlayerScore()
    {
        playerOneScore.text = "Score: " + lc.getPlayerOneScore();
        playerTwoScore.text = "Score: " + lc.getPlayerTwoScore();
    }

}
