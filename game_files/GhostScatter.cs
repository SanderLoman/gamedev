using UnityEngine;

// Behavior that causes the ghost to scatter in random directions
public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        // When scatter ends, switch to chase mode
        ghost.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        // Only update movement if ghost is not frightened
        if (node != null && enabled && !ghost.frightened.enabled)
        {
            // Choose a random available direction
            int index = Random.Range(0, node.availableDirections.Count);

            // Avoid reversing direction if possible
            if (node.availableDirections.Count > 1 && node.availableDirections[index] == -ghost.movement.direction)
            {
                index++;

                // Wrap around if index goes out of bounds
                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
