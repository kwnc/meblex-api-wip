using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Dawn;
using Meblex.API.Context;
using Meblex.API.FormsDto.Request;
using Meblex.API.FormsDto.Response;
using Meblex.API.Helper;
using Meblex.API.Interfaces;
using Meblex.API.Models;

namespace Meblex.API.Services
{
    public class ShoppingCartService:IShoppingCartService
    {

        public readonly MeblexDbContext _context;

        public ShoppingCartService(MeblexDbContext context)
        {
            _context = context;
        }
        //Dodać migracje do id tranzakcji

        public int AddOrder(int userId, OrderAddForm order)
        {
            var UserId = Guard.Argument(userId, nameof(userId)).NotNegative().NotZero().Value;
            var Order = Guard.Argument(order, nameof(order)).NotNull().Value;

            var toAdd = new Order()
            {
                Client = _context.Users.Find(UserId).Client,
                Address = Order.Address,
                City = Order.City,
                Delivery = Order.Delivery,
                PostCode = int.Parse(Order.PostCode),
                Reservation = Order.Reservation,
                State = Order.State,
                Street = Order.Street,
                TransactionId = Guid.NewGuid().ToString(),
                OrderLines = Order.OrderLines.Select(x => new OrderLine()
                {
                    PieceOfFurniture = x.PieceOfFurnitureId == null? null : _context.Furniture.Find(x.PieceOfFurnitureId) ?? throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Piece of furniture does not exist"),
                    Part = x.PartId == null ? null : _context.Parts.Find(x.PartId) ?? throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Part does not exist"),
                    Count = OnStock(x) ? x.Count : throw new HttpStatusCodeException(HttpStatusCode.Conflict, "Not sufficient stock of: " 
                                                                                                              + (x.PartId == null ? _context.Furniture.Find(x.PieceOfFurnitureId).Name :  _context.Parts.Find(x.PartId).Name)
                                                                                                              + "Available: "
                                                                                                              + (x.PartId == null ? _context.Furniture.Find(x.PieceOfFurnitureId).Count : _context.Parts.Find(x.PartId).Count)),
                    Price = x.Price,
                    Size = x.Size
                }).ToList()
            };
            _context.Orders.Add(toAdd);

            if (_context.SaveChanges() == 0)
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, "Unable to add data");
            FixStock(order.OrderLines);
            return toAdd.OrderId;
        }

        private void FixStock(List<OrderLineAddForm> lines)
        {
            foreach (var line in lines)
            {
                if (line.PartId != null)
                {
                    var db = _context.Parts.Find(line.PartId);
                    db.Count -= line.Count;
                    _context.Parts.Update(db);
                }
                else
                {
                    var db = _context.Furniture.Find(line.PieceOfFurnitureId);
                    db.Count -= line.Count;
                    _context.Furniture.Update(db);
                }
            }
        }

        private bool OnStock(OrderLineAddForm line)
        {
            if (line.PartId != null)
            {
                return _context.Parts.Find(line.PartId).Count > line.Count;
            }
            else
            {
                return _context.Furniture.Find(line.PieceOfFurnitureId).Count > line.Count;
            }
        }

        public OrderResponse GetClientById(int id)
        {

        }

        public List<OrderResponse> GetAllClientOrders(int userId)
        {

        }

        public List<OrderResponse> GetAllOrders()
        {

        }

        public OrderResponse GetById(int id)
        {

        }
    }
}
