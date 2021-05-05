using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NoiseDetector : MonoBehaviour
{

    [SerializeField] private PlayerMove _noise;
    [SerializeField] private Image _fillImage;
    [SerializeField] private float _noiseLvl;
    [SerializeField] private float _maxNoiseLvl;

    public UnityAction PlayerDetectedEvent;

    private bool _playerDetected;

    private void OnEnable()
    {
        _noise.OnMovingEvent += DetectNoise;
    }
    private void OnDisable()
    {
        _noise.OnMovingEvent -= DetectNoise;
    }

    private void Update()
    {
        FillNoiseSensor();
    }

    private void DetectNoise(bool noise)
    {
        if (noise == true)
        {
            _noiseLvl = Mathf.Clamp(_noiseLvl + 3 * Time.deltaTime, 0, 10) ;
        }
        else
        {
            _noiseLvl = Mathf.Clamp(_noiseLvl - 2 * Time.deltaTime, 0, 10);
        }
        if(_playerDetected == false && _maxNoiseLvl <= _noiseLvl)
        {
            _playerDetected = true;
            PlayerDetectedEvent?.Invoke();
        }
    }

    private void FillNoiseSensor()
    {
        _fillImage.fillAmount = _noiseLvl / _maxNoiseLvl;
    }
}
