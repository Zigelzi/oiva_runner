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
    private bool _isMovingSideways = false;
    private Rigidbody _rb;
    private Vector3 _startingPosition;
    private Status _playerStatus;

    public float CurrentDistanceTravelled { get { return _currentDistanceTravelled; } }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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
    private void Update()
    {
        Vector3 desiredForwardsVelocity = Vector3.right * _maxForwardsVelocity;
        float maxForwardsVelocityChange = _maxForwardsAcceleration * Time.deltaTime;

        _velocity = _rb.velocity;
        _velocity.x = Mathf.MoveTowards(_velocity.x, desiredForwardsVelocity.x, maxForwardsVelocityChange);

        if (!_isMovingSideways) _velocity.z = 0;
        _rb.velocity = _velocity;
        _currentDistanceTravelled = Vector3.Distance(_startingPosition, transform.position);
    }

    public void Move(bool isMovingRight)
    {
        if (!enabled) return;
        _isMovingSideways = true;
        _velocity = _rb.velocity;
        Vector3 movementDirection = isMovingRight ? Vector3.back : Vector3.forward;
        Vector3 desiredSidewaysVelocity = movementDirection * _maxSidewaysVelocity;
        float maxSidewaysVelocityChange = _maxSidewaysAcceleration * Time.deltaTime;

        _velocity.z = Mathf.MoveTowards(_velocity.z, desiredSidewaysVelocity.z, maxSidewaysVelocityChange);
        _rb.velocity = _velocity;
    }

    public void StopSidewaysMovement()
    {
        _isMovingSideways = false;
    }


    private void Disable()
    {
        _rb.velocity = Vector3.zero;
        enabled = false;
    }
}
