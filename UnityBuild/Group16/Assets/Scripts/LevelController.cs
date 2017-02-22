using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //For SceneManager.LoadScene

public class LevelController : MonoBehaviour {

    void Start() { DontDestroyOnLoad(this); }

    //--------------------------
    // Level information

    int currentLevel;
    GameObject levelHolder;

    [Header("Level prefabs")]
    public GameObject prefab_OctagonNeon;       //1
    public GameObject prefab_SquareNeon;        //2
    public GameObject prefab_SquareCaitlin;     //3
    public GameObject prefab_SquareJohnOne;     //4
    public GameObject prefab_SquareJohnTwo;     //5

    public GameObject squLevel;                 //51
    public GameObject octLevel;                 //52

    //--------------------------
    // Player information

    [Header("Player prefabs")]
    public GameObject prefab_playerOne;
    public GameObject prefab_playerTwo;

    GameObject playerOne;
    GameObject playerTwo;
    int playerOneScore;
    int playerTwoScore;
    public int getPlayerOneScore() { return playerOneScore; }
    public int getPlayerTwoScore() { return playerTwoScore; }
    public void mutatePlayerOneScore(int i) { playerOneScore += i; }
    public void mutatePlayerTwoScore(int i) { playerTwoScore += i; }

    //TEMPORARY CODE - START
    //
    public void loadSquSide()
    {
        SceneManager.LoadScene(2);
        StartCoroutine(sideLvSetup());
    }
    IEnumerator sideLvSetup()
    {
        yield return new WaitForSeconds(0.1f);

        currentLevel = 1;

        //Instantiate both players
        playerOne = Instantiate(prefab_playerOne, new Vector2(-5f, 0f), Quaternion.identity);
        playerTwo = Instantiate(prefab_playerTwo, new Vector2(5f, 0f), Quaternion.identity);

        //Reset players scores upon loading a new level
        playerOneScore = playerTwoScore = 0;

    }

    //------

    public void loadSquClick()
    {
        SceneManager.LoadScene(3);
        StartCoroutine(clickLvSetup());
    }

    IEnumerator clickLvSetup()
    {
        yield return new WaitForSeconds(0.1f);

        currentLevel = 1;

        //Instantiate both players
        playerOne = Instantiate(prefab_playerOne, new Vector2(-5f, 0f), Quaternion.identity);
        playerTwo = Instantiate(prefab_playerTwo, new Vector2(5f, 0f), Quaternion.identity);

        //Reset players scores upon loading a new level
        playerOneScore = playerTwoScore = 0;
    }

    //
    //TEMPORARY CODE - END



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
        playerOne = Instantiate(prefab_playerOne, new Vector2(-1f, 0f), Quaternion.identity);
        playerTwo = Instantiate(prefab_playerTwo, new Vector2(1f, 0f), Quaternion.identity);

        //Reset players positions
        ResetPlayers();

        //Spawn in the new level
        switch (lv)
        {
            case 1: //Octagon neon
                levelHolder = Instantiate(prefab_OctagonNeon, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case 2: //Square neon
                levelHolder = Instantiate(prefab_SquareNeon, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case 3: //Square caitlin
                levelHolder = Instantiate(prefab_SquareCaitlin, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case 4: //Square John one
                levelHolder = Instantiate(prefab_SquareJohnOne, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case 5://Square John two
                levelHolder = Instantiate(prefab_SquareJohnTwo, new Vector2(0f, 0f), Quaternion.identity);
                break;

            //Default levels...
            case 51: //Default Square level
                levelHolder = Instantiate(squLevel, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case 52: //Default Octagon level
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

        print("currentLevel: " + currentLevel);

        //Grab the playerOne and playerTwo game objects
        playerOne = GameObject.FindGameObjectWithTag("player1");
        playerTwo = GameObject.FindGameObjectWithTag("player2");

        //Reset the players positions
        switch (currentLevel)
        {
            case 1: //Octagon neon
            case 2: //Square neon
            case 3: //Square caitlin
            case 4: //Square John one
            case 5://Square John two
                playerOne.transform.position = new Vector2(-5f, 0f);
                playerTwo.transform.position = new Vector2(5f, 0f);
                break;

            //Default levels...
            case 51: //Default Square level
                playerOne.transform.position = new Vector2(-5f, 0f);
                playerTwo.transform.position = new Vector2(5f, 0f);
                break;
            case 52: //Default Octagon level
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
