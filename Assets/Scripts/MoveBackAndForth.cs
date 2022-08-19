using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class MoveBackAndForth : MonoBehaviour
    {
		[SerializeField] Transform _startPoint, _endPoint;
		[SerializeField] float _firstDelay, _secondDelay, _speedMoveUp, _speedMoveDown;
		private bool _moveup;
		// Use this for initialization
		void Start()
		{
			StartCoroutine("Move");
		}

		// Update is called once per frame
		void Update()
		{
			if (_moveup)
			{
				transform.position = Vector2.MoveTowards(transform.position, _endPoint.position, _speedMoveUp * Time.deltaTime);
			}
			else
			{
				transform.position = Vector2.MoveTowards(transform.position, _startPoint.position, _speedMoveDown * Time.deltaTime);
			}
		}
		IEnumerator Move()
		{
			while (true)
			{
				yield return new WaitForSeconds(_firstDelay);
				_moveup = true;
				yield return new WaitForSeconds(_secondDelay);
				_moveup = false;
			}
		}
	}
}
