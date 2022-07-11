using UnityEngine;
using TMPro;

public class FrierTimer : MonoBehaviour
{
    [SerializeField] private Button3D button;
    [SerializeField] private Frier frierFrypot;
    [SerializeField] private FrierElevator elevator;
    [SerializeField] private TextMeshPro Counter;
    [SerializeField] private AudioSource audioSource;

    private bool up = true;
    private bool started = false;
    private float cookingTime = 0;



    private void OnEnable()
    {
        button.OnPressed += OnButtonPressed;
        elevator.OnStopped += OnElevatorStopped;
    }
    private void OnDisable()
    {
        button.OnPressed -= OnButtonPressed;
        elevator.OnStopped -= OnElevatorStopped;
    }



    private void OnButtonPressed()
    {
        up = !up;
        elevator.Move(up);

        if (started)
        {
            StopTimer();

            FrierBasket basket = elevator.Basket;
            if (basket != null  && !basket.IsEmpty())
            {
                float _cookingTime = frierFrypot.CookingFactors.CookingTime;
                if (cookingTime > 0 && cookingTime < _cookingTime - 5)
                    GameManager.MakeMistake(MistakeType.FRITADEIRA_TEMPORIZADOR_INVALIDADO);
            }
        }
    }
    private void OnElevatorStopped()
    {
        if (up == false) 
            StartTimer();
    }

    private void StartTimer()
    {
        started = true;

        float _cookingTime = frierFrypot.CookingFactors.CookingTime;
        cookingTime = _cookingTime;

        Counter.text = "" + _cookingTime;
    }
    private void StopTimer()
    {
        started = false;
        Counter.text = "00";

        up = true;
        elevator.Move(up);
    }



    private void Update()
    {
        if (started) Update_Timer();
    }

    private void Update_Timer()
    {
        if (cookingTime > 0)
        {
            int seconds = (int)(cookingTime + 1);
            if(seconds < 10)
                Counter.text = "0" + seconds;
            else 
                Counter.text = "" + seconds;

            cookingTime -= Time.deltaTime;
            if (cookingTime <= 0)
            {
                cookingTime = 0;
                audioSource.Play();
                StopTimer();
            }
        }
    }
}
