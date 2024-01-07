using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private Status _playerStatus;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<Status>();

        if (!_canvasGroup) return;
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void OnEnable()
    {
        _playerStatus.onObstacleHit.AddListener(ShowMenu);
    }

    private void OnDisable()
    {
        _playerStatus.onObstacleHit.RemoveListener(ShowMenu);
    }
    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Clicked!" + currentSceneIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ShowMenu()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    private void ToggleMenu(bool isVisible)
    {
        if (isVisible)
        {
            _canvasGroup.alpha = 1;
        }
        else
        {
            _canvasGroup.alpha = 0;
        }

        _canvasGroup.interactable = isVisible;
        _canvasGroup.blocksRaycasts = isVisible;
    }
}
