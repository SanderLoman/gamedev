using UnityEngine;


/// Handles simple frame-based sprite animation.
/// Attach this component to any GameObject with a SpriteRenderer
/// and assign an array of sprites to animate through them.
[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    
    /// Array of sprites used for the animation frames.
    public Sprite[] sprites = new Sprite[0];

    
    /// Time (in seconds) between each frame of the animation.
    public float animationTime = 0.25f;

    
    /// If true, the animation will loop back to the beginning when finished.
    public bool loop = true;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    
    /// Called when the script instance is being loaded.
    /// Gets a reference to the SpriteRenderer on this GameObject.
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    /// Called when the GameObject becomes enabled.
    /// Makes sure the sprite is visible.
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    
    /// Called when the GameObject becomes disabled.
    /// Hides the sprite.
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    
    /// Called on the first frame the script is active.
    /// Starts the animation loop using InvokeRepeating.
    private void Start()
    {
        InvokeRepeating(nameof(Advance), animationTime, animationTime);
    }

    
    /// Advances the animation to the next frame.
    /// Loops back to the start if needed.
    private void Advance()
    {
        if (!spriteRenderer.enabled) {
            return;
        }

        animationFrame++;

        if (animationFrame >= sprites.Length && loop) {
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < sprites.Length) {
            spriteRenderer.sprite = sprites[animationFrame];
        }
    }

    
    /// Restarts the animation from the beginning.
    public void Restart()
    {
        animationFrame = -1;
        Advance();
    }
}
