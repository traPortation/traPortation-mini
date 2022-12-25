using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TraPortation
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 10f)]
        private float wheelSpeed = 1f;

        [SerializeField, Range(0.005f, 0.5f)]
        private float moveSpeed = 0.01f;

        [SerializeField]
        private Camera mainCamera;
        private Vector3 preMousePos;

        private float Left => transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
        private float Right => transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        private float Top => transform.position.y + mainCamera.orthographicSize;
        private float Bottom => transform.position.y - mainCamera.orthographicSize;

        private float maxSize;
        private float minSize = 1.0f;

        // Start is called before the first frame update
        void Start()
        {
            this.maxSize = Mathf.Min(Const.Map.Height, Const.Map.Width / this.mainCamera.aspect) / 2;

            SetCenter();
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
            mainCamera.orthographicSize = Mathf.Max(Mathf.Min(s, maxSize), minSize);
            // transform.position += transform.forward * delta * wheelSpeed;

            FitCamera();

            return;
        }

        private void MouseDrag(Vector3 mousePos)
        {
            Vector3 diff = mousePos - preMousePos;

            if (diff.magnitude < Vector3.kEpsilon)
                return;

            if (Input.GetMouseButton(1))
                transform.Translate(-diff * moveSpeed * mainCamera.orthographicSize);

            preMousePos = mousePos;

            FitCamera();
        }

        private void FitCamera()
        {
            if (Left < Const.Map.XMin)
            {
                transform.position += new Vector3(Const.Map.XMin - Left, 0, 0);
            }
            if (Right > Const.Map.XMax)
            {
                transform.position += new Vector3(Const.Map.XMax - Right, 0, 0);
            }
            if (Top > Const.Map.YMax)
            {
                transform.position += new Vector3(0, Const.Map.YMax - Top, 0);
            }
            if (Bottom < Const.Map.YMin)
            {
                transform.position += new Vector3(0, Const.Map.YMin - Bottom, 0);
            }
        }

        private void SetCenter()
        {
            transform.position = new Vector3(Const.Map.Center.x, Const.Map.Center.y, Const.Z.Camera);
            this.mainCamera.orthographicSize = maxSize;
        }

        private static bool InGameMap(Vector3 vec)
        {
            return vec.x >= Const.Map.XMin && vec.x <= Const.Map.XMax &&
                   vec.y >= Const.Map.YMin && vec.y <= Const.Map.YMax;
        }
    }
}