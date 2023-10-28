using UnityEngine;

public class SceneLoaderCallback : MonoBehaviour
{
    private bool IsFirstUpdate = true;
    void Update()
    {
        if (IsFirstUpdate)
        {
            IsFirstUpdate = false;
            SceneLoader.SceneLoaderCallback();
        }
    }
}
