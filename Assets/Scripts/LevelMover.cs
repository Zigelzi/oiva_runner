using UnityEngine;

public class LevelMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 8f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * _movementSpeed * Time.deltaTime);

        if (transform.position.x <= -10f)
        {
            transform.position = new Vector3(20f, transform.position.y, transform.position.z);
        }
    }
}
