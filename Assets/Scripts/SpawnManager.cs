using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float delay = 2.0f;
    public bool enable;

    public GameObject[] obstaclePrefab;
    private PlayerController playerControllerScript;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        Invoke("SpawnObstacle",delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
       
        if (!playerControllerScript.gameOver && playerControllerScript.gameStarted && enable)
        {
            delay = Random.Range(1.0f, 3.0f);
            index = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[index], spawnPos, obstaclePrefab[index].transform.rotation);
            delay = Random.Range(0.5f, 3.0f);
            Invoke("SpawnObstacle", delay);
        }
    }
}
