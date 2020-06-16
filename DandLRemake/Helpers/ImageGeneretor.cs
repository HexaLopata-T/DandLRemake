using System;

namespace DandLRemake.Helpers
{
    public class ImageGeneretor
    {
        private readonly int maxStrings = EnvironmentGenerator.ActionPanelHeight + EnvironmentGenerator.LocationPanelHeight + 2;
        private string[] Contaner;
        private string[] Properties;
        private string[] Environment;
        private int propertyContenerWidth = 35;

        public ImageGeneretor()
        {
            Contaner = new string[maxStrings];
            Properties = new string[maxStrings];
            Environment = new string[maxStrings];
        }

        private void ApplyEnvironment(string[] _environment)
        {
            if (_environment.Length > maxStrings)
                throw new Exception("Too many environment strings!");

            for (int i = 0; i < _environment.Length; i++)
            {
                Environment[i] = _environment[i];
            }
        }

        private void ApplyProperties(string[] _properties)
        {
            if (_properties.Length > maxStrings)
                throw new Exception("Too many properties!");

            for (int i = 0; i < _properties.Length; i++)
            {
                Properties[i] = _properties[i];
            }
        }

        public string[] Generate(string[] _properties, string[] _environment)
        {
            ApplyProperties(_properties);
            ApplyEnvironment(_environment);

            for (int i = 0; i < maxStrings; i++)
            {
                Contaner[i] = "|" + Properties[i];
                int spaces;
                if (Properties[i] != null)
                    spaces = propertyContenerWidth - Properties[i].Length;
                else
                    spaces = propertyContenerWidth;

                for (int j = 0; j < spaces; j++)
                {
                    Contaner[i] += " ";
                }
                Contaner[i] += "|" + Environment[i];
            }

            return Contaner;
        }

    }
}