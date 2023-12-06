using UnityEngine;
using UnityEngine.UI;

public class ThrowForcePresenter : MonoBehaviour
{
    private Slider _slider;
    private Throwing _throwing;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0;
        _throwing = GameObject.FindGameObjectWithTag("Player").GetComponent<Throwing>();
    }

    private void Update()
    {
        _slider.value = _throwing.CurrentThrowForcePercentage;
    }
}
