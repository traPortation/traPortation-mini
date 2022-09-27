using TraPortation.Game;
using TraPortation.UI;
using UnityEngine;
using Zenject;

namespace TraPortation
{
    public class SetTrainManager : MonoBehaviour
    {
        GameManager gameManager;
        Vector3 lastMousePosition;
        [SerializeField] GameObject trainIcon;
        [SerializeField] GameObject trainPrefab;

        [Inject]
        public void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        void Update()
        {
            if (this.gameManager.Status != GameStatus.SetTrain)
            {
                return;
            }

            trainIcon.SetActive(true);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            trainIcon.transform.position = new Vector3(mousePosition.x, mousePosition.y, 8f);

            Color trainColor = trainIcon.GetComponent<SpriteRenderer>().color;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, Mathf.Infinity);

            if (hitInfo.collider != null && hitInfo.collider.gameObject.name == "RailView")
            {
                trainColor.a = 1f;
                trainIcon.GetComponent<SpriteRenderer>().material.color = trainColor;

                if (Mathf.Pow(mousePosition.x - lastMousePosition.x, 2) + Mathf.Pow(mousePosition.y - lastMousePosition.y, 2) > 0.1f)
                {
                    var euler = Mathf.Atan2(mousePosition.y - lastMousePosition.y, mousePosition.x - lastMousePosition.x) * Mathf.Rad2Deg;
                    var railEuler = hitInfo.collider.gameObject.transform.rotation.eulerAngles.z;

                    if (Mathf.Abs(euler - railEuler) < 180)
                    {
                        trainIcon.transform.rotation = Quaternion.Euler(0, 0, railEuler);
                    }
                    else
                    {
                        trainIcon.transform.rotation = Quaternion.Euler(0, 0, railEuler + 180);
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    var trainObj = Instantiate(trainPrefab);
                    var train = trainObj.GetComponent<Train>();
                    var rail = hitInfo.collider.gameObject.GetComponent<RailLine>().Rail;
                    rail.AddTrain(train);
                    train.GoTo(mousePosition);
                }
            }
            else
            {
                trainColor.a = 0.5f;
                trainIcon.GetComponent<SpriteRenderer>().material.color = trainColor;
            }

            if (Mathf.Pow(mousePosition.x - lastMousePosition.x, 2) + Mathf.Pow(mousePosition.y - lastMousePosition.y, 2) > 0.1f)
            {
                this.lastMousePosition = mousePosition;
            }
        }
    }
}