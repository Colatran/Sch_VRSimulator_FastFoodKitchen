using System.Collections.Generic;
using UnityEngine;

public class Button3D : MonoBehaviour
{
    [SerializeField] Transform movinTransform;
    [SerializeField] Vector3 movingDirection;
    [SerializeField] float movingDistance;
    [SerializeField] float velocity;
    [ReadOnly, SerializeField] Vector3 startingPosition;
    [ReadOnly, SerializeField] Vector3 targetPosition;

    private void OnValidate()
    {
        startingPosition = movinTransform.position;
        targetPosition = movinTransform.position + movingDirection.normalized * movingDistance;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(startingPosition, Vector3.one * .01f);

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(targetPosition, Vector3.one * .01f);
        Gizmos.DrawLine(startingPosition, targetPosition);
    }



    public delegate void Action();
    public event Action OnPressed;
    public event Action OnReleased;

    private List<FingerTip> fingerTips = new List<FingerTip>();
    private bool pressed = false;
    private float positionPercentage = 0;



    private void OnTriggerEnter(Collider other)
    {
        FingerTip fingerTip = other.GetComponent<FingerTip>();
        if (fingerTip == null) return;

        if(fingerTip.Pressing)
        {
            if (fingerTips.Contains(fingerTip)) return;
            fingerTips.Add(fingerTip);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        FingerTip fingerTip = other.GetComponent<FingerTip>();
        if (fingerTip == null) return;

        fingerTips.Remove(fingerTip);
    }


    private void Update()
    {
        CheckPressed();

        ButtonAnimation();
    }

    private void CheckPressed()
    {
        if (pressed)
        {
            if (fingerTips.Count == 0)
            {
                pressed = false;
                if (OnReleased != null)
                    OnReleased();
            }
        }
        else if (fingerTips.Count > 0)
        {
            pressed = true;
            if (OnPressed != null)
                OnPressed();
        }
    }

    private void ButtonAnimation()
    {
        
        if (pressed)
        {
            if (positionPercentage < 1) positionPercentage += Time.deltaTime * velocity;
        }
        else
        {
            if (positionPercentage > 0) positionPercentage -= Time.deltaTime * velocity;
        }

        movinTransform.position = Vector3.Lerp(startingPosition, targetPosition, positionPercentage);
    }
}
