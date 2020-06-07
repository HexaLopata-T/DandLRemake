using DandLRemake.PropertiesAppointee;
using DandLRemake.Helpers;
using DandLRemake.Events;
using System;
using DandLRemake.Enemies;

namespace DandLRemake
{
    public class GameController
    {
        private PropertyEditor editor;
        private PropertyParser parser;
        private ImageGeneretor generetor;
        private Drawer drawer;
        private EnvironmentGenerator environmentGenerator;

        public Player player;

        private Random random;

        private int battleChance = 20;

        public int BattleChance { get { return battleChance ; } set { if (value >= 0 & value <= 100) { battleChance = value; } ;} }

        IHaveEnvironment thisAction;

        public GameController()
        {
            player = new Player();

            random = new Random();

            editor = player.MyStats;
            parser = new PropertyParser();
            generetor = new ImageGeneretor();
            drawer = new Drawer();
            environmentGenerator = new EnvironmentGenerator();
        }

        public void Update()
        {
            var properties = editor.ReturnProperties();

            var parsedProperties = parser.PropertyArrayParse(properties);

            var environment = environmentGenerator.Generate(thisAction);

            var image = generetor.Generate(parsedProperties, environment);

            drawer.Draw(image);
        }

        public void PlayTurn()
        { 
            if (thisAction is Event)
            {
                Choise();
            }
            else if(thisAction is Enemy)
            {
                Battle();
            }
        }

        private void Choise()
        {
            var turn = true; 
            while (turn)
            {
                var choise = Console.ReadKey().KeyChar;
                turn = ((Event)thisAction).Action(choise, editor, ref player);
                if (!turn)
                    ((Event)thisAction).SetChoiseToContinue();
                Update();
                Console.ReadKey();
            }
            player.Hunger();
        }

        private void Battle()
        {
            var turn = true;
            while (((Enemy)thisAction).HP > 0 & !player.IsDead)
            {
                player.Hunger();
                player.SetMovesToShowToReal();
                Update();

                while (turn)
                {
                    var choise = Console.ReadKey().KeyChar;
                    turn = player.Turn(choise, (Enemy)thisAction);
                    if(!turn)
                        player.SetMovesToShowToContinue();
                    Update();
                }
                Console.ReadKey();
                if ((((Enemy)thisAction).HP > 0))
                {
                    ((Enemy)thisAction).Turn(ref player);
                    Update();
                    Console.ReadKey();
                }
                turn = true;
            }

            if (((Enemy)thisAction).HP <= 0)
            {
                Informer.SaveMessege("Враг повержен");
                var IsLevelUP = player.ApplyXP(((Enemy)thisAction).XP);
                player.SetMovesToShowToContinue();
                Update();

                var repeat = true;

                if (IsLevelUP)
                    while (repeat)
                    {
                        player.SetMovesToShowForLevelUp();
                        Informer.SaveMessege("Выберете улучшение");
                        Update();
                        repeat = player.ApplyLevel();
                    }
                player.SetMovesToShowToContinue();
            }

            Update();
            Console.ReadKey();
        }

        public void GenerateRandomAction()
        {
            if (random.Next(1, 101) > BattleChance)
                thisAction = GenerateRandomEvent();
            else
                thisAction = GenerateRandomEnemy();

        }

        public void GameOver()
        {
            Console.Clear();
            Console.WriteLine("GAME OVER");
        }

        public void Start(IHaveEnvironment firstAction)
        {
            thisAction = firstAction;
            Update();
        }

        public Event GenerateRandomEvent()
        {
            var id = random.Next(0, EventList.events.Length);
            return EventList.ReturnEvent(id);
        }

        public Enemy GenerateRandomEnemy()
        {
            var id = random.Next(0, EnemyList.enemies.Length);
            return EnemyList.ReturnEnemy(id);
        }
    }
}