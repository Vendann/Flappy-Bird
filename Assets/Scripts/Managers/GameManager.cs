using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    public Player Player { set {player = value;} }
    [SerializeField] private PipeSpawner spawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text recordText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private AudioSource audioSource;

    private int score;
    public int Score => score;

    private void Start() {
        if (Instance != null)
            DestroyImmediate(gameObject);
        else {
            Instance = this;
            Application.targetFrameRate = 60;
            Pause();
        }
        recordText.text = "Record: " + PlayerPrefs.GetInt("BestScore").ToString();
    }

    public void Play() {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        recordText.gameObject.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        VerticalMoving[] verticalPipes = FindObjectsOfType<VerticalMoving>();
        CirclePipes[] circlePipes = FindObjectsOfType<CirclePipes>();

        for (int i = 0; i < pipes.Length; i++)
            Destroy(pipes[i].gameObject);

        for (int i = 0; i < verticalPipes.Length; i++)
            Destroy(verticalPipes[i].gameObject);

        for (int i = 0; i < circlePipes.Length; i++)
            Destroy(circlePipes[i].gameObject);

            spawner.gameObject.SetActive(true);
            spawner.Repeater();
    }

    public void GameOver() {
        spawner.Stop();
        spawner.StopRepeat();
        audioSource.PlayOneShot(audioSource.clip);
        playButton.SetActive(true);
        gameOver.SetActive(true);
        recordText.gameObject.SetActive(true);
        if (score > PlayerPrefs.GetInt("BestScore")) 
            PlayerPrefs.SetInt("BestScore", score);
        recordText.text = "Record: " + PlayerPrefs.GetInt("BestScore").ToString();
        Pause();
    }

    public void Pause() {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore() {
        score++;
        scoreText.text = score.ToString();
    }
}
