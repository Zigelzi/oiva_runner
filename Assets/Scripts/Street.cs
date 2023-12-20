using System;
using UnityEngine;

public class Street : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 8f;
    [SerializeField] private float _despawnOffset = 25f;

    public static event Action onStreetDestroy;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * _movementSpeed * Time.deltaTime);

        if (transform.position.x <= -_despawnOffset)
        {
            onStreetDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}
