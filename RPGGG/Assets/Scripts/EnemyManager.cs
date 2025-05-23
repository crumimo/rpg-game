using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int totalEnemiesToKill = 10; 
    [SerializeField] private TMP_Text enemyCounterText; 
    [SerializeField] private CanvasGroup victoryCanvasGroup; 
    [SerializeField] private AudioSource victorySound;
    [SerializeField] private float fadeDuration = 1f; 


    private int enemiesKilled = 0;

    private void Start()
    {
        UpdateEnemyCounter();
        victoryCanvasGroup.alpha = 0f;
        victoryCanvasGroup.interactable = false;
        victoryCanvasGroup.blocksRaycasts = false;
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        UpdateEnemyCounter();

        if (enemiesKilled >= totalEnemiesToKill)
        {
            ShowVictoryScreen();
        }
    }

    private void UpdateEnemyCounter()
    {
        enemyCounterText.text = $"Enemies killed: {enemiesKilled} / {totalEnemiesToKill}";
    }

    private void ShowVictoryScreen()
    {
        if (victorySound != null)
        {
            victorySound.Play(); 
        }
    
        StartCoroutine(FadeInCanvas());
        
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    private IEnumerator FadeInCanvas()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            victoryCanvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }

        victoryCanvasGroup.interactable = true;
        victoryCanvasGroup.blocksRaycasts = true;
        Time.timeScale = 0f; 
    }

}