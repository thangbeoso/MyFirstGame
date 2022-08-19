using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class MoveBar : MonoBehaviour
    {
		[SerializeField] float _speedBar;
		[SerializeField] float _survivalTime;		
		void Update()
		{
			transform.Translate(Vector3.left * _speedBar * Time.deltaTime);
			Destroy(gameObject, _survivalTime);
		}
	}
}
