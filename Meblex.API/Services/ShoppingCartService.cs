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

        public OrderResponse GetClientById(int id, int userId)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative().NotZero().Value;
            var UserId = Guard.Argument(userId, nameof(userId)).NotNegative().NotZero().Value;

            var order = _context.Orders.Single(x => x.Client.UserId == UserId && x.OrderId == Id) ?? throw  new HttpStatusCodeException(HttpStatusCode.NotFound);

            var response = new OrderResponse()
            {
                State = order.State,
                PostCode = order.PostCode,
                City = order.City,
                Address = order.Address,
                Delivery = order.Delivery,
                Reservation = order.Reservation,
                OrderId = order.OrderId,
                Street = order.Street,
                TransactionId = order.TransactionId,
                OrderLines = order.OrderLines.Select(x => new OrderLineResponse()
                {
                    Count = x.Count,
                    OrderLineId = x.OrderLineId,
                    Price = x.Price,
                    Size = x.Size,
                    PieceOfFurniture = x.PieceOfFurniture == null ? null : new ShoppingCartFurnitureResponse() { Name = x.PieceOfFurniture.Name, PieceOfFurnitureId = x.PieceOfFurniture.PieceOfFurnitureId, Photos = x.PieceOfFurniture.Photos.Select(z => z.Path).ToList()},
                    Part = x.Part == null ? null: new ShoppingCartPartResponse() { Name = x.Part.Name, PartId = x.Part.PartId}
                }).ToList()
            };

            return response;
        }

        public List<OrderResponse> GetAllClientOrders(int userId)
        {
            var UserId = Guard.Argument(userId, nameof(userId)).NotNegative().NotZero().Value;

            var orders = _context.Orders.Where(x => x.Client.UserId == UserId);
            var responses = new List<OrderResponse>();
            foreach (var order in orders)
            {
                var response = new OrderResponse()
                {
                    State = order.State,
                    PostCode = order.PostCode,
                    City = order.City,
                    Address = order.Address,
                    Delivery = order.Delivery,
                    Reservation = order.Reservation,
                    OrderId = order.OrderId,
                    Street = order.Street,
                    TransactionId = order.TransactionId,
                    OrderLines = order.OrderLines.Select(x => new OrderLineResponse()
                    {
                        Count = x.Count,
                        OrderLineId = x.OrderLineId,
                        Price = x.Price,
                        Size = x.Size,
                        PieceOfFurniture = x.PieceOfFurniture == null ? null : new ShoppingCartFurnitureResponse() { Name = x.PieceOfFurniture.Name, PieceOfFurnitureId = x.PieceOfFurniture.PieceOfFurnitureId, Photos = x.PieceOfFurniture.Photos.Select(z => z.Path).ToList() },
                        Part = x.Part == null ? null : new ShoppingCartPartResponse() { Name = x.Part.Name, PartId = x.Part.PartId }
                    }).ToList()
                };

                responses.Add(response);
            }

            return responses;
        }


    }
}
