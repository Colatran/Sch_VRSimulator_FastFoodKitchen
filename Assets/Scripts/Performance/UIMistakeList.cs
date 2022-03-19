using UnityEngine;

public class UIMistakeList : MonoBehaviour
{
    [SerializeField] GameObjectPool pool;

    private void OnValidate()
    {
        foreach (GameObject _object in pool.Objects)
        {
            _object.GetComponent<UIMistakeButton>().SetList(this);
        }
    }




    public void SetDescription(MistakeType mistake) { }
}
