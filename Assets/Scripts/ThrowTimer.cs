using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThrowTimer : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private bool _isBouncing = false;
    private void Awake()
    {
        _slider = GetComponent<Slider>();

        _slider.value = 0;
        StartCoroutine(SlideBackAndForth());
    }
    private IEnumerator SlideBackAndForth()
    {
        while (_isBouncing)
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
