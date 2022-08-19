using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config/ConfigZombie")]
    public class ConfigZombie : ScriptableObject
    {
        public float _maxSpeed, _radiusFind, _radiusAttack;
        public int _hpMaxZombie, _dmgZombie;
    }
}
