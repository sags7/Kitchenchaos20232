using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenWieldableSO[] inputs;
    public KitchenWieldableSO output;
    public int FryingNeeded;
}
