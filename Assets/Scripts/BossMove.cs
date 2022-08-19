using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RedGunner
{
    public class BossMove : MonoBehaviour
    {
        [SerializeField] Transform _limitedPoint1, _limitedPoint2;
        [SerializeField] SpriteRenderer _bossSprite;
        [SerializeField] BossShoot _bossShoot;
        [SerializeField] ConfigBoss _configBoss;
        [SerializeField] ConfigPlayer _configPlayer;
        [SerializeField] CanvasPlayer _canvasPlayer;        
        public bool _isBossDead;
        private int _currentHpBoss, _currentDmgBoss, _difLevel;
        private Vector3 _currentPoint;
        private bool _isMove;      

        public int CurrentHpBoss
        {
            get => _currentHpBoss;
            set
            {
                _currentHpBoss = value;
                _isBossDead = _currentHpBoss <= 0;
                if (_isBossDead)
                {                    
                    SceneManager.LoadScene("SuccessGame");                    
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _difLevel = _canvasPlayer.DifLevel;
            CurrentHpBoss = _configBoss._maxHpBoss * _difLevel;
            _currentDmgBoss = _configBoss._dmgBoss * _difLevel;
            _currentPoint = new Vector2(
                (_limitedPoint1.position.x + _limitedPoint2.position.x) / 2,
                (_limitedPoint1.position.y + _limitedPoint2.position.y) / 2);            
        }
        // Update is called once per frame
        void Update()
        {
            if (_isBossDead)
            {
                _bossSprite.color = Color.red;
                return;
            }
            CurrentHpBoss = Mathf.Clamp(CurrentHpBoss, 0, _configBoss._maxHpBoss * _difLevel);
            if (CurrentHpBoss >= _configBoss._maxHpBoss * _difLevel * 2 / 3)
            {
                StatusBoss(1);
                _bossShoot._levelBoss = 1;
            }
            else if (CurrentHpBoss >= _configBoss._maxHpBoss * _difLevel / 3)
            {
                StatusBoss(2);
                _bossShoot._levelBoss = 2;
            }
            else
            {
                StatusBoss(3);
                _bossShoot._levelBoss = 3;
            }           
        }

        private void BossTakeDamage(int dmg)
        {
            CurrentHpBoss -= dmg;
        }

        private void StatusBoss(int levelBoss)
        {
            _currentDmgBoss = _configBoss._dmgBoss * _difLevel * levelBoss;
            float dis = Vector2.Distance(transform.position, _currentPoint);
            if (dis > 0.1f)
            {
                transform.Translate((_currentPoint - transform.position).normalized * levelBoss * _configBoss._speedMoveBoss * Time.deltaTime);
            }
            else _isMove = false;
            if (_isMove == false)
            {
                _currentPoint = new Vector2(
                    Random.Range(_limitedPoint1.position.x, _limitedPoint2.position.x),
                    Random.Range(_limitedPoint2.position.y, _limitedPoint1.position.y));
                _isMove = true;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("dan") & _isBossDead == false)
            {
                BossTakeDamage(_configPlayer._dmgPlayer);
                EventManager.Instance.OnBossHpChanged?.Invoke();
                EventManager.Instance.OnScoreChanged?.Invoke(_configPlayer._dmgPlayer);
            }
        }
    }
}
