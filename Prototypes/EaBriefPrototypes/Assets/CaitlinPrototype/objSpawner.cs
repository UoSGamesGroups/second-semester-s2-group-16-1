using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objSpawner : MonoBehaviour {

    float waitTime = 0.5f;
    bool canSpawn;
    bool canSpawnTwo;
    bool canSpawnThree;

    public GameObject spawnItem;

    void Start()
    {
        canSpawn = true;
        canSpawnThree = true;
        StartCoroutine(offSet());
    }

	// Update is called once per frame
	void Update ()
    {
		if (canSpawn)
        {
            StartCoroutine(spawn());
        }
            
        if (canSpawnTwo)
        {
            StartCoroutine(spawnTwo());
        }

        if (canSpawnThree)
        {
            StartCoroutine(spawnThree());
        }

	}

    IEnumerator offSet()
    {
        yield return new WaitForSeconds(waitTime / 2);
        canSpawnTwo = true;
    }

    IEnumerator spawn()
    {
        canSpawn = false;       
        Instantiate(spawnItem, new Vector2(5.5f, 0.72f), Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        canSpawn = true;
    }

    IEnumerator spawnTwo()
    {
        canSpawnTwo = false;
        
        Instantiate(spawnItem, new Vector2(5.5f, 0.13f), Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        canSpawnTwo = true;
    }

    IEnumerator spawnThree()
    {
        canSpawnThree = false;
        Instantiate(spawnItem, new Vector2(5.5f, -0.46f), Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        canSpawnThree = true;
    }
}
