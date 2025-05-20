using System.Collections;
using UnityEngine;

// Controls the ghost's movement inside and out of the home area
public class GhostHome : GhostBehavior
{
    public Transform inside; // Target point inside the home
    public Transform outside; // Exit point outside the home

    private void OnEnable()
    {
        StopAllCoroutines(); // Stop any active transition
    }

    private void OnDisable()
    {
        // When disabled, begin exit animation if still active in the scene
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Bounce ghost back when it hits a wall while inside home
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ghost.movement.SetDirection(-ghost.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        // Temporarily disable physics and control ghost exit movement manually
        ghost.movement.SetDirection(Vector2.up, true);
        ghost.movement.rb.isKinematic = true;
        ghost.movement.enabled = false;

        Vector3 position = transform.position;
        float duration = 0.5f;
        float elapsed = 0f;

        // Move from current position to inside target
        while (elapsed < duration)
        {
            ghost.SetPosition(Vector3.Lerp(position, inside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        // Move from inside to outside target
        while (elapsed < duration)
        {
            ghost.SetPosition(Vector3.Lerp(inside.position, outside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Pick a random left/right direction and resume normal movement
        ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
        ghost.movement.rb.isKinematic = false;
        ghost.movement.enabled = true;
    }
}
