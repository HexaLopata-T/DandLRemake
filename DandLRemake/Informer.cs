using System;
namespace DandLRemake
{
    public static class Informer
    {
        static string messege;

        public static void SaveMessege(string _messege)
        {
            if(messege != "" && messege != null)
                messege += ", " + _messege;
            else
            {
                messege = _messege;
            }
        }

        public static string ReturnMessege()
        {
            var result = messege;
            messege = "";
            return result;
        }
    }
}
