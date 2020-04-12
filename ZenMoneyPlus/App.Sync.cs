using System.Security.Cryptography;
using System.Threading.Tasks;
using ZenMoneyPlus.Models;

namespace ZenMoneyPlus
{
    public static partial class App
    {
        public static class Sync
        {
            public static async Task Pull(string token)
            {
                await using var ctx = new ZenContext();
                var timestampSetting = await ctx.Settings.FindAsync(SettingCodes.SyncTimestamp);

                if (timestampSetting == null || !long.TryParse(timestampSetting.Value, out long timestamp))
                {
                    timestamp = 0;
                }
                
                await using var client = new ZenClient(token);
                await client.PullData(timestamp);
            }
        }
    }
}