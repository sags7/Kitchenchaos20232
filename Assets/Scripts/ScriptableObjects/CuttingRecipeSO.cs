using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenWieldableSO input;
    public KitchenWieldableSO output;
    public int cuttingNeeded;
}
