using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button startButton;
    public Canvas mainMenu;
    public Camera gameCamera;
    private bool hasGameStarted;

    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
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
