using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryFarm.Scenes;

namespace GeometryFarm
{
    public class Game
    {
        private bool isRunning;
        private Scene[] scenes;
        private Scene currntScene;

        public void Run()
        {
            Start();
            while (isRunning)
            {
                Render();
                Input();
                Update();
            }
            End();
        }

        public void Over()
        {
            isRunning = false;
        }

        private void Start()
        {
            isRunning = true;

            scenes = new Scene[(int)SceneType.SIZE];
            
            scenes[(int)SceneType.Main] = new MainScene(this);
            
            currntScene = scenes[(int)SceneType.Main];

            currntScene.Enter();
        }

        private void Render()
        {
            currntScene.Render();
        }

        private void Input()
        {
            currntScene.Input();
        }

        private void Update()
        {
            currntScene.Update();
        }

        private void End()
        {
            currntScene.Exit();
        }

    }
}
