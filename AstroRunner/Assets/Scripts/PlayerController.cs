using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Settings")]

        [Tooltip("Strafe speed of our player.")] public float strafeSpeed = 0.25f;

        [Tooltip("Spell object itself.")] public GameObject _magic;

        [Tooltip("Starting position of our magic spell.")] public Transform _magicStartPosition;

        [Tooltip("Icons that indicate that our player hasn't lost a life.")] public Texture _aliveIcon;

        [Tooltip("Icons that indicate that our player has lost a life.")] public Texture _deadIcon;

        [Tooltip("Array of alive and dead icons.")] public RawImage[] _icons;

        [Tooltip("Panel that shows game over message.")] public GameObject _gameOverPanel;
        
        [Tooltip("Joystick UI placeholder.")] public Joystick joystick;
	
	private Animator _anim;
   
        private Vector3 _startPosition; // starting position of our player(this refers only to x and z axis)

        private Vector3 _horizontalMovement;

        private Rigidbody _rb, _magicRigidBody;

        public static GameObject _player; // reference for our PlayerController script

        public static GameObject _currentPlatform;
	
	public static AudioSource [] _sfx; // array of sound effects 

        private bool _turnLeft, _turnRight, _canJump, _isFalling, _canTurn;
	
	public static bool _isDead = false;
	
        private int _livesLeft;

        private Vector2 _touchStart = Vector2.zero; 
	
        private Vector2 _touchEnd = Vector2.zero;
	
        private Vector2 _touchDelta = Vector2.zero;

        private void OnCollisionEnter([NotNull]Collision _other) 
        {
            if ((_isFalling || _other.gameObject.tag == "Fire" || _other.gameObject.tag == "Wall") && !_isDead)
            {
                _sfx[6].Play();

                if (_isFalling)
                    _anim.SetTrigger("isFalling");
                else 
                    _anim.SetTrigger("isDead");

                _isDead = true;
                _livesLeft--; 
                PlayerPrefs.SetInt("lives", _livesLeft);

                if (_livesLeft > 0) 
                    Invoke(nameof(RestartGame), 2f);
                else 
                {
                    _icons[0].texture = _deadIcon;
                    _gameOverPanel.SetActive(true); // display game over message on screen

                    // we set our last score to be equal of whatever score we achieved
                    PlayerPrefs.SetInt("lastscore", PlayerPrefs.GetInt("score"));

               if (PlayerPrefs.HasKey("highscore"))
               {
                   int _hs = PlayerPrefs.GetInt("highscore");
                    
                   // if our highscore is less than score we obtained earlier
                   if (_hs < PlayerPrefs.GetInt("score"))
                        // set it as new highscore 
                        PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
               }
               else 
                  // we set the highscore at the beggining so we can update it later 
                  PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
             }
        }
            else 
                _currentPlatform = _other.gameObject;  
      }

        private void Start()
        {
            _anim = this.GetComponent<Animator>();
            _rb = this.GetComponent<Rigidbody>();
            _magicRigidBody = _magic.GetComponent<Rigidbody>();
            _sfx = GameObject.FindWithTag("gamedata").GetComponentsInChildren<AudioSource>();
            _player = this.gameObject;
            _startPosition = _player.transform.position;
            GenerateWorld.RunDummy();
            
            _isDead = false;
            _livesLeft = PlayerPrefs.GetInt("lives");

            /*here we loop through array and check if
            we lost a life and if we lost life our 
            alive icon will become dead icon*/
            for (int i = 0; i < _icons.Length; i++)
                if (i >= _livesLeft)
                    _icons[i].texture = _deadIcon;
            }

        private void OnTriggerEnter([NotNull]Collider _other) 
        {
            // if we triggered BoxCollider and if we are not at TSection generate our platforms
            if (_other is BoxCollider && GenerateWorld._lastPlatform.tag != "platformTSection")
                 GenerateWorld.RunDummy();
        
            // if we come to TSection we can turn 
            if (_other is SphereCollider)
                 _canTurn = true; 
        }

        private void OnTriggerExit([NotNull]Collider _other) 
        {
            if (_other is SphereCollider)
                _canTurn = false;
        }

        private void Update() 
        {
            if (PlayerController._isDead) return;

            if (_currentPlatform != null)
            {
                // check position of our player on y axis and compare it to currentPlatform position
                if (this.transform.position.y < (_currentPlatform.transform.position.y -1))
                { 
                    _isFalling = true;
                    OnCollisionEnter(null);
                }
            }

            SwipeCheck();
            DoubleTap();
            LeftRightMovement();
        }

        private void SwipeCheck()
        {
            _touchDelta = Vector2.zero;

            if (Input.touches.Length > 0) 
            {
	            if (Input.touches[0].phase == TouchPhase.Began)
	                _touchStart = Input.touches[0].position;
        
	            else if (Input.touches[0].phase == TouchPhase.Ended)
	            {
	                _touchEnd = Input.touches[0].position;

	                _touchDelta = _touchStart - _touchEnd;

                    // swipe left
	            if (_touchDelta.x > -50 && _canTurn)
                    {
                        _turnLeft = true;
                        TurnLeft();
                    }
                    // swipe right
	            else if(_touchDelta.x < 50 && _canTurn)
                    {
                        _turnRight = true;
                        TurnRight();
                    }
                    // swipe up
	            else if(_touchDelta.y < -50)	
                    {
                        _canJump = true;
                        StartJumping();
                    }
	        }
            }
        }

        private void DoubleTap()
        {
            foreach (Touch _t in Input.touches)
            {
                if (_t.tapCount == 2 && _anim.GetBool("isJumping") == false)
                    CastSpell();
            }
        }
    
        private void StartJumping()
        {
           if (_canJump && _anim.GetBool("isMagic") == false)
            { 
                 _anim.SetBool("isJumping", true);
                 _sfx[2].Play();
                 _rb.AddForce(Vector3.up * 200);
            }
        }
            
        private void StopJumping()
        {
            _anim.SetBool("isJumping", false);
        }

                
        private void StopCastingSpell()
        {
            _anim.SetBool("isMagic", false);
        }

        private void TurnRight()
        {
            if (_canTurn && _turnRight)
                RotateRight();
        }

        private void TurnLeft()
        {
            if (_canTurn && _turnLeft)
                RotateLeft();
        }

        private void LeftRightMovement()
        {
            _horizontalMovement = new Vector3 (joystick.Horizontal, 0f, 0f);
            this.transform.Translate(_horizontalMovement * strafeSpeed);
        }
    
        private void RotateRight()
        {
            this.transform.Rotate(Vector3.up * 90); // we rotate our player by 90 degrees right
            GenerateWorld._dummyTraveller.transform.forward = -this.transform.forward; // set dummyTraveller position
            GenerateWorld.RunDummy();

            if (GenerateWorld._lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();
        
            // then we update our new player position to be equal to the starting position of our player 
            this.transform.position = new Vector3(_startPosition.x, this.transform.position.y, _startPosition.z);
        }

        private void RotateLeft()
        {
            this.transform.Rotate(Vector3.up * -90);
            GenerateWorld._dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld._lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();
        
            this.transform.position = new Vector3(_startPosition.x, this.transform.position.y, _startPosition.z);
        }

        private void CastSpell()
        {
            _anim.SetBool("isMagic", true);
        }

        private void CastMagic()
        {
            /*set starting position of our spell object to be equal 
            to magic starting posititon(right hand of our player)*/
            _magic.transform.position = _magicStartPosition.position;
            _magicRigidBody.velocity = Vector3.zero;
            _magic.SetActive(true); 
            _magicRigidBody.AddForce(this.transform.forward * 1000);
            _sfx[7].Play();
            Invoke(nameof(KillMagic), 1f);
        }

        private void FootStep1()
        {
            _sfx[4].Play();
        }

        private void FootStep2()
        {
            _sfx[3].Play();
        }

        private void KillMagic()
        {   
            _magic.SetActive(false);
        }

        private void RestartGame() 
        {
            SceneManager.LoadScene("Platforms", LoadSceneMode.Single);
        }
    }
}

