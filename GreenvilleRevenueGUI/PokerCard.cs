using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
namespace GreenvilleRevenueGUI
{
    //**********************************************
    // Written by: Dr. Roger Webster
    // For: COP 2362 C# Programming II
    // Where: FSW Computer Science Program www.fsw.edu
    // Professor: Dr. Roger Webster
    // This extends or inherits the Card Object
    // ***********************************************
    public class PokerCard : Card
    {
        public bool isDiscarded { get; set; } = false;
        Boolean discarded = false;// use this to determine of card has been discarded
        public enum Suits
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades,
        }
        Suits CardSuit;
        int CardSuitindex = 0;
        public PokerCard(Image myimage, int myvalue, int CardSuitin)
        {
            base.SetCardImage(myimage);
            base.SetCardValue(myvalue);
            CardSuitindex = CardSuitin;
            switch (CardSuitin)
            {
                case 0:
                    CardSuit = Suits.Clubs;
                    break;
                case 1:
                    CardSuit = Suits.Diamonds;
                    break;
                case 2:
                    CardSuit = Suits.Hearts;
                    break;
                case 3:
                    CardSuit = Suits.Spades;
                    break;
            }
        }


        public Boolean Getdiscarded()
        {

            return (discarded);
        }
        public void Setdiscarded(Boolean discard)
        {
            discarded = discard;

        }

        public String GetCardValueString()
        {
            int val = GetCardValue();

            return (GetValueasString(val));
        }

        public int GetSuitIndex()
        {
            return (CardSuitindex);
        }
        public Suits GetSuit()
        {
            return (CardSuit);
        }

        public String GetSuitasString()
        {
            return (CardSuit.ToString());
        }

        public String GetValueasString(int val)
        {
            String value = "" + val;
            switch (val)
            {
                case 11:
                    value = "ACE";
                    break;
                case 12:
                    value = "JACK";
                    break;
                case 13:
                    value = "KING";
                    break;
                case 14:
                    value = "QUEEN";
                    break;

            }

            return (value);

        }

    }
}


