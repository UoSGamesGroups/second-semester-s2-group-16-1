using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //For SceneManager.LoadScene

public class gameHandler : MonoBehaviour {

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
        SceneManager.LoadScene(0);
    }
}
