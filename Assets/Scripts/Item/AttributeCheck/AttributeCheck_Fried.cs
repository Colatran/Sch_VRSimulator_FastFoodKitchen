
public class AttributeCheck_Fried : AttributeCheck
{
    public override void OnAddAttribute(ItemAttribute flag)
    {
        if (flag == ItemAttribute.SALT)
        {
            GameManager.MakeMistake(MistakeType.PRODUTO_FRITO_SALGADO);
        }
    }
}
