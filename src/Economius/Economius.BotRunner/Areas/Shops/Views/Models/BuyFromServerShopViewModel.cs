﻿using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shops.Views.Models
{
    public class BuyFromServerShopViewModel : IViewModel
    {
        public object Name { get; internal set; }
        public object Price { get; internal set; }
    }
}