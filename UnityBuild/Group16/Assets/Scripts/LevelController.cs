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

    public GameObject prefab_SquareCaitlin;         //3
    public GameObject prefab_ovalCaitlin;           //4
    public GameObject prefab_inwardsOvalCaitlin;    //5
    public GameObject prefabs_inwardsCircleCaitlin; //6
    public GameObject prefab_zigzagCaitlin;         //7

    public GameObject prefab_SquareJohnOne;     //8
    public GameObject prefab_SquareJohnTwo;     //9

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

    [Header("Charlie reactive obstacles")]
    public GameObject prefab_obstacle_neonSpinningClub;
    public GameObject prefab_obstacle_neonBounceCircle;
    public GameObject prefab_obstacle_neonSemiTopLeft;
    public GameObject prefab_obstacle_neonSemiTopRight;
    public GameObject prefab_obstacle_neonSemiBottomRight;
    public GameObject prefab_obstacle_neonSemiBottomLeft;

    [Header("Caitlin reactive obstacles")]
    public GameObject prefab_obstacles_caitlinReactiveSpinner;
    public GameObject prefab_obstacle_caitlinFanLeft;
    public GameObject prefab_obstacle_caitlinFanRight;
    public GameObject prefab_obstacle_caitlinElectric;
    public GameObject prefab_obstacle_caitlinFlipper;

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
    public Sprite sprite_johnBeaniePlayerOne;
    public Sprite sprite_johnBeaniePlayerTwo;
    public Sprite sprite_johnFootballPlayerOne;
    public Sprite sprite_johnFootballPlayerTwo;
    public Sprite sprite_johnHelmetPlayerOne;
    public Sprite sprite_johnHelmetPlayerTwo;

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

                //Charlie
            case 1:
                levelHolder = Instantiate(prefab_OctagonNeon, new Vector2(0f, 0f), Quaternion.identity);
                Instantiate(prefab_obstable_expandingOctagonNeon, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case 2:
                levelHolder = Instantiate(prefab_SquareNeon, new Vector2(0f, 0f), Quaternion.identity);
                spawnCharlie();
                break;

            
                //Caitlin
            case 3:
                levelHolder = Instantiate(prefab_SquareCaitlin, new Vector2(0f, 0f), Quaternion.identity);
                spawnCaitlin(0);
                //updateBackground(whiteBackground);
                break;
            case 4:
                levelHolder = Instantiate(prefab_ovalCaitlin, new Vector2(0f, 0f), Quaternion.identity);
                spawnCaitlin(0);
                break;

            case 5:
                levelHolder = Instantiate(prefab_inwardsOvalCaitlin, new Vector2(0f, 0f), Quaternion.identity);
                break;

            case 6:
                levelHolder = Instantiate(prefabs_inwardsCircleCaitlin, new Vector2(0f, 0f), Quaternion.identity);
                spawnCaitlin(1);
                break;

            case 7:
                levelHolder = Instantiate(prefab_zigzagCaitlin, new Vector2(0f, 0f), Quaternion.identity);
                break;

                //John
            case 8:
                levelHolder = Instantiate(prefab_SquareJohnOne, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case 9:
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
                //John
            case 1: //Octagon neon
            case 2: //Square neon
                playerOne.GetComponent<SpriteRenderer>().sprite = sprite_neonPlayerOne;
                playerTwo.GetComponent<SpriteRenderer>().sprite = sprite_neonPlayerTwo;
                break;

                //Caitlin
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
                playerOne.GetComponent<SpriteRenderer>().sprite = sprite_caitlinPlayerOne;
                playerTwo.GetComponent<SpriteRenderer>().sprite = sprite_caitlinPlayerTwo;
                break;

                //John
            case 8:
                playerOne.GetComponent<SpriteRenderer>().sprite = sprite_johnHelmetPlayerOne;
                playerTwo.GetComponent<SpriteRenderer>().sprite = sprite_johnHelmetPlayerTwo;
                break;
            case 9:
                playerOne.GetComponent<SpriteRenderer>().sprite = sprite_johnBeaniePlayerOne;
                playerTwo.GetComponent<SpriteRenderer>().sprite = sprite_johnBeaniePlayerTwo;
                break;

            //Default levels...
            case 51: //Default Square level
            case 52: //Default Octagon level
            default:
                break;
        }
    }
    
    void spawnCaitlin(int arg)
    {
        if (arg == 1)
        {
            Instantiate(prefab_obstacle_caitlinSpinningCross, new Vector2(0f, 0f), Quaternion.identity);
            return;
        }
        else if (arg == 0)
        {
            //Should we spawn dynamic or reactive obstacles?
            int i = Random.Range(0, 2);
            //Dynamic
            if (i == 0)
            {
                GameObject temp;
                int j = Random.Range(0, 3);
                switch (j)
                {
                    case 0:
                        Instantiate(prefab_obstacle_caitlinSpinningCross, new Vector2(0f, 0f), Quaternion.identity);
                        break;
                    case 1:
                        temp = Instantiate(prefab_obstacle_caitlinMovingPlatformOne, new Vector2(-1.76f, 3.4f), Quaternion.identity);
                        temp.transform.Rotate(0, 0, 90);
                        temp = Instantiate(prefab_obstacle_caitlinMovingPlatformTwo, new Vector2(1.76f, -3.4f), Quaternion.identity);
                        temp.transform.Rotate(0, 0, 90);
                        break;
                    case 2:
                    default:
                        Instantiate(prefab_obstacle_caitlinExpandingCircle, new Vector2(0f, 0f), Quaternion.identity);
                        break;
                }
            }
            //Reactive
            else
            {
                int j = Random.Range(0, 3);
                GameObject temp;
                switch (j)
                {
                    case 0:
                        temp = Instantiate(prefab_obstacles_caitlinReactiveSpinner, new Vector2(0, 0), Quaternion.identity);
                        temp.transform.Rotate(0, 0, 90);
                        break;
                    case 1:
                        temp = Instantiate(prefab_obstacle_caitlinFanLeft, new Vector2(-2.15f, 3.95f), Quaternion.identity);
                        temp.transform.Rotate(0, 0, -40);
                        temp = Instantiate(prefab_obstacle_caitlinFanRight, new Vector2(2.15f, -3.95f), Quaternion.identity);
                        temp.transform.Rotate(0, 0, 140);
                        break;
                    case 2:
                    default:
                        Instantiate(prefab_obstacle_caitlinElectric, new Vector2(0, 0), Quaternion.identity);
                        break;
                }
            }
        }
    }

    void spawnCharlie()
    {
        //Should we spawn dynamic or reactive obstacles?
        int i = Random.Range(0, 2);

        //Dynamic
        if (i == 0)
        {
            //Spawning default obstacles
            int j = Random.Range(0, 3);
            switch (j)
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

            return;
        }
        //Dynamic
        else
        {
            int j = Random.Range(0, 3);
            switch (j)
            {
                case 0: //Rotating club
                    Instantiate(prefab_obstacle_neonSpinningClub, new Vector2(-3.75f, 2.9f), Quaternion.identity);
                    Instantiate(prefab_obstacle_neonSpinningClub, new Vector2(3.75f, 2.9f), Quaternion.identity);
                    Instantiate(prefab_obstacle_neonSpinningClub, new Vector2(3.75f, -2.9f), Quaternion.identity);
                    Instantiate(prefab_obstacle_neonSpinningClub, new Vector2(-3.75f, -2.9f), Quaternion.identity);
                    break;
                case 1: //Rotating semi circles
                    Instantiate(prefab_obstacle_neonSemiTopLeft, new Vector2(-3.75f, 2.9f), Quaternion.identity);
                    Instantiate(prefab_obstacle_neonSemiTopRight, new Vector2(3.75f, 2.9f), Quaternion.identity);
                    Instantiate(prefab_obstacle_neonSemiBottomRight, new Vector2(3.75f, -2.9f), Quaternion.identity);
                    Instantiate(prefab_obstacle_neonSemiBottomLeft, new Vector2(-3.75f, -2.9f), Quaternion.identity);
                    break;
                case 2:
                default:
                    //Instantiate(prefab_obstacle_neonBounceCircle, new Vector2(-5.5f, 3.85f), Quaternion.identity);
                    Instantiate(prefab_obstacle_neonBounceCircle, new Vector2(5.5f, 3.85f), Quaternion.identity);
                    Instantiate(prefab_obstacle_neonBounceCircle, new Vector2(5.5f, -3.85f), Quaternion.identity);
                    Instantiate(prefab_obstacle_neonBounceCircle, new Vector2(-5.5f, -3.85f), Quaternion.identity);
                    break;
            }
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
            case 4: //Caitlin new 1
            case 5: //Caitlin new 2
            case 6: //Caitlin new 3
            case 7: //Caitlin new 4
            case 8: //Square John one
            case 9://Square John two
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

        removeBallsFromPlay();
    }

    void removeBallsFromPlay()
    {
        //Destroy all of the current balls in the level
        foreach (GameObject ball in currentBalls)
        {
            Destroy(ball.gameObject);
        }
        currentBalls.Clear();
    }

    void clearBalls()
    {
        //Destroy all of the current balls in the level
        //foreach (GameObject ball in currentBalls)
        //{
        //    Destroy(ball.gameObject);
        //}
        currentBalls.Clear();
    }

}
