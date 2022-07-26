

public class ContentCheck_Box : ContentCheck
{
    public override void OnAdd(Item item)
    {
        if (item.Has(ItemAttribute.DIRT)) GameManager.MakeMistake(MistakeType.PRODUTO_CONTAMINADO);
    }
}
