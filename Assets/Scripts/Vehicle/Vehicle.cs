using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using Const;

public abstract class Vehicle : MovingObject
{
    public int Wage { get; protected set; }
    public int Capacity { get; protected set; }
    protected LinkedList<Person> people = new LinkedList<Person>();
    public INode NextNode => this.path.NextNode;
    public void AddPerson(Person person)
    {
        // 人数がCapacityを超えるときはあれこれする
        this.people.AddLast(person);
    }

    // メソッド名よくないかも
    public void RemovePerson(StationNode node)
    {
        for (var p = people.First; p != null;)
        {
            var next = p.Next;
            if (p.Value.DecideToGetOff(node))
            {
                people.Remove(p);
                p.Value.GetOff(node);
            }
            p = next;
        }
    }
}
