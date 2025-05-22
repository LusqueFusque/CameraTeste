using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Camera mainCamera;
    public float followSpeed = 10f;  // Suavização

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        FollowMouse();
    }

    void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z);  // Profundidade de foco

        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        targetPosition.z = transform.position.z;  // Mantém o mesmo Z

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
