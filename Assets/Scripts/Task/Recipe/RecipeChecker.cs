using System.Collections.Generic;
using UnityEngine;

public class RecipeChecker : MonoBehaviour
{
    [SerializeField] Recipe[] recipes;


    private void OnTriggerEnter(Collider other)
    {
        Item item = GetComponent<Item>();
        if (item == null) return;

        if (item.Is(ItemType.BOX))
        {
            List<Attachment> endChildren = item.Attachment.EndChildren;
            List<Item> items = new List<Item>();

            foreach (Attachment attachment in endChildren)
                items.Add(attachment.GetComponent<Item>());

            CheckRecipe(items);
        }
        else
        {
            Attachment endParent = item.Attachment.EndParent;
            if (endParent == null)
            {
                GameManager.MakeMistake(MistakeType.PREPARADOR_FALTA_CAIXA);
            }
            else
            {
                Item parent = endParent.GetComponent<Item>();
                if (parent == null) return;
                if (!parent.Is(ItemType.BOX)) GameManager.MakeMistake(MistakeType.PREPARADOR_FALTA_CAIXA);
            }
        }
    }

    public Recipe CheckRecipe(List<Item> items)
    {
        #region Divide
        bool incomplete = false;

        ItemType breadType = GetBread(items);
        if (breadType == ItemType.NONE) return null;

        List<Item> sauces = GetSector(items, ItemType.SAUCE, MistakeType.PREPARADOR_MISTUROU_MOLHOS);

        List<Item> ingredients = GetSector(items, ItemType.SAUCE, MistakeType.PREPARADOR_MISTUROU_MOLHOS);

        ItemType beefType = ItemType.NONE;
        ItemType cheeseType = ItemType.NONE;
        List<Item> beefSequence = GetBeefSector(items, ref beefType, ref cheeseType);

        if (beefType == ItemType.NONE)
        {
            GameManager.MakeMistake(MistakeType.PREPARADOR_FALTA_HAMBURGUER);
            incomplete = true;
        }
        if (cheeseType == ItemType.NONE)
        {
            GameManager.MakeMistake(MistakeType.PREPARADOR_FALTA_QUEIJO);
            incomplete = true;
        }
        if (incomplete) return null;
        #endregion

        #region Analize
        RecipeResult[][] results = new RecipeResult[recipes.Length][];
        for (int i = 0; i < results.Length; i++)
        {
            results[i] = recipes[i].Check(breadType, beefType, cheeseType, sauces, ingredients, beefSequence);
        }
        #endregion

        //SELECT
        int[] score = new int[results.Length];

        for (int i = 0; i < score.Length; i++)
            score[i] = GetScore(results[i]);






        return null;
    }


    private ItemType GetBread(List<Item> items)
    {
        Item bun_upper = items[0];
        Item bun_lower = items[items.Count - 1];
        ItemType bunType = ItemType.NONE;

        if (!(bun_upper.Is(ItemType.BREAD) && bun_lower.Is(ItemType.BREAD)))
        {
            GameManager.MakeMistake(MistakeType.PREPARADOR_FALTA_PAO);
            return bunType;
        }

        if (bun_upper.Is(ItemType.BREAD_NORMAL)) bunType = ItemType.BREAD_NORMAL;
        else if (bun_upper.Is(ItemType.BREAD_SEEDS)) bunType = ItemType.BREAD_SEEDS;
        else if (bun_upper.Is(ItemType.BREAD_NOGLUTEN)) bunType = ItemType.BREAD_NOGLUTEN;

        if (!bun_lower.Is(bunType))
        {
            GameManager.MakeMistake(MistakeType.PREPARADOR_MISTUROU_PAO);
            bunType = ItemType.NONE;
            return bunType;
        }

        items.RemoveAt(items.Count - 1);
        items.RemoveAt(0);

        return bunType;
    }

    private List<Item> GetSector(List<Item> items, ItemType type, MistakeType mistake)
    {
        bool inSector = false;
        bool toFail = true;
        List<Item> sector = new List<Item>();

        for (int i = items.Count - 1; i < -1; i--)
        {
            Item item = items[i];

            if (item.Is(type))
            {
                inSector = true;
                sector.Add(item);
                items.RemoveAt(i);
            }
            else if (toFail)
            {
                if (inSector)
                {
                    toFail = true;
                    GameManager.MakeMistake(mistake);
                }
            }
        }

        return sector;
    }

    private List<Item> GetBeefSector(List<Item> items, ref ItemType beefType, ref ItemType cheeseType)
    {
        List<Item> sector = new List<Item>();
        beefType = default;
        cheeseType = default;

        for (int i = items.Count - 1; i < -1; i--)
        {
            Item item = items[i];

            if (item.Is(ItemType.BEEF))
            {
                if (item.Is(ItemType.BEEF_NORMAL)) beefType = ItemType.BEEF_NORMAL;
                else beefType = ItemType.BEEF_VEGAN;
            }
            else if (item.Is(ItemType.FRIED))
            {
                if (item.Is(ItemType.FRIED_CHIKEN_FILLET)) beefType = ItemType.FRIED_CHIKEN_FILLET;
                else beefType = ItemType.FRIED_FISH_FILLET;
            }
            else if (item.Is(ItemType.CHEESE))
            {
                if (item.Is(ItemType.CHEESE_NORMAL)) cheeseType = ItemType.CHEESE_NORMAL;
                else if (item.Is(ItemType.CHEESE_CHEDDAR)) cheeseType = ItemType.CHEESE_CHEDDAR;
                else cheeseType = ItemType.CHEESE_SOY;
            }
            else
            {
                GameManager.MakeMistake(MistakeType.PREPARADOR_ITEM_INVALIDO);
                return sector;
            }
        }

        return sector;
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
