using UnityEngine;

// Teleports a character to the connected passage on the other side
[RequireComponent(typeof(Collider2D))]
public class Passage : MonoBehaviour
{
    public Transform connection; // Target position to teleport to

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = connection.position;
        position.z = other.transform.position.z; // Keep original Z depth
        other.transform.position = position;
    }
}
