using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

    private void Update()
    {
        if (gameOverScreen.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                RestartGame();
            }
        }
    }

    public void LoseLife()
    {
        life--;
        lifeSprites[life]?.SetActive(false);
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
        StartCoroutine(EndGame());


    }
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        yield return null;
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
    public void SpawnPrism(Prism instance, Vector3 position, Quaternion quaternion) {
        StartCoroutine(Spawnprism(instance, position, quaternion));
    }

    IEnumerator Spawnprism(Prism instance, Vector3 position, Quaternion quaternion) {
        yield return new WaitForSeconds(1.5f);
        Instantiate(instance, position, quaternion);
    }
}
