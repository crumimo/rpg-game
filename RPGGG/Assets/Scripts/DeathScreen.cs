using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;  
    [SerializeField] private AudioSource deathSound;    
    [SerializeField] private float fadeDuration = 1f;      
    [SerializeField] private float delayAfterDeath = 1f;   

    private bool isShowing = false;

    void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    
    public void ShowDeathScreen()
    {
        if (!isShowing)
        {
            isShowing = true;
            
            StartCoroutine(ShowDeathCoroutine());
        }
    }

    private IEnumerator ShowDeathCoroutine()
    {
        yield return new WaitForSeconds(delayAfterDeath);
        
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);
            deathSound.Play();
            yield return null;
        }
        
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        Time.timeScale = 0f;
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}