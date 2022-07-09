using UnityEngine;

public class AssetHolder : MonoBehaviour
{
    [SerializeField] AssetHolderObject assetObject;

    public static AssetHolder Instance;
    public static AssetHolderObject Asset
    {
        get => Instance.assetObject;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
