using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //For SceneManager.LoadScene

public class LevelController : MonoBehaviour {

    void Start() { DontDestroyOnLoad(this); }

    int currentLevel;
    GameObject levelHolder;
    public GameObject playerOnePrefab;
    public GameObject PlayerTwoPrefab;

    public GameObject squLevel;
    public GameObject octLevel;

    GameObject playerOne;
    GameObject playerTwo;
    int playerOneScore;
    int playerTwoScore;
    public int getPlayerOneScore() { return playerOneScore; }
    public int getPlayerTwoScore() { return playerTwoScore; }
    public void mutatePlayerOneScore(int i) { playerOneScore += i; }
    public void mutatePlayerTwoScore(int i) { playerTwoScore += i; }

    public void loadLevel(int lv)
    {
        //Change scene to the mainGame
        SceneManager.LoadScene(1);

        //Setup level
        //This is a coroutine because we have to wait atleast one frame
        //for the scene to load before we start setting up our level
        StartCoroutine(levelSetup(lv));
    }

    IEnumerator levelSetup(int lv)
    {
        yield return new WaitForSeconds(0.1f);

        //Destroy the current level
        if (levelHolder != null)
        { Destroy(levelHolder.gameObject); }

        //Reset players scores upon loading a new level
        playerOneScore = playerTwoScore = 0;

        //Update the current level
        currentLevel = lv;

        //Destroy the current players
        if (playerOne != null)
        { Destroy(playerOne.gameObject); }
        if (playerTwo != null)
        { Destroy(playerTwo.gameObject); }

        //Instantiate both players
        playerOne = Instantiate(playerOnePrefab, new Vector2(-1f, 0f), Quaternion.identity);
        playerTwo = Instantiate(PlayerTwoPrefab, new Vector2(1f, 0f), Quaternion.identity);

        //Reset players positions
        ResetPlayers();

        ////Grab the playerOne and playerTwo game objects
        //playerOne = GameObject.FindGameObjectWithTag("player1");
        //playerTwo = GameObject.FindGameObjectWithTag("player2");

        //Spawn in the new level
        switch (lv)
        {
            case 1:
                levelHolder = Instantiate(squLevel, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case 2:
                levelHolder = Instantiate(octLevel, new Vector2(0f, 0f), Quaternion.identity);
                break;
            default:
                print("ERROR: Invalid level.");
                break;
        }
    }

    //Reset players
    public void ResetPlayers()
    {
        //Reset the players positions
        switch(currentLevel)
        {
            case 1: //Square level
                playerOne.transform.position = new Vector2(-6f, 0f);
                playerTwo.transform.position = new Vector2(6f, 0f);
                break;
            case 2: //Octagon level
                playerOne.transform.position = new Vector2(-4f, 0f);
                playerTwo.transform.position = new Vector2(4f, 0f);
                break;
            default:
                Debug.Log("Level error.");
                break;
        }

        //Reset the players velocity
        playerOne.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        playerTwo.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        
        //Output players score
        print("Player one score: " + playerOneScore);
        print("Player two score: " + playerTwoScore);
        
    }

}
