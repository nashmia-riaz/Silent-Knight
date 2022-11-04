using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;

    public GameObject mainMenu;

    [SerializeField]
    float loadingTime = 2;

    public Image loadingBar;
    public GameObject loadingScreen;

    public Transform healthBar;
    public GameObject healthPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        StartCoroutine(ProgressLoadingBar());
    }

    /// <summary>
    /// Coroutine that plays loading bar at start of game while game is initializing.
    /// </summary>
    /// <returns></returns>
    IEnumerator ProgressLoadingBar()
    {
        int timer = 0;
        float currentFilled = 0;
        float remiainingFill = 1;

        //while timer is not done, fill the loading bar exponentially 
        while(timer <= loadingTime)
        {
            //bar always fills to half of remaining amount, so it never truly finishes filling
            loadingBar.fillAmount = currentFilled;
            currentFilled += remiainingFill / 2f;
            remiainingFill = 1f - currentFilled;
            timer++;
            yield return new WaitForSeconds(0.1f);
        }

        //once timer is done, show the game
        gameObject.GetComponent<Animator>().SetTrigger("LoadingFadeOut");
    }

    /// <summary>
    /// called to initialize UI when game is started (when user presses a key)
    /// </summary>
    /// <param name="playerHealth">current player health used to initialize the UI</param>
    public void OnGameStart(int playerHealth)
    {
        gameObject.GetComponent<Animator>().SetTrigger("MenuFadeOut");
        gameObject.GetComponent<Animator>().SetTrigger("GameFadeIn");
        for (int i = 0; i < playerHealth; i++)
        {
            Instantiate(healthPrefab).transform.SetParent(healthBar);
        }
    }

    /// <summary>
    /// Whenever player loses health, call this function to update the uI
    /// </summary>
    /// <param name="currentHealth">current player health</param>
    public void OnLoseHealth(int currentHealth)
    {
        //delete any health indicators beyond player's current health
        for(int i = healthBar.childCount - 1; i >= 0; i--)
        {
            if (i > currentHealth - 1)
                Destroy(healthBar.GetChild(i).gameObject);
        }

        //if remaining health is 1, glow the health UI 
        if (currentHealth == 1)
            healthBar.GetChild(0).GetComponent<Animator>().SetTrigger("Glow Red");
    }

    public void OnGameOver()
    {
        gameObject.GetComponent<Animator>().SetTrigger("GameOverFadeIn");
    }
}
