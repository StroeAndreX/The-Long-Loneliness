using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public Vector3 initPosition; 
    public Oxygen oxygen; 

    public GameObject deathPlayer;
    public GameObject ParticleObject;

    public GameObject[] enemies;

    public bool isDeath = false;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;

        //oxygen = transform.GetComponent<Oxygen>();
    }

    // Update is called once per frame
    void Update()
    {
        if(oxygen.oxygenQuantity <= 0)
        {
            ParticleObject.SetActive(false);
            isDeath = true;
            StartCoroutine(death());
            oxygen.resetOxygenQuantity(); 
        }
        if (isDeath) {
        foreach(GameObject t in enemies)
        {
            if(t.activeInHierarchy == false)
            {
                t.GetComponent<Enemy>().hp = 100;
                t.SetActive(true);
                t.GetComponent<BooAttack>().resetAttack();
                t.GetComponent<Enemy>().hp = 100;
            }
            else
            {
                t.GetComponent<Enemy>().hp = 100;
            }
         
        }}

    }    


    IEnumerator death() 
    {
        Instantiate(deathPlayer, this.transform.position, Quaternion.identity);      
        yield return new WaitForSeconds(0.5f);
        transform.position = initPosition;  
        isDeath = false; 
        ParticleObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "enemy")
        {
            ParticleObject.SetActive(false);
            isDeath = true;
            StartCoroutine(death());
            oxygen.resetOxygenQuantity(); 
        }
    }
}
