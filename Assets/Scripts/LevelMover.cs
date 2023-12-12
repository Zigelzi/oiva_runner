using System;
using UnityEngine;

public class LevelMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 8f;
    [SerializeField] private float _despawnOffset = 25f;

    public static event Action onLevelDestroy;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * _movementSpeed * Time.deltaTime);

        if (transform.position.x <= -_despawnOffset)
        {
            onLevelDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}
