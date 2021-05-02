using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform[] enemies; 
    public GameObject[] enemiesX;
    public GameObject player; 
    
    private Vector3 nextPos; 
    public PlayerMovements playerMovements;

    private bool attack = false;
    private Vector3 initPosition;

    public float speed = 100f;

    private ParticleSystem _particleSystem;
    
    private AudioSource _audioSource;

    private void Start() {
        initPosition = transform.position;
        _particleSystem = this.GetComponent<ParticleSystem>();

        playerMovements = player.GetComponent<PlayerMovements>();
        _audioSource = GetComponent<AudioSource>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Death>().isDeath)
        {

            foreach (GameObject t in enemiesX)
            {
                t.SetActive(true);
                t.SetActive(true);
                t.SetActiveRecursively(true);
                t.GetComponent<Enemy>().hp = 100;
            }

            return;
        }

        Transform enemy = GetClosestEnemy(enemies);
        bool _kAttack = Input.GetKeyDown(KeyCode.X);

        if(_kAttack && playerMovements.canAttack) 
        {
            _audioSource.Play();
            attack = true;
        }

        if(attack && playerMovements.canAttack) 
        {
            arcMove(transform.position, enemy.position, 1f);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.zero);
        if(hit.collider.tag == "enemy")
        {
            StartCoroutine( waiter());
        }


  
    }

    public void startWaiter() 
    {
            StartCoroutine( waiter());
    }

    public IEnumerator waiter() 
    {
        yield return new WaitForSeconds(0.1f);

        transform.position = new Vector3(player.transform.position.x + 0.32f, player.transform.position.y + 0.59f, -6);
        attack = false;

        yield return new WaitForSeconds(0.5f);
    }
    
    private int alt = 1;
    void arcMove(Vector3 startPos, Vector3 targetPos, float arcHeight)
    {   
        float distCovered = speed; // (Time.time - startTime) * (15f * Time.deltaTime); 
        float distance = Vector3.Distance(startPos, targetPos);
        float fractionOfJurney = (distCovered / distance) * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPos.x + Random.Range (-1.2f, 1.2f), targetPos.y + Random.Range (-1.2f, 1.2f), startPos.z), speed * Time.deltaTime);


    }

    static Quaternion LookAt2D(Vector2 forward) 
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }

        if(tMin.gameObject.activeInHierarchy == false) return null; 

        return tMin;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "shot") playerMovements.canAttack = true;

        if(other.tag == "enemy") StartCoroutine( waiter());
    }
    
    // private void OnTriggerExit2D(Collider2D other) {
    //     if(other.tag == "shot") 
    //     {
    //         playerMovements.canAttack = false;
    //         StartCoroutine( waiter());
    //     }
    // }
}
