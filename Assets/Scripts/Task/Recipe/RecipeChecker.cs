using System.Collections.Generic;
using UnityEngine;

public class RecipeChecker : MonoBehaviour
{
    [SerializeField] Recipe[] recipes;
    [SerializeField] Orderer_Prep orderer;

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null) return;

        if (item.Is(ItemType.BOX))
        {
            OnHamburguerIn(item);
        }
        else return;
    }

    private void OnHamburguerIn(Item parent)
    {
        List<Attachment> endChildren = parent.Attachment.EndChildren;
        List<Item> items = new List<Item>();

        foreach (Attachment attachment in endChildren)
            items.Add(attachment.GetComponent<Item>());

        Recipe recipe = GetRecipe(items);

        Destroy(parent.gameObject);

        if (recipe == null) return;
        else orderer.ServeOrder(recipe);
    }


    public Recipe GetRecipe(List<Item> items)
    {
        int i;

        #region Divide
        List<Item> beefSequence = GetBeefSequence(items);

        List<Item> bread = GetType(items, ItemType.BREAD);
        List<Item> sauces = GetType(items, ItemType.SAUCE);
        List<Item> ingredients = GetType(items, ItemType.INGREDIENT);
        List<Item> cheese = GetType(items, ItemType.CHEESE);
        List<Item> fried = GetType(items, ItemType.FRIED);
        List<Item> beef = GetType(items, ItemType.BEEF);

        if (bread.Count == 0) GameManager.MakeMistake(MistakeType.PREPARADOR_FALTA_PAO);
        if (cheese.Count == 0) GameManager.MakeMistake(MistakeType.PREPARADOR_FALTA_QUEIJO);
        if (fried.Count > 0)
        {
            if (beef.Count > 0) GameManager.MakeMistake(MistakeType.PREPARADOR_MISTUROU_CARNETIPO);
            else beef = fried;
        }
        else if (beef.Count == 0)
        {
            GameManager.MakeMistake(MistakeType.PREPARADOR_FALTA_CARNE);
        }
        #endregion

        #region CheckSectors
        ItemType breadType = GetBreadType(bread);
        ItemType beefType = GetBeefType(beef);
        ItemType cheeseType = GetCheeseType(cheese);
        #endregion

        #region Analize
        RecipeResult[][] results = new RecipeResult[recipes.Length][];
        for (i = 0; i < results.Length; i++)
        {
            results[i] = recipes[i].Check(breadType, beefType, cheeseType, sauces, ingredients, beefSequence);
        }
        #endregion

        #region Select
        int[] scores = new int[results.Length];
        for (i = 0; i < scores.Length; i++)
            scores[i] = GetScore(results[i]);

        int highestScore = scores[0];
        int highestScoreIndex = 0;
        bool tie = false;
        for (i = 1; i < scores.Length; i++)
        {
            int score = scores[i];
            if (score > highestScore)
            {
                highestScore = score;
                highestScoreIndex = i;
                tie = false;
            }
            else if (score == highestScore)
            {
                tie = true;
            }
        }

        if (tie)
        {
            GameManager.MakeMistake(MistakeType.PREPARADOR_RECEITA_ERRADA);
            return null;
        }
        #endregion

        RecipeResult[] result = results[highestScoreIndex];

        if (result[0] == RecipeResult.INCORRECT) GameManager.MakeMistake(MistakeType.PREPARADOR_RECEITA_ERRADA_PAO);

        if (result[1] == RecipeResult.INCORRECT) GameManager.MakeMistake(MistakeType.PREPARADOR_RECEITA_ERRADA_MOLHO);
        else if (result[1] == RecipeResult.MISSING) GameManager.MakeMistake(MistakeType.PREPARADOR_RECEITA_ERRADA_MOLHO_FALTA);
        else if (result[1] == RecipeResult.TOOMUCH) GameManager.MakeMistake(MistakeType.PREPARADOR_RECEITA_ERRADA_MOLHO_MUITO);

        if (result[2] == RecipeResult.INCORRECT) GameManager.MakeMistake(MistakeType.PREPARADOR_RECEITA_ERRADA_INGREDIENTES);
        else if (result[2] == RecipeResult.MISSING) GameManager.MakeMistake(MistakeType.PREPARADOR_RECEITA_ERRADA_INGREDIENTES_FALTA);
        else if (result[2] == RecipeResult.TOOMUCH) GameManager.MakeMistake(MistakeType.PREPARADOR_RECEITA_ERRADA_INGREDIENTES_MUITO);

        if (result[3] == RecipeResult.INCORRECT) GameManager.MakeMistake(MistakeType.PREPARADOR_RECEITA_ERRADA_CARNE);
        if (result[4] == RecipeResult.INCORRECT) GameManager.MakeMistake(MistakeType.PREPARADOR_RECEITA_ERRADA_QUEIJO);
        if (result[5] == RecipeResult.INCORRECT) GameManager.MakeMistake(MistakeType.PREPARADOR_RECEITA_ERRADA_DESORGANIZADO);

        return recipes[highestScoreIndex];
    }

    public List<Item> GetBeefSequence(List<Item> items)
    {
        List<Item> sequence = new List<Item>();

        for (int i = 0; i< items.Count; i++)
        {
            Item item = items[i];

            if (item.Is(ItemType.BEEF) || item.Is(ItemType.FRIED) || item.Is(ItemType.CHEESE))
                sequence.Add(item);
        }

        return sequence;
    }
    public List<Item> GetType(List<Item> items, ItemType type)
    {
        List<Item> itemsType = new List<Item>();
        for (int i = 0; i < items.Count;)
        {
            Item item = items[i];
            if(item.Is(type))
            {
                itemsType.Add(item);
                items.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
        return itemsType;
    }
    public ItemType GetBreadType(List<Item> items)
    {
        Item item = items[0];
        ItemType type;

        if (item.Is(ItemType.BREAD_NORMAL)) type = ItemType.BREAD_NORMAL;
        else if (item.Is(ItemType.BREAD_SEEDS)) type = ItemType.BREAD_SEEDS;
        else type = ItemType.BREAD_NOGLUTEN;

        for (int i = 1; i < items.Count; i++)
        {
            Item_Cookable itemCookable = items[i] as Item_Cookable;
            if(!itemCookable.IsCooked)
            {
                GameManager.MakeMistake(MistakeType.PREPARADOR_TORRADEIRA_NAOTORROU);
                break;
            }
        }
        for (int i = 1; i < items.Count; i++)
        {
            item = items[i];
            if (!item.Is(type))
            {
                GameManager.MakeMistake(MistakeType.PREPARADOR_MISTUROU_PAO);
                break;
            }
        }

        return type;
    }
    public ItemType GetBeefType(List<Item> items)
    {
        Item item = items[0];
        ItemType type;

        if (item.Is(ItemType.BEEF))
        {
            if (item.Is(ItemType.BEEF_NORMAL)) type = ItemType.BEEF_NORMAL;
            else type = ItemType.BEEF_VEGAN;
        }
        else
        {
            if (item.Is(ItemType.FRIED_CHIKEN_FILLET)) type = ItemType.FRIED_CHIKEN_FILLET;
            else type = ItemType.FRIED_FISH_FILLET;
        }

        for (int i = 1; i < items.Count; i++)
        {
            item = items[i];
            if (!item.Is(type))
            {
                GameManager.MakeMistake(MistakeType.PREPARADOR_MISTUROU_CARNE);
                return type;
            }
        }

        return type;
    }
    public ItemType GetCheeseType(List<Item> items)
    {
        Item item = items[0];
        ItemType type;

        if (item.Is(ItemType.CHEESE_NORMAL)) type = ItemType.CHEESE_NORMAL;
        else if (item.Is(ItemType.CHEESE_CHEDDAR)) type = ItemType.CHEESE_CHEDDAR;
        else type = ItemType.CHEESE_SOY;

        for (int i = 1; i < items.Count; i++)
        {
            item = items[i];
            if (!item.Is(type))
            {
                GameManager.MakeMistake(MistakeType.PREPARADOR_MISTUROU_QUEIJOS);
                return type;
            }
        }

        return type;
    }


    private int GetScore(RecipeResult[] results)
    {
        int score = 0;
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] == RecipeResult.CORRECT) score += 2;
            else if (results[i] == RecipeResult.TOOMUCH) score += 2;
        }

        return score;
    }
}