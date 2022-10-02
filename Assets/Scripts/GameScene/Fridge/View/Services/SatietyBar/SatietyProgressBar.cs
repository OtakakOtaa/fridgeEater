using System;
using TMPro;
using UnityEngine;

namespace GameScene.Fridge
{
    public class SatietyProgressBar
    {
        private readonly Material _progressBarShader;
        private readonly TextMeshProUGUI _percentValue;
        private readonly Transform _progressBar;

        private const string Percent = "%";

        public SatietyProgressBar(Transform progressBar, Material progressBarShader, TextMeshProUGUI percentValue)
        {
            _progressBar = progressBar;
            _progressBarShader = progressBarShader;
            _percentValue = percentValue;
        }
        
        public void LookAtCamera()
        {
            _progressBar.LookAt(UnityEngine.Camera.main.transform);
        }
        
        public void ChangeProgress(int value)
        {
            _progressBarShader.SetFloat(ProgressBarShader.Progress, value);
            _percentValue.text = Percent + value;
        }
        
        private static class ProgressBarShader
        {
            public static readonly int Progress = Shader.PropertyToID("_Progress");
        }
    }
}