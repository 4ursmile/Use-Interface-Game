using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float GravityScale = 1;
    [SerializeField] List<GameObject> GamePrefab;
    [SerializeField] float SpawnRate = 1;
    [SerializeField] TextMeshProUGUI testScore;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI StartMenu;
    static public GameManager instance;
    float Score = 0;
    bool isGameActive = true;
    private void Awake()
    {
        isGameActive = false;
        instance = this;
        gameOverText.gameObject.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= GravityScale;
        testScore.text = "score: " + Score.ToString();
    }
    public void GameOverApper()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UploadScore(int score2Add)
    {
        if (!isGameActive) return;
        Score += score2Add;
        if (Score < 0)
            GameOverApper();
        testScore.text = "score: " + Score.ToString();
    }
    IEnumerator SpawnnObject()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(SpawnRate);
            if (!isGameActive) yield return null;
            Instantiate(GamePrefab[Random.Range(0, GamePrefab.Count)]);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LevelButton(float SpawnLevel)
    {
        SpawnRate = SpawnLevel;
        isGameActive = true;
        StartMenu.gameObject.SetActive(false);
        StartCoroutine(SpawnnObject());

    }
}
