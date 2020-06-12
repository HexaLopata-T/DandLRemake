using DandLRemake.Items;
using System;

namespace DandLRemake
{
    class Program
    {
        public static void Main()
        {
            GameController controller = new GameController();

            controller.player.ApplyItem(new Shuriken(1));

            controller.Start(controller.GenerateRandomEnemy());
            while(true)
            {
                controller.PlayTurn();

                if (controller.player.IsDead)
                {
                    controller.GameOver();
                    Console.WriteLine("\nПродолжить? д/н");
                    char answer = Console.ReadKey().KeyChar;
                    if(answer == 'д' | answer == 'l' | answer == 'Д' | answer == 'L')
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
        }
    }
}
