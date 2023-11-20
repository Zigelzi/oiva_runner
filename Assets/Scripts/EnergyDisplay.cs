using TMPro;
using UnityEngine;

public class EnergyDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text _energyValue;
    Energy _energy;

    private void Awake()
    {
        _energy = GameObject.FindGameObjectWithTag("Player").GetComponent<Energy>();
    }

    private void Update()
    {
        if (!_energyValue) return;

        _energyValue.text = _energy.CurrentEnergy.ToString("F1");
    }
}
