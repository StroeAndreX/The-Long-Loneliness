using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // Fade - Next Level
    public int index;
    public string nextLevel;
    
    public Image black;
    public Animator animator; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.name == "Play")
                {
                    StartCoroutine(Fading());
                }
            }
        }
    }

     void OnMouseDown(){
   // this object was clicked - do something
                    StartCoroutine(Fading());

        }

    IEnumerator Fading() 
    {
        animator.SetBool("Fade", true);
        yield return new WaitUntil(()=>black.color.a == 1);
        SceneManager.LoadScene("Deep 1");
    }        
}
