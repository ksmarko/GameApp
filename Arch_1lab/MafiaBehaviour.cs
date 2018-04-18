using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_1lab
{
    public class MafiaBehaviour : IPlayBehaviour
    {
        public delegate void GameStateHandler(string message);
        public static event GameStateHandler MafiaEvent;
        private int _playersCount;

        public MafiaBehaviour(int playersCount)
        {
            this._playersCount = playersCount;
        }

        public void Play()
        {
            MafiaEvent.Invoke("We are playing mafia...");

            Random rand = new Random(DateTime.Now.Millisecond);
            MafiaEvent.Invoke($"Count of players - {_playersCount}\n...\nSome static script to simulate game\n...\n");

            if (rand.Next(-5, 10) > 0)
                MafiaEvent.Invoke("The mafia is dead");
            else
                MafiaEvent.Invoke("The mafia is winner");
        }
    }
}
