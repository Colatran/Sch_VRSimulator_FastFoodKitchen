using UnityEngine;
using TMPro;

public class Orderer_Prep : Orderer
{
    [SerializeField] TMP_Text[] counters;
    [SerializeField] Recipe[] recipes;
    private int[] orders;
    private int served;


    private void Awake()
    {
        orders = new int[recipes.Length];
    }


    public override void MakeOrder()
    {
        AddToOrder(Random.Range(0, recipes.Length));
    }
    public override int TotalServed()
    {
        return served;
    }

    public void ServeOrder(Recipe recipe)
    {
        int recipeIndex = GetOrderIndex(recipe);

        if(orders[recipeIndex] == 0)
        {
            GameManager.MakeMistake(MistakeType.PREPARADOR_NENHUMPEDIDO);
        }
        else
        {
            RemoveToOrder(recipeIndex);
        }
    }

    private void AddToOrder(int index)
    {
        orders[index]++;
        counters[index].text = orders[index] + "";
    }
    private void RemoveToOrder(int index)
    {
        orders[index]--;
        counters[index].text = orders[index] + "";
    }

    private int GetOrderIndex(Recipe recipe)
    {
        for (int i = 0; i< recipes.Length; i++)
        {
            if (recipes[i] == recipe) return i;
        }
        return 0;
    }
}
