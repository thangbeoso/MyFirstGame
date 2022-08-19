using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform _player;
        private bool _lockPos = true;
        void Update()
        {
            if (transform.position.x >= 80.5f)
                _lockPos = false;
            if (_lockPos)
            {
                transform.position = new Vector3(
                    Mathf.Clamp(_player.position.x, -0.5f, 80.5f),
                    transform.position.y,
                    transform.position.z);
            }  
        }
    }
}
