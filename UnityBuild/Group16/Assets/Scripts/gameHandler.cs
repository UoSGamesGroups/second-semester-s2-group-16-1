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

    [Header("Terrain Object")]
    public GameObject terrain;

    [Header("Charlie sprites")]
    public Sprite charlie_terrain_ice;
    public Sprite charlie_terrain_sand;
    public Sprite charlie_terrain_rubber;

    [Header("Caitlin sprites")]
    public Sprite caitlin_terrain_ice;
    public Sprite caitlin_terrain_sand;
    public Sprite caitlin_terrain_rubber;

    [Header("No terrain")]
    public Sprite terrain_empty;

    GameObject canvas;
    CanvasController cc;
    GameObject levelController;
    LevelController lc;

    LevelController.LevelTerrain gameTerrain;
    bool terrainOn;
    
    void Start()
    {
        //The game isn't over
        gameOver = false;

        //Grab the levelcontroller
        levelController = GameObject.FindGameObjectWithTag("levelController");
        lc = levelController.GetComponent<LevelController>();

        //Setup the terrain
        terrainOn = true;
        terrainSetup();

        //Grab the canvas
        canvas = GameObject.Find("Canvas");
        cc = canvas.GetComponent<CanvasController>();
        cc.roundTimer.text = "Time: " + roundTimer;

        //Start the timer
        StartCoroutine(timerTick());
    }

    void terrainSetup()
    {
        gameTerrain = lc.currentTerrain;
        if (gameTerrain == LevelController.LevelTerrain.terrain_random)
        {
            Debug.Log("Choosing random terrain");
            chooseRandomTerrain();
        }
        else if (gameTerrain == LevelController.LevelTerrain.terrain_dynamic)
        {
            StartCoroutine(terrainTick());
        }

        updateTerrainBG();
        
    }

    void chooseRandomTerrain()
    {
        int i = Random.Range(0, 2);
        switch (i)
        {
            case 0:
                gameTerrain = LevelController.LevelTerrain.terrain_ice;
                break;
            case 1:
                gameTerrain = LevelController.LevelTerrain.terrain_sand;
                break;
            case 2:
            default:
                gameTerrain = LevelController.LevelTerrain.terrain_rubber;
                break;
        }
        Debug.Log("Current terrain: " + gameTerrain);
    }

    void updateTerrainBG()
    {
        switch (lc.selectedLevel)
        {
            case 1:     //Neon oct
                break;
            case 2:     //Neon squ
                break;
            case 3:     //Caitlin squ
                setupTerrainCaitlinSquare();
                break;
            case 4:     //John 1
                break;
            case 5:     //John 2
                break;
            default:
                break;
        }
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
            else if (lc.getPlayerTwoScore() > lc.getPlayerOneScore())
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

    IEnumerator terrainTick()
    {
        //If the game is running
        if(!gameOver)
        {
            Debug.Log("Dynamic terrain changing...");

            //Swap whether the terrain is on or not
            terrainOn = !terrainOn;

            //If its on, set it to a random terrain
            if (terrainOn)
            {
                chooseRandomTerrain();
                updateTerrainBG();
                yield return new WaitForSeconds(7f);
            }
            //Else, make it nothing
            else
            {
                gameTerrain = LevelController.LevelTerrain.terrain_no;
                updateTerrainBG();
                yield return new WaitForSeconds(7f);
            }
            updateTerrainBG();
            StartCoroutine(terrainTick());
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

    void setupTerrainCaitlinSquare()
    {
        Debug.Log("Hello");
        switch (gameTerrain)
        {
            case LevelController.LevelTerrain.terrain_ice:
                terrain.GetComponent<SpriteRenderer>().sprite = caitlin_terrain_ice;
                Debug.Log("ice");
                break;
            case LevelController.LevelTerrain.terrain_sand:
                terrain.GetComponent<SpriteRenderer>().sprite = caitlin_terrain_sand;
                Debug.Log("sand");
                break;
            case LevelController.LevelTerrain.terrain_rubber:
                terrain.GetComponent<SpriteRenderer>().sprite = caitlin_terrain_rubber;
                Debug.Log("rubber");
                break;
            case LevelController.LevelTerrain.terrain_no:
            case LevelController.LevelTerrain.terrain_dynamic:
            default:
                terrain.GetComponent<SpriteRenderer>().sprite = terrain_empty;
                Debug.Log("default");
                break;
        }
    }
}
