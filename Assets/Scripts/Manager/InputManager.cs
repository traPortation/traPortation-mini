using UnityEngine;
using UnityEngine.EventSystems;

namespace TraPortation
{
    public class InputManager : MonoBehaviour
    {
        Camera mainCamera;
        EventSystem eventSystem;


        void Start()
        {
            this.mainCamera = Camera.main;
            this.eventSystem = EventSystem.current;
        }

        public Vector3 GetMousePosition()
        {
            return this.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        public GameObject RayCast(int mask = -1)
        {
            if (this.eventSystem.IsPointerOverGameObject())
                return null;

            Ray ray = this.mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitInfo;
            if (mask == -1)
                hitInfo = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            else
                hitInfo = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, mask);

            if (hitInfo.collider != null)
                return hitInfo.collider.gameObject;
            else
                return null;
        }
    }
}