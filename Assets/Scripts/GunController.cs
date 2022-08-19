using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
	public class GunController : MonoBehaviour
	{
		[SerializeField] GameObject _ammo;
		[SerializeField] Transform _ammoPoint;
		[SerializeField] float _firingCycle;
		[SerializeField] int _timeShoot, _timeStopShoot, _numberAmmo;
		private float _startShoot, _timeWaitShoot;
		private bool _statusShoot;
		private int _countShoot;
		// Use this for initialization
		void Start()
		{
			StartCoroutine(AutoShoot());
			_startShoot = Time.time;
			_countShoot = _numberAmmo;
		}

		// Update is called once per frame
		void Update()
		{
			if (_statusShoot)
			{				
				_timeWaitShoot = Time.time - _startShoot;
				if (_timeWaitShoot >= _firingCycle & _countShoot > 0)
				{
					GameObject ammo = Instantiate(
					_ammo, _ammoPoint.transform.position,
					_ammoPoint.transform.rotation) as GameObject;
					_startShoot = Time.time;
					_countShoot--;
				}				
			}	
			else _countShoot = _numberAmmo;
		}
		IEnumerator AutoShoot()
		{
			while (true)
			{
				_statusShoot = true;
				yield return new WaitForSeconds(_timeShoot);
				_statusShoot = false;
				yield return new WaitForSeconds(_timeStopShoot);
			}
		}
	}
}

