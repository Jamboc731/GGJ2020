using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IPressable
{

    public UnityEvent OnPress;

    public void press()
    {
        OnPress.Invoke();
    }
}

public interface IPressable
{
    void press();
}
