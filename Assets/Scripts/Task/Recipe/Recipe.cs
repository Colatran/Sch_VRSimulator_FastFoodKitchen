using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "NewObject/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] ItemType breadType;
    [SerializeField] ItemType beefType;
    [SerializeField] int beefCount = 1;
    [SerializeField] ItemType cheeseType;
    [SerializeField] ItemType[] sauces;
    [SerializeField] ItemType[] ingredients;


    public RecipeResult[] Check(List<Attachment> attachments)
    {
        List<Item> items = new List<Item>();
        foreach (Attachment child in attachments)
            items.Add(child.GetComponent<Item>());

        //0 - Pão
        //1 - Molhos
        //2 - Ingredientes
        //3 - Queijo e Carne
        RecipeResult[] results = new RecipeResult[5];

        int itemIndex;
        List<Item> listBuffer = new List<Item>();




        #region Bun
        itemIndex = 0;
        Item bun_upper = items[itemIndex];
        RecipeResult bun_upper_result = Check_Bread(bun_upper);
        if (bun_upper_result != RecipeResult.MISSING) items.RemoveAt(itemIndex);

        itemIndex = items.Count - 1;
        Item bun_lower = items[itemIndex];
        RecipeResult bun_lower_result = Check_Bread(bun_lower);
        if (bun_lower_result != RecipeResult.MISSING) items.RemoveAt(itemIndex);

        if (bun_upper_result == RecipeResult.CORRECT || bun_lower_result == RecipeResult.CORRECT)
            results[0] = RecipeResult.CORRECT;
        else
            results[0] = RecipeResult.INCORRECT;

        if (bun_upper_result == RecipeResult.CORRECT && bun_upper_result == RecipeResult.INCORRECT ||
            bun_upper_result == RecipeResult.INCORRECT && bun_upper_result == RecipeResult.CORRECT)
            GameManager.MakeMistake(MistakeType.PREPARADOR_MISTUROU_PAO);
        #endregion

        #region
        foreach (Item item in items)
        {
            if (item.Is(ItemType.SAUCE)) ;
        }

        #endregion



        /*for (int i = items.Count - 1; i > -1;)
        {
            Item item = items[i];

            if (stage == 0)
            {
                if (item.Is(ItemType.BREAD))
                {
                    if (item.Is(breadType)) results[stage] = RecipeResult.CORRECT;
                    else results[stage] = RecipeResult.INCORRECT;
                    i--;
                }
                else results[stage] = RecipeResult.MISSING;

                stage++;
            }

            else if (stage == 1)
            {
                if (item.Is(ItemType.SAUCE))
                {
                    //sauceCount++;

                    bool invalidSauce = true;
                    foreach (ItemType sauce in sauces)
                    {
                        if (item.Is(sauce))
                        {
                            invalidSauce = false;

                            break;
                        }
                    }
                    if (invalidSauce)
                    {
                        //return 1;
                    }
                    else
                    {

                    }
                }
                else
                {
                    //if(sauceCount == 0) results[stage] = RecipeResult.MISSING;
                    //else if (sauceCount >= sauces.Length * 2)
                    {
                        //Make Mistake
                    }
                    i++;
                    stage++;
                }
            }
        }*/

        return results;
    }


    private RecipeResult Check_Bread(Item bread)
    {
        if (bread.Is(ItemType.BREAD))
            if (bread.Is(breadType)) return RecipeResult.CORRECT;
            else return RecipeResult.INCORRECT;
        else return RecipeResult.MISSING;
    }




    private struct IngredientQuantity
    {
        public ItemType type;
        public int quantity;

        public IngredientQuantity(ItemType type)
        {
            this.type = type;
            quantity = 0;
        }
        private IngredientQuantity(ItemType type, int quantity)
        {
            this.type = type;
            this.quantity = quantity;
        }

        public IngredientQuantity Add() => new IngredientQuantity(type, quantity + 1);
        public bool Is(ItemType type) => this.type == type;
    }
}