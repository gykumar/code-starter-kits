using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowlingGame;
using System.Collections;

namespace BowlingTest
{
        
    [TestFixture]
    public class BowlingTest 
    {
        BowlingGame.BowlingGame game;

        public BowlingTest()
        {
        }

        [SetUp]
        public void SetUp()
        {
            game = new BowlingGame.BowlingGame();
        }

       
        [Test]
        public void GutterBalls()
        {
            ArrayList lstFrames=new ArrayList();
            for (int i = 0; i < 20;i++)
            {
                game.AddRoll(0, lstFrames);
            }
            Assert.AreEqual(0, game.GetScore(lstFrames));
        }

        [Test]
        public void Threes()
        {
            ArrayList lstFrames = new ArrayList();
            for (int i = 0; i < 20; i++)
            {
                game.AddRoll(3, lstFrames);
            }
            Assert.AreEqual(60, game.GetScore(lstFrames));            
        }

        [Test]
        public void AllStrikeORPerfect()
        {
            ArrayList lstFrames = new ArrayList();
            for (int i = 0; i < 22; i++)
            {
                game.AddRoll(10, lstFrames);
            }
            Assert.AreEqual(300, game.GetScore(lstFrames));
        }

        [Test]
        public void Spare()
        {
            ArrayList lstFrames = new ArrayList();
            
                game.AddRoll(4, lstFrames);
                game.AddRoll(3, lstFrames);
                game.AddRoll(4, lstFrames);
                game.AddRoll(6, lstFrames);
                game.AddRoll(7, lstFrames);
                game.AddRoll(2, lstFrames);
                game.AddRoll(3, lstFrames);
                game.AddRoll(5, lstFrames);
                game.AddRoll(3, lstFrames);
                game.AddRoll(2, lstFrames);
            
            Assert.AreEqual(46, game.GetScore(lstFrames));
        }

        [Test]
        public void SpareWithStrike()
        {
            ArrayList lstFrames = new ArrayList();

            game.AddRoll(4, lstFrames);
            game.AddRoll(3, lstFrames);
            game.AddRoll(4, lstFrames);
            game.AddRoll(6, lstFrames);
            game.AddRoll(7, lstFrames);
            game.AddRoll(2, lstFrames);
            game.AddRoll(3, lstFrames);
            game.AddRoll(5, lstFrames);
            game.AddRoll(3, lstFrames);
            game.AddRoll(2, lstFrames);

            game.AddRoll(10, lstFrames);
            game.AddRoll(6, lstFrames);
            game.AddRoll(3, lstFrames);
            game.AddRoll(4, lstFrames);
            game.AddRoll(5, lstFrames);
            game.AddRoll(3, lstFrames);
            game.AddRoll(2, lstFrames);
            game.AddRoll(3, lstFrames);
            game.AddRoll(7, lstFrames);
            game.AddRoll(10, lstFrames);

            Assert.AreEqual(108, game.GetScore(lstFrames));
        }    


    }

}

