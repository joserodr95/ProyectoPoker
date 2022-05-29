using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using static HandsCalculator;

namespace Tests.PlayMode
{
    public class TieBreakerPlayTests
    {
        private static Dictionary<int, RankCardsTuple> dictHands;
        
        private static readonly CardsGroup WinnerHand = new CardsGroup();
        private static readonly CardsGroup LoserHand = new CardsGroup();
        private static readonly CardsGroup LoserHand2 = new CardsGroup();
        
        private static void BuildHandDict()
        {
            dictHands = new Dictionary<int, RankCardsTuple>
            {
                {1, new RankCardsTuple(WinnerHand.CalculateHandRank(), WinnerHand.cards)},
                {2, new RankCardsTuple(LoserHand.CalculateHandRank(), LoserHand.cards)},
                {3, new RankCardsTuple(LoserHand2.CalculateHandRank(), LoserHand2.cards)},
            };
        }
        
        private static void AssertWinner()
        {
            BuildHandDict();
            
            int winner = DeclareWinner(dictHands, false);
            Assert.AreEqual(1, winner);
            Assert.AreNotEqual(2, winner);
            Assert.AreNotEqual(3, winner);
        }
        
        [Test]
        public void RoyalStraightFlush()
        {
            WinnerHand.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Ten),
                new Card(ESuit.Clubs, ERank.Jack),
                new Card(ESuit.Clubs, ERank.Queen),
                new Card(ESuit.Clubs, ERank.King),
                new Card(ESuit.Clubs, ERank.Ace),
            };
            
