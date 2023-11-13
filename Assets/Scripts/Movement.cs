using System;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;

    private Vector3 _startingPosition;

    public TravelDistanceChanged onTravelDistanceChanged;

    [Serializable]
    public class TravelDistanceChanged : UnityEvent<int> { }

    private void Awake()
    {
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime);
        GetTravelDistance();
    }

    private void GetTravelDistance()
    {
        int distance = (int)Vector3.Distance(_startingPosition, transform.position);

        onTravelDistanceChanged?.Invoke(distance);
    }
}
