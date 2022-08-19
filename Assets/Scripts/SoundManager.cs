using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;
        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SoundManager>();
                }
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "SoundManager";
                    _instance = obj.AddComponent<SoundManager>();
                }
                return _instance;
            }
        }
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }        
        public AudioSource _audioSourceBG;        
    }
}
