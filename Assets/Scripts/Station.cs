using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

public class Station : MonoBehaviour
{
    /*
        LinkedList: 連結リスト
        要素の追加、削除がO(1)
    */

    private LinkedList<Person> people = new LinkedList<Person>();
    public int ID;
    public StationNode Node { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// 駅に人を追加する
    /// </summary>
    /// <param name="person"></param>
    public void AddPerson(Person person)
    {
        this.people.AddLast(person);
    }

    /// <summary>
    /// 駅から乗り物に人を追加する
    /// </summary>
    /// <param name="vehicle"></param>
    public void AddPersonToTrain(Vehicle vehicle)
    {
        for (var p = this.people.First; p != null;)
        {
            // TODO: 乗り物が満員のときは乗れない
            var next = p.Next;
            var person = p.Value;

            if (person.DecideToRide(vehicle))
            {
                // 乗り物に人を乗せる
                person.Ride(vehicle);

                // 駅から人を取り除く
                this.people.Remove(p);
            }
            p = next;
        }
    }
    /// <summary>
    /// StationNodeを割り当てる
    /// Instantiate時に一度だけ呼ぶ
    /// </summary>
    /// <param name="node"></param>
    public void SetNode(StationNode node)
    {
        if (this.Node == null)
        {
            this.Node = node;
        }
        else
        {
            // 例外投げるのはあんまよくないかも
            throw new System.Exception("Stationのノードへの再代入");
        }
    }
}
