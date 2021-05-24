using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    private Queue<Person> queue = new Queue<Person>();
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Person")) //タグで衝突した物体を判定（要設定：Personのプレハブのタグとコライダー）
        {
            Person passenger = collision.gameObject.GetComponent<Person>();
            queue.Enqueue(passenger);
            Debug.Log(queue.Count);
        }
        /*
        if (collision.gameObject.CompareTag("Transport"))
        {
            MovingObject transport = collision.gameObject.GetComponent<MovingObject>();
            int TRANSPORT_EMPTY_SEATS = 2; //乗り物クラスの仕様に従って実装
            for (int i = 0; i < TRANSPORT_EMPTY_SEATS; i++)
            {
                Person passenger = queue.Dequeue();
                //transport.passengers.Add(passenger); //乗り物クラスの仕様に従って実装
            }
        }
        */
    }
}
