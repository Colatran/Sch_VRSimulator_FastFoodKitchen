using UnityEngine;

public class ContentCheck_BoardFried : ContentCheck
{
    [SerializeField] ItemType friedType;
    [SerializeField] ItemType gridType;


    public override void OnAdd(Item item)
    {
        if (item.Is(ItemType.BOARDINTERIOR))
        {
            //ERRO  -grelha sujo
            if (item.Has(ItemAttribute.DIRT))
                GameManager.MakeMistake(MistakeType.GAVETAFRITO_GRELHA_SUJA);

            //ERRO  -papel
            //ERRO  -grelha errada
            if (item.Is(ItemType.BOARDINTERIOR_PAPER))
                GameManager.MakeMistake(MistakeType.GAVETAFRITO_PAPEL);
            else if (!item.Is(gridType))
                GameManager.MakeMistake(MistakeType.GAVETAFRITO_GRELHA_ERRADA);
        }

        else if (item.Is(ItemType.FRIED))
        {
            //ERRO  -produto-   contaminado
            if (item.Has(ItemAttribute.DIRT))
                GameManager.MakeMistake(MistakeType.GAVETA_PRODUTO_CONTAMINADO);

            //ERRO  -gaveta-    falta grelha
            if (!container.Contains(ItemType.BOARDINTERIOR_GRID))
                GameManager.MakeMistake(MistakeType.GAVETAFRITO_GRELHA_FALTA);

            if (MustSkipItem(item)) return;

            //ERRO  -produto-   cru
            //ERRO  -produto-   queimado
            Item_Cookable itemCookable = (item as Item_Cookable);
            if (itemCookable.IsUndercooked)
                GameManager.MakeMistake(MistakeType.GAVETA_PRODUTO_CRU);
            else if (itemCookable.IsOvercooked)
                GameManager.MakeMistake(MistakeType.GAVETA_PRODUTO_QUEIMADO);

            //ERRO  -frito-      tipo errado
            if (!item.Is(friedType))
                GameManager.MakeMistake(MistakeType.GAVETAFRITO_FRITO_TIPOERRADO);

            //Defenir BatchId do tabuleiro
            if (container.BatchId == 0)
            {
                int batchId = item.BatchId;
                container.BatchId = batchId;
            }
            //ERRO  -frito-      misturou produto velho com novo
            else if (IsOldBatch(item))
                GameManager.MakeMistake(MistakeType.GAVETA_PRODUTO_MISTURADO_LOTE);
        }

        else
        {
            if (MustSkipItem(item)) return;

            if (item.Is(ItemType.BEEF))
                GameManager.MakeMistake(MistakeType.GAVETAFRITO_BIFE);
        }
    }
}
