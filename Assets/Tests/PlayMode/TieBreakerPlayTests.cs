using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using static HandsCalculator;

namespace Tests.PlayMode
{
    public class TieBreakerPlayTests
    {
        private readonly CardsGroup _goodActualHand = new CardsGroup();
        private readonly CardsGroup _badActualHand = new CardsGroup();
        
        [Test]
        public void RoyalStraightFlush()
        {
            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Clubs, ERank.Jack),
                new Card(ESuit.Clubs, ERank.Ace),
                new Card(ESuit.Clubs, ERank.Ten),
                new Card(ESuit.Clubs, ERank.King),
                new Card(ESuit.Clubs, ERank.Queen),
            };
            Assert.AreEqual(HandsCalculator.EHandRanks.RoyalStraightFlush, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Hearts, ERank.Jack),
                new Card(ESuit.Clubs, ERank.Ace),
                new Card(ESuit.Clubs, ERank.Ten),
                new Card(ESuit.Clubs, ERank.King),
                new Card(ESuit.Clubs, ERank.Queen),
            };
            Assert.AreNotEqual(HandsCalculator.EHandRanks.RoyalStraightFlush, _badActualHand.CalculateHandRank());
        }
        
        [UnityTest]
        public IEnumerator HandRankTestsWithEnumeratorPasses()
        {
            // Skips a frame
            yield return null;
        
            RoyalStraightFlush();
        }
    }
}