using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "NewObject/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] Ingredient[] ingredients;

    [System.Serializable]
    public struct Ingredient
    {
        public ItemType itemType;
        public int min;
        public int max;

        public Ingredient(ItemType itemType, int min, int max)
        {
            this.itemType = itemType;
            this.min = min;
            this.max = max;
        }
    }
}
