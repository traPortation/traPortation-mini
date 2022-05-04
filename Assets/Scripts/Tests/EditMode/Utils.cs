using System.Collections.Generic;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;
using System.Linq;

#nullable enable

namespace Tests
{
    public class LinkedListTest
    {
        [Test]
        public void DeletableForEachTest()
        {
            var list = new LinkedList<int>(Enumerable.Range(0, 10));

            Utils.LinkedList.DeletableForEach(list, (v, action) =>
            {
                if (v % 2 != 0) action();
            });

            CollectionAssert.AreEqual(list, Enumerable.Range(0, 5).Select(v => v * 2));
        }
    }
}