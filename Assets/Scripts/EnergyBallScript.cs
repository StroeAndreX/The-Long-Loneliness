using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction {
    LEFT ,
    RIGHT ,
    UP ,
    DOWN
}


public class EnergyBallScript : MonoBehaviour
{
    public int direction = 0;

    private Direction myDir;

    // Start is called before the first frame update
    void Start()
    {
        myDir = (Direction)direction;
    }

    // Update is called once per frame
    void Update()
    {
        switch(myDir) {
            case Direction.LEFT:
            
                transform.position = new Vector3(transform.position.x - (20f * Time.deltaTime), transform.position.y - (10f * Time.deltaTime), -1);
                break;
            case Direction.RIGHT:
                transform.position = new Vector3(transform.position.x + (20f * Time.deltaTime), transform.position.y + (10f * Time.deltaTime), -1);

                break;
            case Direction.UP:
                transform.position = new Vector3(transform.position.x - (20f * Time.deltaTime), transform.position.y + (10f * Time.deltaTime), -1);

                break;
            case Direction.DOWN:
                transform.position = new Vector3(transform.position.x + (20f * Time.deltaTime), transform.position.y - (10f * Time.deltaTime), -1);

                break;
        }

        if(transform.position.x > 20 || transform.position.x < -20)
        {
            Destroy(this.gameObject);
        }
    }
}
