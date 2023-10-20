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
    public GameObject youWin;
    public GameObject getReady;
    public GameObject[] life;
    private int scoreIndex;
    public GameObject prefabs;
    public float incrementAmount;
    public int incrementSpeed;
    public Sprite deathSprite;

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
        prefabs.GetComponent<Pipes>().speed = 5;
        playerHealth = player.GetComponent<Health>();
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        getReady.SetActive(false);
        youWin.SetActive(false);
        

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
        //dead = true;
        Pause();
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        if (scoreIndex == incrementSpeed) 
        {

            prefabs.GetComponent<Pipes>().speed = prefabs.GetComponent<Pipes>().speed + incrementAmount;
            scoreIndex = 0;
        }
        else
        {
            scoreIndex++;
        }
    }

    private void Update()
    {
        if (score == 10)
        {
           spawner.minHeight = -1.5f;
            spawner.maxHeight = 1.5f;
        }

        if (score == 20)
        {
            spawner.minHeight = -2f;
            spawner.maxHeight = 2f;
        }

        if (score ==50)
        {
            youWin.SetActive(true);
            playButton.SetActive(true);
            player.enabled = false;
            Pause();
        }

        if (score == 35)
        {
            spawner.spawnRate = 0.1f;
        }
        //dead = player.isDead;       
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
            player.isDead = true;
            player.GetComponent<SpriteRenderer>().sprite = deathSprite;
            GameOver(); 
        }

    }
}
