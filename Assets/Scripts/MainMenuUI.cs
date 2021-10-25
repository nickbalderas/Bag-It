using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button startButton;
    public Canvas mainMenu;
    public Camera gameCamera;

    public GameObject main;
    public Button backToMenu;
    
    public Button howToPlayButton;
    public GameObject howToPlay;
    
    private bool hasGameStarted;

    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        howToPlayButton.onClick.AddListener(ShowHowToPlay);
        backToMenu.onClick.AddListener(() => SceneManager.LoadScene(0));
    }

    private void Update()
    {
        if (hasGameStarted)
        {
           TransitionCamera();
           StartCoroutine("LoadMainScene");
        }
    }

    private void StartGame()
    {
        mainMenu.enabled = false;
        hasGameStarted = true;
    }

    private void ShowHowToPlay()
    {
        howToPlay.SetActive(true);
    }

    private void TransitionCamera()
    {
        if (gameCamera.transform.rotation.x < 90)
        {
            var cameraRotation = Camera.main.transform.rotation;
            Camera.main.transform.rotation = Quaternion.Lerp(cameraRotation, Quaternion.Euler(90, -90, 0), 1 * Time.deltaTime);
        }
    }

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
