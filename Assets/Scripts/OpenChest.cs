using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class OpenChest : MonoBehaviour
    {
		[SerializeField] Animator _aniChest;
		private bool _touched = false;		
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.DownArrow) & (_touched))
			{
				_aniChest.SetBool("StatusChest", true);
				EventManager.Instance.OnScoreChanged?.Invoke(20);
				_touched = false;
			}            
		}
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
				_touched = true;
            }
        }       
    }
}
