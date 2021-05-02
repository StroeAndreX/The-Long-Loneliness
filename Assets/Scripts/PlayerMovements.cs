using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovements: MonoBehaviour {
    private int _kLeft;
    private int _kRight;
    private int _kUp;
    private int _kDown;

    private int _canMove;
    private int _newInput;

    private float _movementDelayTimer = 0;
    private int _movementDelay = 20;  

    private Vector3 velocity = Vector3.zero;
    public Vector3 nextPlatform = Vector3.zero; 
    private float startTime; 

    public int crystals = 0;
    public int neededCrystals = 0;
    private Death death;

    public bool canAttack = false;
    public Attack attack;

    private AudioSource _audioSource;
    public AudioClip crystal;
    public AudioClip walk;

    void Start() {
        _canMove = 1; 
        _newInput = 0; 

        startTime = Time.time;
        nextPlatform = transform.position;

        death = transform.gameObject.GetComponent<Death>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        isometricMove();
        travelWayPoint();
        collectCrystal();

        animate();
    }

    private bool nextLevelPlatform = false; 

    public KeyCode nextLevelKeyCode; 
    void isometricMove() {
        // _kLeft = Convert.ToInt32(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
        // _kRight = Convert.ToInt32(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
        // _kUp = Convert.ToInt32(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
        // _kDown = Convert.ToInt32(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));
        _kLeft = Convert.ToInt32(Input.GetKey(KeyCode.LeftArrow));
        _kRight = Convert.ToInt32(Input.GetKey(KeyCode.RightArrow));
        _kUp = Convert.ToInt32(Input.GetKey(KeyCode.UpArrow));
        _kDown = Convert.ToInt32(Input.GetKey(KeyCode.DownArrow));

        //Check if there are movements inputs
        _newInput = (_kRight + _kLeft + _kUp + _kDown) * _canMove; 
        
        // Delay between moving from one platform to another
        if(nextLevelPlatform && Input.GetKeyDown(nextLevelKeyCode))
        {
            nextLevelPlatform = false;

            if(crystals == neededCrystals)
            {
                StartCoroutine(Fading());
            }
            return;
        }

        if(isTravel)
        {
            if(_newInput >= 1) isTravel = false; 

            _newInput = 0;
            return; 
        }

        if(_newInput >= 1 && _movementDelayTimer == 0 && !isTravel)
        {
            nextPlatform = move(_kLeft, _kRight, _kDown, _kUp);
            return; 
        } 
        else 
        {
            _movementDelayTimer += 80f * Time.deltaTime;
            if(_movementDelayTimer >= _movementDelay) _movementDelayTimer = 0;
            if(_newInput == 0) _movementDelayTimer = 0; // ---> In this way the user can spam the key to move faster ... 
        }

        // Return to original position in case of death 
        if(death.isDeath)
        {
            nextPlatform = death.initPosition;
            transform.position = nextPlatform;

            StartCoroutine( attack.waiter());
        } 

        float distCovered =  15f; // (Time.time - startTime) * (15f * Time.deltaTime); 
        float distance = Vector3.Distance(transform.position, nextPlatform);
        float fractionOfJurney = (distCovered / distance) * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, nextPlatform, 15f * Time.deltaTime);
    }   

    Vector3 move(int left, int right, int down, int up) 
    {
        _movementDelayTimer = 1;
        Vector2 nextPlatform;
        // float x = transform.position.x + (float)((-left * 0.25) + (right * 0.25) + (down * 0.25) + (up * 0.25));
        // float y = transform.position.y + (float)((-left * 0.25) + (right * 0.25) + (-down * 0.25) + (-up * 0.25));
        float x = transform.position.x + (float)((-left * 0.8) + (right * 0.8) + (down * 0.8) + (-up * 0.8));
        float y = transform.position.y + (float)((-left * 0.8) + (right * 0.8) + (-down * 0.8) + (up * 0.8));
        nextPlatform = new Vector2(x, y);

        Debug.DrawLine(transform.position, nextPlatform);

        RaycastHit2D _ray = Physics2D.Raycast(nextPlatform, Vector3.zero);

        if(_ray.collider != null)
        {
            _audioSource.PlayOneShot(walk);
        }
        Vector3 nextPlatformPosition = new Vector3(_ray.collider.transform.position.x, _ray.collider.transform.position.y, -1);
        Vector3 smoothMove = Vector3.SmoothDamp(transform.position, nextPlatformPosition, ref velocity, 0.001f * Time.deltaTime);

        _canMove = 1; 

        return smoothMove;
    }
    
    private float increase = 0;
    void animate() {
        float scale;

        increase += 0.0001f + Time.deltaTime;

        scale = (float)(1f + (Mathf.Abs(Mathf.Sin(increase) * 0.1f)));
        Debug.Log(increase);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.tag == "nextLevel") nextLevelPlatform = true; 

        if(other.tag == "waypoint")  canTravel = true;
           
        if(other.tag == "crystal") 
        {
            canCollectCrystal = true;
            crystalObject = other.gameObject;
        }

        if(other.tag == "shot") canAttack = true;
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "nextLevel") nextLevelPlatform = false;     

        if(other.tag == "waypoint")  canTravel = false;

        if(other.tag == "crystal") 
        {
            canCollectCrystal = false; 
            crystalObject = null;
        }

        if(other.tag == "shot") 
        {
            canAttack = false;
            attack.startWaiter();
        }

    }


    #region Crystals 
    
    private bool canCollectCrystal = false; 
    private GameObject crystalObject;
    private void collectCrystal() 
    {
        bool _kCollect = Input.GetKeyDown(KeyCode.C);

        if(_kCollect && canCollectCrystal)
        {
            _audioSource.PlayOneShot(crystal);

            crystals++;
            Destroy(crystalObject);
        }
    }

    #endregion

    #region WayPoints -- 

    public bool isTravel = false;
    public bool canTravel = false;
    void travelWayPoint() {
        bool _kTravel = Input.GetKeyDown(KeyCode.Z);

        if(_kTravel && canTravel)
        {
            isTravel = true;
        }

        if(isTravel) 
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Debug.Log(hit.collider.tag);
            if(Input.GetMouseButtonDown(0))
            {
                if(hit.collider.tag == "waypoint") 
                {
                    Vector3 smoothMove = Vector3.SmoothDamp(transform.position, hit.collider.transform.position, ref velocity, 0.05f * Time.deltaTime);
                    nextPlatform = smoothMove;
                    transform.position = smoothMove;

                    isTravel = false;
                }
            }
        }
    }

    #endregion

    #region NextLevel

    // Fade - Next Level
    public int index;
    public String nextLevel;
    
    public Image black;
    public Animator animator; 

    IEnumerator Fading() 
    {
        animator.SetBool("Fade", true);
        yield return new WaitUntil(()=>black.color.a == 1);
        SceneManager.LoadScene(nextLevel);
    }    

    #endregion

}



/* 
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 20;

        RaycastHit2D nextPlaform;
        Debug.DrawRay(mousePosition, mousePosition - Camera.main.ScreenToWorldPoint(mousePosition), Color.blue);

        float horizontalInput = Input.GetAxis ("Horizontal"); 
        float verticalInput = Input.GetAxis ("Vertical"); 

        Debug.Log(horizontalInput / 2);

        nextPlaform = Physics2D.Raycast(new Vector2(this.transform.position.x + (horizontalInput / 2), this.transform.position.y - (horizontalInput / 2)), Vector3.zero);

        if(nextPlaform.collider != null && nextPlaform.collider.name != "Player")
        {
            Vector3 nextPos = new Vector3(nextPlaform.collider.transform.position.x, nextPlaform.collider.transform.position.y, -1);
            // Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, nextPos, ref velocity, 0.05f * Time.deltaTime);//Vector3.Lerp(transform.position, camPos, cameraSpeed * Time.deltaTime); ;
            transform.position = nextPos;

            Debug.Log("Next Platform ID: " + nextPlaform.collider.gameObject.name);
        }


*/


/* 

  TODO: 
  -Movememnts
  -TP
  -Attack 
  
  */