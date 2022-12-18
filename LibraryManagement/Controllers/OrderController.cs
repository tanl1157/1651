using Base.DL.DbAccess;
using Base.Entity.MappedEntities;
using Base.Entity.ResponseModels;
using Base.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class OrderController : BasicAuthorizationController
    {
        readonly IUnitOfWork _uow;
        public OrderController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var response = new MainResponse<List<OrderViewModel>>();

            try
            {
                response.Data = _uow.OrderRepository.GetViewModels();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(List<Order> order)
        {
            var response = new MainResponse<int>();

            try
            {
                foreach (var item in order)
                {
                    item.RebindDate();
                }
                _uow.OrderRepository.InsertRange(order);
                response.Data = _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        public ActionResult Update(int id)
        {
            var order = _uow.OrderRepository.GetById(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult Update(Order order)
        {
            var response = new MainResponse<int>();

            try
            {
                order.RebindDate();

                var oldOrder = _uow.OrderRepository.GetById(order.ID);

                oldOrder.State = order.State;
                oldOrder.ActualEndDate = order.ActualEndDate;

                _uow.OrderRepository.Update(oldOrder);
                response.Data = _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var response = new MainResponse<int>();

            try
            {
                _uow.OrderRepository.Delete(id);
                response.Data = _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        public ActionResult Expired()
        {
            return View();
        }

        public ActionResult GetExpired()
        {
            var response = new MainResponse<List<OrderViewModel>>();

            try
            {
                response.Data = _uow.OrderRepository.GetViewModels().Where(_ => _.State == Base.Entity.Shared.OrderState.Renting && (DateTime.Now - _.EndDate).TotalDays > 1).ToList();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }
    }
}