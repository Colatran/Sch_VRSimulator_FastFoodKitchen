using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    [SerializeField] Renderer render;
    [SerializeField] float interactibleRadius = 0;

    protected virtual void OnValidate()
    {
        if (render == null) render = GetComponent<Renderer>();
        if (render == null) render = GetComponentInChildren<Renderer>();

        if (gameObject.layer != 8 && gameObject.layer != 5) Debug.LogError(gameObject.name + " - Interactible - tem de estar na layer 8(Interactible)!!!");
        if (GetComponent<Collider>() == null) Debug.LogError(gameObject.name + " - Interactible - precisa de um collider");
        else if (GetComponent<Collider>().isTrigger) Debug.LogError(gameObject.name + " - Interactible - collider não pode ser Trigger!!!");
    }

    private void Start()
    {
        materials = render.materials;
    }



    private Material[] materials;
    private int hilight = 0;
    public float InteractibleRadius { get => interactibleRadius; }

    public void AddHilight()
    {
        hilight++;
        if (hilight == 1) HilightOn();
    }
    public void RemoveHilight()
    {
        hilight--;
        if (hilight == 0) HilightOff();
    }

    private void HilightOn()
    {
        Material[] newMaterials = render.materials;

        for (int i = 0; i < render.materials.Length; i++)
            newMaterials[i] = GameManager.Asset.Material_Hilight;
        
        render.materials = newMaterials;
    }
    private void HilightOff() 
    {
        render.materials = materials;
    }


    public abstract void Interact(HandInteractor sender, bool grab);
}
