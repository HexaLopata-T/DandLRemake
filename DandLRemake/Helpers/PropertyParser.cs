using DandLRemake.PropertiesAppointee;

namespace DandLRemake
{
    public class PropertyParser
    {
        public string PropertyParse(Property property)
        {
            return property.ToString();
        }

        public string[] PropertyArrayParse(Property[] properties)
        {
            string[] result = new string[properties.Length];
            for(int i = 0; i < properties.Length; i++)
            {
                result[i] = PropertyParse(properties[i]);
            }
            return result;
        }
    }
}
