using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_1lab
{
    public abstract class Game
    {
        public delegate void GameStateHandler(string message);
        public abstract event GameStateHandler GameEvent;

        public Settings Settings { get; set; }
        public string Name { get; protected set; }
        public IPlayBehaviour playBehaviour;

        protected int _playersCount;
        public abstract void Play();
        
        public virtual bool Verify(Settings other)
        {
            if (other == null)
                throw new NullReferenceException();
            else
            {
                if (other.PlayersCount >= Settings.PlayersCount)
                    return true;
                else return false;
            }
        }

        public virtual String GetRules()
        {
            return $"Name of game {this.Name}" +
                $"\nMinimal count  of players {Settings.PlayersCount}\n...";
        }
    }
}
