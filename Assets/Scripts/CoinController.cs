using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class CoinController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                EventManager.Instance.OnScoreChanged.Invoke(5);
                Destroy(gameObject);
            }            
        }
    }
}