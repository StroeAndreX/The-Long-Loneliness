using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Experimental.Rendering.Universal;
using static PlayerMovements; 


public class WayPointFX: MonoBehaviour {
    // Start is called before the first frame update
    public Light2D pointLight;
    public PlayerMovements newPlayerMovements = new PlayerMovements(); 

    public GameObject associatedKey; 
    private SpriteRenderer associatedKeyRenderer; 
    private Color associatedKeyColor; 

    public KeyCode wayPointCode;

    private AudioSource _audioSource;

    private void Start() {
        associatedKeyRenderer = associatedKey.GetComponent<SpriteRenderer>();
        associatedKeyColor = associatedKeyRenderer.material.color;

        _audioSource = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update() {
        if (newPlayerMovements.isTravel) {
            if(associatedKeyColor.a < 1)
            {
                associatedKeyColor.a += 0.1f; 
                associatedKeyRenderer.color = associatedKeyColor;
            }
            //associatedKey.SetActive(true);
            if (pointLight.intensity < 2) {
                pointLight.intensity += 0.15f;
            }

            bool _kWayPoint = Input.GetKeyDown(wayPointCode);
            if(_kWayPoint)
            {
                _audioSource.Play();

                Vector3 thisPos =  this.gameObject.transform.position;
                newPlayerMovements.gameObject.transform.position = new Vector3(thisPos.x, thisPos.y, -1);
                newPlayerMovements.nextPlatform = new Vector3(thisPos.x, thisPos.y, -1);
                newPlayerMovements.isTravel = false;
            }
        }

        if (!newPlayerMovements.isTravel) {
            //associatedKey.SetActive(false);
            if(associatedKeyColor.a > 0)
            {
                associatedKeyColor.a -= 0.1f; 
                associatedKeyRenderer.color = associatedKeyColor;
            }
            if (pointLight.intensity > 0.69) {
                pointLight.intensity -= 0.15f;
            }


        }
    }

    

}