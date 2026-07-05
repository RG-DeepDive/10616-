using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Magma"))
        {
            transform.position = startPosition;

            if (GetComponent<Rigidbody2D>() != null)
            {
                GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            }
        }
    }
}