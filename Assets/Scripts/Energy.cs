using UnityEngine;
using UnityEngine.Events;

public class Energy : MonoBehaviour
{
    [SerializeField] private float _energyConsumption = 2f;
    [SerializeField] private float _maxEnergy = 100f;

    [SerializeField] private float _currentEnergy = -1f;
    Throwing _throwing;

    public float CurrentEnergy { get { return _currentEnergy; } }
    public UnityEvent onEnergyDepleted;

    private void Awake()
    {
        _currentEnergy = _maxEnergy;
        _throwing = GetComponent<Throwing>();
    }

    private void OnEnable()
    {
        _throwing.onScooterThrow.AddListener(Refill);
    }

    private void OnDisable()
    {
        _throwing.onScooterThrow.RemoveListener(Refill);
    }

    private void Update()
    {
        Consume();
    }

    private void Consume()
    {
        if (_throwing == null) return;
        if (_currentEnergy <= 0) return;

        if (_throwing.CurrentScooter)
        {
            _currentEnergy -= _energyConsumption * Time.deltaTime;
        }

        if (_currentEnergy <= 0)
        {
            _currentEnergy = 0;
            onEnergyDepleted?.Invoke();
        }
    }

    private void Refill()
    {
        _currentEnergy = _maxEnergy;
    }

}
