using System;
using UnityEngine;

public class Street : MonoBehaviour
{
    public static event Action onStreetDestroy;

    private void OnDestroy()
    {
        onStreetDestroy?.Invoke();
    }
}
