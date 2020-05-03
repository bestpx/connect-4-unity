using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GameTestSimplePasses()
        {
            var game = new Game(7, 6);
            var player1 = new Player(1);
            var player2 = new Player(2);
            Assert.AreEqual(game.CheckState(), 0);
            Assert.IsTrue(game.CanPlayAtColumn(0), "should be able to play at column 0");
            Assert.IsTrue(game.CanPlayAtColumn(1), "should be able to play at column 1");
            Assert.IsTrue(game.CanPlayAtColumn(2), "should be able to play");
            Assert.IsTrue(game.CanPlayAtColumn(3), "should be able to play");
            Assert.IsTrue(game.CanPlayAtColumn(4), "should be able to play");
            Assert.IsTrue(game.CanPlayAtColumn(5), "should be able to play");
            Assert.IsTrue(game.CanPlayAtColumn(6), "should be able to play");
            game.PlayAt(player1.Id, 0);
            game.PlayAt(player1.Id, 1);
            game.PlayAt(player1.Id, 2);
            game.PlayAt(player1.Id, 3);
            Assert.AreEqual(game.CheckState(), player1.Id);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator GameTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
