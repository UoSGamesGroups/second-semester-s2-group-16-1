using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectCanvasController : MonoBehaviour {
   
    //Level controller
    GameObject levelController;
    LevelController lc;

    //Sprites
    [Header("Sprites")]
    public Sprite sprite_octagonNeon;
    public Sprite sprite_squareNeon;
    public Sprite sprite_squareJohnOne;
    public Sprite sprite_squareJohnTwo;
    public Sprite sprite_squareCaitlinOne;

    //UI Images
    [Header("UI Preview Image")]
    public GameObject previewImage;

    //
    int numOfLevels = 5;
    int currentLevel;

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
	}

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

        updatePreviewImage();
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

        updatePreviewImage();
    }

    public void loadLevel()
    {
        lc.selectedLevel = currentLevel;
        SceneManager.LoadScene(1);
    }

    void updatePreviewImage()
    {
        switch (currentLevel)
        {
            case 1:
                previewImage.GetComponent<Image>().sprite = sprite_octagonNeon;
                break;
            case 2:
                previewImage.GetComponent<Image>().sprite = sprite_squareNeon;
                break;
            case 3:
                previewImage.GetComponent<Image>().sprite = sprite_squareCaitlinOne;
                break;
            case 4:
                previewImage.GetComponent<Image>().sprite = sprite_squareJohnOne;
                break;
            case 5:
                previewImage.GetComponent<Image>().sprite = sprite_squareJohnTwo;
                break;
            default:
                break;
        }
    }

}
