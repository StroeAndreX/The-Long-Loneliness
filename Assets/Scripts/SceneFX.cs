using UnityEngine;
using System.Collections;

using static PlayerMovements; 
using UnityEngine.Experimental.Rendering.Universal;

public class SceneFX : MonoBehaviour {
    
     [Header("Entities Data")]
    public PlayerMovements newPlayerMovements = new PlayerMovements(); 
    public Light2D pointLight;
    public Light2D globalLight;

    private void Update() {
        if(newPlayerMovements.isTravel)
        {
            // if(pointLight.intensity < 2)
            // {
            //     pointLight.intensity += 0.15f; 
            // }   

            if(globalLight.intensity > 0.4)
            {
                globalLight.intensity -= 0.025f;
            }
        }

        if(!newPlayerMovements.isTravel)
        {
            // if(pointLight.intensity > 0.69)
            // {
            //     pointLight.intensity -= 0.15f; 
            // }   

            if(globalLight.intensity < 1)
            {
                globalLight.intensity += 0.025f;
            }
        }

    }

}