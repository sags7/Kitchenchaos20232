using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _playButton.onClick.AddListener(() => SceneLoader.Load(SceneLoader.Scene.GameScene));
        _quitButton.onClick.AddListener(() => Application.Quit());
    }

    private void Awake()
    {
        Time.timeScale = 1f;
    }
}
