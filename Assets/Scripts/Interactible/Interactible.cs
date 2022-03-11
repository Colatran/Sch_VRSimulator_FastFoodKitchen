using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] Renderer render;
    [SerializeField] float interactibleRadius = 0;

    private Material material;
    private int hilight = 0;

    public bool IsHilighted { get => hilight > 0; }
    public bool NotHilighted { get => hilight == 0; }
    public float InteractibleRadius { get => interactibleRadius; }

    protected virtual void OnValidate()
    {
        if (render == null) render = GetComponent<Renderer>();
        if (render == null) render = GetComponentInChildren<Renderer>();
    }

    private void Start()
    {
        material = render.material;
    }


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

    private void HilightOn() => render.material = GameManager.Asset.hilightMaterial;
    private void HilightOff() => render.material = material;


    public virtual void Interact(GameObject gObject) { }
}
