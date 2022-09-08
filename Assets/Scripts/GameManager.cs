using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int life = 3;
    [SerializeField] private UnityEvent onGameOver;
    private AudioSource audioSource;

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseLife()
    {
        life--;
        if (life <= 0)
            GameOver();
    }

    public void GetLife()
    {
        life++;
    }

    private void OnGameOver()
    {
        audioSource.Play();
    }

    private void GameOver()
    {
        onGameOver?.Invoke();
    }
}
