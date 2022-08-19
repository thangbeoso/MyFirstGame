using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

namespace RedGunner
{
    public class RedController : MonoBehaviour
    {
        private static RedController _instance;
        public static RedController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<RedController>();
                }
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<RedController>();
                }
                return _instance;
            }
        }
        [SerializeField] GameObject[] _objBullet;
        [SerializeField] Transform[] _btnBullet;
        [SerializeField] Transform _shootPoint;
        [SerializeField] ConfigPlayer _configPlayer;
        [SerializeField] Rigidbody2D _rg;
        [SerializeField] Animator _anim;
        [SerializeField] ConfigBoss _configBoss;
        [SerializeField] CanvasPlayer _canvasPlayer;
        [SerializeField] TextMeshProUGUI _txtLife;
        [SerializeField] AudioClip _aClipBoss;
        [SerializeField] PlayerState _redState;
        [SerializeField] LayerMask _layerTarget;       
        private Vector2 _checkPoint;
        private int _countJump, _currentBullet;
        private bool _statusGround, _statusCanJump, _isPlayerDead;
        private bool _isInputEnabled = true;
        private int _hpCurrentPlayer;
        private float t = 0;
        public int HpPlayer
        {
            get => _hpCurrentPlayer;
            set
            {
                _hpCurrentPlayer = value;
                _hpCurrentPlayer = Mathf.Clamp(_hpCurrentPlayer, 0, _configPlayer._hpMaxPlayer);
                _isPlayerDead = _hpCurrentPlayer <= 0;
                if (_isPlayerDead)
                {
                    PlayerDead();
                    RedState = PlayerState.Dead;
                    _isInputEnabled = false;
                }
            }
        }

        public PlayerState RedState { get => _redState; set => _redState = value; }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            HpPlayer = _configPlayer._hpMaxPlayer;
            _checkPoint = transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.down * _configPlayer._distanceGround);
        }


        void Update()
        {
            if (_isInputEnabled)
            {
                float scaleX = Mathf.Abs(transform.localScale.x);
                var horizontal = Input.GetAxis("Horizontal");
                _statusGround = Physics2D.Raycast(transform.position, Vector2.down, _configPlayer._distanceGround, _layerTarget);
                if (_statusGround)
                {
                    transform.rotation = Quaternion.identity;
                    RedState = PlayerState.Idle;
                    PlayerIdle();
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    RedState = PlayerState.RunRight;
                    PlayerMove(scaleX, horizontal);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    RedState = PlayerState.RunLeft;
                    PlayerMove(-scaleX, horizontal);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow) & _statusCanJump)
                {
                    RedState = PlayerState.Jump;
                    PlayerJump();
                }
                if (Input.GetMouseButtonDown(1))
                {
                    RedState = PlayerState.Shoot;
                    PlayerShoot();
                }
            }
            if (_countJump > 0)
            {
                _statusCanJump = true;
            }
            else _statusCanJump = false;
        }

        public void SelectedBullet()
        {
            for (int i = 0; i < _btnBullet.Length; i++)
            {
                var objBullet = EventSystem.current.currentSelectedGameObject.name;
                if (objBullet == null)
                {
                    _currentBullet = 0;
                }
                else if (objBullet == _btnBullet[i].name)
                {
                    _currentBullet = i;
                }
            }
        }

        private void PlayerIdle()
        {
            if (t <= 1)
            {
                t += Time.deltaTime * 2;
            }
            Vector2 rigid = _rg.velocity;
            rigid.x = Mathf.Lerp(_rg.velocity.x, 0, t);            
            _rg.velocity = rigid;

            _anim.SetInteger("StatusRed", 0);
            _statusCanJump = true;
            _countJump = _configPlayer._maxJump;
        }

        private void PlayerMove(float scaleX, float horizontal)
        {
            _anim.SetInteger("StatusRed", 1);
            _rg.velocity = new Vector2(_configPlayer._speedRun * horizontal, _rg.velocity.y);
            transform.localScale = new Vector2(scaleX, transform.localScale.y);
            t = 0;
        }

        private void PlayerJump()
        {
            _anim.SetInteger("StatusRed", 2);
            _rg.velocity = new Vector2(_rg.velocity.x, _configPlayer._speedJump);
            _countJump--;
            t = 0;
        }

        private void PlayerShoot()
        {
            GameObject ammoShoot = Instantiate(_objBullet[_currentBullet], _shootPoint.position, _shootPoint.rotation) as GameObject;
            if (transform.localScale.x > 0)
            {
                ammoShoot.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _configPlayer._firingForce);
            }
            else
            {
                Vector2 scale2 = ammoShoot.transform.localScale;
                scale2.x = -scale2.x;
                ammoShoot.transform.localScale = scale2;
                ammoShoot.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _configPlayer._firingForce);
            }
        }

        private void PlayerDead()
        {
            _anim.SetInteger("StatusRed", 3);
            _rg.velocity = new Vector2(0, _rg.velocity.y);
        }

        private void MoveCheckPoint()
        {            
            if (_canvasPlayer.Life > 0)
            {
                transform.position = _checkPoint;
                HpPlayer = _configPlayer._hpMaxPlayer;
                _isInputEnabled = true;
                PlayerIdle();
                RedState = PlayerState.Idle;
                _txtLife.SetText($"Life: {_canvasPlayer.Life}");
                EventManager.Instance.OnCheckPoint?.Invoke();
            }
        }
        private void DefeatGame()
        {
            _canvasPlayer.Life--;
            if (_canvasPlayer.Life == 0 & _isPlayerDead)
            {
                SceneManager.LoadScene("DefeatGame");
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Moving Ground"))
            {
                transform.SetParent(collision.transform);
                RedState = PlayerState.Idle;
            }
            else
            {
                transform.SetParent(null);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("coin"))
            {
                Destroy(collision.gameObject);
            }
            if (collision.CompareTag("checkpoint"))
            {
                _checkPoint = collision.transform.position;
                collision.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (collision.CompareTag("ammoBoss"))
            {
                RedController.Instance.HpPlayer -= _configBoss._dmgBoss * _canvasPlayer.DifLevel;
                EventManager.Instance.OnPlayerHpChanged?.Invoke();
            }
            if (collision.CompareTag("ammogun"))
            {
                RedController.Instance.HpPlayer -= 5;
                EventManager.Instance.OnPlayerHpChanged?.Invoke();
            }
            if (collision.CompareTag("emptywall"))
            {
                SoundManager.Instance._audioSourceBG.clip = _aClipBoss;
                SoundManager.Instance._audioSourceBG.Play();
            }
        }
        public enum PlayerState
        {
            None = 0,
            Idle = 1,
            RunRight = 2,
            RunLeft = 3,
            Shoot = 4,
            Jump = 5,
            Dead = 6
        }
    }
}
