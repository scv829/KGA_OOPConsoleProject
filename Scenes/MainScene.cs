using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryFarm.Enums;

namespace GeometryFarm.Scenes
{
    public class MainScene : Scene
    {

        private string input;

        public MainScene(Game game) : base(game)
        {
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
        }

        public override void Input()
        {
            // 키 입력 받기
            input = Console.ReadLine();
            
        }

        public override void Render()
        {
            // 타이틀 보여주기
            Console.WriteLine("도형 농장");
            Console.WriteLine("1. 게임 시작");
            Console.WriteLine("2. 게임 종료");
            // 1. 게임 시작
            // 2. 게임 종료
        }

        public override void Update()
        {
            // 키 입력 받은걸 기준으로 맵 이동
            switch (input)
            {
                case "1":
                    Console.Clear();
                    game.ChangeScene(SceneType.Farm);
                    break;
                case "2":
                    Console.WriteLine("게임 종료");
                    game.Over();
                    break;
                default:
                    Console.WriteLine("입력 오류");
                    break;
            }
            
        }
    }
}
