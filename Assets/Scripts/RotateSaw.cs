using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class RotateSaw : MonoBehaviour
    {
        [SerializeField] float _speedRotate;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.forward * _speedRotate * Time.deltaTime);
        }
    }
}
