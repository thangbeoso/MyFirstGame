using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class Ammo : MonoBehaviour {
        [SerializeField] float _speedAmmo;

	    // Update is called once per frame
	    void Update () {
		    transform.Translate (Vector2.right * _speedAmmo * Time.deltaTime);
	    }    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("MainMap") || collision.CompareTag("Player"))
            {
                Destroy(gameObject);
            }    
        }
    }
}
