using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;
using System.Linq;
using UnityEngine.TestTools;
using UnityEditor;

namespace Tests
{
    public class StationManagerTest
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/StationManager.prefab");
        StationManager stationManager;

        [SetUp]
        public void SetUp()
        {
            var obj = GameObject.Instantiate(prefab);
            this.stationManager = obj.GetComponent<StationManager>();
        }

        [UnityTest]
        public IEnumerator Test() {
            var station = this.stationManager.AddStation(new Vector3(1, 1, 1));

            Assert.AreEqual(station, this.stationManager.GetStation(station.ID));

            // TODO: Boardについても書く
            yield return null;
        }
    }
}