            LoserHand.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Nine),
                new Card(ESuit.Clubs, ERank.Ten),
                new Card(ESuit.Clubs, ERank.Jack),
                new Card(ESuit.Clubs, ERank.Queen),
                new Card(ESuit.Clubs, ERank.King),
            };
            
            LoserHand2.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Two),
                new Card(ESuit.Clubs, ERank.Three),
                new Card(ESuit.Clubs, ERank.Four),
                new Card(ESuit.Clubs, ERank.Five),
                new Card(ESuit.Clubs, ERank.Ace),
            };
            
            Assert.AreEqual(EHandRanks.RoyalStraightFlush, WinnerHand.CalculateHandRank());
            
            Assert.AreNotEqual(EHandRanks.RoyalStraightFlush, LoserHand.CalculateHandRank());
            Assert.AreNotEqual(EHandRanks.RoyalStraightFlush, LoserHand2.CalculateHandRank());
            
            AssertWinner();
        }

        [Test]
        public void StraightFlush()
        {
            WinnerHand.cards = new List<Card>
            {
                new Card(ESuit.Hearts, ERank.Queen),
                new Card(ESuit.Hearts, ERank.Jack),
                new Card(ESuit.Hearts, ERank.Ten),
                new Card(ESuit.Hearts, ERank.Nine),
                new Card(ESuit.Hearts, ERank.Eight),
            };
            
            LoserHand.cards = new List<Card>
            {
                new Card(ESuit.Diamonds, ERank.Five),
                new Card(ESuit.Diamonds, ERank.Four),
                new Card(ESuit.Diamonds, ERank.Three),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Diamonds, ERank.Ace),
            };
            
            LoserHand2.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Ten),
                new Card(ESuit.Clubs, ERank.Nine),
                new Card(ESuit.Clubs, ERank.Eight),
                new Card(ESuit.Clubs, ERank.Seven),
                new Card(ESuit.Clubs, ERank.Six),
            };
            
            Assert.AreEqual(EHandRanks.StraightFlush, WinnerHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.StraightFlush, LoserHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.StraightFlush, LoserHand2.CalculateHandRank());
            
            AssertWinner();
        }
        
        [Test]
        public void FourOfKind()
        {
            WinnerHand.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Seven),
                new Card(ESuit.Diamonds, ERank.Seven),
                new Card(ESuit.Hearts, ERank.Seven),
                new Card(ESuit.Spades, ERank.Seven),
                new Card(ESuit.Hearts, ERank.Queen),
            };
            
            LoserHand.cards = new List<Card>
            {
                new Card(ESuit.Hearts, ERank.Seven),
                new Card(ESuit.Diamonds, ERank.Seven),
                new Card(ESuit.Spades, ERank.Seven),
                new Card(ESuit.Clubs, ERank.Seven),
                new Card(ESuit.Spades, ERank.Jack),
            };
            
            LoserHand2.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Hearts, ERank.Six),
                new Card(ESuit.Spades, ERank.Six),
                new Card(ESuit.Hearts, ERank.Jack),
            };
            
            Assert.AreEqual(EHandRanks.FourOfKind, WinnerHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.FourOfKind, LoserHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.FourOfKind, LoserHand2.CalculateHandRank());
            
            AssertWinner();
        }
        
        [Test]
        public void FullHouse()
        {
            WinnerHand.cards = new List<Card>
            {
                new Card(ESuit.Diamonds, ERank.Four),
                new Card(ESuit.Spades, ERank.Four),
                new Card(ESuit.Clubs, ERank.Four),
                new Card(ESuit.Diamonds, ERank.Nine),
                new Card(ESuit.Clubs, ERank.Nine),
            };
            
            LoserHand.cards = new List<Card>
            {
                new Card(ESuit.Diamonds, ERank.Four),
                new Card(ESuit.Spades, ERank.Four),
                new Card(ESuit.Clubs, ERank.Four),
                new Card(ESuit.Clubs, ERank.Five),
                new Card(ESuit.Diamonds, ERank.Five),
            };
            
            LoserHand2.cards = new List<Card>
            {
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Spades, ERank.Two),
                new Card(ESuit.Clubs, ERank.Two),
                new Card(ESuit.Clubs, ERank.Ace),
                new Card(ESuit.Diamonds, ERank.Ace),
            };
            
            Assert.AreEqual(EHandRanks.FullHouse, WinnerHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.FullHouse, LoserHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.FullHouse, LoserHand2.CalculateHandRank());
            
            AssertWinner();
        }
        
        [Test]
        public void Flush()
        {
            WinnerHand.cards = new List<Card>
            {
                new Card(ESuit.Hearts, ERank.Jack),
                new Card(ESuit.Hearts, ERank.Ten),
                new Card(ESuit.Hearts, ERank.Nine),
                new Card(ESuit.Hearts, ERank.Five),
                new Card(ESuit.Hearts, ERank.Four),
            };
            
            LoserHand.cards = new List<Card>
            {
                new Card(ESuit.Hearts, ERank.Jack), 
                new Card(ESuit.Hearts, ERank.Ten),  
                new Card(ESuit.Hearts, ERank.Nine), 
                new Card(ESuit.Hearts, ERank.Five), 
                new Card(ESuit.Hearts, ERank.Three),
            };
            
            LoserHand2.cards = new List<Card>
            {
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Diamonds, ERank.Jack),
                new Card(ESuit.Diamonds, ERank.Nine),
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Diamonds, ERank.Four),
            };
            
            Assert.AreEqual(EHandRanks.Flush, WinnerHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.Flush, LoserHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.Flush, LoserHand2.CalculateHandRank());
            
            AssertWinner();
        }
        
        [Test]
        public void Straight()
        {
            WinnerHand.cards = new List<Card>
            {
                new Card(ESuit.Diamonds, ERank.Nine),
                new Card(ESuit.Clubs, ERank.King),
                new Card(ESuit.Clubs, ERank.Queen),
                new Card(ESuit.Diamonds, ERank.Jack),
                new Card(ESuit.Spades, ERank.Ten),
            };
            
            LoserHand.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Five), 
                new Card(ESuit.Diamonds, ERank.Four),  
                new Card(ESuit.Hearts, ERank.Three), 
                new Card(ESuit.Clubs, ERank.Two), 
                new Card(ESuit.Diamonds, ERank.Ace),
            };
            
            LoserHand2.cards = new List<Card>
            {
                new Card(ESuit.Spades, ERank.Queen),
                new Card(ESuit.Spades, ERank.King),
                new Card(ESuit.Clubs, ERank.Ace),
                new Card(ESuit.Hearts, ERank.Two),
                new Card(ESuit.Diamonds, ERank.Three),
            };
            
            Assert.AreEqual(EHandRanks.Straight, WinnerHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.Straight, LoserHand.CalculateHandRank());
            
            Assert.AreNotEqual(EHandRanks.Straight, LoserHand2.CalculateHandRank());
            
            AssertWinner();
        }
        
        [Test]
        public void ThreeOfKind()
        {
            WinnerHand.cards = new List<Card>
            {
                new Card(ESuit.Diamonds, ERank.Three),
                new Card(ESuit.Spades, ERank.Three),
                new Card(ESuit.Clubs, ERank.Three),
                new Card(ESuit.Clubs, ERank.Jack),
                new Card(ESuit.Hearts, ERank.Seven),
            };
            
            LoserHand.cards = new List<Card>
            {
                new Card(ESuit.Diamonds, ERank.Three),
                new Card(ESuit.Spades, ERank.Three),
                new Card(ESuit.Clubs, ERank.Three),
                new Card(ESuit.Clubs, ERank.Jack),
                new Card(ESuit.Clubs, ERank.Five),
            };
            
            LoserHand2.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Two),
                new Card(ESuit.Spades, ERank.Two),
                new Card(ESuit.Hearts, ERank.Two),
                new Card(ESuit.Clubs, ERank.Ace),
                new Card(ESuit.Diamonds, ERank.King),
            };
            
            Assert.AreEqual(EHandRanks.ThreeOfKind, WinnerHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.ThreeOfKind, LoserHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.ThreeOfKind, LoserHand2.CalculateHandRank());
            
            AssertWinner();
        }
        
        [Test]
        public void TwoPair()
        {
            WinnerHand.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Five),
                new Card(ESuit.Spades, ERank.Five),
                new Card(ESuit.Clubs, ERank.Three),
                new Card(ESuit.Diamonds, ERank.Three),
                new Card(ESuit.Spades, ERank.Queen),
            };
            
            LoserHand.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Five),
                new Card(ESuit.Spades, ERank.Five),
                new Card(ESuit.Clubs, ERank.Three),
                new Card(ESuit.Diamonds, ERank.Three),
                new Card(ESuit.Spades, ERank.Jack),
            };
            
            LoserHand2.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Five),
                new Card(ESuit.Spades, ERank.Five),
                new Card(ESuit.Clubs, ERank.Two),
                new Card(ESuit.Diamonds, ERank.Two),
                new Card(ESuit.Spades, ERank.Ace),
            };
            
            Assert.AreEqual(EHandRanks.TwoPair, WinnerHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.TwoPair, LoserHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.TwoPair, LoserHand2.CalculateHandRank());
            
            AssertWinner();
        }
        
        [Test]
        public void OnePair()
        {
            WinnerHand.cards = new List<Card>
            {
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Hearts, ERank.Six),
                new Card(ESuit.Spades, ERank.Queen),
                new Card(ESuit.Clubs, ERank.Eight),
                new Card(ESuit.Diamonds, ERank.Seven),
            };
            
            LoserHand.cards = new List<Card>
            {
                new Card(ESuit.Diamonds, ERank.Six),
                new Card(ESuit.Hearts, ERank.Six),
                new Card(ESuit.Spades, ERank.Queen),
                new Card(ESuit.Clubs, ERank.Eight),
                new Card(ESuit.Spades, ERank.Three),
            };
            
            LoserHand2.cards = new List<Card>
            {
                new Card(ESuit.Diamonds, ERank.Five),
                new Card(ESuit.Hearts, ERank.Five),
                new Card(ESuit.Spades, ERank.Four),
                new Card(ESuit.Clubs, ERank.Three),
                new Card(ESuit.Diamonds, ERank.Two),
            };
            
            Assert.AreEqual(EHandRanks.OnePair, WinnerHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.OnePair, LoserHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.OnePair, LoserHand2.CalculateHandRank());
            
            AssertWinner();
        }
        
        [Test]
        public void HighCard()
        {
            WinnerHand.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Queen),
                new Card(ESuit.Clubs, ERank.Ten),
                new Card(ESuit.Diamonds, ERank.Seven),
                new Card(ESuit.Clubs, ERank.Five),
                new Card(ESuit.Diamonds, ERank.Three),
            };
            
            LoserHand.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Queen),
                new Card(ESuit.Clubs, ERank.Ten),
                new Card(ESuit.Diamonds, ERank.Seven),
                new Card(ESuit.Clubs, ERank.Five),
                new Card(ESuit.Diamonds, ERank.Two),
            };
            
            LoserHand2.cards = new List<Card>
            {
                new Card(ESuit.Clubs, ERank.Queen),
                new Card(ESuit.Clubs, ERank.Ten),
                new Card(ESuit.Diamonds, ERank.Seven),
                new Card(ESuit.Clubs, ERank.Four),
                new Card(ESuit.Diamonds, ERank.Three),
            };
            
            Assert.AreEqual(EHandRanks.HighCard, WinnerHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.HighCard, LoserHand.CalculateHandRank());
            Assert.AreEqual(EHandRanks.HighCard, LoserHand2.CalculateHandRank());

            AssertWinner();
        }
        
        // Makes it work correctly in PlayMode
        [UnityTest]
        public IEnumerator TieBreakerTestsWithEnumeratorPasses()
        {
            // Skips a frame
            yield return null;
        
            RoyalStraightFlush();
            StraightFlush();
            FourOfKind();
            FullHouse();
            Flush();
            Straight();
            ThreeOfKind();
            TwoPair();
            OnePair();
            HighCard();
        }
    }
}