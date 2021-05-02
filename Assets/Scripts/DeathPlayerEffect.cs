using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayerEffect : MonoBehaviour
{   
    private SpriteRenderer spriteRenderer;
    private Color spriteColor; 
    private void Start() {
         spriteRenderer = GetComponent<SpriteRenderer>();
         spriteColor = spriteRenderer.material.color;
    }


    // Update is called once per frame
    void Update()
    {
        spriteColor.a -= 0.5f * Time.deltaTime;

        spriteRenderer.color = spriteColor;

        if(spriteColor.a <= 0) Destroy(this.gameObject);
    }
}
