using UnityEngine;

public class CookingFactors : MonoBehaviour
{
    [SerializeField] float cookingTime = 60;
    [SerializeField] float maxTemperature = 85;
    [ReadOnly, SerializeField] float cookingTimeFactor = 0;
    [ReadOnly, SerializeField] float maxTemperatureFactor = 0;

    public float CookingTime { get => cookingTime; }
    public float CookingTimeFactor { get => cookingTimeFactor; }
    public float MaxTemperature { get => maxTemperature; }
    public float MaxTemperatureFactor { get => maxTemperatureFactor; }


    private void OnValidate()
    {
        cookingTimeFactor = 1 / cookingTime;
        maxTemperatureFactor = maxTemperature / cookingTime;
    }

}
