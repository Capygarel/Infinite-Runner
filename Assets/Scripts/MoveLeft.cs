using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10;
    private PlayerController playerControllerScript;
    private float leftBound = -10;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerControllerScript.gameOver && playerControllerScript.gameStarted)
            transform.Translate(Vector3.left * Time.deltaTime * speed);

        if (playerControllerScript.isRunning)
            speed = 30;
        else
            speed = 10;

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
            Destroy(gameObject);
    }
}
