using UnityEngine;

// Base class for all pellet types
[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour
{
    public int points = 10; // Score gained when eaten

    // Virtual method so PowerPellet can override it
    protected virtual void Eat()
    {
        GameManager.Instance.PelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Trigger only when Pacman touches this pellet
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
