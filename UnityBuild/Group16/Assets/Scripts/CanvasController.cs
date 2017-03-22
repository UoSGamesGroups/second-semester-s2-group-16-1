using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    [Header("Text")]
    public Text playerOneScore;
    public Text playerTwoScore;
    public Text roundTimer;

    GameObject levelController;
    LevelController lc;

    [Header("Left buttons")]
    public GameObject playerOneBallBG;
    public GameObject playerOneBalloonButton;
    public GameObject playerOneGumButton;
    public GameObject playerOneSlimeButton;
    public GameObject playerOneSteelButton;

    [Header("Right buttons")]
    public GameObject playerTwoBallBG;
    public GameObject playerTwoBalloonButton;
    public GameObject playerTwoGumButton;
    public GameObject playerTwoSlimeButton;
    public GameObject playerTwoSteelButton;

    [Header("Player win objects")]
    public GameObject playerWinBackground;
    public Text playerWinText;
    public GameObject playerWinButton;

    // Use this for initialization
    void Start ()
    {
        levelController = GameObject.Find("LevelController");
        lc = levelController.GetComponent<LevelController>();

        playerWinBackground.SetActive(false);
        playerWinText.text = "";
        playerWinButton.SetActive(false);

        //showPlayerOneBallGUI(false);
        //showPlayerTwoBallGUI(false);
	}

    public void UpdatePlayerScore()
    {
        playerOneScore.text = lc.getPlayerOneScore() + "-" + lc.getPlayerTwoScore();
        playerTwoScore.text = lc.getPlayerTwoScore() + "-" + lc.getPlayerOneScore();
        //playerOneScore.text = "Score: " + lc.getPlayerOneScore();
        //playerTwoScore.text = "Score: " + lc.getPlayerTwoScore();
    }

    public void showPlayerOneBallGUI(bool b)
    {
        if (b)
        {
            if (playerOneBallBG != null)
            {
                playerOneBallBG.SetActive(true);
            }
            playerOneBalloonButton.SetActive(true);
            playerOneGumButton.SetActive(true);
            playerOneSlimeButton.SetActive(true);
            playerOneSteelButton.SetActive(true);
        }
        else
        {
            if (playerOneBallBG != null)
            {
                playerOneBallBG.SetActive(false);
            }
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
            if (playerTwoBallBG != null)
            {
                playerTwoBallBG.SetActive(true);
            }
            playerTwoBalloonButton.SetActive(true);
            playerTwoGumButton.SetActive(true);
            playerTwoSlimeButton.SetActive(true);
            playerTwoSteelButton.SetActive(true);
        }
        else
        {
            if (playerTwoBallBG != null)
            {
                playerTwoBallBG.SetActive(false);
            }
            playerTwoBalloonButton.SetActive(false);
            playerTwoGumButton.SetActive(false);
            playerTwoSlimeButton.SetActive(false);
            playerTwoSteelButton.SetActive(false);
        }
        
    }

}
