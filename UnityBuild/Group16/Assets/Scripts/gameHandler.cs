using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //For SceneManager.LoadScene

public class gameHandler : MonoBehaviour
{
    public bool gameOver;

    [Header("Ball prefabs")]
    public GameObject prefab_regularBall;
    public GameObject prefab_balloonBall;
    public GameObject prefab_gumBall;
    public GameObject prefab_slimeBall;
    public GameObject prefab_steelBall;

    [Header("Round timer")]
    public int roundTimer;

    GameObject canvas;
    CanvasController cc;

    void Start()
    {
        gameOver = false;

        canvas = GameObject.Find("Canvas");
        cc = canvas.GetComponent<CanvasController>();
        cc.roundTimer.text = "Time: " + roundTimer;
        StartCoroutine(timerTick());
    }

    IEnumerator timerTick()
    {
        yield return new WaitForSeconds(1.0f);
        roundTimer--;
        cc.roundTimer.text = "Time: " + roundTimer;
        if (roundTimer <= 0)
        {
            gameOver = true;
            cc.playerWinBackground.SetActive(true);
            cc.playerWinButton.SetActive(true);

            LevelController lc = GameObject.FindGameObjectWithTag("levelController").GetComponent<LevelController>();

            if (lc.getPlayerOneScore() > lc.getPlayerTwoScore())
            {
                cc.playerWinText.text = "Player one wins!";
            }
            else if (lc.getPlayerTwoScore() < lc.getPlayerOneScore())
            {
                cc.playerWinText.text = "Player two wins!";
            }
            else
            {
                cc.playerWinText.text = "It's a draw!";
            }
        }
        else
        {
            StartCoroutine(timerTick());
        }
    }

    public void setCurrentBallOne(int ball)
    {
        GameObject playerOne = GameObject.FindGameObjectWithTag("player1");
        TouchController tc = playerOne.GetComponent<TouchController>();

        tc.selectBall(ball);
    }

    public void setCurrentBallTwo(int ball)
    {
        GameObject playerTwo = GameObject.FindGameObjectWithTag("player2");
        TouchController tc = playerTwo.GetComponent<TouchController>();

        tc.selectBall(ball);
    }

    public void returnToLevelSelect()
    {
        //LevelController lc = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        //lc.currentBalls.Clear();

        //gameOver = false;

        SceneManager.LoadScene(0);
    }
}
