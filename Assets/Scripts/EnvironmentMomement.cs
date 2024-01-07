using UnityEngine;

public class EnvironmentMomement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 8f;
    [SerializeField] private float _despawnOffset = 25f;

    private Status _playerStatus;

    private bool _isMovementOverridden = false;

    public bool IsMovementOverridden
    {
        get { return _isMovementOverridden; }
        set { _isMovementOverridden = value; }
    }

    private void Awake()
    {
        _playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<Status>();
    }

    private void OnEnable()
    {
        _playerStatus.onObstacleHit.AddListener(Disable);
    }

    private void OnDisable()
    {
        _playerStatus.onObstacleHit.RemoveListener(Disable);
    }

    private void Update()
    {
        if (_isMovementOverridden) return;

        transform.Translate(Vector3.left * _movementSpeed * Time.deltaTime, Space.World);

        if (transform.position.x <= -_despawnOffset)
        {
            Destroy(gameObject);
        }
    }

    private void Disable()
    {
        enabled = false;
    }
}
