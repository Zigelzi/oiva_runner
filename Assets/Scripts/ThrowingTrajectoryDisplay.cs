using UnityEngine;

public class ThrowingTrajectoryDisplay : MonoBehaviour
{
    [SerializeField] private LineRenderer _trajectoryLine;
    [SerializeField]
    [Range(0, 50f)] private float _lineLenght = 10f;

    Throwing _throwing;

    private void Awake()
    {
        _throwing = GetComponent<Throwing>();
        if (!_trajectoryLine) return;
        _trajectoryLine.enabled = false;
    }

    private void Update()
    {
        if (!_throwing) return;
        if (!_trajectoryLine) return;

        //if (_throwing.IsSelectingThrowDirection)
        //{
        //    _trajectoryLine.enabled = true;
        //    _trajectoryLine.positionCount = 2;
        //    _trajectoryLine.SetPosition(0, _throwing.CarryingPosition.position);
        //    _trajectoryLine.SetPosition(1, transform.position + _throwing.CurrentThrowDirection * _lineLenght);
        //}
        //else
        //{
        //    _trajectoryLine.enabled = false;
        //}
    }
}
