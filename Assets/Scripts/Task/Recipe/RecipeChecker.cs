using System.Collections.Generic;
using UnityEngine;

public class RecipeChecker : MonoBehaviour
{
    [SerializeField] Recipe[] recipes;


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

        if (recipe == null) Debug.Log(null);
        else Debug.Log(recipe.name);
    }


    public Recipe GetRecipe(List<Item> items)
    {
        int i;
        #region Divide
        bool incomplete = false;

        ItemType breadType = GetBread(items);
        if (breadType == ItemType.NONE)
        {
            return null;
        }

        Debug.Log("itemsCount " + items.Count);
        List<Item> sauces = GetSector(items, ItemType.SAUCE, MistakeType.PREPARADOR_MISTUROU_MOLHOS);
        Debug.Log("itemsCount " + items.Count);
        List<Item> ingredients = GetSector(items, ItemType.SAUCE, MistakeType.PREPARADOR_MISTUROU_MOLHOS);
        Debug.Log("itemsCount " + items.Count);

        ItemType beefType = ItemType.NONE;
        ItemType cheeseType = ItemType.NONE;
        List<Item> beefSequence = GetBeefSector(items, ref beefType, ref cheeseType);

        Debug.Log("itemsCount " + items.Count);
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
        if (incomplete)
        {
            return null;
        }
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
            return null;
        }
        else return recipes[highestScoreIndex];
        #endregion
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
        Debug.Log("GetSector");
        bool inSector = false;
        bool toFail = true;
        List<Item> sector = new List<Item>();

        for (int i = items.Count - 1; i < -1; i--)
        {
            Debug.Log("Start");
            Item item = items[i];

            if (item.Is(type))
            {
                Debug.Log("IsType");
                inSector = true;
                sector.Add(item);
                items.RemoveAt(i);
            }
            else if (toFail)
            {
                Debug.Log("else");
                if (inSector)
                {
                    Debug.Log("fail");
                    toFail = true;
                    GameManager.MakeMistake(mistake);
                }
            }
        }
        Debug.Log("itemsCount " + items.Count);
        return sector;
    }

    private List<Item> GetBeefSector(List<Item> items, ref ItemType beefType, ref ItemType cheeseType)
    {
        Debug.Log("GetBeefSector");
        List<Item> sector = new List<Item>();
        beefType = ItemType.NONE;
        cheeseType = ItemType.NONE;

        bool unmistakenBeef = true;
        bool unmistakenCheese = true;

        for (int i = items.Count - 1; i > -1; i--)
        {
            Debug.Log("Start");
            Item item = items[i];
          
            if (item.Is(ItemType.BEEF))
            {
                Debug.Log("  IsBeef");
                if (unmistakenBeef)
                {
                    Debug.Log("    unmistaken");
                    if (item.Is(ItemType.BEEF_NORMAL))
                    {
                        Debug.Log("      BEEF_NORMAL");
                        unmistakenBeef = SetOrMistake(ItemType.BEEF_NORMAL, ref beefType, MistakeType.PREPARADOR_MISTUROU_CARNES);
                    }
                    else
                    {
                        Debug.Log("      else");
                        unmistakenBeef = SetOrMistake(ItemType.BEEF_VEGAN, ref beefType, MistakeType.PREPARADOR_MISTUROU_CARNES);
                    }              
                }
            }
            else if (item.Is(ItemType.FRIED))
            {
                Debug.Log("  IsFried");
                if (unmistakenBeef)
                {
                    Debug.Log("    unmistaken");
                    if (item.Is(ItemType.FRIED_CHIKEN_FILLET))
                    {
                        Debug.Log("      FRIED_CHIKEN_FILLET");
                        unmistakenBeef = SetOrMistake(ItemType.FRIED_CHIKEN_FILLET, ref beefType, MistakeType.PREPARADOR_MISTUROU_CARNES);
                    }
                    else
                    {
                        Debug.Log("      else");
                        unmistakenBeef = SetOrMistake(ItemType.FRIED_FISH_FILLET, ref beefType, MistakeType.PREPARADOR_MISTUROU_CARNES);
                    }
                }
            }
            else if (item.Is(ItemType.CHEESE))
            {
                Debug.Log("  IsCheese");
                if (unmistakenCheese)
                {
                    Debug.Log("    unmistaken");
                    if (item.Is(ItemType.CHEESE_NORMAL))
                    {
                        Debug.Log("      CHEESE_NORMAL");
                        unmistakenCheese = SetOrMistake(ItemType.CHEESE_NORMAL, ref cheeseType, MistakeType.PREPARADOR_MISTUROU_QUEIJOS);
                    } 
                    else if (item.Is(ItemType.CHEESE_CHEDDAR))
                    {
                        Debug.Log("      CHEESE_CHEDDAR");
                        unmistakenCheese = SetOrMistake(ItemType.CHEESE_CHEDDAR, ref cheeseType, MistakeType.PREPARADOR_MISTUROU_QUEIJOS);
                    }
                    else
                    {
                        Debug.Log("      else");
                        unmistakenCheese = SetOrMistake(ItemType.CHEESE_SOY, ref cheeseType, MistakeType.PREPARADOR_MISTUROU_QUEIJOS);
                    }  
                }
            }
            else
            {
                Debug.Log("  else");
                GameManager.MakeMistake(MistakeType.PREPARADOR_ITEM_INVALIDO);
                return sector;
            }
        }

        return sector;
    }

    private bool SetOrMistake(ItemType type, ref ItemType beefType, MistakeType mistakeType)
    {
        if (beefType != ItemType.NONE && beefType != type)
        {
            Debug.Log("        SetOrMistake if");
            GameManager.MakeMistake(mistakeType);
            return false;
        }
        else
        {
            Debug.Log("        SetOrMistake else");
            beefType = type;
            return true;
        }
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