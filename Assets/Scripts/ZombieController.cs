using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RedGunner
{
    public class ZombieController : MonoBehaviour
    {
        [SerializeField] Transform _player;
        [SerializeField] ConfigZombie _configZombie;
        [SerializeField] ConfigPlayer _configPlayer;
        [SerializeField] CanvasPlayer _canvasPlayer;
        [SerializeField] LayerMask _layerTarget;
        [SerializeField] ZombieState _zomState;
        [SerializeField] bool _statusFind, _statusAttack;
        private Rigidbody2D _rgZombie;
        private Animator _animZombie;
        private Vector2 scale;
        private int _hpCurrentZombie, _difLevel;
        private bool _isZombieDead;
        public int HpZombie
        {
            get => _hpCurrentZombie;
            set
            {
                _hpCurrentZombie = value;
                _isZombieDead = _hpCurrentZombie <= 0;
                if (_isZombieDead)
                {
                    ZombieDead();
                    ZomState = ZombieState.Dead;
                }
            }
        }

        public ZombieState ZomState { get => _zomState; set => _zomState = value; }

        void Start()
        {
            _difLevel = _canvasPlayer.DifLevel;
            _player = RedController.Instance.transform;
            HpZombie = _configZombie._hpMaxZombie * _difLevel;
            _animZombie = GetComponent<Animator>();
            _rgZombie = GetComponent<Rigidbody2D>();
            scale.x = transform.localScale.x;
            scale.y = transform.localScale.y;

        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(transform.position, _radiusFind);
        //    Gizmos.DrawWireSphere(transform.position, _radiusAttack);
        //}


        // Update is called once per frame
        void Update()
        {

            _statusFind = Physics2D.CircleCast(transform.position, _configZombie._radiusFind, Vector2.zero, 0f, _layerTarget);
            _statusAttack = Physics2D.CircleCast(transform.position, _configZombie._radiusAttack, Vector2.zero, 0f, _layerTarget);
            if (_isZombieDead == false)
            {
                if (_statusFind)
                {
                    if (_player.position.x >= transform.position.x)
                    {
                        ZombieMovement(_configZombie._maxSpeed, scale.x);
                        ZomState = ZombieState.MoveRight;
                    }
                    else
                    {
                        ZombieMovement(-_configZombie._maxSpeed, -scale.x);
                        ZomState = ZombieState.MoveLeft;
                    }
                }
                else
                {
                    ZombieIdle();
                    ZomState = ZombieState.Idle;
                }
                if (_statusAttack)
                {
                    ZombieAttack();
                    ZomState = ZombieState.Attack;
                }
            }
        }

        private void ZombieIdle()
        {
            _animZombie.SetInteger("ZStatus", 1);
            _rgZombie.velocity = new Vector2(0, _rgZombie.velocity.y);
        }

        private void ZombieMovement(float speed, float scaleX)
        {

            _animZombie.SetInteger("ZStatus", 2);
            _rgZombie.velocity = new Vector2(speed, _rgZombie.velocity.y);
            transform.localScale = new Vector2(scaleX, scale.y);
        }
        private void ZombieAttack()
        {
            _animZombie.SetInteger("ZStatus", 3);
        }
        private void ZombieDead()
        {
            _animZombie.SetInteger("ZStatus", 4);
            _rgZombie.velocity = new Vector2(0, _rgZombie.velocity.y);
            Destroy(gameObject, 1f);
        }

        private void ZombieTakeDamage(int damage)
        {
            HpZombie -= damage;
        }

        private void PlayerTakeDamage()
        {
            RedController.Instance.HpPlayer -= _configZombie._dmgZombie * _difLevel;
            EventManager.Instance.OnPlayerHpChanged?.Invoke();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("dan") & _isZombieDead == false)
            {
                ZombieTakeDamage(_configPlayer._dmgPlayer);
                EventManager.Instance.OnScoreChanged?.Invoke(_configPlayer._dmgPlayer);
            }
        }
        public enum ZombieState
        {
            None = 0,
            Idle = 1,
            MoveLeft = 2,
            MoveRight = 3,
            Attack = 4,
            Dead = 5
        }
    }
}
