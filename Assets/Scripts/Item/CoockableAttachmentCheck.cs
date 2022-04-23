using UnityEngine;

public class CoockableAttachmentCheck : MonoBehaviour
{
    [SerializeField] Item_Cookable item;

    private void OnValidate()
    {
        if (item == null) item = GetComponent<Item_Cookable>();
    }



    public void OnAttach()
    {
        if(item.IsCooked || item.IsOvercooked)
        {
            if (item.Attachment.DirectParent.GetComponent<HandPhysicsController>() != null)
            {
                GameManager.MakeMistake(MistakeType.PRODUTO_COMASMAOS);
            }
        }
    }
}
