using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFarm.Scenes
{
    public abstract class Scene
    {
        protected Game game;

        public Scene(Game game)
        {
            this.game = game;
        }

        public abstract void Enter();
        public abstract void Render();
        public abstract void Input();
        public abstract void Update();
        public abstract void Exit();
    }
}
