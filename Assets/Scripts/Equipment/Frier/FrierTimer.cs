using UnityEngine;
using TMPro;

public class FrierTimer : MonoBehaviour
{
    [SerializeField] private Button3D button;
    [SerializeField] private TextMeshPro Counter;
    [SerializeField] private Frier frierFrypot;

    private float cookingTime = 0;
    private bool activated = false;

    public bool Activated { get => activated; }
    public delegate void Action();
    public event Action OnActivated;


    private void OnEnable()
    {
        button.OnPressed += OnButtonPressed;
    }
    private void OnDisable()
    {
        button.OnPressed -= OnButtonPressed;
    }


    private void OnButtonPressed()
    {
        float _cookingTime = frierFrypot.CookingFactors.CookingTime;

        if (cookingTime > 0 && cookingTime < _cookingTime - 5)
            GameManager.MakeMistake(MistakeType.FRITADEIRA_TEMPORIZADOR_INVALIDADO);

        cookingTime = _cookingTime;

        if(!activated)
        {
            activated = true;

            if (OnActivated != null)
                OnActivated();
        }
    }




    private void Update()
    {
        if (cookingTime > 0)
        {
            Counter.text = "" + (int)cookingTime;
            cookingTime -= Time.deltaTime;
            if (cookingTime <= 0)
            {
                cookingTime = 0;
                OnFinishedTime();
            }
        }
    }



    private void OnFinishedTime()
    {
        activated = false;
        Counter.text = "00";
    }
}
