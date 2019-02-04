using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Picks.Infrastructure.Models
{
    public class Basket
    {
        internal const string customerBasketKey = "customer_basket";

        private List<Picture> _basketRows = new List<Picture>();

        public virtual void AddPicture(Picture p)
        {

            var cartRow = _basketRows.Where(x => x.Id.Equals(p.Id)).FirstOrDefault();
            if (cartRow == null)
            {
                _basketRows.Add(p);
            }
        }

        public virtual void RemovePicture(Guid id)
        {
            _basketRows.RemoveAll(x => x.Id.Equals(id));
        }

        public virtual void EmptyBasket()
        {
            _basketRows.Clear();
        }

        public virtual IEnumerable<Picture> BasketRows => _basketRows;
    }
}