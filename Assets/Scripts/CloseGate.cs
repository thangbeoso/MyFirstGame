using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class CloseGate : MonoBehaviour
    {
        [SerializeField] Transform _player, _targetPoint;
        [SerializeField] GameObject _pnlHpBoss;
        [SerializeField] float _speedClose;        
        private bool _statusClose;
        private Vector2 _startPoint;
        void Start()
        {
            _startPoint = transform.position;
        }
        void Update()
        {
            if (_player.position.x >= transform.position.x)
            {
                _statusClose = true;               
                _pnlHpBoss.SetActive(true);                
            }
            else _statusClose = false;
            if (_statusClose)
            {
                GateMove(_targetPoint.position);                
            }
            else
            {
                GateMove(_startPoint);
            }            
        }
        void GateMove(Vector2 point)
        {
            transform.position = Vector2.MoveTowards(transform.position, point, _speedClose * Time.deltaTime);
        }
    }
}
