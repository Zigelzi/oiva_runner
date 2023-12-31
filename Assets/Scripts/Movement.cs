using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float _maxAcceleration = 10f;
    [SerializeField] private float _maxXVelocity = 8f;
    [SerializeField, Range(0, 5f)] private float _maxZMovement = 3.5f;

    private float _currentDistanceTravelled = 0;
    private Status _playerStatus;

    public float CurrentDistanceTravelled { get { return _currentDistanceTravelled; } }
    private void Awake()
    {
        _playerStatus = GetComponent<Status>();
    }

    private void OnEnable()
    {
        _playerStatus.onObstacleHit.AddListener(Disable);
    }

    private void OnDisable()
    {
        _playerStatus.onObstacleHit.RemoveListener(Disable);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredVelocity = Vector3.right * _maxXVelocity;
        float maxVelocityChange = _maxAcceleration * Time.deltaTime;


        if (_velocity.x < desiredVelocity.x)
        {
            _velocity.x = Mathf.Min(_velocity.x + maxVelocityChange, desiredVelocity.x);
        }

        _currentDistanceTravelled += _velocity.x * Time.deltaTime;
    }

    public void Move(bool isMovingRight)
    {
        Vector3 movementDirection = isMovingRight ? Vector3.back : Vector3.forward;
        Debug.Log(movementDirection * Time.deltaTime * 2f);
        Vector3 newPosition = transform.localPosition + movementDirection * Time.deltaTime * 2f;
        newPosition.z = Mathf.Clamp(newPosition.z, -_maxZMovement, _maxZMovement);
        transform.localPosition = newPosition;

    }


    private void Disable()
    {
        enabled = false;
    }
}
