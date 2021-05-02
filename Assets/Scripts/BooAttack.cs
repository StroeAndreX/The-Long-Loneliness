using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooAttack : MonoBehaviour {
    public GameObject energyBall; 
    public int attackType = 0;

    private int alternateShooting = 1; 

    private void Start() {
        StartCoroutine(attack());
    }

    public void resetAttack() 
    {
        StartCoroutine(attack());
    }

    IEnumerator attack() {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            if(attackType == 0)
            {
                for(int i = 0; i < 4; i++)
                {
                    GameObject newGameObject = Instantiate(energyBall, transform.position, Quaternion.identity);
                    EnergyBallScript energyBallScript = newGameObject.GetComponent<EnergyBallScript>();
                    energyBallScript.direction = i;
                }
            }

            if(attackType == 1)
            {
                yield return new WaitForSeconds(1f);
            
                for(int i = 0; i < 2; i++)
                {
                    GameObject newGameObject = Instantiate(energyBall, transform.position, Quaternion.identity);
                    EnergyBallScript energyBallScript = newGameObject.GetComponent<EnergyBallScript>();
                    energyBallScript.direction = i;
                }

                yield return new WaitForSeconds(1f);
            
                for(int i = 2; i < 4; i++)
                {
                    GameObject newGameObject = Instantiate(energyBall, transform.position, Quaternion.identity);
                    EnergyBallScript energyBallScript = newGameObject.GetComponent<EnergyBallScript>();
                    energyBallScript.direction = i;
                }
            }

            if(attackType == 2)
            {
                yield return new WaitForSeconds(0.6f);
            
                for(int i = 0; i < 2; i++)
                {
                    GameObject newGameObject = Instantiate(energyBall, transform.position, Quaternion.identity);
                    EnergyBallScript energyBallScript = newGameObject.GetComponent<EnergyBallScript>();
                    energyBallScript.direction = i;
                }
            }

            if(attackType == 3)
            {
                yield return new WaitForSeconds(0.7f);
    
                for(int i = 2; i < 4; i++)
                {
                    GameObject newGameObject = Instantiate(energyBall, transform.position, Quaternion.identity);
                    EnergyBallScript energyBallScript = newGameObject.GetComponent<EnergyBallScript>();
                    energyBallScript.direction = i;
                }
            }

        }

    } 
}