using UnityEngine;

public class TEST_ButtonMistake : MonoBehaviour
{
    [SerializeField] Button3D button;

    private void OnEnable()
    {
        button.OnPressed += OnPressed;
    }
    private void OnDisable()
    {
        button.OnPressed -= OnPressed;
    }


    private void OnPressed()
    {
        GameManager.MakeMistake(MistakeType.BIFE_CRUSALGADO);
        GameManager.MakeMistake(MistakeType.BIFE_ORDEMERRADA);
        GameManager.MakeMistake(MistakeType.BIFE_SAL_MUITO);
        GameManager.MakeMistake(MistakeType.BIFE_SAL_SEM);
        GameManager.MakeMistake(MistakeType.FRITADEIRA_BIFENACUBA);
    }
}
