using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_1lab
{
    public class MonopoliaBehaviour : IPlayBehaviour
    {
        public delegate void GameStateHandler(string message);
        public static event GameStateHandler MonopoliaEvent;
        private int _playersCount;

        public MonopoliaBehaviour(int playersCount)
        {
            this._playersCount = playersCount;
        }

        public void Play()
        {
            Player.PlayerEvent += new Player.GameStateHandler((string message) => MonopoliaEvent.Invoke(message));
            Random rand = new Random(DateTime.Now.Millisecond);
            Player[] players = new Player[_playersCount];
            int[] field = new int[70];
            int counter = 0;

            for (int i = 0; i < field.Length; i++)
                field[i] = -1;

            for (int i = 0; i < players.Length; i++)
                players[i] = new Player(i);

            MonopoliaEvent.Invoke($"Count of players - {_playersCount}, they all have a $10 000\n");

            while (field.Contains(-1))
            {
                counter++;

                for (int i = 0; i < players.Length; i++)
                {
                    int moveStep = rand.Next(1, 6);
                    MonopoliaEvent.Invoke($"Player {i} throwed the cube. Number - {moveStep}\nPlayer {i} moved");

                    if (players[i].IndexOnField + moveStep >= field.Length)
                        players[i].IndexOnField = field.Length - players[i].IndexOnField + moveStep - 2;
                    else
                        players[i].IndexOnField += moveStep;

                    if (field[players[i].IndexOnField] == -1) //empty field
                    {
                        int price = rand.Next(100, 300);
                        if (players[i].Money >= price)
                        {
                            MonopoliaEvent.Invoke($"The field is empty. Player {i} buy the product");
                            field[players[i].IndexOnField] = players[i].Index;
                            players[i].AddProduct();
                            players[i].Withdraw(price);
                        }
                        else
                        {
                            MonopoliaEvent.Invoke($"Player {i} has not enough money for this product. Next player moves.");
                            continue;
                        }
                    }
                    else //other player's field
                    {
                        MonopoliaEvent.Invoke($"The field is foreign. Penalty for Player {i}");
                        players[i].Withdraw(rand.Next(100, 200));
                    }
                }

                MonopoliaEvent.Invoke($"\nThe circle {counter + 1} completed\n");

                foreach (var el in players)
                    el.AddMoney(1500);

                string _field = "";
                foreach (var el in field)
                    _field += el + "  ";

                MonopoliaEvent.Invoke($"\n\nGame field is: {_field}\n\n");
            }

            MonopoliaEvent.Invoke($"Game ends. Player {SelectWinner(players).Index} is winner. He has ${SelectWinner(players).Money}");
        }

        private Player SelectWinner(Player[] players)
        {
            int max = players[0].Money;
            Player player = players[0];
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].Money > max)
                {
                    max = players[i].Money;
                    player = players[i];
                }
            }

            return player;
        }

        class Player
        {
            public delegate void GameStateHandler(string message);
            public static event GameStateHandler PlayerEvent;

            public int Products { get; private set; }
            public int Money { get; protected set; } = 10000;
            public int IndexOnField { get; set; }
            public int Index { get; }

            public Player(int index)
            {
                this.Index = index;
                IndexOnField = 0;
                Products = 0;
            }

            public void AddProduct()
            {
                Products++;
            }

            public void AddMoney(int sum)
            {
                Money += sum;
                PlayerEvent.Invoke($"{sum} added for Player {Index} account");
            }

            public void Withdraw(int sum)
            {

                if (sum > Money)
                    PlayerEvent.Invoke($"Player {Index} has not enough money");
                else
                {
                    Money -= sum;
                    PlayerEvent.Invoke($"{sum} removed for Player {Index} account");
                }
            }
        }
    }
}
