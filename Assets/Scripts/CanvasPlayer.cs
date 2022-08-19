using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

namespace RedGunner
{
    public class CanvasPlayer : MonoBehaviour
    {
        [SerializeField] ConfigPlayer _configPlayer;
        [SerializeField] ConfigBoss _configBoss;
        [SerializeField] Image _hideBarPlayer, _mainBarPlayer;
        [SerializeField] Image _hideBarBoss, _mainBarBoss;
        [SerializeField] BossMove _bossCtrl;
        [SerializeField] TextMeshProUGUI _txtScore;
        [SerializeField] TextMeshProUGUI _txtLife;
        [SerializeField] int _difLevel, _life, _score;

        public int DifLevel { get => _difLevel; set => _difLevel = value; }
        public int Life { get => _life; set => _life = value; }
        public int Score { get => _score; set => _score = value; }

        private void Awake()
        {
            _difLevel = PlayerPrefs.GetInt("DifLevel", 1);
            Life = PlayerPrefs.GetInt("Life", 5);
            Score = 0;
        }


        void Start()
        {
            EventManager.Instance.OnPlayerHpChanged += Handle_OnPlayerHpChanged;
            EventManager.Instance.OnBossHpChanged += Handle_OnBossHpChanged;
            EventManager.Instance.OnCheckPoint += Handle_CheckPoint;
            EventManager.Instance.OnScoreChanged += Handle_OnScoreChanged;
            _txtLife.SetText($"Life: {Life}");
        }

        public void Handle_OnScoreChanged(int extrascore)
        {
            Score += extrascore;
            _txtScore.SetText($"Score: {Score}");
            PlayerPrefs.SetInt("Score", Score);
        }

        private void Handle_OnBossHpChanged()
        {
            if (_mainBarBoss == null || _hideBarBoss == null) return;
            _mainBarBoss.fillAmount = (float)_bossCtrl.CurrentHpBoss / (_configBoss._maxHpBoss * DifLevel);
            var colorB = _hideBarBoss.color;
            colorB.a = 1;
            _hideBarBoss.color = colorB;
            colorB.a = 0;
            _hideBarBoss.DOFillAmount(_mainBarBoss.fillAmount, 0.5f);
            _hideBarBoss.DOBlendableColor(colorB, 0.5f);
        }

        private void Handle_OnPlayerHpChanged()
        {
            if (_mainBarPlayer == null || _hideBarPlayer == null) return;
            _mainBarPlayer.fillAmount = (float)RedController.Instance.HpPlayer / _configPlayer._hpMaxPlayer;
            var color = _hideBarPlayer.color;
            color.a = 1;
            _hideBarPlayer.color = color;
            color.a = 0;
            _hideBarPlayer.DOFillAmount(_mainBarPlayer.fillAmount, 0.5f);
            _hideBarPlayer.DOBlendableColor(color, 0.5f);
        }
        private void Handle_CheckPoint()
        {
            _mainBarPlayer.fillAmount = (float)RedController.Instance.HpPlayer / _configPlayer._hpMaxPlayer;
        }
        void Update()
        {
            if (_mainBarPlayer.fillAmount >= 2f / 3f)
            {
                _mainBarPlayer.color = Color.green;
            }
            else if (_mainBarPlayer.fillAmount >= 1f / 3f)
            {
                _mainBarPlayer.color = Color.yellow;
            }
            else _mainBarPlayer.color = Color.red;

            if (_mainBarBoss.fillAmount >= 2f / 3f)
            {
                _mainBarBoss.color = Color.green;
            }
            else if (_mainBarBoss.fillAmount >= 1f / 3f)
            {
                _mainBarBoss.color = Color.yellow;
            }
            else _mainBarBoss.color = Color.red;
        }
        private void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}
