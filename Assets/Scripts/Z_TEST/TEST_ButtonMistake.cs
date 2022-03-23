using UnityEngine;
using System.Collections.Generic;

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
        foreach (KeyValuePair<MistakeType, Mistake> mistake in MistakeLibrary.mistakes)
        {
            GameManager.MakeMistake(mistake.Key);
        }
    }
}
