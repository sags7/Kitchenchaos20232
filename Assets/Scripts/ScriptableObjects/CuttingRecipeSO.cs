using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenWieldableSO[] inputs;
    public KitchenWieldableSO output;
    public int cuttingNeeded;
}
