using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace RedGunner
{
    public class CanvasSuccessDefeat : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _txtDifficult;
        [SerializeField] TextMeshProUGUI _txtScore;
        [SerializeField] ConfigColorSwatch _configColorSwatch;
        [SerializeField] AudioClip _aClipReset;
        private int _diflevelCSD,_lifeCSD;

        void Awake()
        {            
            SoundManager.Instance._audioSourceBG.clip = _aClipReset;
            SoundManager.Instance._audioSourceBG.Play();
            _diflevelCSD = PlayerPrefs.GetInt("DifLevel", 1);
            _lifeCSD = PlayerPrefs.GetInt("Life", 5);
        }

        void Start()
        {
            switch (_diflevelCSD)
            {
                case 1:
                    _txtDifficult.SetText($"Difficult: Easy");
                    _txtDifficult.color = _configColorSwatch._colorSwatches[0];
                    break;
                case 2:
                    _txtDifficult.SetText($"Difficult: Normal");
                    _txtDifficult.color = _configColorSwatch._colorSwatches[1];
                    break;
                case 3:
                    _txtDifficult.SetText($"Difficult: Hard");
                    _txtDifficult.color = _configColorSwatch._colorSwatches[2];
                    break;
                default:
                    break;
            }
            _txtScore.SetText($"Score: {PlayerPrefs.GetInt("Score", 0)}");
        }

        public void ResetGame()
        {
            SceneManager.LoadScene("StartGame");
            PlayerPrefs.SetInt("DifLevel", 1);
            PlayerPrefs.SetInt("Life", 5);            
        }
        public void ExitGame()
        {
            Application.Quit();
        }    
    }
}
