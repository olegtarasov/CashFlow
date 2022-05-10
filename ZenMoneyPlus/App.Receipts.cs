// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using Serilog;
// using ZenMoneyPlus.Clients;
// using ZenMoneyPlus.Data;
//
// namespace ZenMoneyPlus;
//
// public static partial class App
// {
//     public static class Receipts
//     {
//         private static readonly ILogger _log = Log.ForContext(typeof(Receipts));
//
//         public static async Task GetMissingReceipts(string token, bool retryFailed = false)
//         {
//             await using var ctx = new ZenContext();
//             await using var client = new ZenClient(token);
//
//             var transactions = await ctx.Transactions
//                                         .Where(x => x.QrCode != null && x.QrCode.Length > 0 && x.Receipt == null && x.GetReceiptFailed == retryFailed)
//                                         .ToArrayAsync();
//                 
//             for (int i = 0; i < transactions.Length; i++)
//             {
//                 _log.Information($"Getting {i + 1} out of {transactions.Length} missing receipts.");
//                 var receipt = await client.GetReceipt(transactions[i].QrCode);
//                 if (receipt == null)
//                 {
//                     _log.Warning($"Failed to get a receipt for transaction {transactions[i].QrCode}");
//                     transactions[i].GetReceiptFailed = true;
//                     continue;
//                 }
//
//                 receipt.ReceiptItems = receipt.Items.ToList();
//                 receipt.TransactionId = transactions[i].Id;
//                 await ctx.Receipts.AddAsync(receipt);
//                     
//             }
//                 
//             await ctx.SaveChangesAsync();
//         }
//     }
// }