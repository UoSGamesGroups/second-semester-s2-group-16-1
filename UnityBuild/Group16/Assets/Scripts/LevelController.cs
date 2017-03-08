using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //For SceneManager.LoadScene

public class LevelController : MonoBehaviour {

    void Start() { DontDestroyOnLoad(this); }

    public enum LevelTerrain
    {
        terrain_no,
        terrain_random,
        terrain_dynamic,
        terrain_ice,
        terrain_sand,
        terrain_rubber,
    }

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

    [Header("Level Backgrounds")]
    public Sprite whiteBackground;
    public Sprite neonBackground;

    [Header("Charlie obstacles")]
    public GameObject prefab_obstable_expandingOctagonNeon;

    [Header("Caitlin obstacles")]
    public GameObject prefab_obstacle_caitlinSpinningCross;
    public GameObject prefab_obstacle_caitlinExpandingCircle;
    public GameObject prefab_obstacle_caitlinMovingPlatformOne;
    public GameObject prefab_obstacle_caitlinMovingPlatformTwo;

    [Header("Default obstacles")]
    public GameObject prefab_obstacle_defaultSpinningFan;
    public GameObject prefab_obstacle_defaultExpandingSquare;
    public GameObject prefab_obstacle__defaultExpandingCircle;

    [Header("Misc")]
    public int selectedLevel;

    //--------------------------
    // Terrain information
    [Header("Terrain sprites")]

    //--------------------------
    // Player information

    [Header("Player prefabs")]
    public GameObject prefab_playerOne;
    public GameObject prefab_playerTwo;

    [Header("Player sprites")]
    public Sprite sprite_neonPlayerOne;
    public Sprite sprite_neonPlayerTwo;
    public Sprite sprite_caitlinPlayerOne;
    public Sprite sprite_caitlinPlayerTwo;

    GameObject playerOne;
    GameObject playerTwo;
    int playerOneScore;
    int playerTwoScore;
    public int getPlayerOneScore() { return playerOneScore; }
    public int getPlayerTwoScore() { return playerTwoScore; }
    public void mutatePlayerOneScore(int i) { playerOneScore += i; }
    public void mutatePlayerTwoScore(int i) { playerTwoScore += i; }

    //--------------------------
    // Current game info

    public List<GameObject> currentBalls;
    public LevelTerrain currentTerrain;

    //--------------------------
    // Methods

    public void loadLevel(int lv)
    {
        //Change scene to the mainGame
        SceneManager.LoadScene(2);

        //Clear any current balls in play
        clearBalls();

        //Setup level
        //This is a coroutine because we have to wait atleast one frame
        //for the scene to load before we start setting up our level
        StartCoroutine(levelSetup(lv));
    }

    IEnumerator levelSetup(int lv)
    {
        //Wait for the next frame
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
        loadPlayerSprites(lv);

        //Reset players positions
        ResetPlayers();

        //Spawn in the new level
        //And change the background
        switch (lv)
        {
            case 1: //Octagon neon
                levelHolder = Instantiate(prefab_OctagonNeon, new Vector2(0f, 0f), Quaternion.identity);
                Instantiate(prefab_obstable_expandingOctagonNeon, new Vector2(0f, 0f), Quaternion.identity);
                updateBackground(neonBackground);
                break;
            case 2: //Square neon
                levelHolder = Instantiate(prefab_SquareNeon, new Vector2(0f, 0f), Quaternion.identity);
                spawnDefault();
                updateBackground(neonBackground);
                break;
            case 3: //Square caitlin
                levelHolder = Instantiate(prefab_SquareCaitlin, new Vector2(0f, 0f), Quaternion.identity);
                spawnCaitlin();
                updateBackground(whiteBackground);
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

    void updateBackground(Sprite spr)
    {
        GameObject.Find("backgroundImage").GetComponent<SpriteRenderer>().sprite = spr;
    }

    void loadPlayerSprites(int lv)
    {
        switch (lv)
        {
            case 1: //Octagon neon
            case 2: //Square neon
                playerOne.GetComponent<SpriteRenderer>().sprite = sprite_neonPlayerOne;
                playerTwo.GetComponent<SpriteRenderer>().sprite = sprite_neonPlayerTwo;
                break;
            case 4: //Square John one
            case 5://Square John two
            case 3: //Square caitlin
                playerOne.GetComponent<SpriteRenderer>().sprite = sprite_caitlinPlayerOne;
                playerTwo.GetComponent<SpriteRenderer>().sprite = sprite_caitlinPlayerTwo;
                break;
            //Default levels...
            case 51: //Default Square level
            case 52: //Default Octagon level
            default:
                break;
        }
    }
    
    void spawnCaitlin()
    {
        int i = Random.Range(0, 3);

        switch (i)
        {
            case 0:
                Instantiate(prefab_obstacle_caitlinSpinningCross, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case 1:
                Instantiate(prefab_obstacle_caitlinMovingPlatformOne, new Vector2(-1.76f, 3.4f), Quaternion.Euler(new Vector3(0, 0, 90f)));
                Instantiate(prefab_obstacle_caitlinMovingPlatformTwo, new Vector2(1.76f, -3.4f), Quaternion.Euler(new Vector3(0, 0, 90f)));
                break;
            case 2:
            default:
                Instantiate(prefab_obstacle_caitlinExpandingCircle, new Vector2(0f, 0f), Quaternion.identity);
                break;
        }
    }

    void spawnDefault()
    {
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:
                Instantiate(prefab_obstacle_defaultExpandingSquare, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case 1:
                Instantiate(prefab_obstacle__defaultExpandingCircle, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case 2:
            default:
                Instantiate(prefab_obstacle_defaultSpinningFan, new Vector2(0f, 0f), Quaternion.identity);
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
                playerOne.transform.position = new Vector2(-3.5f, 0f);
                playerTwo.transform.position = new Vector2(3.5f, 0f);
                break;
            case 2: //Square neon
            case 3: //Square caitlin
            case 4: //Square John one
            case 5://Square John two
                playerOne.transform.position = new Vector2(-4f, 0f);
                playerTwo.transform.position = new Vector2(4f, 0f);
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

        clearBalls();
    }

    void clearBalls()
    {
        //Destroy all of the current balls in the level
        foreach (GameObject ball in currentBalls)
        {
            Destroy(ball.gameObject);
        }
        currentBalls.Clear();
    }

}
