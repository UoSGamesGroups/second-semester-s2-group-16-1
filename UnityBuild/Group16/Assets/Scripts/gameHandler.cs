using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //For SceneManager.LoadScene

public class gameHandler : MonoBehaviour
{
    [Header("Ball prefabs")]
    public GameObject prefab_regularBall;
    public GameObject prefab_balloonBall;
    public GameObject prefab_gumBall;
    public GameObject prefab_slimeBall;
    public GameObject prefab_steelBall;

    public void setCurrentBallOne(int ball)
    {
        GameObject playerOne = GameObject.FindGameObjectWithTag("player1");
        PlayerOneController tc = playerOne.GetComponent<PlayerOneController>();

        tc.selectBall(ball);
    }

    public void setCurrentBallTwo(int ball)
    {
        GameObject playerTwo = GameObject.FindGameObjectWithTag("player2");
        PlayerTwoController tc = playerTwo.GetComponent<PlayerTwoController>();

        tc.selectBall(ball);
    }

    public void returnToLevelSelect()
    {
        SceneManager.LoadScene(0);
    }
}
