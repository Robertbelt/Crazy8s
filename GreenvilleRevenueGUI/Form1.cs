using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenvilleRevenueGUI
{
    public partial class Form1 : Form
    {
        private Cards DeckofCards = new Cards();
        private Hand PlayerHand;
        private Hand DealerHand;
        private Card BackCard;
        private PokerCard CardonTopofPile;

        private GameManager gameManager = new GameManager();

        private Boolean GameOver = false;
        private List<Button> dealerButtons;
        private List<Button> playerButtons;

        private delegate bool dealerDelegate(int index, Hand hand, PokerCard cardOntop);

        public Form1()
        {
            InitializeComponent();

            PlayerHand = new Hand(true);
            DealerHand = new Hand(false);
            BackCard = DeckofCards.GetBackCard();
            PlayerButton4.Image = BackCard.GetCardImage();
            ResetAllHands();
            this.Size = new Size(800, 600);
            dealerButtons = new List<Button> { DealerButton1, DealerButton2, DealerButton3, DealerButton4, DealerButton5 };
            playerButtons = new List<Button> { PlayerButton1, PlayerButton2, PlayerButton3, PlayerButton4, PlayerButton5 };
            ResetGame(true);
        }

        //------------------------------------------------
        // BIG PURPLE BUTTON
        //------------------------------------------------
        private void Button1_Click(object sender, EventArgs e)
        {
            if (DeckofCards.GetCurrentCardNumber() >= 52)
            {
                MessageBox.Show("All cards have been dealt, GAME OVER!");
                GameOver = true;
            }
            if (GameOver)
            {
                ResetGame(true);
                GameOver = false;
            }

            DealACardtoCardPile();
        }

        private void ResetGame(bool shuffleDeck)
        {
            ResetAllHands();
            GameOver = false;

            if (shuffleDeck)
            {
                DeckofCards.shuffleCards();
            }

            DealACardtoDealer();
            DealACardtoPlayer();

            DealACardtoDealer();
            DealACardtoPlayer();

            DealACardtoDealer();
            DealACardtoPlayer();

            DealACardtoDealer();
            DealACardtoPlayer();

            DealACardtoDealer();
            DealACardtoPlayer();

            DealACardtoCardPile();
        }

        private void ResetAllHands()
        {
            DealerHand.ResetHand();
            PlayerHand.ResetHand();
            PlayerButton1.Image = BackCard.GetCardImage();
            PlayerButton2.Image = BackCard.GetCardImage();
            PlayerButton3.Image = BackCard.GetCardImage();
            PlayerButton4.Image = BackCard.GetCardImage();
            PlayerButton5.Image = BackCard.GetCardImage();
            DealerButton1.Image = BackCard.GetCardImage();
            DealerButton2.Image = BackCard.GetCardImage();
            DealerButton3.Image = BackCard.GetCardImage();
            DealerButton4.Image = BackCard.GetCardImage();
            DealerButton5.Image = BackCard.GetCardImage();
        }

        private void DealACardtoCardPile()
        {
            PokerCard ACard = DeckofCards.GetNextCard();
            ChangeCardinCardPile(ACard);
        }

        private void ChangeCardinCardPile(PokerCard ACard)
        {
            ACard.isDiscarded = true;
            CardonTopofPile = ACard;
            TopCardButton.Image = ACard.GetCardImage();
        }

        private void DealACardtoPlayer()
        {
            int cardnumber = PlayerHand.GetNumberofCards();
            if (cardnumber < 5)
            {
                PokerCard ACard = DeckofCards.GetNextCard();
                PlayerHand.DealACardtoMe(ACard);
                updateGraphics(PlayerHand, cardnumber, false);
            }
        }

        private void DealACardtoDealer()
        {
            int cardnumber = DealerHand.GetNumberofCards();
            if (cardnumber < 5)
            {
                PokerCard ACard = DeckofCards.GetNextCard();
                DealerHand.DealACardtoMe(ACard);
                updateGraphics(DealerHand, cardnumber, false);
            }
        }

        private void PlayCard(int index, Hand hand)
        {
            if (hand.isPlayer && !gameManager.CheckIfValidMove(PlayerHand, index, CardonTopofPile)) //Check if player move is valid
            {
                return;
            }
            else
            {
                CardonTopofPile = gameManager.PlayCard(hand, index, CardonTopofPile);
                updateGraphics(hand, index, true);

                if (hand.isPlayer && !gameManager.HasWon(PlayerHand)) // If hand is player, start dealer turn
                {
                    DealerLogic();
                }
            }

            if (gameManager.HasWon(hand)) // Check for win
            {
                if (hand.isPlayer)
                {
                    MessageBox.Show("Congratulations, You won!");
                }
                else
                {
                    MessageBox.Show("Dealer Won.");
                }
                GameOver = true;
            }
        }

        private List<Button> GetButtonList(Hand handToUpdate)
        {
            List<Button> buttonListToUse;
            if (handToUpdate.isPlayer)
            {
                buttonListToUse = playerButtons;
            }
            else buttonListToUse = dealerButtons;

            return buttonListToUse;
        }

        private void updateGraphics(Hand handToUpdate, int index, bool updateCardOnTop)
        {
            List<Button> buttonListToUse = GetButtonList(handToUpdate); // Returns which list of Buttons to use for update.
            PokerCard card = handToUpdate.GetaCard(index);

            if (card.isDiscarded)
            {
                buttonListToUse[index].Image = BackCard.GetCardImage(); ;
            }
            else
            {
                buttonListToUse[index].Image = card.GetCardImage();
            }

            if (updateCardOnTop)
            {
                TopCardButton.Image = CardonTopofPile.GetCardImage();
            }
        }

        //---------------------------------------------------
        //PLAYER CARD BUTTONS 
        //---------------------------------------------------

        private void button4_Click(object sender, EventArgs e)
        {
            PlayCard(0, PlayerHand);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PlayCard(1, PlayerHand);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PlayCard(2, PlayerHand);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PlayCard(3, PlayerHand);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            PlayCard(4, PlayerHand);
        }

        //--------------------------------------------------------------------
        //Hit me this looks thru the player hand to find a card that is
        // discarded and then deals a card to that index in the hand
        //--------------------------------------------------------------------

        private void button10_Click(object sender, EventArgs e)
        {
            // Reset Game if theres no more moves and the deck of card is out.
            if (DeckofCards.GetCurrentCardNumber() >= 52)
            {
                MessageBox.Show("All cards have been dealt, GAME OVER!");
                ResetGame(true);
                return;
            }

            // Replace discarded card with new card.
            if (!GameOver && gameManager.HasADiscardedCard(PlayerHand))
            {
                AddCardToHand(PlayerHand);
            }

            // If out of discarded card cede turn to dealer.
            else if (!gameManager.HasADiscardedCard(PlayerHand))
            {
                DealerLogic();
            }
        }

        private void AddCardToHand(Hand hand)
        {
            for (int i = 0; i < 5; i++) // Looping through cards in deck to find a discarded card
            {
                PokerCard card = hand.GetaCard(i);
                if (card.isDiscarded)
                {
                    hand.setACard(i, DeckofCards.GetNextCard());
                    updateGraphics(hand, i, true);

                    if (!hand.isPlayer)
                    {
                        Task.Delay(1000).Wait(); //if hand is dealer, add delay.
                    }
                    break;
                }
            }
        }

        private void DealerLogic()
        {
            bool hasValidMove = gameManager.HasAValidMove(DealerHand, CardonTopofPile); // Check if hand has any valid moves

            if (hasValidMove)
            {
                int index = gameManager.BestDealerMove(DealerHand, CardonTopofPile); // Return index of best move.
                PlayCard(index, DealerHand);
            }

            // If dealer doesn't have a valid move but there are discarded cards; draw a card.
            else if (!hasValidMove && gameManager.HasADiscardedCard(DealerHand))
            {
                AddCardToHand(DealerHand);
                DealerLogic();
            }
        }

        // Deal 8s button
        private void button2_Click(object sender, EventArgs e)
        {

            DeckofCards.shuffleCards();
            DeckofCards.DealCrazy8stoBoth();
            ResetGame(false);
        }
    }
}