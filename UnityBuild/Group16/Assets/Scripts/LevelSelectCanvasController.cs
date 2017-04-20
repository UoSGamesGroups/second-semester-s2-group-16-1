using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectCanvasController : MonoBehaviour {
   
    //-----------------
    // Level Select

    //Level controller
    GameObject levelController;
    LevelController lc;

    //Sprites
    [Header("Level sprites")]
    public Sprite sprite_octagonNeon;   //1
    public Sprite sprite_squareNeon;    //2

    public Sprite sprite_squareCaitlinOne;          //3
    public Sprite sprite_ovalCaitlin;               //4
    public Sprite sprite_inwardsOvalCaitlin;        //5
    public Sprite sprite_inwardsCircleCaitlin;      //6
    public Sprite sprite_zigzagCaitlin;             //7

    public Sprite sprite_squareJohnOne; //8
    public Sprite sprite_squareJohnTwo; //9

    //UI Images
    [Header("Level UI Preview Image")]
    public GameObject levelPreviewImage;

    int numOfLevels = 9;
    int currentLevel;

    //-----------------
    // Terrain Select

    //Sprites
    [Header("Terrain sprites")]
    public Sprite sprite_noTerrain;
    public Sprite sprite_dynamicTerrain;
    public Sprite sprite_randomTerrain;
    public Sprite sprite_iceTerrain;
    public Sprite sprite_sandTerrain;
    public Sprite sprite_rubberTerrain;

    [Header("Terrain UI Preview Image")]
    public GameObject terrainPreviewImage;

    int numberOfTerrains = 6;
    int currentTerrain;


    // Use this for initialization
    void Start ()
    {
        GameObject[] lcs = GameObject.FindGameObjectsWithTag("levelController");
        if (lcs.Length > 1)
        {
            for (int i  = 1; i < lcs.Length; i++)
            {
                Destroy(lcs[i].gameObject);
            }
        }

        levelController = GameObject.Find("LevelController");
        lc = levelController.GetComponent<LevelController>();

        currentLevel = 1;
        currentTerrain = 1;
    }

    //--------------------------------
    // Level methods

    public void increaseLevel()
    {
        if (currentLevel < numOfLevels)
        {
            currentLevel++;
        }
        else if (currentLevel == numOfLevels)
        {
            currentLevel = 1;
        }

        updateLevelPreviewImage();
    }

    public void decreaseLevel()
    {
        if (currentLevel > 1)
        {
            currentLevel--;
        }
        else if (currentLevel == 1)
        {
            currentLevel = numOfLevels;
        }

        updateLevelPreviewImage();
    }

    void updateLevelPreviewImage()
    {
        switch (currentLevel)
        {
            case 1:
                levelPreviewImage.GetComponent<Image>().sprite = sprite_octagonNeon;
                break;
            case 2:
                levelPreviewImage.GetComponent<Image>().sprite = sprite_squareNeon;
                break;
            case 3:
                levelPreviewImage.GetComponent<Image>().sprite = sprite_squareCaitlinOne;
                break;
            case 4:
                levelPreviewImage.GetComponent<Image>().sprite = sprite_ovalCaitlin;
                break;
            case 5:
                levelPreviewImage.GetComponent<Image>().sprite = sprite_inwardsOvalCaitlin;
                break;
            case 6:
                levelPreviewImage.GetComponent<Image>().sprite = sprite_inwardsCircleCaitlin;
                break;
            case 7:
                levelPreviewImage.GetComponent<Image>().sprite = sprite_zigzagCaitlin;
                break;
            case 8:
                levelPreviewImage.GetComponent<Image>().sprite = sprite_squareJohnOne;
                break;
            case 9:
                levelPreviewImage.GetComponent<Image>().sprite = sprite_squareJohnTwo;
                break;
            default:
                break;
        }
    }

    //--------------------------------
    // Terrain methods

    public void increaseTerrain()
    {
        if (currentTerrain < numberOfTerrains)
        {
            currentTerrain++;
        }
        else if (currentTerrain == numberOfTerrains)
        {
            currentTerrain = 1;
        }

        updateTerrainPreviewImage();
    }

    public void decreaseTerrain()
    {
        if (currentTerrain > 1)
        {
            currentTerrain--;
        }
        else if (currentTerrain == 1)
        {
            currentTerrain = numberOfTerrains;
        }

        updateTerrainPreviewImage();
    }

    void updateTerrainPreviewImage()
    {
        switch (currentTerrain)
        {
            case 1:
                terrainPreviewImage.GetComponent<Image>().sprite = sprite_noTerrain;
                break;
            case 2:
                terrainPreviewImage.GetComponent<Image>().sprite = sprite_dynamicTerrain;
                break;
            case 3:
                terrainPreviewImage.GetComponent<Image>().sprite = sprite_randomTerrain;
                break;
            case 4:
                terrainPreviewImage.GetComponent<Image>().sprite = sprite_iceTerrain;
                break;
            case 5:
                terrainPreviewImage.GetComponent<Image>().sprite = sprite_sandTerrain;
                break;
            case 6:
                terrainPreviewImage.GetComponent<Image>().sprite = sprite_rubberTerrain;
                break;
            default:
                break;
        }
    }

    //--------------------------------
    // Game behaviour

    //public void loadLevel()
    //{
    //    lc.selectedLevel = currentLevel;
    //    SceneManager.LoadScene(1);
    //}

    public void loadLevel()
    {
        //Update the level controllers selected level
        lc.selectedLevel = currentLevel;

        //Update the levelcontrollers current terrain
        switch (currentTerrain)
        {
            case 2:
                lc.currentTerrain = LevelController.LevelTerrain.terrain_dynamic;
                break;
            case 3:
                lc.currentTerrain = LevelController.LevelTerrain.terrain_random;
                break;
            case 4:
                lc.currentTerrain = LevelController.LevelTerrain.terrain_ice;
                break;
            case 5:
                lc.currentTerrain = LevelController.LevelTerrain.terrain_sand;
                break;
            case 6:
                lc.currentTerrain = LevelController.LevelTerrain.terrain_rubber;
                break;
            case 1:
            default:
                lc.currentTerrain = LevelController.LevelTerrain.terrain_no;
                break;
        }

        Debug.Log("Terrain chosen: " + lc.currentTerrain);

        //Load into the game
        lc.loadLevel(lc.selectedLevel);
    }
}