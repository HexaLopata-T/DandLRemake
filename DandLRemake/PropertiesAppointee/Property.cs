namespace DandLRemake.PropertiesAppointee
{
    public class Property
    {
        public string Name { get; private set; }
        public int MaxValue { get; set; }
        public int Value { get; set; }
        public bool Bar { get; set; }

        public Property(string _name, int _value, bool _bar)
        {
            Name = _name;
            MaxValue = _value;
            Value = MaxValue;
            Bar = _bar;
        }

        public Property(string _name,int _maxvalue, int _value, bool _bar)
        {
            Name = _name;
            MaxValue = _maxvalue;
            Value = _value;
            Bar = _bar;
        }

        public override string ToString()
        {
            if (!Bar)
                return Name + ": " + Value;
            else
            {
                string result = Name + ": " + "[";
                for(int i = 0; i < Value/150; i++)
                {
                    result += "#";
                }
                for (int i = 0; i < MaxValue/150 - Value / 150; i++)
                {
                    result += " ";
                }
                result += "]";

                return result;
            }
        }
    }
}
