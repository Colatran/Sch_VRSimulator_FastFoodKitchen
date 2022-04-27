using UnityEngine;

public class ContentCheck_BoardBeef : ContentCheck
{
    [SerializeField] ItemType beefType;


    public override void OnAdd(Item item)
    {
        if (item.Is(ItemType.BOARDINTERIOR_PAPER))
        {
            //ERRO  -papel sujo
            if (item.Has(ItemAttribute.DIRT))
                GameManager.MakeMistake(MistakeType.GAVETABIFE_PAPEL_SUJO);

            if (MustSkipItem(item)) return;

            //ERRO  -reutilizou papel
            if (IsOldBatch(item))
                GameManager.MakeMistake(MistakeType.GAVETABIFE_PAPEL_REUTILIZADO);

            //ERRO  -muito papel
            int paperCount = container.FindAll(ItemType.BOARDINTERIOR_PAPER).Length;
            if (paperCount > 1)
                GameManager.MakeMistake(MistakeType.GAVETABIFE_PAPEL_MUITO);
        }

        else if (item.Is(ItemType.BEEF))
        {
            //ERRO  -produto-   contaminado
            if (item.Has(ItemAttribute.DIRT))
                GameManager.MakeMistake(MistakeType.GAVETA_PRODUTO_CONTAMINADO);

            //ERRO  -gaveta-    falta papel
            if (!container.Contains(ItemType.BOARDINTERIOR_PAPER))
                GameManager.MakeMistake(MistakeType.GAVETABIFE_PAPEL_FALTA);

            if (MustSkipItem(item)) return;

            //ERRO  -produto-   cru
            //ERRO  -produto-   queimado
            //ERRO  -bife-      sem sal
            Item_Cookable itemCookable = (item as Item_Cookable);
            if (itemCookable.IsUndercooked)
                GameManager.MakeMistake(MistakeType.GAVETA_PRODUTO_CRU);
            else if (itemCookable.IsOvercooked)
                GameManager.MakeMistake(MistakeType.GAVETA_PRODUTO_QUEIMADO);
            else if (!item.Has(ItemAttribute.SALT))
                GameManager.MakeMistake(MistakeType.GAVETABIFE_BIFE_SEMSAL);

            //ERRO  -bife-      tipo errado
            if (!item.Is(beefType))
                GameManager.MakeMistake(MistakeType.GAVETABIFE_BIFE_TIPOERRADO);

            //Defenir BatchId do tabuleiro e do papel no tabuleiro
            if (container.BatchId == 0)
            {
                int batchId = item.BatchId;
                container.BatchId = batchId;

                foreach (Item content in container.FindAll(ItemType.BOARDINTERIOR_PAPER))
                {
                    content.BatchId = batchId;
                    Item_Cookable contentCookable = (content as Item_Cookable);
                    contentCookable.SetHeatSource(HeatSource.COOKER);
                }
            }
            //ERRO  -bife-      misturou produto velho com novo
            else if (IsOldBatch(item))
                GameManager.MakeMistake(MistakeType.GAVETA_PRODUTO_MISTURADO_LOTE);
        }

        else
        {
            if (MustSkipItem(item)) return;

            if (item.Is(ItemType.FRIED))
                GameManager.MakeMistake(MistakeType.GAVETABIFE_FRITO);
        }
    }
}
