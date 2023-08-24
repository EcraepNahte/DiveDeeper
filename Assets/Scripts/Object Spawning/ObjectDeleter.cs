using UnityEngine;

public class ObjectDeleter : MonoBehaviour
{
    // Destroys everything that touches it
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
