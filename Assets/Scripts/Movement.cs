using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float _maxForwardsAcceleration = 10f;
    [SerializeField] private float _maxForwardsVelocity = 10f;
    [SerializeField] private float _maxSidewaysAcceleration = 10f;
    [SerializeField] private float _maxSidewaysVelocity = 8f;
    [SerializeField, Range(0, 5f)] private float _maxZMovement = 3.5f;

    private float _currentDistanceTravelled = 0;
    private Vector3 _startingPosition;
    private Status _playerStatus;

    public float CurrentDistanceTravelled { get { return _currentDistanceTravelled; } }
    private void Awake()
    {
        _playerStatus = GetComponent<Status>();
        _startingPosition = transform.position;
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
        Vector3 desiredForwardsVelocity = Vector3.right * _maxForwardsVelocity;
        float maxForwardsVelocityChange = _maxForwardsAcceleration * Time.deltaTime;


        if (_velocity.x < desiredForwardsVelocity.x)
        {
            _velocity.x = Mathf.Min(_velocity.x + maxForwardsVelocityChange, desiredForwardsVelocity.x);
        }

        transform.position += _velocity * Time.deltaTime;
        _currentDistanceTravelled = Vector3.Distance(_startingPosition, transform.position);
    }

    public void Move(bool isMovingRight)
    {
        if (!enabled) return;

        Vector3 movementDirection = isMovingRight ? Vector3.back : Vector3.forward;
        Vector3 sidewaysMovement = movementDirection * _maxSidewaysVelocity * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + sidewaysMovement;
        newPosition.z = Mathf.Clamp(newPosition.z, -_maxZMovement, _maxZMovement);
        transform.localPosition = newPosition;

    }


    private void Disable()
    {
        enabled = false;
    }
}
