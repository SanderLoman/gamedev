using UnityEngine;

/// Controls an individual ghost's behavior and state, including movement and collision.
[DefaultExecutionOrder(-10)]
[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }

    // Initial behavior on game start (e.g. scatter, chase)
    public GhostBehavior initialBehavior;

    // Optional: Target transform (used by AI)
    public Transform target;

    // Points given when eaten in frightened mode.
    public int points = 200;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        home = GetComponent<GhostHome>();
        scatter = GetComponent<GhostScatter>();
        chase = GetComponent<GhostChase>();
        frightened = GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    // Resets the ghost to its initial state and behavior.
    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();

        frightened.Disable();
        chase.Disable();
        scatter.Enable();

        if (home != initialBehavior) home.Disable();
        if (initialBehavior != null) initialBehavior.Enable();
    }

    // Forces ghost position without affecting Z depth.
    public void SetPosition(Vector3 position)
    {
        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (frightened.enabled)
                GameManager.Instance.GhostEaten(this);
            else
                GameManager.Instance.PacmanEaten();
        }
    }
}
