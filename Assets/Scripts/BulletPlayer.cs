using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class BulletPlayer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "chest"
                & collision.gameObject.tag != "Player"
                & collision.gameObject.tag != "ammoBoss"
                & collision.gameObject.tag != "ammogun"
                & collision.gameObject.tag != "dan"
                & collision.gameObject.tag != "checkpoint")
            {
                Destroy(gameObject);
            }
        }
    }
}

