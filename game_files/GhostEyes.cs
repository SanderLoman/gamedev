using UnityEngine;

// Handles the ghost's eye direction visuals
public class GhostEyes : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Movement Movement { get; private set; }

    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        // Update sprite based on current direction
        if (Movement.direction == Vector2.up)
            SpriteRenderer.sprite = up;
        else if (Movement.direction == Vector2.down)
            SpriteRenderer.sprite = down;
        else if (Movement.direction == Vector2.left)
            SpriteRenderer.sprite = left;
        else if (Movement.direction == Vector2.right)
            SpriteRenderer.sprite = right;
    }
}
