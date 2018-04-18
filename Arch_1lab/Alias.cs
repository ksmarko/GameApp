using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_1lab
{
    public class Alias : Game
    {
        public override event GameStateHandler GameEvent;
        private bool _isVerified = false;

        public Alias(int playersCount)
        {
            Settings = new Settings(4, false, false, true, false, false);
            Name = "Alias";
            _playersCount = playersCount;

            playBehaviour = new AliasBehaviour(_playersCount);
            AliasBehaviour.AliasEvent += new AliasBehaviour.GameStateHandler((string message) => GameEvent.Invoke(message));
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
