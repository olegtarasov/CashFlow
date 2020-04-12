using System;
using System.Threading.Tasks;

namespace ZenMoneyPlus
{
    public static partial class App
    {
        public static class Categories
        {
            public static async Task List()
            {
                Console.WriteLine("Listing cats!");
            }
        }
    }
}