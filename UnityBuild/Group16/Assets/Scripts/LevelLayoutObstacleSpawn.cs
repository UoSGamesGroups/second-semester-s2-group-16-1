using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutObstacleSpawn : MonoBehaviour {

    [Header("Static obstacles")]
    public GameObject prefabStaticOctagon;

    [Header("Dynamic obstacles")]
    public GameObject prefabDynamicSquare;
    public GameObject prefabDynamicCircle;
    public GameObject prefabDynamicFan;

	// Use this for initialization
	void Start ()
    {
		if (this.gameObject.name == "OctagonLevel(Clone)")
        { octLevelSpawn(); }
        else if (this.gameObject.name == "SquareLevel(Clone)" || this.gameObject.name == "SquareLevel2(Clone)")
        { squLevelSpawn(); }
	}

    void octLevelSpawn()
    {
        GameObject[] objArray = { prefabStaticOctagon };
        int index = Random.Range(0, objArray.Length - 1);
        Instantiate(objArray[index], new Vector2(0.0f, 0.0f), Quaternion.identity);
    }

    void squLevelSpawn()
    {
        GameObject[] objArray = { prefabDynamicCircle, prefabDynamicSquare, prefabDynamicFan };
        int index = Random.Range(0, objArray.Length);
        Instantiate(objArray[index], new Vector2(0.0f, 0.0f), Quaternion.identity);
    }

}
