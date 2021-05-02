using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIndex : MonoBehaviour
{
    public GameObject spriteToChange;
    public GameObject player;

    public float zIndex;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") 
        {
            Debug.Log("Collided");
            spriteToChange.transform.position = new Vector3(spriteToChange.transform.position.x, spriteToChange.transform.position.y, zIndex);
        }
    }
}
