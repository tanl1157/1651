using Base.DL.DbAccess;
using Base.Entity.MappedEntities;
using Base.Entity.ResponseModels;
using Base.Entity.ViewModels;
using LibraryManagement.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    [RolesAuthorize(Roles = "Admin")]
    public class BookController : BasicAuthorizationController
    {
        readonly IUnitOfWork _uow;
        public BookController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var response = new MainResponse<List<BookViewModel>>();

            try
            {
                response.Data = _uow.BookRepository.GetViewModels();
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
        public ActionResult Create(Book book)
        {
            var response = new MainResponse<int>();

            try
            {
                _uow.BookRepository.Insert(book);
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
            var book = _uow.BookRepository.GetById(id);
            return View(book);
        }

        [HttpPost]
        public ActionResult Update(Book book)
        {
            var response = new MainResponse<int>();

            try
            {
                _uow.BookRepository.Update(book);
                response.Data = _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        [HttpPost]
        public ActionResult ValidateDelete(int id)
        {
            var response = new MainResponse<int>();

            try
            {
                var orderCount = _uow.OrderRepository.GetAll().ToList().Count(_ => _.BookID == id);
                response.Data = orderCount;
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
                _uow.BookRepository.Delete(id);
                response.Data = _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }
    }
}