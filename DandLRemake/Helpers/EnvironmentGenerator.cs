using System;

namespace DandLRemake.Helpers
{
    public class EnvironmentGenerator
    {
        private string[] image;
        public const int ActionPanelHeight = 5;
        public const int LocationPanelHeight = 23;


        public string[] Generate(IHaveEnvironment environment)
        {
            var _environment = environment.ReturnEnvironment();

            if (_environment.Length > LocationPanelHeight)
                throw new Exception("Too big location");

            image = new string[LocationPanelHeight + ActionPanelHeight + 2];

            Clear();

            for (int i = 0; i < _environment.Length; i++)
            {
                image[i] = _environment[i];
            }
            image[_environment.Length] = Informer.ReturnMessege();
            image[_environment.Length + 1] = "______________________________________________________________________________";

            if (environment is Enemy)
            {
                for(int i = 0; i < ActionPanelHeight; i++)
                {
                    image[i + 2 + _environment.Length] = Player.MovesToShow[i];
                }
            }
            else if(environment is Event)
            {
                for (int i = 0; i < ActionPanelHeight; i++)
                {
                    image[i + 2 + _environment.Length] = ((Event)environment).Choise[i];
                }
            }

            return image;
        }

        private void Clear()
        {
            for(int i = 0; i < image.Length; i++)
            {
                image[i] = string.Empty;
            }
        }
    }
}
