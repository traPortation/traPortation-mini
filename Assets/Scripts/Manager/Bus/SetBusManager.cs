using TraPortation.Const;
using TraPortation.Game;
using UnityEngine;
using Zenject;

namespace TraPortation
{
    public class SetBusManager : MonoBehaviour
    {
        GameManager manager;
        [SerializeField] GameObject busIcon;
        [SerializeField] GameObject busPrefab;
        int nextBusId = 0;

        [Inject]
        public void Construct(GameManager manager)
        {
            this.manager = manager;
        }
        void Update()
        {
            if (this.manager.Status != GameStatus.SetBus)
            {
                if (busIcon.activeSelf)
                {
                    busIcon.SetActive(false);
                }
                return;
            }

            if (!busIcon.activeSelf)
                busIcon.SetActive(true);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Z.MouseIcon;
            busIcon.transform.position = mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, Mathf.Infinity);

            var color = busIcon.GetComponent<SpriteRenderer>().color;

            if (hitInfo.collider != null && hitInfo.collider.gameObject.name == "BusRailView")
            {
                color.a = 1f;
                busIcon.GetComponent<SpriteRenderer>().material.color = color;

                // TODO: 向きの処理

                if (Input.GetMouseButtonDown(0))
                {
                    var busObj = Instantiate(busPrefab, mousePosition, Quaternion.identity);
                    var bus = busObj.GetComponent<Bus>();
                    bus.SetId(nextBusId);
                    nextBusId++;

                    var rail = hitInfo.collider.gameObject.GetComponent<UI.BusRailLine>().Rail;
                    rail.AddBus(bus, mousePosition);
                }
            }
            else
            {
                color.a = 0.5f;
                busIcon.GetComponent<SpriteRenderer>().material.color = color;
            }
        }

        void AddBus(Vector3 position)
        {
        }
    }
}