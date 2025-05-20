using UnityEngine;

// Power pellet that triggers frightened state in ghosts
public class PowerPellet : Pellet
{
    public float duration = 8f; // How long ghosts stay frightened

    protected override void Eat()
    {
        GameManager.Instance.PowerPelletEaten(this);
    }
}
