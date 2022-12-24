using TraPortation.Game;
using TraPortation.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation
{
    public class SetTrainManager : MonoBehaviour
    {
        GameManager gameManager;
        Vector3 lastMousePosition;
        bool direction = true;
        [SerializeField] GameObject trainIcon;
        [SerializeField] GameObject trainPrefab;
        int nextTrainId = 0;

        [Inject]
        public void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        void Start()
        {
            trainIcon.transform.position = new Vector3(Const.Map.XMin - 1, Const.Map.YMin - 1, Const.Z.MouseIcon);
            trainIcon.SetActive(false);
        }

        void Update()
        {
            if (this.gameManager.Status != GameStatus.SetTrain)
            {
                if (trainIcon.activeSelf)
                {
                    trainIcon.transform.position = new Vector3(Const.Map.XMin - 1, Const.Map.YMin - 1, Const.Z.MouseIcon);
                    trainIcon.SetActive(false);
                }

                return;
            }
            if (!trainIcon.activeSelf)
            {
                trainIcon.SetActive(true);
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            trainIcon.transform.position = new Vector3(mousePosition.x, mousePosition.y, Const.Z.MouseIcon);

            Color trainColor = trainIcon.GetComponent<SpriteRenderer>().color;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, Mathf.Infinity);

            if (!EventSystem.current.IsPointerOverGameObject() && hitInfo.collider != null && hitInfo.collider.gameObject.name == "RailView" && this.gameManager.ManageMoney.ExpenseCheck(Const.Train.VehicleCost))
            {
                trainColor.a = 1f;
                trainIcon.GetComponent<SpriteRenderer>().material.color = trainColor;

                if (Mathf.Pow(mousePosition.x - lastMousePosition.x, 2) + Mathf.Pow(mousePosition.y - lastMousePosition.y, 2) > 0.1f)
                {
                    var euler = Mathf.Atan2(mousePosition.y - lastMousePosition.y, mousePosition.x - lastMousePosition.x) * Mathf.Rad2Deg;

                    // なんか逆になるので180度足している
                    var railEuler = hitInfo.collider.gameObject.transform.rotation.eulerAngles.z + 180;

                    if (Mathf.Abs(euler - railEuler) < 180)
                    {
                        trainIcon.transform.rotation = Quaternion.Euler(0, 0, railEuler);
                        this.direction = true;
                    }
                    else
                    {
                        trainIcon.transform.rotation = Quaternion.Euler(0, 0, railEuler + 180);
                        this.direction = false;
                    }

                    this.lastMousePosition = mousePosition;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    this.gameManager.ManageMoney.ExpenseMoney(Const.Train.VehicleCost);
                    var trainObj = Instantiate(trainPrefab);
                    var train = trainObj.GetComponent<Train>();
                    train.transform.position = new Vector3(mousePosition.x, mousePosition.y, Const.Z.Train);
                    train.SetId(nextTrainId);
                    nextTrainId++;
                    var rail = hitInfo.collider.gameObject.GetComponent<RailLine>().Rail;
                    rail.AddTrain(train, mousePosition, this.direction);
                }
            }
            else
            {
                trainColor.a = 0.5f;
                trainIcon.GetComponent<SpriteRenderer>().material.color = trainColor;
            }
        }
    }
}