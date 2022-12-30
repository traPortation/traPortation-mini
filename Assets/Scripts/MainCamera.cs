using System.Collections;
using System.Collections.Generic;
using TraPortation.Game;
using UnityEngine;
using Zenject;


namespace TraPortation
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 10f)]
        private float wheelSpeed = 3f;

        [SerializeField, Range(0.005f, 0.5f)]
        private float moveSpeed = 0.01f;

        [SerializeField]
        private Camera mainCamera;
        private Vector3 preMousePos;

        private GameManager manager;

        private float Left => this.currentPosition.x - mainCamera.orthographicSize * mainCamera.aspect;
        private float Right => this.currentPosition.x + mainCamera.orthographicSize * mainCamera.aspect;
        private float Top => this.currentPosition.y + mainCamera.orthographicSize;
        private float Bottom => this.currentPosition.y - mainCamera.orthographicSize;

        private float maxSize;
        private float minSize = 1.0f;
        private float currentSize;
        private Vector3 currentPosition;

        // Start is called before the first frame update
        void Start()
        {
            this.maxSize = Mathf.Min(Const.Map.Height, Const.Map.Width / this.mainCamera.aspect) / 2;

            SetCenter();
            this.currentSize = this.mainCamera.orthographicSize;
            this.currentPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            MouseUpdate();

            CameraUpdate();

            return;
        }

        [Inject]
        void Construct(GameManager manager)
        {
            this.manager = manager;
        }

        private void CameraUpdate()
        {
            this.mainCamera.orthographicSize = Mathf.Lerp(this.mainCamera.orthographicSize, this.currentSize, 0.1f);
            this.transform.position = Vector3.Lerp(this.transform.position, this.currentPosition, 0.3f);
        }


        private void MouseUpdate()
        {
            if (this.manager.Status == GameStatus.SubMenu) return;

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
            float s = this.currentSize;
            s = s + (-1f) * delta * wheelSpeed;
            this.currentSize = Mathf.Max(Mathf.Min(s, maxSize), minSize);
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
                this.currentPosition += -diff * moveSpeed / 10 * mainCamera.orthographicSize;

            preMousePos = mousePos;

            FitCamera();
        }

        private void FitCamera()
        {
            if (Left < Const.Map.XMin)
            {
                this.currentPosition += new Vector3(Const.Map.XMin - Left, 0, 0);
            }
            if (Right > Const.Map.XMax)
            {
                this.currentPosition += new Vector3(Const.Map.XMax - Right, 0, 0);
            }
            if (Top > Const.Map.YMax)
            {
                this.currentPosition += new Vector3(0, Const.Map.YMax - Top, 0);
            }
            if (Bottom < Const.Map.YMin)
            {
                this.currentPosition += new Vector3(0, Const.Map.YMin - Bottom, 0);
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