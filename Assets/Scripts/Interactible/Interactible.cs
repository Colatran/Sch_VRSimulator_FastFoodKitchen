using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] Renderer render;
    private Material material;

    private int hilight = 0;
    public bool IsHilighted { get => hilight > 0; }
    public bool NotHilighted { get => hilight == 0; }


    protected virtual void OnValidate()
    {
        if (render == null) render = GetComponent<Renderer>();
        if (render == null) render = GetComponentInChildren<Renderer>();
    }

    private void Start()
    {
        material = render.material;
    }


    public void AddHilight(string by)
    {
        hilight++;
        if (hilight == 1) HilightOn();
    }
    public void RemoveHilight(string by)
    {
        hilight--;
        if (hilight == 0) HilightOff();
    }

    private void HilightOn() => render.material = GameManager.Asset.hilightMaterial;
    private void HilightOff() => render.material = material;


    public virtual void Interact(GameObject gObject) { }
}
