using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public Text playerOneScore;
    public Text playerTwoScore;

    GameObject levelController;
    LevelController lc;

    public GameObject playerOneBallBG;
    public GameObject playerOneBalloonButton;
    public GameObject playerOneGumButton;
    public GameObject playerOneSlimeButton;
    public GameObject playerOneSteelButton;

    public GameObject playerTwoBallBG;
    public GameObject playerTwoBalloonButton;
    public GameObject playerTwoGumButton;
    public GameObject playerTwoSlimeButton;
    public GameObject playerTwoSteelButton;

    // Use this for initialization
    void Start ()
    {
        levelController = GameObject.Find("LevelController");
        lc = levelController.GetComponent<LevelController>();

        showPlayerOneBallGUI(false);
        showPlayerTwoBallGUI(false);
	}

    public void UpdatePlayerScore()
    {
        playerOneScore.text = "Score: " + lc.getPlayerOneScore();
        playerTwoScore.text = "Score: " + lc.getPlayerTwoScore();
    }

    public void showPlayerOneBallGUI(bool b)
    {
        if (b)
        {
            playerOneBallBG.SetActive(true);
            playerOneBalloonButton.SetActive(true);
            playerOneGumButton.SetActive(true);
            playerOneSlimeButton.SetActive(true);
            playerOneSteelButton.SetActive(true);
        }
        else
        {
            playerOneBallBG.SetActive(false);
            playerOneBalloonButton.SetActive(false);
            playerOneGumButton.SetActive(false);
            playerOneSlimeButton.SetActive(false);
            playerOneSteelButton.SetActive(false);
        }
    }

    public void showPlayerTwoBallGUI(bool b)
    {
        if(b)
        {
            playerTwoBallBG.SetActive(true);
            playerTwoBalloonButton.SetActive(true);
            playerTwoGumButton.SetActive(true);
            playerTwoSlimeButton.SetActive(true);
            playerTwoSteelButton.SetActive(true);
        }
        else
        {
            playerTwoBallBG.SetActive(false);
            playerTwoBalloonButton.SetActive(false);
            playerTwoGumButton.SetActive(false);
            playerTwoSlimeButton.SetActive(false);
            playerTwoSteelButton.SetActive(false);
        }
        
    }

}
