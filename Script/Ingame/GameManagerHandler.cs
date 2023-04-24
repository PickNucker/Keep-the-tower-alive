using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerHandler : MonoBehaviour
{
    public static GameManagerHandler instance;

    [SerializeField] Text gameTimeText;

    MMF_Player endScreen;
    bool end;

    float gameTime;
    void Start()
    {
        end = false;
        Time.timeScale = 1;
        instance = this;
        endScreen = GetComponent<MMF_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!end)
            gameTime += Time.deltaTime;
    }

    public void GameOver()
    {
        end = true;
        gameTimeText.text = "You lasted: " + gameTime.ToString("0.00") + " sec.";
        endScreen.PlayFeedbacks();
        Invoke(nameof(PauseGame), 2f);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }


}
