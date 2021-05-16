using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class Card
    {
        
        Image image;
        int value;
        Boolean IsAce;
        public Card(Image myimage, int myvalue)
        {
            image = myimage;
            value = myvalue;
            IsAce = false;
        }
        public Card()
        {
            IsAce = false;
        }

        public Image GetCardImage()
        {
            return image;
        }
        public void SetCardImage(Image myimage)
        {
            image = myimage;
        }

        public int GetCardValue()
        {
            return value;
        }
        public void SetCardValue(int myvalue)
        {
            value = myvalue;
        }
        public void SetCardToAce()
        {
            IsAce = true;
        }
        public Boolean GetCardisanAce()
        {
            return IsAce;
        }
        public void SetCardToAceValue1()
        {
            if (IsAce)
            {
                value = 1;
            }
        }
        public void SetCardToAceValue11()
        {
            if (IsAce)
            {
                value = 11;
            }
        }

    }
}
