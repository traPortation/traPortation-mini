using TraPortation.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation
{
    public class SetBusManager : MonoBehaviour
    {
        GameManager manager;
        InputManager inputManager;
        [SerializeField] GameObject busIcon;
        [SerializeField] GameObject busPrefab;
        MouseIcon icon;
        int nextBusId = 0;

        [Inject]
        public void Construct(GameManager manager, InputManager inputManager)
        {
            this.manager = manager;
            this.inputManager = inputManager;
        }

        void Start()
        {
            this.icon = new MouseIcon(busIcon, this.inputManager);
            this.icon.SetActive(false);
        }

        void Update()
        {
            if (this.manager.Status != GameStatus.SetBus)
            {
                this.icon.SetActive(false);
                return;
            }


            this.icon.SetActive(true);
            this.icon.Update();

            var obj = this.inputManager.RayCast();

            if (obj != null
                && obj.name == "BusRailView"
                && this.manager.ManageMoney.ExpenseCheck(Const.Money.BusCost))
            {
                this.icon.SetAlpha(1.0f);

                // TODO: 向きの処理

                if (Input.GetMouseButtonDown(0) && this.manager.ManageMoney.Expense(Const.Money.BusCost))
                {
                    var pos = this.inputManager.GetMousePosition();
                    var busObj = Instantiate(busPrefab, new Vector3(pos.x, pos.y, Const.Z.Bus), Quaternion.identity);
                    var bus = busObj.GetComponent<Bus>();
                    bus.SetId(nextBusId);
                    nextBusId++;

                    var rail = obj.GetComponent<UI.BusRailLine>().Rail;
                    rail.AddBus(bus, pos);
                }
            }
            else
            {
                this.icon.SetAlpha(0.5f);
            }
        }

        void AddBus(Vector3 position)
        {
        }
    }
}