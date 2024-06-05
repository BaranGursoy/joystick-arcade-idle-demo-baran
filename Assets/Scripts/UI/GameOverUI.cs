using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverObject;
    private void Awake()
    {
        GameActions.GameFinished += ActivateGameOverUI;
    }

    private void OnDestroy()
    {
        GameActions.GameFinished -= ActivateGameOverUI;
    }

    private void ActivateGameOverUI()
    {
        gameOverObject.SetActive(true);
    }
}
