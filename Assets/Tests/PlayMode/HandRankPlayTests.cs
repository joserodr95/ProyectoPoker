using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using static HandsCalculator;

namespace Tests.PlayMode
{
    public class HandRankPlayTests
    {
        private readonly CardsGroup goodActualHand = new CardsGroup();
        private readonly CardsGroup badActualHand = new CardsGroup();
        
        [Test]
        public void RoyalStraightFlush()
        {
            goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.CLUBS, ERank.JACK),
                new Card(ESuit.CLUBS, ERank.AS),
                new Card(ESuit.CLUBS, ERank.TEN),
                new Card(ESuit.CLUBS, ERank.KING),
                new Card(ESuit.CLUBS, ERank.QUEEN),
            };
            Assert.AreEqual(EHandRanks.RoyalStraightFlush, CalculateHandRank(goodActualHand));
            
            badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.HEARTS, ERank.JACK),
                new Card(ESuit.CLUBS, ERank.AS),
                new Card(ESuit.CLUBS, ERank.TEN),
                new Card(ESuit.CLUBS, ERank.KING),
                new Card(ESuit.CLUBS, ERank.QUEEN),
            };
            Assert.AreNotEqual(EHandRanks.RoyalStraightFlush, CalculateHandRank(badActualHand));
        }
        
        [Test]
        public void StraightFlush()
        {

            goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.HEARTS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.AS),
                new Card(ESuit.HEARTS, ERank.FIVE),
                new Card(ESuit.HEARTS, ERank.FOUR),
                new Card(ESuit.HEARTS, ERank.THREE),
            };
            Assert.AreEqual(EHandRanks.StraightFlush, CalculateHandRank(goodActualHand));
            
            badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.CLUBS, ERank.JACK),
                new Card(ESuit.CLUBS, ERank.AS),
                new Card(ESuit.CLUBS, ERank.TEN),
                new Card(ESuit.CLUBS, ERank.KING),
                new Card(ESuit.CLUBS, ERank.QUEEN),
            };
            Assert.AreNotEqual(EHandRanks.StraightFlush, CalculateHandRank(badActualHand));
        }
        
        [Test]
        public void FourOfKind()
        {

            goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.SPADES, ERank.FIVE),
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.CLUBS, ERank.SIX),
                new Card(ESuit.HEARTS, ERank.SIX),
            };
            Assert.AreEqual(EHandRanks.FourOfKind, CalculateHandRank(goodActualHand));
            
            badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.CLUBS, ERank.SIX),
                new Card(ESuit.HEARTS, ERank.FIVE),
                new Card(ESuit.SPADES, ERank.FIVE),
            };
            Assert.AreNotEqual(EHandRanks.FourOfKind, CalculateHandRank(badActualHand));
        }
        
        [Test]
        public void FullHouse()
        {

            goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SEVEN),
                new Card(ESuit.SPADES, ERank.SIX),
                new Card(ESuit.CLUBS, ERank.SIX),
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.SPADES, ERank.SEVEN),
            };
            Assert.AreEqual(EHandRanks.FullHouse, CalculateHandRank(goodActualHand));
            
            badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.SPADES, ERank.SIX),
                new Card(ESuit.CLUBS, ERank.SIX),
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.SPADES, ERank.SEVEN),
            };
            Assert.AreNotEqual(EHandRanks.FullHouse, CalculateHandRank(badActualHand));
        }
        
        [Test]
        public void Flush()
        {

            goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SEVEN),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.SEVEN),
            };
            Assert.AreEqual(EHandRanks.Flush, CalculateHandRank(goodActualHand));
            
            badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.FIVE),
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.SEVEN),
            };
            Assert.AreNotEqual(EHandRanks.Flush, CalculateHandRank(badActualHand));
        }
        
        [Test]
        public void Straight()
        {

            goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.FIVE),
            };
            Assert.AreEqual(EHandRanks.Straight, CalculateHandRank(goodActualHand));
            
            badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.FIVE),
            };
            Assert.AreNotEqual(EHandRanks.Straight, CalculateHandRank(badActualHand));
        }
        
        [Test]
        public void ThreeOfKind()
        {

            goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.SIX),
            };
            Assert.AreEqual(EHandRanks.ThreeOfKind, CalculateHandRank(goodActualHand));
            
            badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.HEARTS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.DIAMONDS, ERank.THREE),
            };
            Assert.AreNotEqual(EHandRanks.ThreeOfKind, CalculateHandRank(badActualHand));
        }
        
        [Test]
        public void TwoPair()
        {

            goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.DIAMONDS, ERank.SIX),
            };
            Assert.AreEqual(EHandRanks.TwoPair, CalculateHandRank(goodActualHand));
            
            badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.SIX),
            };
            Assert.AreNotEqual(EHandRanks.TwoPair, CalculateHandRank(badActualHand));
        }
        
        [Test]
        public void OnePair()
        {

            goodActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.THREE),
                new Card(ESuit.DIAMONDS, ERank.SIX),
            };
            Assert.AreEqual(EHandRanks.OnePair, CalculateHandRank(goodActualHand));
            
            badActualHand.cards = new List<Card>()
            {
                new Card(ESuit.DIAMONDS, ERank.SIX),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.HEARTS, ERank.FOUR),
                new Card(ESuit.DIAMONDS, ERank.TWO),
                new Card(ESuit.DIAMONDS, ERank.SIX),
            };
            Assert.AreNotEqual(EHandRanks.OnePair, CalculateHandRank(badActualHand));
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
