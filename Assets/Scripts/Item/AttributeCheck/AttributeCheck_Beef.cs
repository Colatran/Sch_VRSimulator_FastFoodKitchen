using UnityEngine;

public class AttributeCheck_Beef : AttributeCheck
{
    [SerializeField] Item_Cookable item;

    public override void OnAddAttribute(ItemAttribute flag)
    {
        if (flag == ItemAttribute.SALT)
        {
            if (item.IsUndercooked)
            {
                GameManager.MakeMistake(MistakeType.PRODUTO_BIFE_CRUSALGADO);
            }

            else if(item.IsCooked)
            {
                if (attributes.FindAll(x => x == ItemAttribute.SALT).Count > 2)
                    GameManager.MakeMistake(MistakeType.PRODUTO_BIFE_MUITOSAL);
            }
        }
    }
}
