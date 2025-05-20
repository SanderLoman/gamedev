using UnityEngine;
using UnityEngine.UI;

// Controls the overall game state: score, lives, win/loss logic, and round handling.
// Uses a Singleton pattern to allow global access via GameManager.Instance.
[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Scene References")]
    [SerializeField] private Ghost[] ghosts;
    [SerializeField] private Pacman pacman;
    [SerializeField] private Transform pellets;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    private int ghostMultiplier = 1;

    private void Awake()
    {
        if (Instance != null) DestroyImmediate(gameObject);
        else Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    private void Start()
    {
        if (!scoreText || !livesText || !gameOverText)
            Debug.LogWarning("⚠️ UI Text references not set!", this);

        if (!pellets)
            Debug.LogWarning("⚠️ Pellets container not set!", this);

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
            NewGame();
    }

    // Starts a new game session.
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    // Prepares a new round by resetting all pellets and characters.
    private void NewRound()
    {
        if (gameOverText) gameOverText.enabled = false;

        if (pellets != null)
        {
            foreach (Transform pellet in pellets)
                pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    // Resets the state of Pacman and all ghosts.
    private void ResetState()
    {
        foreach (Ghost ghost in ghosts)
            if (ghost != null) ghost.ResetState();

        if (pacman != null) pacman.ResetState();
    }

    // Triggers game-over state.
    private void GameOver()
    {
        if (gameOverText) gameOverText.enabled = true;

        foreach (Ghost ghost in ghosts)
            if (ghost != null) ghost.gameObject.SetActive(false);

        if (pacman != null) pacman.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        if (livesText)
            livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
        this.score = score;
        if (scoreText)
            scoreText.text = score.ToString().PadLeft(2, '0');
    }

    // Called when Pacman is caught by a ghost.
    public void PacmanEaten()
    {
        if (pacman != null) pacman.DeathSequence();

        SetLives(lives - 1);

        if (lives > 0) Invoke(nameof(ResetState), 3f);
        else GameOver();
    }

    // Called when a ghost is eaten by Pacman.
    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);
        ghostMultiplier++;
    }

    // Called when a pellet is collected by the player.
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(score + pellet.points);

        if (!HasRemainingPellets())
        {
            if (pacman != null) pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    // Called when a power pellet is collected.
    public void PowerPelletEaten(PowerPellet pellet)
    {
        foreach (Ghost ghost in ghosts)
            if (ghost != null) ghost.frightened.Enable(pellet.duration);

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    // Checks if there are any pellets remaining in the level.
    private bool HasRemainingPellets()
    {
        if (pellets == null) return false;

        foreach (Transform pellet in pellets)
            if (pellet.gameObject.activeSelf) return true;

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }
}
