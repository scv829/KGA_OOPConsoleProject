using GeometryFarm.Enums;
using System.Text;

namespace GeometryFarm.Scenes
{
    public class MainScene : Scene
    {

        private string input;
        private StringBuilder sb;

        public MainScene(Game game) : base(game)
        {
            sb = new StringBuilder();
        }

        public override void Enter()
        {
            sb.AppendLine("\r\n                                                ##                                                          \r\n   ####                                         ##                       #######                            \r\n  ##  ##                                        ##                       ##                                 \r\n ##        #####    #####   ### ##    #####   ######   ## ###   ##  ##   ##        ######  ## ###   ### ##  \r\n ##       ##   ##  ##   ##  ## # ##  ##   ##    ##     ###      ##  ##   #####    ##   ##  ###      ## # ## \r\n ##  ###  #######  ##   ##  ## # ##  #######    ##     ##       ##  ##   ##       ##   ##  ##       ## # ## \r\n  ##  ##  ##       ##   ##  ## # ##  ##         ##     ##       ##  ##   ##       ##  ###  ##       ## # ## \r\n   #####   #####    #####   ##   ##   #####      ###   ##        #####   ##        ### ##  ##       ##   ## \r\n                                                                    ##                                      \r\n                                                                 ####                                       \r\n");
            sb.AppendLine("\r\n #                        #                 #                             ##                ##          #     #    \r\n##                        #                 #                            #  #              #  #               #    \r\n #                 ###   ###    ###  ###   ###                              #              #  #  #  #  ##    ###   \r\n #                ##      #    #  #  #  #   #                              #               #  #  #  #   #     #    \r\n #     ##           ##    #    # ##  #      #                             #     ##         ## #  #  #   #     #    \r\n###    ##         ###      ##   # #  #       ##                          ####   ##          ##    ###  ###     ##  \r\n                                                                                              #                    \r\n");
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
            Console.WriteLine(sb.ToString());
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
                    Console.Clear();
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
