﻿using System.Collections;
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

    /// <summary>
    /// 満員かどうか
    /// </summary>
    bool isFull => this.Capacity <= this.people.Count;

    /// <summary>
    /// 人を追加する
    /// </summary>
    /// <param name="person"></param>
    /// <returns>成功したかどうか</returns>
    public bool AddPerson(Person person)
    {
        if (this.isFull) return false;

        this.people.AddLast(person);

        return true;
    }

    // メソッド名よくないかも
    public void RemovePerson(StationNode node)
    {
        Utils.LinkedList.DeletableForEach(this.people, (person, deleteAction) =>
        {
            if (person.DecideToGetOff(node))
            {
                deleteAction();
                person.GetOff(node);
            }
        });
    }
}
