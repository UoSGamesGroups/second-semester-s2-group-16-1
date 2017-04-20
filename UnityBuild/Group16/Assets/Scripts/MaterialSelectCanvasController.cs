using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSelectCanvasController : MonoBehaviour {

    //Level controller
    GameObject levelController;
    LevelController lc;

    //Sprites
    [Header("Sprites")]
    public Sprite sprite_noTerrain;
    public Sprite sprite_dynamicTerrain;
    public Sprite sprite_randomTerrain;
    public Sprite sprite_iceTerrain;
    public Sprite sprite_sandTerrain;
    public Sprite sprite_rubberTerrain;

    [Header("UI Preview Image")]
    public GameObject terrainPreviewImage;


    int numberOfTerrains = 6;
    int currentTerrain;

	// Use this for initialization
	void Start ()
    {
        GameObject[] lcs = GameObject.FindGameObjectsWithTag("levelController");
        if (lcs.Length > 1)
        {
            for (int i = 1; i < lcs.Length; i++)
            {
                Destroy(lcs[i].gameObject);
            }
        }

        currentTerrain = 1;
        levelController = GameObject.Find("LevelController");
        lc = levelController.GetComponent<LevelController>();
    }
	
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

    public void loadLevel()
    {
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
}
