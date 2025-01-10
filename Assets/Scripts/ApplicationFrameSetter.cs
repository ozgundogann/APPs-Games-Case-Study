using System;
using Unity.VisualScripting;
using UnityEngine;

public class ApplicationFrameSetter : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}