using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThrowTimer : MonoBehaviour
{
    Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        _slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SlideBackAndForth()
    {
        yield return null;
    }
}
