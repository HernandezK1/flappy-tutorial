using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public Spawner spawner;
    private Health playerHealth;
    private int score;
    public GameObject[] life;
    private void Awake()
    {
        Application.targetFrameRate = 60;

        Pause();

    }

    private void Start()
    {
        
    }
    public void Play()
    {
        playerHealth = player.GetComponent<Health>();
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i <pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        spawner.minHeight = -1f;
        spawner.maxHeight = 1f;

        playerHealth.currentHealth = Health.maxHealth;
        life[0].gameObject.SetActive(true);
        life[1].gameObject.SetActive(true);
        life[2].gameObject.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);

        Pause();
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        if (score == 10)
        {
           spawner.minHeight = -2f;
            spawner.maxHeight = 2f;
        }

        if (score == 20)
        {
            spawner.minHeight = -3f;
            spawner.maxHeight = 3f;
        }

        if (score ==50)
        {
            gameOver.SetActive(true);
            playButton.SetActive(true);
            player.enabled = false;
            Pause();
        }
    }
    public void TakeDamage()
    {
        playerHealth.TakeDamage(1);
        if (playerHealth.currentHealth < 3)
        {
            life[2].gameObject.SetActive(false);
        }
        if (playerHealth.currentHealth < 2)
        {
            life[1].gameObject.SetActive(false);
        }
        if (playerHealth.currentHealth < 1)
        {
            life[0].gameObject.SetActive(false);
        }
        if(playerHealth.currentHealth == 0)
        {
            GameOver(); 
        }

    }
}
