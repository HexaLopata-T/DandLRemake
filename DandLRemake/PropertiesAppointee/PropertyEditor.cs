using System;

namespace DandLRemake.PropertiesAppointee
{
    public class PropertyEditor
    {
        private readonly Property[] properties;

        public PropertyEditor()
        {
            properties = new Property[]
            {
                new Property("Здоровье", 100, true),
                new Property("Мана", 20, true),
                new Property("Защита", 0, false),
                new Property("Золото", 0, false),
                new Property("Сытость", 250, true),
                new Property("Мораль", 250, true),
                new Property("Опыт",100, 0, false),
            };
        }

        public void SetValue(int id, int _value)
        {
            if (properties[id] != null)
                properties[id].Value = _value;
            else
                throw new Exception("Property isn't exist!");
        }

        public int GetValue(int id)
        {
            if (properties[id] != null)
                return properties[id].Value;
            else
                throw new Exception("Property isn't exist!");
        }

        public void SetMaxValue(int id, int _maxValue)
        {
            if (properties[id] != null)
                properties[id].MaxValue = _maxValue;
            else
                throw new Exception("Property isn't exist!");
        }

        public int GetMaxValue(int id)
        {
            if (properties[id] != null)
                return properties[id].MaxValue;
            else
                throw new Exception("Property isn't exist!");
        }

        public Property[] ReturnProperties()
        {
            return properties;
        }

        public Property ReturnProperty(int id)
        {
            return properties[id];
        }

        public override string ToString()
        {
            string _properties = "";
            for(int i = 0; i < properties.Length; i++)
            {
                _properties += properties[i].ToString();
            }

            return _properties;
        }

    }
}