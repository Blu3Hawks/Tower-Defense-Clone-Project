using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour, IDataPersistence
{
    [SerializeField] private CinemachineVirtualCamera VRCamera;
    [SerializeField] private bool useEdgeScroll = false;
    private bool dragMouseActive = false;

    Vector2 lastMousePos;

    float targetFOV = 50f;
    [SerializeField] float FOVMin = 10f;
    [SerializeField] float FOVMax = 100f;

    void Update()
    {
        HandleCameraMovement();
        HandleCameraRotation();
        HandleEdgeMouseMovement();
        HandleMouseDragMovement();
        HandleMouseZoom();
    }

    private void HandleCameraMovement()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        float moveSpeed = 50f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraRotation()
    {
        float rotateDir = 0f;

        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;

        float rotateSpeed = 100f;

        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }

    private void HandleEdgeMouseMovement()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (useEdgeScroll)
        {
            float edgeSize = 20f;
            if (Input.mousePosition.x < edgeSize) inputDir.x = -1f;
            if (Input.mousePosition.y < edgeSize) inputDir.z = +1f;
            if (Input.mousePosition.x > Screen.width - edgeSize) inputDir.x = +1f;
            if (Input.mousePosition.y > Screen.height - edgeSize) inputDir.y = +1f;
        }
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        float moveSpeed = 50f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleMouseDragMovement()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(1))
        {
            dragMouseActive = true;
            lastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            dragMouseActive = false;
        }
        if (dragMouseActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePos;

            inputDir.x = -mouseMovementDelta.x;
            inputDir.z = -mouseMovementDelta.y;

            lastMousePos = Input.mousePosition;
        }
        
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        float moveSpeed = 50f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleMouseZoom()
    {
        if(Input.mouseScrollDelta.y > 0 )
        {
            targetFOV -= 3f;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFOV += 3f;
        }
        float zoomSpeed = 6f;
        targetFOV = Mathf.Clamp(targetFOV,FOVMin,FOVMax);
        VRCamera.m_Lens.FieldOfView = Mathf.Lerp(VRCamera.m_Lens.FieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.cameraPos;
        this.targetFOV = data.cameraZoom;
    }

    public void SaveData(GameData data) 
    { 
        data.cameraPos = this.transform.position;
        data.cameraZoom = this.targetFOV;
    }

}
