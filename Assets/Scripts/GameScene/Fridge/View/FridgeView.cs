using EventsSystem.Signals;
using TMPro;
using UniRx;
using UnityEngine;

namespace GameScene.Fridge
{
    public class FridgeView : MonoBehaviour
    {
        [Header("ProgressBar")]
        [SerializeField] private Material _progressBarShader;
        [SerializeField] private TextMeshProUGUI _percentValue;
        [SerializeField] private Transform _progressBar;

        private SatietyProgressBar _satietyProgressBar;

        private readonly CompositeDisposable _disposable = new();
        
        private void Start()
        {
            _satietyProgressBar = new SatietyProgressBar(_progressBar, _progressBarShader, _percentValue);
        }

        private void Update()
        {
            _satietyProgressBar.LookAtCamera();
        }

        public void SubscribeToModel(FridgeInitializeSignal fridgeInitializeSignal)
        {
            fridgeInitializeSignal.FridgeSatietyReadable.Current
                .Subscribe(value =>
                {
                    _satietyProgressBar.ChangeProgress(value);
                }).AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}
