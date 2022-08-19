using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config/ConfigPlayer")]
    public class ConfigPlayer : ScriptableObject
    {
        public float _speedJump, _speedRun, _firingForce, _distanceGround;
        public int _maxJump, _hpMaxPlayer, _dmgPlayer;
    }
}
