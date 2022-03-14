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
                new Card(ESuit.CLUBS, ERank.JACK),
                new Card(ESuit.CLUBS, ERank.AS),
                new Card(ESuit.CLUBS, ERank.TEN),
                new Card(ESuit.CLUBS, ERank.KING),
                new Card(ESuit.CLUBS, ERank.QUEEN),
            };
            Assert.AreEqual(EHandRanks.RoyalStraightFlush, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.HEARTS, ERank.JACK),
                new Card(ESuit.CLUBS, ERank.AS),
                new Card(ESuit.CLUBS, ERank.TEN),
                new Card(ESuit.CLUBS, ERank.KING),
                new Card(ESuit.CLUBS, ERank.QUEEN),
            };
            Assert.AreNotEqual(EHandRanks.RoyalStraightFlush, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void StraightFlush()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.HEARTS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.AS),
                new Card(ESuit.HEARTS, ERank.FIVE),
                new Card(ESuit.HEARTS, ERank.FOUR),
                new Card(ESuit.HEARTS, ERank.THREE),
            };
            Assert.AreEqual(EHandRanks.StraightFlush, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.CLUBS, ERank.JACK),
                new Card(ESuit.CLUBS, ERank.AS),
                new Card(ESuit.CLUBS, ERank.TEN),
                new Card(ESuit.CLUBS, ERank.KING),
                new Card(ESuit.CLUBS, ERank.QUEEN),
            };
            Assert.AreNotEqual(EHandRanks.StraightFlush, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void FourOfKind()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.SPADES, ERank.FIVE),
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.CLUBS, ERank.SIX),
                new Card(ESuit.HEARTS, ERank.SIX),
            };
            Assert.AreEqual(EHandRanks.FourOfKind, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.CLUBS, ERank.SIX),
                new Card(ESuit.HEARTS, ERank.FIVE),
                new Card(ESuit.SPADES, ERank.FIVE),
            };
            Assert.AreNotEqual(EHandRanks.FourOfKind, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void FullHouse()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SEVEN),
                new Card(ESuit.SPADES, ERank.SIX),
                new Card(ESuit.CLUBS, ERank.SIX),
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.SPADES, ERank.SEVEN),
            };
            Assert.AreEqual(EHandRanks.FullHouse, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.SPADES, ERank.SIX),
                new Card(ESuit.CLUBS, ERank.SIX),
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.SPADES, ERank.SEVEN),
            };
            Assert.AreNotEqual(EHandRanks.FullHouse, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void Flush()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SEVEN),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.SEVEN),
            };
            Assert.AreEqual(EHandRanks.Flush, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.FIVE),
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.SEVEN),
            };
            Assert.AreNotEqual(EHandRanks.Flush, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void Straight()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.FIVE),
            };
            Assert.AreEqual(EHandRanks.Straight, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.FIVE),
            };
            Assert.AreNotEqual(EHandRanks.Straight, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void ThreeOfKind()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.SIX),
            };
            Assert.AreEqual(EHandRanks.ThreeOfKind, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.HEARTS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.DIAMONDS, ERank.THREE),
            };
            Assert.AreNotEqual(EHandRanks.ThreeOfKind, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void TwoPair()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.DIAMONDS, ERank.SIX),
            };
            Assert.AreEqual(EHandRanks.TwoPair, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.SIX),
            };
            Assert.AreNotEqual(EHandRanks.TwoPair, _badActualHand.CalculateHandRank());
        }
        
        [Test]
        public void OnePair()
        {

            _goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.SIX),
            };
            Assert.AreEqual(EHandRanks.OnePair, _goodActualHand.CalculateHandRank());
            
            _badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.DIAMONDS, ERank.SIX),
            };
            Assert.AreNotEqual(EHandRanks.OnePair, _badActualHand.CalculateHandRank());
        }
        
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
