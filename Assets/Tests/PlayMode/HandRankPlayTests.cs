using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using static HandsCalculator;

namespace Tests.PlayMode
{
    public class HandRankPlayTests
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
            Assert.AreEqual(EHandRanks.RoyalStraightFlush, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Hearts, ERank.Jack),
                new Card(ESuit.Clubs, ERank.Ace),
                new Card(ESuit.Clubs, ERank.Ten),
                new Card(ESuit.Clubs, ERank.King),
                new Card(ESuit.Clubs, ERank.Queen),
            };
            Assert.AreNotEqual(EHandRanks.RoyalStraightFlush, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void StraightFlush()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Hearts, ERank.Two),
                new Card(ESuit.Hearts, ERank.Ace),
                new Card(ESuit.Hearts, ERank.Four),
                new Card(ESuit.Hearts, ERank.Five),
                new Card(ESuit.Hearts, ERank.Three),
            };
            Assert.AreEqual(EHandRanks.StraightFlush, _goodActualHand.CalculateHandRank());
            
            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Hearts, ERank.Seven),
                new Card(ESuit.Hearts, ERank.Six),
                new Card(ESuit.Hearts, ERank.Ten),
                new Card(ESuit.Hearts, ERank.Nine),
                new Card(ESuit.Hearts, ERank.Eight),
            };
            Assert.AreEqual(EHandRanks.StraightFlush, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Clubs, ERank.Jack),
                new Card(ESuit.Clubs, ERank.Ace),
                new Card(ESuit.Clubs, ERank.Ten),
                new Card(ESuit.Clubs, ERank.King),
                new Card(ESuit.Clubs, ERank.Queen),
            };
            Assert.AreNotEqual(EHandRanks.StraightFlush, _badActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Clubs, ERank.Queen),
                new Card(ESuit.Clubs, ERank.King),
                new Card(ESuit.Clubs, ERank.Ace),
                new Card(ESuit.Clubs, ERank.Two),
                new Card(ESuit.Clubs, ERank.Three),
            };
            Assert.AreNotEqual(EHandRanks.StraightFlush, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void FourOfKind()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Hearts, ERank.Six),
                new Card(ESuit.Spades, ERank.Five),
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Clubs, ERank.Six),
                new Card(ESuit.Hearts, ERank.Six),
            };
            Assert.AreEqual(EHandRanks.FourOfKind, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Hearts, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Clubs, ERank.Six),
                new Card(ESuit.Hearts, ERank.Five),
                new Card(ESuit.Spades, ERank.Five),
            };
            Assert.AreNotEqual(EHandRanks.FourOfKind, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void FullHouse()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Diamonds, ERank.Seven),
                new Card(ESuit.Spades, ERank.Six),
                new Card(ESuit.Clubs, ERank.Six),
                new Card(ESuit.Hearts, ERank.Six),
                new Card(ESuit.Spades, ERank.Seven),
            };
            Assert.AreEqual(EHandRanks.FullHouse, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Spades, ERank.Six),
                new Card(ESuit.Clubs, ERank.Six),
                new Card(ESuit.Hearts, ERank.Six),
                new Card(ESuit.Spades, ERank.Seven),
            };
            Assert.AreNotEqual(EHandRanks.FullHouse, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void Flush()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Diamonds, ERank.Seven),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Three),
                new Card(ESuit.Diamonds, ERank.Seven),
            };
            Assert.AreEqual(EHandRanks.Flush, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Diamonds, ERank.Three),
                new Card(ESuit.Diamonds, ERank.Four),
                new Card(ESuit.Diamonds, ERank.Five),
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Seven),
            };
            Assert.AreNotEqual(EHandRanks.Flush, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void Straight()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Diamonds, ERank.Four),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Hearts, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Three),
                new Card(ESuit.Diamonds, ERank.Five),
            };
            Assert.AreEqual(EHandRanks.Straight, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Diamonds, ERank.Four),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Three),
                new Card(ESuit.Diamonds, ERank.Five),
            };
            Assert.AreNotEqual(EHandRanks.Straight, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void ThreeOfKind()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Hearts, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Three),
                new Card(ESuit.Diamonds, ERank.Six),
            };
            Assert.AreEqual(EHandRanks.ThreeOfKind, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Hearts, ERank.Three),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Diamonds, ERank.Three),
            };
            Assert.AreNotEqual(EHandRanks.ThreeOfKind, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void TwoPair()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Hearts, ERank.Four),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Diamonds, ERank.Six),
            };
            Assert.AreEqual(EHandRanks.TwoPair, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Hearts, ERank.Four),
                new Card(ESuit.Diamonds, ERank.Three),
                new Card(ESuit.Diamonds, ERank.Six),
            };
            Assert.AreNotEqual(EHandRanks.TwoPair, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void OnePair()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Diamonds, ERank.Four),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Hearts, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Three),
                new Card(ESuit.Diamonds, ERank.Six),
            };
            Assert.AreEqual(EHandRanks.OnePair, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Hearts, ERank.Four),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Diamonds, ERank.Six),
            };
            Assert.AreNotEqual(EHandRanks.OnePair, _badActualHand.CalculateHandRank());
        }
        
        // Makes it work correctly in PlayMode
        [UnityTest]
        public IEnumerator HandRankTestsWithEnumeratorPasses()
        {
            // Skips a frame
            yield return null;
        
            RoyalStraightFlush();
            StraightFlush();
            FourOfKind();
            FullHouse();
            Flush();
            ThreeOfKind();
            TwoPair();
            OnePair();
        }
    }
}
