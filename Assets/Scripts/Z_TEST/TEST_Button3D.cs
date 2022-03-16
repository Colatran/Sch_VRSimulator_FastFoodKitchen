using UnityEngine;

public class TEST_Button3D : MonoBehaviour
{
    [SerializeField] Button3D button;
    [SerializeField] Renderer m_renderer;

    private void OnEnable()
    {
        button.OnPressed += OnPressed;
        button.OnReleased += OnReleased;
    }
    private void OnDisable()
    {
        button.OnPressed -= OnPressed;
        button.OnReleased -= OnReleased;
    }


    private void OnPressed()
    {
        m_renderer.material = GameManager.Asset.Material_UHC_TimerOn;
    }
    private void OnReleased()
    {
        m_renderer.material = GameManager.Asset.Material_UHC_TimerOff;
    }
}
