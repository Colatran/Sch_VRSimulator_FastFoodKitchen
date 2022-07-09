using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "NewObject/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] ItemType breadType;
    [SerializeField] ItemType[] sauces;
    [SerializeField] Ingredient[] ingredients;
    [SerializeField] ItemType beefType;
    [SerializeField] ItemType cheeseType;
    [SerializeField] bool[] beefSeqence;


    
    public RecipeResult[] Check(
        ItemType breadType, ItemType beefType, ItemType cheeseType,
        List<Item> sauce, List<Item> ingredients, List<Item> beefSequence)
    {
        //0 - Pão
        //1 - Molhos
        //2 - Ingredientes
        //3 - Carne
        //4 - Queijo
        //5 - Sequencia Carne e Queijo
        RecipeResult[] results = new RecipeResult[6];

        results[0] = Check_Ingredient(breadType, this.breadType);
        results[3] = Check_Ingredient(beefType, this.beefType);
        results[4] = Check_Ingredient(cheeseType, this.cheeseType);
        results[1] = Check_Sauce(sauce, sauces);
        results[2] = Check_Ingredients(ingredients);
        results[5] = Check_BeefSequence(beefSequence);

        return results;
    }

    private RecipeResult Check_Ingredient(ItemType other_type, ItemType my_type)
    {
        if (other_type == my_type) return RecipeResult.CORRECT;
        else return RecipeResult.INCORRECT;
    }
    
    private RecipeResult Check_Ingredients(List<Item> items)
    {
        int[] quantities = new int[ingredients.Length];

        foreach (Item item in items)
        {
            int quantityIndex = FindIngredientIndex(item);
            if (quantityIndex == -1) return RecipeResult.INCORRECT;
            else quantities[quantityIndex]++;
        }

        for (int i = 0; i < ingredients.Length; i++)
        {
            int quantity = quantities[i];
            Ingredient ingredient = ingredients[i];

            if (quantity < ingredient.min) return RecipeResult.MISSING;
            if (quantity > ingredient.max) return RecipeResult.TOOMUCH;
        }

        return RecipeResult.CORRECT;
    }
    
    private RecipeResult Check_Sauce(List<Item> items, ItemType[] ingredients)
    {
        int[] quantities = new int[ingredients.Length];

        foreach (Item item in items)
        {
            int quantityIndex = FindSauceIndex(item);
            if (quantityIndex == -1) return RecipeResult.INCORRECT;
            else quantities[quantityIndex]++;
        }

        foreach (int quantity in quantities)
        {
            if (quantity < 1) return RecipeResult.MISSING;
            if (quantity > 2) return RecipeResult.TOOMUCH;
        }

        return RecipeResult.CORRECT;
    }
    
    private RecipeResult Check_BeefSequence(List<Item> items)
    {
        if (items.Count != beefSeqence.Length) return RecipeResult.INCORRECT;

        for (int i = 0; i < items.Count; i++)
        {
            if (GetItemType(items[0]) != beefSeqence[i]) return RecipeResult.INCORRECT;
        }

        return RecipeResult.CORRECT;
    }



    private int FindSauceIndex(Item item)
    {
        for (int i = 0; i < sauces.Length; i++)
            if (item.Is(sauces[i])) return i;
        return -1;
    }

    private int FindIngredientIndex(Item item)
    {
        for (int i = 0; i < ingredients.Length; i++)
            if (item.Is(ingredients[i].type)) return i;
        return -1;
    }

    private bool GetItemType(Item item)
    {
        if (item.Is(ItemType.BEEF) || item.Is(ItemType.FRIED)) return true;
        else return false;
    }



    [System.Serializable]
    private struct Ingredient
    {
        public ItemType type;
        public int min;
        public int max;
    }
}