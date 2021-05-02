using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 100f; 
    private SpriteRenderer spriteRenderer;
    private Color spriteColor; 

    public Attack listOfEnemies; 

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        spriteColor = spriteRenderer.material.color;

        player = GameObject.Find("Player");
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "bullet")
        {
            spriteColor = new Color(1f, 1f, 1f, 1f);
            spriteRenderer.color = spriteColor;

            if(hp <= 0) 
            {
                hp = 100;
                this.gameObject.SetActive(false);
                

                return;
            }

            hp -= 1000f * Time.deltaTime;
            Debug.Log(hp);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "bullet")
        {
            spriteColor = new Color(0f, 0f, 0f, 1f);


            spriteRenderer.color = spriteColor;
        }
    }
}
