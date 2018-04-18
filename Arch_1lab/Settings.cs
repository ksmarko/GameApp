using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_1lab
{
    public class Settings
    {
        public int PlayersCount { get; protected set; }
        public bool Chips { get; protected set; }
        public bool GameField { get; protected set; }
        public bool Cards { get; protected set; }
        public bool Cube { get; protected set; }
        public bool Money { get; protected set; }
        
        public Settings(int minPlayersCount, bool chips, bool gameField, bool cards, 
            bool cube, bool money)
        {
            if (minPlayersCount > 1)
                this.PlayersCount = minPlayersCount;
            else
                throw new ArgumentException("Number must be positive and greater than 1");

            this.Chips = chips;
            this.GameField = gameField;
            this.Cards = cards;
            this.Cube = cube;
            this.Money = money;
        }
    }
}
