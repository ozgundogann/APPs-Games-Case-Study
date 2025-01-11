using System;
using Unity.VisualScripting;
using UnityEngine;

public class ApplicationFrameSetter : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}