using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    private Queue<Person> queue = new Queue<Person>();
    public int stationId;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Person")) //タグで衝突した物体を判定（要設定：Personのプレハブのタグとコライダー）
        {   
            Person passenger = collision.gameObject.GetComponent<Person>();
            if (passenger.moving.GetOnId == this.stationId && passenger.moving.HowToMove == Const.Move.Walking) { // 人が持つ目的地駅IDと駅IDが等しい時、駅に入れる
                queue.Enqueue(passenger);
                Debug.Log(queue.Count);
            }
        }
        // 乗り物への人の乗り降りに必要なpassengers配列をVehicleクラスに実装が必要
        /*
        if (collision.gameObject.CompareTag("Transport")) // 乗り物が駅に着いた時
        {
            Vehicle transport = collision.gameObject.GetComponent<Vehicle>();
            for (int i = 0; i < transport.passengers.Count; i++) { // 人が乗り物から降りる操作
                if (transport.passengers[i].moving.GetOffId == this.stationId) { // 目的地駅IDと駅IDが等しいときに人を降ろす
                    queue.Enqueue(transport.passengers[i]);
                    transport.passengers.Remove(i);
                    i--;
                }
            }
            while (transport.capacity >= transport.passengers.Count && queue.Count > 0) { // passengers配列の要素数が定員を超えない間、人を乗せる
                Person passenger = queue.Dequeue();
                transport.passengers.Add(passenger);
            }
        }
        */
    }
}
