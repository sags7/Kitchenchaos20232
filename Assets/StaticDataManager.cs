using UnityEngine;

public class StaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        BaseCounter.ClearStaticData();
        CuttingCounter.ClearStaticData();
        TrashCounter.ClearStaticData();
    }
}
