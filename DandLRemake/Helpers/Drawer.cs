using System;

namespace DandLRemake.Helpers
{
    public class Drawer
    {
        public void Draw(string[] image)
        {
            Console.Clear();
            for (int i = 0; i < image.Length; i++)
            {
                Console.WriteLine(image[i]);
            }
        }
    }
}
