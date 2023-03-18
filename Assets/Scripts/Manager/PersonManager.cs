using System.Collections.Generic;
using MessagePipe;
using TraPortation.Event.Train;
using UnityEngine;
using Zenject;

namespace TraPortation
{
    public class PersonManager : MonoBehaviour
    {
        DiContainer container;
        List<Person> people = new List<Person>();
        public int PeopleCount => this.people.Count;
        [SerializeField] GameObject peopleFolder;
        [SerializeField] GameObject person;
        int count = 0;

        [Inject]
        public void Construct(DiContainer container, ISubscriber<GetOnTrainEvent> subscriber)
        {
            this.container = container;

            subscriber.Subscribe(_ =>
            {
                count++;
                // 10回ごとに人を追加
                if (count % 10 == 0)
                {
                    this.AddPeopleOnRandomPoint();
                }
            });
        }

        public void AddPeopleOnRandomPoint()
        {
            var x = Random.Range(Const.X.Min, Const.X.Max);
            var y = Random.Range(Const.Y.Min, Const.Y.Max);

            var start = new Vector3(x, y, Const.Z.Person);
            var obj = this.container.InstantiatePrefab(this.person);
            obj.transform.position = start;
            obj.transform.parent = this.peopleFolder.transform;

            var p = obj.GetComponent<Person>();
            this.people.Add(p);
        }
    }
}