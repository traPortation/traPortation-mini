using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)]
    private float wheelSpeed = 1f;

    [SerializeField, Range(0.1f, 10f)]
    private float moveSpeed = 0.3f;

    [SerializeField]
    private Camera mainCamera;
    private Vector3 preMousePos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MouseUpdate();
        return;
    }


    private void MouseUpdate()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0.0f)
            MouseWheel(scrollWheel);

        if (Input.GetMouseButtonDown(0) ||
           Input.GetMouseButtonDown(1) ||
           Input.GetMouseButtonDown(2))
            preMousePos = Input.mousePosition;

        MouseDrag(Input.mousePosition);
    }

    private void MouseWheel(float delta)
    {
        float s = mainCamera.orthographicSize;
        s = s + (-1f) * delta * wheelSpeed;
        mainCamera.orthographicSize = s;
        // transform.position += transform.forward * delta * wheelSpeed;
        return;
    }

    private void MouseDrag(Vector3 mousePos)
    {
        Vector3 diff = mousePos - preMousePos;

        if (diff.magnitude < Vector3.kEpsilon)
            return;

        if (Input.GetMouseButton(1))
            transform.Translate(-diff * Time.deltaTime * moveSpeed);

        preMousePos = mousePos;
    }

}
