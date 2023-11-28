using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private Energy _energy;
    private Goal _goal;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _energy = GameObject.FindGameObjectWithTag("Player").GetComponent<Energy>();
        _goal = FindAnyObjectByType<Goal>();

        if (!_canvasGroup) return;
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void OnEnable()
    {
        _energy.onEnergyDepleted.AddListener(ShowMenu);
        _goal.onGoalReach.AddListener(ShowMenu);
    }

    private void OnDisable()
    {
        _energy.onEnergyDepleted.RemoveListener(ShowMenu);
        _goal.onGoalReach.RemoveListener(ShowMenu);
    }
    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
