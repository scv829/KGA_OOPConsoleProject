using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryFarm.Scenes;
using GeometryFarm.Enums;


namespace GeometryFarm
{
    public class Game
    {
        private bool isRunning;
        private Scene[] scenes;
        private Scene currntScene;

        private Player player { get; set; }
        public Player Player { get { return player; } set { player = value; } }

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

        public void ChangeScene(SceneType sceneType)
        {
            currntScene.Exit();
            currntScene = scenes[(int)sceneType];
            currntScene.Enter();
        }

        public void Over()
        {
            isRunning = false;
        }

        private void Start()
        {
            isRunning = true;

            player = new Player("테스트");

            scenes = new Scene[(int)SceneType.SIZE];
            
            scenes[(int)SceneType.Main] = new MainScene(this);
            scenes[(int)SceneType.Farm] = new FarmScene(this);
            scenes[(int)SceneType.VarietyStore] = new VarietyStoreScene(this);
            
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
