
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Slider progressBar;
    [SerializeField] Text progressText;
    [SerializeField] RectTransform rotate;

    float timer;
    public async void LoadNextScene()
    {
        timer = 0;
        progressBar.value = 0;

        var scene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        scene.allowSceneActivation = false;

        do
        {
            await Task.Delay(100);
            timer = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(1000);
        scene.allowSceneActivation = true;
    }

    private void Update()
    {
        progressBar.value = Mathf.MoveTowards(progressBar.value, timer + 0.1f, 3 * Time.deltaTime);
        progressText.text = "Loading... " + (progressBar.value * 100).ToString("0") + "%";
        rotate.rotation = Quaternion.Euler(0, 0, rotate.rotation.z * Time.deltaTime * 10f); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
