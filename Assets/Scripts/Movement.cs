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
    private int _currentTrack = -1;
    private bool _isMovingSideways = false;
    private Rigidbody _rb;
    private int[] _tracks = { 0, 1, 2 };
    private Vector3 _startingPosition;
    private Status _playerStatus;

    public float CurrentDistanceTravelled { get { return _currentDistanceTravelled; } }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerStatus = GetComponent<Status>();
        _startingPosition = transform.position;

        _currentTrack = 1;
        SetRunPosition(_currentTrack);
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

        if (isMovingRight)
        {
            if (_currentTrack < _tracks.Length) _currentTrack++;
        }
        else
        {
            if (_currentTrack > 0) _currentTrack--;
        }
        SetRunPosition(_currentTrack);
    }

    public void StopSidewaysMovement()
    {
        _isMovingSideways = false;
    }

    private void SetRunPosition(int trackNumber)
    {
        if (trackNumber == 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 2.5f);
        }
        if (trackNumber == 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        if (trackNumber == 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -2.5f);
        }
    }


    private void Disable()
    {
        _rb.velocity = Vector3.zero;
        enabled = false;
    }
}
