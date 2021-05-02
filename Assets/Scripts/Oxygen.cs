using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Oxygen : MonoBehaviour
{
    public float decreaseIntensity = 0.25f; 
    public float oxygenQuantity = 100f;

    private TextMeshPro mText;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        mText = gameObject.GetComponent<TextMeshPro>();

        player = GameObject.Find("Player");
    }

    private bool canDecrease = false;
    // Update is called once per frame
    void Update()
    {
        if(!canDecrease)
        {
            int _kLeft = Convert.ToInt32(Input.GetKey(KeyCode.LeftArrow));
            int _kRight = Convert.ToInt32(Input.GetKey(KeyCode.RightArrow));
            int _kUp = Convert.ToInt32(Input.GetKey(KeyCode.UpArrow));
            int _kDown = Convert.ToInt32(Input.GetKey(KeyCode.DownArrow));

            //Check if there are movements inputs
            int _newInput = (_kRight + _kLeft + _kUp + _kDown);

            if(_newInput >= 1) canDecrease = true;
        }

        if(oxygenQuantity >= 0 && canDecrease)
        {
            if(player.GetComponent<Death>().isDeath) 
            {
                canDecrease = false;
                return;
            }

            oxygenQuantity -= decreaseIntensity * Time.deltaTime;

            if(oxygenQuantity <= 15)
            {
                mText.color = Color.red; 
            }
        }        


        string textLevel = string.Format("{0:0.00}", oxygenQuantity); 
        mText.SetText("Oxygen: " + textLevel);
    }


    public void resetOxygenQuantity() 
    {
        oxygenQuantity = 100f;
    }
}

