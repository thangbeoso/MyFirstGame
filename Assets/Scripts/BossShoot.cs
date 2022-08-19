using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class BossShoot : MonoBehaviour
    {
        [SerializeField] GameObject _ammoBoss;
        [SerializeField] Transform _player, _followPlayer;
        [SerializeField] ConfigBoss _configBoss;
        [SerializeField] BossMove _bossMove;
        [SerializeField] CanvasPlayer _canvasPlayer;        
        [SerializeField] float _firingCycleBoss;
        [SerializeField] int _timeBossShoot, _timeBossStopShoot;
        public int _levelBoss;
        private float _bossStartShoot, _timeBossWaitShoot;
        private bool _statusBossShoot;
        private int _countBossShoot, _difLevel;
        // Use this for initialization
        void Start()
        {
            _difLevel = _canvasPlayer.DifLevel;
            _countBossShoot = _configBoss._numberAmmoBoss * _levelBoss;
            _bossStartShoot = Time.time;
            StartCoroutine(AutoShoot());
        }

        // Update is called once per frame
        void Update()
        {
            if (_bossMove._isBossDead) return;
            _followPlayer.rotation = Quaternion.FromToRotation(_followPlayer.position, _player.position - _followPlayer.position);
            if (_statusBossShoot)
            {
                _timeBossWaitShoot = Time.time - _bossStartShoot;
                if (_timeBossWaitShoot > _firingCycleBoss & _countBossShoot > 0)
                {
                    GameObject ammo = Instantiate(_ammoBoss, _followPlayer.position, _followPlayer.rotation) as GameObject;
                    _bossStartShoot = Time.time;
                    _countBossShoot--;
                }
            }
            else _countBossShoot = _configBoss._numberAmmoBoss * _levelBoss;
        }
        IEnumerator AutoShoot()
        {
            while (true)
            {
                _statusBossShoot = true;
                yield return new WaitForSeconds(_timeBossShoot);
                _statusBossShoot = false;
                yield return new WaitForSeconds(_timeBossStopShoot);
            }
        }
    }
}
