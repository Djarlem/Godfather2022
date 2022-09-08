using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int life = 3;
    [SerializeField] private UnityEvent onGameOver;
    private AudioSource audioSource;
    [SerializeField] private List<GameObject> lifeSprites = new List<GameObject>();
    [SerializeField] private GameObject gameOverScreen;
    private int numberOfLife;
    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        onGameOver.AddListener(OnGameOver);
        numberOfLife = life;
    }

    public void LoseLife()
    {
        life--;
        lifeSprites[life - 1].SetActive(false);
        if (life <= 0)
            GameOver();
    }

    public void ResetLife()
    {
        life = numberOfLife;
        foreach (GameObject lifeSprite in lifeSprites)
        {
            lifeSprite.SetActive(true);
        }
    }

    private void OnGameOver()
    {
        audioSource.Play();
    }

    private void GameOver()
    {
        onGameOver?.Invoke();
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
