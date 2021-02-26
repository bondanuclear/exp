using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestSuiteSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        private Rocket game;
        [UnityTest]
        public IEnumerator TestSuiteCheckCollision()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            GameObject gameGameObject =
       MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Cube"));
            game = gameGameObject.GetComponent<Rocket>();
            yield return new WaitForSeconds(0.1f);
            
            Object.Destroy(game.gameObject);
        }
    }
}
