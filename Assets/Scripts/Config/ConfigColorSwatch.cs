using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedGunner
{
    [CreateAssetMenu(fileName ="Config",menuName = "Config/ConfigColorSwatch")] 
    public class ConfigColorSwatch : ScriptableObject
    {
        public Color[] _colorSwatches;        
    }
}
