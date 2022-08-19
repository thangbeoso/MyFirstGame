using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RedGunner
{
    public class CanvasStart : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("GameRedRunner");
        }        
        public void SetDifLevel(int diflevel)
        {
            PlayerPrefs.SetInt("DifLevel", diflevel);            
        }
        public void SetLife(int life)
        {
            PlayerPrefs.SetInt("Life", life);
        }
    }
}
