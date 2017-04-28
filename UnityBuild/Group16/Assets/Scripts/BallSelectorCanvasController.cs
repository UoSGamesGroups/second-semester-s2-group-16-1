using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSelectorCanvasController : MonoBehaviour {

    [Header("Left chosen balls")]
    public GameObject[] leftBalls = new GameObject[3];

    [Header("Right chosen balls")]
    public GameObject[] rightBalls = new GameObject[3];

    [Header("Sprites")]
    public Sprite sprite_ball_none;
    public Sprite sprite_ball_balloon;
    public Sprite sprite_ball_steel;
    public Sprite sprite_ball_gum;
    public Sprite sprite_ball_slime;
    public Sprite sprite_ball_fire;
    public Sprite sprite_ball_smoke;

    [Header("Misc")]
    LevelController lc;

	// Use this for initialization
	void Start ()
    {
        lc = GameObject.FindGameObjectWithTag("levelController").GetComponent<LevelController>();
	}

    public void loadLevel()
    {
        lc.loadGame(lc.selectedLevel);
    }

    bool contains(BallController.ballType[] ballArray, int size, BallController.ballType type)
    {
        for (int i = 0; i < size; i++)
        {
            if (ballArray[i] == type)
            {
                return true;
            }
        }
        return false;
    }

    public void Balloon(int player)
    {
        switch (player)
        {
            case 1:
                if (contains(lc.player1Balls, 3, BallController.ballType.balloon)) return;
                for (int i = 0; i < 3; i++)
                {
                    if (lc.player1Balls[i] == BallController.ballType.none)
                    {
                        lc.player1Balls[i] = BallController.ballType.balloon;
                        leftBalls[i].GetComponent<Image>().sprite = sprite_ball_balloon;
                        break;
                    }
                }
                break;
            case 2:
                if (contains(lc.player2Balls, 3, BallController.ballType.balloon)) return;
                for (int i = 0; i < 3; i++)
                {
                    if (lc.player2Balls[i] == BallController.ballType.none)
                    {
                        lc.player2Balls[i] = BallController.ballType.balloon;
                        rightBalls[i].GetComponent<Image>().sprite = sprite_ball_balloon;
                        break;
                    }
                }
                break;
            default:
                Debug.Log("Error setting player ball.");
                break;
        }
    }

    public void Steel(int player)
    {
        switch (player)
        {
            case 1:
                if (contains(lc.player1Balls, 3, BallController.ballType.steel)) return;
                for (int i = 0; i < 3; i++)
                {
                    if (lc.player1Balls[i] == BallController.ballType.none)
                    {
                        lc.player1Balls[i] = BallController.ballType.steel;
                        leftBalls[i].GetComponent<Image>().sprite = sprite_ball_steel;
                        break;
                    }
                }
                break;
            case 2:
                if (contains(lc.player2Balls, 3, BallController.ballType.steel)) return;
                for (int i = 0; i < 3; i++)
                {
                    if (lc.player2Balls[i] == BallController.ballType.none)
                    {
                        lc.player2Balls[i] = BallController.ballType.steel;
                        rightBalls[i].GetComponent<Image>().sprite = sprite_ball_steel;
                        break;
                    }
                }
                break;
            default:
                Debug.Log("Error setting player ball.");
                break;
        }
    }

    public void Gum(int player)
    {
        switch (player)
        {
            case 1:
                if (contains(lc.player1Balls, 3, BallController.ballType.gum)) return;
                for (int i = 0; i < 3; i++)
                {
                    if (lc.player1Balls[i] == BallController.ballType.none)
                    {
                        lc.player1Balls[i] = BallController.ballType.gum;
                        leftBalls[i].GetComponent<Image>().sprite = sprite_ball_gum;
                        break;
                    }
                }
                break;
            case 2:
                if (contains(lc.player2Balls, 3, BallController.ballType.gum)) return;
                for (int i = 0; i < 3; i++)
                {
                    if (lc.player2Balls[i] == BallController.ballType.none)
                    {
                        lc.player2Balls[i] = BallController.ballType.gum;
                        rightBalls[i].GetComponent<Image>().sprite = sprite_ball_gum;
                        break;
                    }
                }
                break;
            default:
                Debug.Log("Error setting player ball.");
                break;
        }
    }

    public void Slime(int player)
    {
        switch (player)
        {
            case 1:
                if (contains(lc.player1Balls, 3, BallController.ballType.slime)) return;
                for (int i = 0; i < 3; i++)
                {
                    if (lc.player1Balls[i] == BallController.ballType.none)
                    {
                        lc.player1Balls[i] = BallController.ballType.slime;
                        leftBalls[i].GetComponent<Image>().sprite = sprite_ball_slime;
                        break;
                    }
                }
                break;
            case 2:
                if (contains(lc.player2Balls, 3, BallController.ballType.slime)) return;
                for (int i = 0; i < 3; i++)
                {
                    if (lc.player2Balls[i] == BallController.ballType.none)
                    {
                        lc.player2Balls[i] = BallController.ballType.slime;
                        rightBalls[i].GetComponent<Image>().sprite = sprite_ball_slime;
                        break;
                    }
                }
                break;
            default:
                Debug.Log("Error setting player ball.");
                break;
        }
    }

    public void Fire(int player)
    {
        switch (player)
        {
            case 1:
                if (contains(lc.player1Balls, 3, BallController.ballType.fire)) return;
                for (int i = 0; i < 3; i++)
                {
                    if (lc.player1Balls[i] == BallController.ballType.none)
                    {
                        lc.player1Balls[i] = BallController.ballType.fire;
                        leftBalls[i].GetComponent<Image>().sprite = sprite_ball_fire;
                        break;
                    }
                }
                break;
            case 2:
                if (contains(lc.player2Balls, 3, BallController.ballType.fire)) return;
                for (int i = 0; i < 3; i++)
                {
                    if (lc.player2Balls[i] == BallController.ballType.none)
                    {
                        lc.player2Balls[i] = BallController.ballType.fire;
                        rightBalls[i].GetComponent<Image>().sprite = sprite_ball_fire;
                        break;
                    }
                }
                break;
            default:
                Debug.Log("Error setting player ball.");
                break;
        }
    }

    public void Smoke(int player)
    {
        switch (player)
        {
            case 1:
                if (contains(lc.player1Balls, 3, BallController.ballType.smoke)) return;
                for (int i = 0; i < 3; i++)
                {
                    if (lc.player1Balls[i] == BallController.ballType.none)
                    {
                        lc.player1Balls[i] = BallController.ballType.smoke;
                        leftBalls[i].GetComponent<Image>().sprite = sprite_ball_smoke;
                        break;
                    }
                }
                break;
            case 2:
                if (contains(lc.player2Balls, 3, BallController.ballType.smoke)) return;
                for (int i = 0; i < 3; i++)
                {
                    if (lc.player2Balls[i] == BallController.ballType.none)
                    {
                        lc.player2Balls[i] = BallController.ballType.smoke;
                        rightBalls[i].GetComponent<Image>().sprite = sprite_ball_smoke;
                        break;
                    }
                }
                break;
            default:
                Debug.Log("Error setting player ball.");
                break;
        }
    }

    public void clearPlayer(int player)
    {
        switch (player)
        {
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    lc.player1Balls[i] = BallController.ballType.none;
                    leftBalls[i].GetComponent<Image>().sprite = sprite_ball_none;
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++) 
                {
                    lc.player2Balls[i] = BallController.ballType.none;
                    rightBalls[i].GetComponent<Image>().sprite = sprite_ball_none;
                }
                break;
            default:
                Debug.Log("Error clearing player balls");
                break;
        }
    }
}
