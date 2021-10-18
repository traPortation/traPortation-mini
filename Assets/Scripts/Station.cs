using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    /*
        LinkedList: 連結リスト
        要素の追加、削除がO(1)
    */
    private LinkedList<IPerson> people = new LinkedList<IPerson>();
    public int ID;
    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// 駅に人を追加する
    /// </summary>
    /// <param name="person"></param>
    public void AddPerson(IPerson person)
    {
        people.AddLast(person);
    }

    /// <summary>
    /// 駅から乗り物に人を追加する
    /// </summary>
    /// <param name="vehicle"></param>
    public void AddPersonToTrain(Vehicle vehicle)
    {
        for (var p = people.First; p != null;)
        {
            // TODO: 乗り物が満員のときは乗れない
            var next = p.Next;
            if (p.Value.DecideToRide(vehicle))
            {
                p.Value.Ride(vehicle);
                people.Remove(p);
            }
            p = next;
        }
    }
}
