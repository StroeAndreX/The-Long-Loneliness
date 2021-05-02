using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using static PlayerMovements;

public class CrystalsNeededText : MonoBehaviour
{   

    public GameObject player;   
    public string neededCrystals;

    private PlayerMovements playerController;
    private TextMeshPro mText;

    private void Start() {
        mText = gameObject.GetComponent<TextMeshPro>();
        playerController = player.GetComponent<PlayerMovements>();
    }

    // Update is called once per frame
    void Update()
    {
        mText.SetText(playerController.crystals.ToString() + "/" + neededCrystals);
    }
}
