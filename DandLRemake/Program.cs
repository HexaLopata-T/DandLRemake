using System;

namespace DandLRemake
{
    class Program
    {
        public static void Main()
        {
            GameController controller = new GameController();

            controller.Start(controller.GenerateRandomEnemy());
            while(true)
            {
                controller.PlayTurn();

                if (controller.player.IsDead)
                {
                    controller.GameOver();
                    Console.WriteLine("\nПродолжить? д/н");
                    char answer = Console.ReadKey().KeyChar;
                    if(answer == 'д' | answer == 'y' | answer == 'l' | answer == 'н' | answer == 'Д' | answer == 'Y' | answer == 'L' | answer == 'Н')
                    {
                        controller = new GameController();
                        controller.GenerateRandomAction();
                        Console.WriteLine("\nНовая игра создана");
                        Console.ReadKey();
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    controller.GenerateRandomAction();
                }

                controller.Update();
            }
;
        }
    }
}
