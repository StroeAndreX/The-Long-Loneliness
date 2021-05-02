using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEnable : MonoBehaviour
{
    public GameObject textEdit; 

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "crystal") {
            textEdit.SetActive(true);
        }
    }
 
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "crystal") {
            textEdit.SetActive(false);
        }
    }
}
