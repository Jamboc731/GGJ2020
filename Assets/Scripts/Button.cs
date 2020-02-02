using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IPressable
{

    [SerializeField] private UnityAction OnPress;

    public void press()
    {
        OnPress();
    }
}

public interface IPressable
{
    void press();
}
