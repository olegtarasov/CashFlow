using AutoMapper;
using ZenMoneyPlus.Models;

namespace ZenMoneyPlus
{
    public static partial class App
    {
        internal static IMapper Mapper;

        static App()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tag, Tag>()
                    .ForMember(x => x.ChildrenTags, m => m.Ignore())
                    .ForMember(x => x.ParentTag, m => m.Ignore())
                    .ForMember(x => x.TransactionTags, m => m.Ignore());

                cfg.CreateMap<Transaction, Transaction>()
                    .ForMember(x => x.Tag, m => m.Ignore())
                    .ForMember(x => x.TransactionTags, m => m.Ignore());
            }).CreateMapper();
        }
    }
}