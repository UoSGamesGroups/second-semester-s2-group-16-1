using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    [Header("Fonts")]
    public Font charlieFont;
    public Font caitlinFont;

    [Header("Text")]
    public Text playerOneScore;
    public Text playerTwoScore;
    public Text roundTimerLeft;
    public Text roundTimerRight;

    GameObject levelController;
    LevelController lc;

    [Header("Left buttons")]
    GameObject playerOneBallBG;
    public GameObject[] playerOneButtons;


    [Header("Right buttons")]
    GameObject playerTwoBallBG;
    public GameObject[] playerTwoButtons;

    [Header("Player win objects")]
    public GameObject playerWinBackground;
    public Text playerWinText;
    public GameObject playerWinButton;

    [Header("Button sprites")]
    public Sprite sprite_balloon;
    public Sprite sprite_steel;
    public Sprite sprite_gum;
    public Sprite sprite_slime;
    public Sprite sprite_fire;
    public Sprite sprite_smoke;

    // Use this for initialization
    void Start ()
    {
        levelController = GameObject.Find("LevelController");
        lc = levelController.GetComponent<LevelController>();

        playerWinBackground.SetActive(false);
        playerWinText.text = "";
        playerWinButton.SetActive(false);

        //Setup balls
        for (int i = 0; i < playerOneButtons.Length; i++)
        {
            if (lc.player1Balls[i] == BallController.ballType.balloon) playerOneButtons[i].GetComponent<Image>().sprite = sprite_balloon;
            else if (lc.player1Balls[i] == BallController.ballType.steel) playerOneButtons[i].GetComponent<Image>().sprite = sprite_steel;
            else if (lc.player1Balls[i] == BallController.ballType.gum) playerOneButtons[i].GetComponent<Image>().sprite = sprite_gum;
            else if (lc.player1Balls[i] == BallController.ballType.slime) playerOneButtons[i].GetComponent<Image>().sprite = sprite_slime;
            else if (lc.player1Balls[i] == BallController.ballType.fire) playerOneButtons[i].GetComponent<Image>().sprite = sprite_fire;
            else if (lc.player1Balls[i] == BallController.ballType.smoke) playerOneButtons[i].GetComponent<Image>().sprite = sprite_smoke;
        }
        for (int i = 0; i < playerTwoButtons.Length; i++)
        {
            if (lc.player2Balls[i] == BallController.ballType.balloon) playerTwoButtons[i].GetComponent<Image>().sprite = sprite_balloon;
            else if (lc.player2Balls[i] == BallController.ballType.steel) playerTwoButtons[i].GetComponent<Image>().sprite = sprite_steel;
            else if (lc.player2Balls[i] == BallController.ballType.gum) playerTwoButtons[i].GetComponent<Image>().sprite = sprite_gum;
            else if (lc.player2Balls[i] == BallController.ballType.slime) playerTwoButtons[i].GetComponent<Image>().sprite = sprite_slime;
            else if (lc.player2Balls[i] == BallController.ballType.fire) playerTwoButtons[i].GetComponent<Image>().sprite = sprite_fire;
            else if (lc.player2Balls[i] == BallController.ballType.smoke) playerTwoButtons[i].GetComponent<Image>().sprite = sprite_smoke;
        }

        //Setup fonts
        switch (lc.selectedLevel)
        {
            //Charlie + john
            case 0:
            case 1:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 7:
            case 8:
                playerOneScore.font = charlieFont;
                playerTwoScore.font = charlieFont;
                roundTimerLeft.font = charlieFont;
                roundTimerRight.font = charlieFont;
                break;

            //Caitlin
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
                playerOneScore.font = caitlinFont;
                playerTwoScore.font = caitlinFont;
                roundTimerLeft.font = caitlinFont;
                roundTimerRight.font = caitlinFont;
                break;

            default:
                Debug.Log("Error setting font.");
                break;
        }

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
            foreach (GameObject button in playerOneButtons)
            {
                button.SetActive(true);
            }
        }
        else
        {
            if (playerOneBallBG != null)
            {
                playerOneBallBG.SetActive(false);
            }
            foreach (GameObject button in playerOneButtons)
            {
                button.SetActive(false);
            }
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
            foreach (GameObject button in playerTwoButtons)
            {
                button.SetActive(true);
            }
        }
        else
        {
            if (playerTwoBallBG != null)
            {
                playerTwoBallBG.SetActive(false);
            }
            foreach (GameObject button in playerTwoButtons)
            {
                button.SetActive(false);
            }
        }
        
    }

}
