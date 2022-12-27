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
        InputManager inputManager;
        Vector3 lastMousePosition;
        bool direction = true;
        [SerializeField] GameObject trainIcon;
        [SerializeField] GameObject trainPrefab;
        MouseIcon icon;
        int nextTrainId = 0;

        [Inject]
        public void Construct(GameManager gameManager, InputManager inputManager)
        {
            this.gameManager = gameManager;
            this.inputManager = inputManager;
        }

        void Start()
        {
            this.icon = new MouseIcon(trainIcon, this.inputManager);
            this.icon.SetActive(false);
        }

        void Update()
        {
            if (this.gameManager.Status != GameStatus.SetTrain)
            {
                this.icon.SetActive(false);

                return;
            }

            this.icon.SetActive(true);
            this.icon.Update();


            var obj = this.inputManager.RayCast();

            if (obj != null && obj.name == "RailView"
                && this.gameManager.ManageMoney.ExpenseCheck(Const.Money.TrainCost))
            {
                this.icon.SetAlpha(1.0f);
                var mousePosition = this.inputManager.GetMousePosition();

                if (Mathf.Pow(mousePosition.x - lastMousePosition.x, 2) + Mathf.Pow(mousePosition.y - lastMousePosition.y, 2) > 0.1f)
                {
                    var euler = Mathf.Atan2(mousePosition.y - lastMousePosition.y, mousePosition.x - lastMousePosition.x) * Mathf.Rad2Deg;

                    // なんか逆になるので180度足している
                    var railEuler = obj.transform.rotation.eulerAngles.z + 180;

                    if (Mathf.Abs(euler - railEuler) < 180)
                    {
                        this.icon.obj.transform.rotation = Quaternion.Euler(0, 0, railEuler);
                        this.direction = true;
                    }
                    else
                    {
                        this.icon.obj.transform.rotation = Quaternion.Euler(0, 0, railEuler + 180);
                        this.direction = false;
                    }

                    this.lastMousePosition = mousePosition;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    // TODO: エラーハンドリング
                    var ok = this.gameManager.ManageMoney.Expense(Const.Money.TrainCost);
                    if (ok)
                    {
                        var trainObj = Instantiate(trainPrefab);
                        var train = trainObj.GetComponent<Train>();
                        train.transform.position = new Vector3(mousePosition.x, mousePosition.y, Const.Z.Train);
                        train.SetId(nextTrainId);
                        nextTrainId++;
                        var rail = obj.GetComponent<RailLine>().Rail;
                        rail.AddTrain(train, mousePosition, this.direction);
                    }
                }
            }
            else
            {
                this.icon.SetAlpha(0.5f);
            }
        }
    }
}