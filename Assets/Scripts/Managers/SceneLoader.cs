using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static Scene _targetScene;
    public enum Scene
    {
        MainMenuScene,
        LoadingScene,
        GameScene
    }

    public static void Load(Scene targetScene)
    {
        _targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void SceneLoaderCallback() => SceneManager.LoadScene(_targetScene.ToString());

}
