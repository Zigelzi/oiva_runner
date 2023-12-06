using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    [Range(0, 50f)]
    private float _traumaStep = .1f;

    [SerializeField] private float _traumaDecaySpeed = .2f;

    private CinemachineVirtualCamera _cmCamera;
    private float _currentTrauma = 0f;
    private float _maxTrauma = 1f;
    private Throwing _throwing;
    private void Awake()
    {
        _cmCamera = GetComponent<CinemachineVirtualCamera>();
        _throwing = GameObject.FindGameObjectWithTag("Player").GetComponent<Throwing>();
    }

    private void OnEnable()
    {
        _throwing.onScooterThrow.AddListener(IncreaseTrauma);
    }

    private void Update()
    {
        if (_currentTrauma > 0)
        {
            SetCameraShake(_currentTrauma);
            _currentTrauma -= Time.deltaTime * _traumaDecaySpeed;
        }
        else
        {
            SetCameraShake(0);
        }
    }

    private void OnDisable()
    {
        _throwing.onScooterThrow.RemoveListener(IncreaseTrauma);
    }

    private void IncreaseTrauma()
    {
        _currentTrauma = Mathf.Min(_currentTrauma + _traumaStep, _maxTrauma);
    }

    private void SetCameraShake(float intensity)
    {
        CinemachineBasicMultiChannelPerlin cmShake = _cmCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cmShake.m_AmplitudeGain = intensity;

    }
}
