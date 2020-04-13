using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZenMoneyPlus.Models;

namespace ZenMoneyPlus
{
    public static partial class App
    {
        public static class Categories
        {
            public static async Task List()
            {
                await using var ctx = new ZenContext();
                var topCats = await ctx.Tags.Where(x => x.Parent == null).ToArrayAsync();

                foreach (var cat in topCats)
                    PrintCat(cat, 0);
            }

            private static void PrintCat(Tag tag, int level)
            {
                var sb = new StringBuilder();
                for (int i = 0; i < level; i++) 
                    sb.Append('\t');

                sb.Append(tag.Title).Append($" ({tag.Id})");

                Console.WriteLine(sb.ToString());

                foreach (var child in tag.ChildrenTags)
                    PrintCat(child, level + 1);
            }
        }
    }
}