using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GreenvilleRevenueGUI
{
    //**********************************************
    // Written by: Dr. Roger Webster
    // For: COP 2362 C# Programming II
    // Where: FSW Computer Science Program www.fsw.edu
    // Professor: Dr. Roger Webster
    // ***********************************************
    class Cards
    {
        Random ranNumberGenerator;

        PokerCard[] AllCards = new PokerCard[52];
        int currentcardnumber = 0;

        Card ACardBack;
        int tries = 0;

        
        public Cards()
        {
            ranNumberGenerator = new Random();
            LoadCards();
            shuffleCards();
        }
        public void LoadCards()
        {
            PokerCard ACard;
            String msg = "";
            try
            {
                string[] list = Directory.GetFiles(@"cards", "*.gif");
                Array.Sort(list);
                int suit = 0;
                for (int index = 0; index < 52; index++)
                {
                    int value = GetNextCardValue(index);
                    Image image = Image.FromFile(list[index]);

                    ACard = new PokerCard(image, value, suit++);
                    if (suit == 4)
                    {
                        suit = 0;
                    }
                    if (index > 31 && index < 36)
                    {
                        ACard.SetCardToAce();
                    }
                    AllCards[index] = ACard;
                }
                string[] list2 = Directory.GetFiles(@"cards", "Wfswbackcard*.gif");
                Image Backimage = Image.FromFile(list2[0]);
                ACardBack = new Card(Backimage, 0);
            }

            catch (Exception e)
            {
                if (tries < 2)
                {
                    msg = "Error Please make sure the card files in the Directory. \nWhen you put the cards in the Directory hit OK button.\n\n " + e.ToString();
                    MessageBox.Show(msg);
                    tries++;
                    LoadCards();

                }
                else
                {
                    Environment.Exit(1);
                }

            }

        }

        private int GetNextCardValue(int currentcardnumber)
        {
            int cardvalue = 10;
            if (currentcardnumber < 33)
            {
                cardvalue = (currentcardnumber / 4) + 2;
            }

            if (currentcardnumber > 31 && currentcardnumber < 36)
                cardvalue = 11;//aces

            if (currentcardnumber > 35 && currentcardnumber < 40)
                cardvalue = 12;//jack

            if (currentcardnumber > 39 && currentcardnumber < 44)
                cardvalue = 13;//kings

            if (currentcardnumber > 43 && currentcardnumber < 48)
                cardvalue = 14;//queen

            return cardvalue;
        }

        public PokerCard GetNextCard()
        {

            if (currentcardnumber >= 52)
            {
                
               
                currentcardnumber = 0;
                //shuffleCards();

            }
            return AllCards[currentcardnumber++];
        }
        public int GetCurrentCardNumber()
        {
            return currentcardnumber;
        }
        public void shuffleCards()
        {
            ResetCardsDiscarded();//added for crazy 8's
            int timestoshuffle = ranNumberGenerator.Next(41, 100);
            for (int index = 0; index < timestoshuffle; index++)
            {
                int r1 = ranNumberGenerator.Next(0, 52);
                int r2 = ranNumberGenerator.Next(0, 52);
                PokerCard TempCard1 = AllCards[r1];
                if (TempCard1.GetCardisanAce())
                {
                    TempCard1.SetCardToAceValue11();
                }
                PokerCard TempCard2 = AllCards[r2];
                if (TempCard2.GetCardisanAce())
                {
                    TempCard2.SetCardToAceValue11();
                }
                AllCards[r1] = TempCard2;
                AllCards[r2] = TempCard1;
            }
            String msg = " ";
            for (int index = 0; index < 52; index++)
            {
                int value = AllCards[index].GetCardValue();
                msg = msg + "i: " + index + " value is : " + value + "\n";

                Console.WriteLine("i: " + index + " value is : " + value);
            }
            // MessageBox.Show(msg);
        }

        public Card GetBackCard()
        {
            return ACardBack;
        }


        public void ResetCardsDiscarded()
        {
            for (int index = 0; index < 52; index++)
            {

                AllCards[index].isDiscarded = false;
            }
        }


        public void DealCrazy8stoBoth()
        {

            for (int index = 0; index < 52; index++)
            {
                Card TempCard1 = AllCards[index];

                if (TempCard1.GetCardValue() == 8)
                {
                    if (index == 0 || index == 9 || index == 10)
                    {
                        //do nothing it is already in its place
                    }
                    else
                    {
                        putAceinposition(index);
                    }
                }
            }
            currentcardnumber = 0;//RWW

        }

        private void putAceinposition(int index)
        {
            //recall 8s go in (index == 0 || index == 9 || index == 10 )
            Boolean notlocatedthenew8 = true;
            int index8 = 0;
            PokerCard Card8 = AllCards[index];

            while (notlocatedthenew8)
            {
                Card TempCard1 = AllCards[index8];
                if (TempCard1.GetCardValue() == 8)
                {
                    if (index8 == 10)
                        return;//all done
                    if (index8 == 9)
                        index8 = 10;
                    if (index8 == 0)
                        index8 = 9;// 0 is already in its place go to the next pos


                }
                else
                {
                    // its ok to swap them AllCards[aceindex] is not an ace
                    PokerCard OriginalCard = AllCards[index8];
                    AllCards[index8] = Card8;
                    AllCards[index] = OriginalCard;
                    notlocatedthenew8 = false;
                }
            }

        }

    }
}