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
    public class AudienceController : BasicAuthorizationController
    {
        readonly IUnitOfWork _uow;
        public AudienceController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var response = new MainResponse<List<AudienceViewModel>>();

            try
            {
                response.Data = _uow.AudienceRepository.GetViewModels();
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
        public ActionResult Create(Audience audience)
        {
            var response = new MainResponse<int>();

            try
            {
                _uow.AudienceRepository.Insert(audience);
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
            var audience = _uow.AudienceRepository.GetById(id);
            return View(audience);
        }

        [HttpPost]
        public ActionResult Update(Audience audience)
        {
            var response = new MainResponse<int>();

            try
            {
                _uow.AudienceRepository.Update(audience);
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
                var orderCount = _uow.OrderRepository.GetAll().ToList().Count(_ => _.AudienceID == id);
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
                _uow.AudienceRepository.Delete(id);
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