using UnityEngine;


// Base class for ghost behavior modes like Chase, Scatter, or Frightened.
// Handles timed enable/disable logic.
[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    // Reference to the owning ghost. 
    public Ghost ghost { get; private set; }

    // How long this behavior stays active when enabled with duration. 
    public float duration;

    private void Awake()
    {
        ghost = GetComponent<Ghost>();
    }

    // Enables this behavior for the default duration. 
    public void Enable()
    {
        Enable(duration);
    }

    // Enables the behavior for a specified duration. 
    public virtual void Enable(float duration)
    {
        enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    // Disables this behavior. 
    public virtual void Disable()
    {
        enabled = false;
        CancelInvoke();
    }
}
