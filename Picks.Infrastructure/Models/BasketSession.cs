using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Picks.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.Infrastructure.Models
{
    public class BasketSession : Basket
    {
        [JsonIgnore] //No need to Deserialize or Serialize the ISession property
        public ISession Session { get; private set; }

        public static Basket GetBasket(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            BasketSession basket = session.Get<BasketSession>(customerBasketKey) ?? new BasketSession();
            basket.Session = session;
            return basket;
        }

        public override void AddPicture(Picture p)
        {
            base.AddPicture(p);
            CommitToSession();
        }

        public override void RemovePicture(Guid id)
        {
            base.RemovePicture(id);
            CommitToSession();
        }

        public override void EmptyBasket()
        {
            base.EmptyBasket();
            Session.Remove(customerBasketKey);
        }

        private void CommitToSession()
        {
            Session.Set(customerBasketKey, this);
        }
    }
}