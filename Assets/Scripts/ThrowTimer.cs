using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThrowTimer : MonoBehaviour
{
    private Slider _slider;
    private Throwing _throwing;
    private Coroutine _currentThrowTimer;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0;
        _throwing = GameObject.FindGameObjectWithTag("Player").GetComponent<Throwing>();
    }

    private void OnEnable()
    {
        _throwing.onScooterPickup.AddListener(StartThrowTimer);
        _throwing.onScooterThrow.AddListener(StopThrowTimer);
    }

    private void OnDisable()
    {
        _throwing.onScooterPickup.RemoveListener(StartThrowTimer);
        _throwing.onScooterThrow.RemoveListener(StopThrowTimer);
    }

    private void StartThrowTimer()
    {
        _currentThrowTimer = StartCoroutine(SlideBackAndForth());
    }

    private void StopThrowTimer()
    {
        StopCoroutine(_currentThrowTimer);
        _slider.value = 0;
    }
    private IEnumerator SlideBackAndForth()
    {
        while (true)
        {
            while (_slider.value < 1)
            {
                _slider.value += Mathf.Lerp(_slider.minValue, _slider.maxValue, .5f * Time.deltaTime);
                yield return null;
            }
            while (_slider.value > 0)
            {
                _slider.value -= Mathf.Lerp(_slider.minValue, _slider.maxValue, .5f * Time.deltaTime);
                yield return null;
            }
        }
    }
}
