using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

namespace Utils
{
    public static class LinkedList
    {
        /// <summary>
        /// 要素削除に対応したForEach
        /// </summary>
        public static void DeletableForEach<T>(LinkedList<T> list, Action<T, Action> action)
        {
            for (var p = list.First; p != null;)
            {
                var next = p.Next;
                action(p.Value, () => list.Remove(p));
                p = next;
            }
        }
    }
}