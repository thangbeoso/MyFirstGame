using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class BarController : MonoBehaviour
    {
		[SerializeField] Transform _startPoint;
		[SerializeField] GameObject _bar;
		[SerializeField] float _firstWaitTime, _cycle;
		private float _barTime,_startTime, _cooldown;		
		void FixedUpdate()
		{
			Invoke("CreatMoveBar", _firstWaitTime);
			_barTime = Time.time - _firstWaitTime;
		}		
		public void CreatMoveBar()
		{
			_cooldown = _barTime - _startTime;
			if (_cooldown >= _cycle || _cooldown == 0)
			{
				_startTime = _barTime;
				GameObject bar = Instantiate(
					_bar,
					_startPoint.position,
					_startPoint.rotation) as GameObject;
			}
		}
	}
}
