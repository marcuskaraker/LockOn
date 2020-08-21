using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Healthbar playerHealthbar;
    public GameObject gameStartScreen;
    public GameObject gameOverScreen;
    public Text scoreText;
    public Text highScoreText;

    private RectTransform playerHealthBarRectTransform;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealthBarRectTransform = playerHealthbar.GetComponent<RectTransform>();

        gameStartScreen.SetActive(!GameManager.hasStartedGame);       
    }

    private void Update()
    {
        scoreText.text = GameManager.Instance.score.ToString();
        highScoreText.text = "High Score: " + GameManager.Instance.HighScore;

        if (player)
        {
            playerHealthbar.gameObject.SetActive(true);
            playerHealthbar.SetHealthPercentage(GameManager.Instance.PlayerDestructible.health / GameManager.Instance.PlayerDestructible.maxHealth);
            playerHealthBarRectTransform.position = GameManager.Instance.MainCamera.WorldToScreenPoint(player.transform.position);
        }
        else
        {
            playerHealthbar.gameObject.SetActive(false);
        }

        if (GameManager.Instance.IsGameOver)
        {
            gameOverScreen.SetActive(true);
            gameOverScreen.transform.localScale = Vector3.Lerp(gameOverScreen.transform.localScale, Vector3.one, Time.deltaTime * 10);
        }
        else
        {
            gameOverScreen.SetActive(false);
            gameOverScreen.transform.localScale = Vector3.zero;
        }
    }
}
