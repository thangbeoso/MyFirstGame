using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class ShipController : MonoBehaviour
    {
        [SerializeField] Transform[] _movePoints;
        [SerializeField] Transform _startDraw1, _endDraw1;
        [SerializeField] GameObject _player;
        [SerializeField] LayerMask _layer1;
        [SerializeField] float _speedMove, _speedRotation;
        [SerializeField] bool _horizontalCol;
        private float _maxDistance, _distance;
        private int i = 0;
        private bool _statusGo = true;
        void Start()
        {
            _maxDistance = Mathf.Abs(_startDraw1.position.x - transform.position.x);
        }
        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawLine(_startDraw1.position, _endDraw1.position);            
        //}

        void Update()
        {
            _distance = _player.transform.position.x - transform.position.x;
            _horizontalCol = Physics2D.Linecast(_startDraw1.position, _endDraw1.position, _layer1);
            if (_horizontalCol)
            {
                if (_distance >= -_maxDistance & _distance <= _maxDistance)
                {
                    RotateShip();
                }
                MoveShip();
            }
            if (i == 0)
            {
                _statusGo = true;
            }
            else if (i == _movePoints.Length - 1)
            {
                _statusGo = false;
            }
        }
        void RotateShip()
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, _speedRotation * (-_distance / _maxDistance));
        }
        private void MoveShip()
        {
            float distanceShip = Vector2.Distance(transform.position, _movePoints[i].position);
            if (distanceShip >= 0.1f)
            {
                transform.Translate((_movePoints[i].position - transform.position).normalized * _speedMove * Time.deltaTime);
            }
            else if (_statusGo == true)
            {
                i++;
            }
            else if (_statusGo == false)
            {
                i--;
            }
        }        
    }
}
