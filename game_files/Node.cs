using System.Collections.Generic;
using UnityEngine;

// A node represents a tile in the maze where movement decisions can be made
public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;

    // List of directions available from this node (no wall in that direction)
    public readonly List<Vector2> availableDirections = new();

    private void Start()
    {
        availableDirections.Clear();

        // Check all 4 directions and store the ones that are walkable
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,
            Vector2.one * 0.5f,
            0f,
            direction,
            1f,
            obstacleLayer
        );

        if (hit.collider == null)
        {
            availableDirections.Add(direction);
        }
    }
}
