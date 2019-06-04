using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.FormsDto.Response
{
    public class OrderResponse
    {
        public int OrderId { get; set; }

        public string TransactionId { get; set; }

        public string Delivery { get; set; }

        public bool Reservation { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public int PostCode { get; set; }

        public virtual List<OrderLineResponse> OrderLines { get; set; }
    }
}
