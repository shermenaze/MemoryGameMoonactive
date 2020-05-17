using System.Collections;
using System.Collections.Generic;
using CardGame.SaveSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerPrefs_Save_System
    {
        // A Test behaves as an ordinary method
        [Test]
        public void PlayerPrefs_Save_SystemSimplePasses()
        {
            //PlayerPrefsSaveSystem.
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PlayerPrefs_Save_SystemWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
