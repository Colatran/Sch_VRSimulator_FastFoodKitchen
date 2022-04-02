using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] Renderer render;
    [SerializeField] float interactibleRadius = 0;

    protected virtual void OnValidate()
    {
        if (render == null) render = GetComponent<Renderer>();
        if (render == null) render = GetComponentInChildren<Renderer>();

        if(gameObject.layer != 8 && gameObject.layer != 5) Debug.LogError(gameObject.name + " - Interactible - tem de estar na layer 8(Interactible)!!!");
        if (GetComponent<Collider>() == null) Debug.LogError(gameObject.name + " - Interactible - precisa de um collider");
        else if (GetComponent<Collider>().isTrigger) Debug.LogError(gameObject.name + " - Interactible - collider não pode ser Trigger!!!");
    }

    private void Start()
    {
        material = render.material;
    }



    private Material material;
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

    private void HilightOn() => render.material = GameManager.Asset.Material_Hilight;
    private void HilightOff() => render.material = material;


    public virtual void Interact(GameObject sender, bool grab) { }
}
