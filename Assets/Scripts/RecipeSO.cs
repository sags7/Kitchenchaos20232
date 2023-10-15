using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public KitchenWieldableSO[] inputs;
    public KitchenWieldableSO output;
    public int cuttingNeeded;
}
