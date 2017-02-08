using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    int currentLevel;

    GameObject playerOne;
    GameObject playerTwo;

    int playerOneScore;
    int playerTwoScore;

    public int getPlayerOneScore() { return playerOneScore; }
    public int getPlayerTwoScore() { return playerTwoScore; }
    public void mutatePlayerOneScore(int i) { playerOneScore += i; }
    public void mutatePlayerTwoScore(int i) { playerTwoScore += i; }

    void Start()
    {

        currentLevel = 1;

        playerOne = GameObject.FindGameObjectWithTag("player1");
        playerTwo = GameObject.FindGameObjectWithTag("player2");
    }

    public void ResetPlayers()
    {

        switch(currentLevel)
        {
            case 1:
                playerOne.transform.position = new Vector2(-6f, 0f);
                playerOne.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                playerTwo.transform.position = new Vector2(6f, 0f);
                playerTwo.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                break;
            default:
                Debug.Log("Level error.");
                break;
        }

        print("Player one score: " + playerOneScore);
        print("Player two score: " + playerTwoScore);
        
    }

}
