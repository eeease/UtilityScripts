using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections;

[Serializable]
public class SwipeEvents : UnityEvent{ }

public class EventTest : MonoBehaviour
{

    public SwipeEvents OnEvent;

}