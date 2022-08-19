using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config/ConfigBoss")]
    public class ConfigBoss : ScriptableObject
    {
        public int _maxHpBoss, _dmgBoss, _numberAmmoBoss;
        public float _speedMoveBoss;
    }
}
