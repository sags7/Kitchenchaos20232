using System.Collections.Generic;
using UnityEngine;

public class IngredientContainerUI : MonoBehaviour
{
    [SerializeField] private Transform _ingredientIconTemplate;
    [SerializeField] private Transform _ingredientContainer;

    private void ClearVisuals()
    {
        foreach (Transform child in transform)
        {
            if (child == _ingredientIconTemplate) continue;
            Destroy(child.gameObject);
        }
    }

    public void CreateIngredientIcons(List<IngredientRequirement> list)
    {
        ClearVisuals();
        foreach (IngredientRequirement requirement in list)
        {
            for (int i = requirement.Amount; i > 0; i--)
            {
                Transform newIcon = Instantiate(_ingredientIconTemplate, _ingredientContainer);
                newIcon.gameObject.SetActive(true);
                newIcon.GetComponent<IndividualIngredientIconUI>().SetSprite(requirement.Ingredient._sprite);
            }
        }
    }
}
