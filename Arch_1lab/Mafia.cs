using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_1lab
{
    class Mafia : Game
    {
        public override event GameStateHandler GameEvent;
        private bool _isVerified = false;

        public Mafia(int playersCount)
        {
            Settings = new Settings(7, false, false, true, false, false);
            Name = "Mafia";
            playBehaviour = new MafiaBehaviour(_playersCount);
            _playersCount = playersCount;

            MafiaBehaviour.MafiaEvent += new MafiaBehaviour.GameStateHandler((string message) => GameEvent.Invoke(message));
        }

        public override void Play()
        {
            if (!_isVerified)
                throw new InvalidOperationException("Game is not verified");

            playBehaviour.Play();
        }
       
        public override bool Verify(Settings other)
        {
            _isVerified = (base.Verify(other) && other.Cards) ? true : false;
            _playersCount = other.PlayersCount;

            return _isVerified;
        }
    }
}
