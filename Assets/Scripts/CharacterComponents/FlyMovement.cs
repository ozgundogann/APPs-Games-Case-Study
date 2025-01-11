using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class FlyMovement : MonoBehaviour
{
    [Header("LineRenderer")]
    [SerializeField] private LineRenderer lineRenderer_L;
    [SerializeField] private LineRenderer lineRenderer_R;
    [SerializeField] private float trailOpeningDelay = 0.3f;

    [Header("FlyMovement Resources")] 
    [SerializeField] private float leanAngle;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float rollSpeed;

    [SerializeField] private Transform characterMesh;
    [SerializeField] private DynamicJoystick dynamicJoystick;

    private float horizontal;
    private Vector3 targetPos;
    private Coroutine newRoutine;


    private void Update()
    {
        GetMovementInputs();
        RotateFlight();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(ApplyTrailsWithDelay));
    }

    private IEnumerator ApplyTrailsWithDelay()
    {
        yield return new WaitForSeconds(trailOpeningDelay);
        lineRenderer_L.enabled = true;
        lineRenderer_R.enabled = true;

        lineRenderer_L.SetPosition(0, Vector3.zero);
        lineRenderer_L.SetPosition(1, Vector3.zero);

        lineRenderer_R.SetPosition(0, Vector3.zero);
        lineRenderer_R.SetPosition(1, Vector3.zero);


        Vector3 targetPosition_L = new Vector3(-.3f, 0, 0);
        Vector3 targetPosition_R = new Vector3(-.3f, 0, 0);


        float duration = 0.5f;

        DOTween.To(
            () => lineRenderer_L.GetPosition(1),
            x => lineRenderer_L.SetPosition(1, x),
            targetPosition_L,
            duration
        );

        DOTween.To(
            () => lineRenderer_R.GetPosition(1),
            x => lineRenderer_R.SetPosition(1, x),
            targetPosition_R,
            duration
        );
    }

    private void OnDisable()
    {
        lineRenderer_L.enabled = false;
        lineRenderer_R.enabled = false;
    }

    private void RotateFlight()
    {
        float rotationAmount = horizontal * rotationSpeed * Time.deltaTime;

        transform.Rotate(0, rotationAmount, 0);

        float leanAngle = horizontal * this.leanAngle;

        Quaternion leanRotation = Quaternion.Euler(0, 0, -leanAngle);

        characterMesh.localRotation = Quaternion.Slerp(
            characterMesh.localRotation,
            leanRotation,
            rollSpeed * Time.deltaTime
        );
    }

    private void GetMovementInputs()
    {
        horizontal = dynamicJoystick.Horizontal;
    }
}