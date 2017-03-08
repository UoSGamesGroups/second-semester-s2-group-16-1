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
    public GameObject previewImage;


    int numberOfOptions = 6;
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
        if (currentTerrain < numberOfOptions)
        {
            currentTerrain++;
        }
        else if (currentTerrain == numberOfOptions)
        {
            currentTerrain = 1;
        }

        updatePreviewImage();
    }

    public void decreaseTerrain()
    {
        if (currentTerrain > 1)
        {
            currentTerrain--;
        }
        else if (currentTerrain == 1)
        {
            currentTerrain = numberOfOptions;
        }

        updatePreviewImage();
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

    void updatePreviewImage()
    {
        switch (currentTerrain)
        {
            case 1:
                previewImage.GetComponent<Image>().sprite = sprite_noTerrain;
                break;
            case 2:
                previewImage.GetComponent<Image>().sprite = sprite_dynamicTerrain;
                break;
            case 3:
                previewImage.GetComponent<Image>().sprite = sprite_randomTerrain;
                break;
            case 4:
                previewImage.GetComponent<Image>().sprite = sprite_iceTerrain;
                break;
            case 5:
                previewImage.GetComponent<Image>().sprite = sprite_sandTerrain;
                break;
            case 6:
                previewImage.GetComponent<Image>().sprite = sprite_rubberTerrain;
                break;
            default:
                break;
        }
    }
}
