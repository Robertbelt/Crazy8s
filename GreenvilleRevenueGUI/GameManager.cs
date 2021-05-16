namespace GreenvilleRevenueGUI
{
    // TODO: If Button = clicked, pass index, hand and what the card ontop of pile is. Check if button clicked meets condition.
    // If passed, move the card clicked to the top of the pile.
    internal class GameManager
    {
        public bool CheckIfValidMove(Hand hand, int index, PokerCard topOfDeck)
        {
            if (index >= 5)
            {
                return false;
            }

            bool isCrazy8 = Crazy8Check(hand, index);
            bool isSameValue = SameValueCheck(hand, index, topOfDeck);
            bool isSameSuit = SameSuitCheck(hand, index, topOfDeck);
            bool isDiscarded = IsDiscarded(hand, index);

            if ((isCrazy8 || isSameValue || isSameSuit) && !isDiscarded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasAValidMove(Hand hand, PokerCard topOfDeck)
        {
            for (int i = 0; i < 5; i++)
            {
                if ((SameSuitCheck(hand, i, topOfDeck) || SameValueCheck(hand, i, topOfDeck) || Crazy8Check(hand, i)) && !IsDiscarded(hand, i))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasADiscardedCard(Hand hand)
        {
            for (int i = 0; i < 5; i++)
            {
                if (hand.GetaCard(i).isDiscarded)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Crazy8Check(Hand hand, int index)
        {
            if (hand.GetaCard(index).GetCardValue() == 8)
            {
                return true;
            }
            else return false;
        }

        public bool SameValueCheck(Hand hand, int index, PokerCard topOfDeck)
        {
            if (hand.GetaCard(index).GetCardValue() == topOfDeck.GetCardValue())
            {
                return true;
            }
            else return false;
        }

        public bool SameSuitCheck(Hand hand, int index, PokerCard topOfDeck)
        {
            if (hand.GetaCard(index).GetSuit() == topOfDeck.GetSuit())
            {
                return true;
            }
            else return false;
        }

        public bool IsDiscarded(Hand hand, int index)
        {
            if (hand.GetaCard(index).isDiscarded)
            {
                return true;
            }

            return false;
        }

        public bool HasWon(Hand hand)
        {
            int amountOfDiscarded = 0;

            for (int i = 0; i < 5; i++)
            {
                if (hand.GetaCard(i).isDiscarded)
                {
                    amountOfDiscarded++;
                }
            }

            if (amountOfDiscarded == 5)
            {
                return true;
            }
            else return false;
        }

        public PokerCard PlayCard(Hand hand, int index, PokerCard topOfDeck)
        {
            if (CheckIfValidMove(hand, index, topOfDeck))
            {
                PokerCard newTopHand = hand.GetaCard(index);
                newTopHand.isDiscarded = true;
                hand.setACard(index, newTopHand);
                return newTopHand;
            }

            return hand.GetaCard(index);
        }

        public int BestDealerMove(Hand hand, PokerCard topOfDeck)
        {
            for (int i = 0; i < 5; i++)
            {
                if (SameValueCheck(hand, i, topOfDeck) && !IsDiscarded(hand, i) && hand.GetaCard(i).GetCardValue() != 8)
                {
                    return i;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (SameSuitCheck(hand, i, topOfDeck) && !IsDiscarded(hand, i) && hand.GetaCard(i).GetCardValue() != 8)
                {
                    return i;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (Crazy8Check(hand, i) && !IsDiscarded(hand, i))
                {
                    return i;
                }
            }

            return 5;
        }
    }
}