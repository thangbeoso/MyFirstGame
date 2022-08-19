using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    public class EventManager : MonoBehaviour
    {
        private static EventManager _instance;
        public static EventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<EventManager>();
                }
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "EventManager";
                    _instance = obj.AddComponent<EventManager>();
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
        public Action<int> OnScoreChanged;        
        public Action OnPlayerHpChanged;
        public Action OnBossHpChanged;
        public Action OnCheckPoint;        
    }
}
