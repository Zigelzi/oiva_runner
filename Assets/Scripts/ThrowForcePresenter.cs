using UnityEngine;
using UnityEngine.UI;

public class ThrowForcePresenter : MonoBehaviour
{
    [SerializeField] private Color _initialBarColor = Color.gray;
    [SerializeField] private Color _powerupActiveColor;
    [SerializeField] private Image _sliderBackgroundImage;
    private Slider _slider;
    private Throwing _throwing;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0;
        _throwing = GameObject.FindGameObjectWithTag("Player").GetComponent<Throwing>();

        if (!_sliderBackgroundImage) return;
        _sliderBackgroundImage.color = _initialBarColor;
    }

    private void OnEnable()
    {
        _throwing.onThrowForcePickup.AddListener(SetSliderPowerupColor);
        _throwing.onThrowForceFade.AddListener(SetSliderInitialColor);
    }

    private void Update()
    {
        _slider.value = _throwing.CurrentThrowForcePercentage;
    }

    private void OnDisable()
    {

    }

    private void SetSliderPowerupColor()
    {
        if (!_sliderBackgroundImage) return;
        _sliderBackgroundImage.color = _powerupActiveColor;
    }

    private void SetSliderInitialColor()
    {
        if (!_sliderBackgroundImage) return;
        _sliderBackgroundImage.color = _initialBarColor;
    }
}
