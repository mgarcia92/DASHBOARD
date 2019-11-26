using B2B_DASHBOARD.Models;
using B2B_DASHBOARD.Models.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B2B_DASHBOARD.Controllers
{
    [Authorize]
    public class ConfigurationController : Controller
    {
        
        // GET: Configuration
        public ActionResult Index()
        {
            RepositoryMysql<USERS> repository = new RepositoryMysql<USERS>("sp_get_all_users");
            ViewBag.usuarios  =  repository.List<USERS>();
            return View();
        }
        [HttpPost]
        public string UpdateUser(string data)
        {
            try
            {
                USERS user = JsonConvert.DeserializeObject<USERS>(data);
                RepositoryMysql<USERS> repository = new RepositoryMysql<USERS>("sp_update_user");
                int result = repository.Update(user);
                if (result > 0)
                {
                    return JsonConvert.SerializeObject(new { MSG = "Actualizado" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { ERROR_MESSAGE = "Error Consulta SQL" });
                }
            }
            catch (Exception ex)
            {

                return JsonConvert.SerializeObject(new { ERROR_MESSAGE = ex.Message });
            }
        }
       
        public List<USERS> GetUser(string id)
        {
            try
            {
                List<USERS> list = new List<USERS>();
                RepositoryMysql<USERS> repository = new RepositoryMysql<USERS>("sp_get_user");
                USERS usuario = repository.Find<USERS>(id, "USERNAME");
                if (usuario != null)
                {
                    list.Add(usuario);
                    return list; 
                }
                else
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<USERS> GetUsersActive()
        {
            try
            {
                List<USERS> usuarios;
                RepositoryMysql<USERS> repository = new RepositoryMysql<USERS>("sp_get_users_active");
                usuarios = repository.List<USERS>();
                if (usuarios != null)
                {
                    return usuarios;
                }
                else
                {
                    return new List<USERS>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
  
        public List<Rol> GetAllRol()
        {
            RepositoryMysql<Rol> repository = new RepositoryMysql<Rol>("sp_get_all_rol");
            return repository.List<Rol>();
        }
        [HttpPost]
        public PartialViewResult ViewUser(string id,string option)
        {
            dynamic usuario = new ExpandoObject();
            List<USERS> users;
            if (!string.IsNullOrEmpty(option) && option == "Active")
            {
                users = GetUsersActive();
            }
            else
            {
                users = GetUser(id);
            }           
            List<Rol> roles = GetAllRol();
            usuario.user = users;
            usuario.rol = roles.Where(x => x.ID_ROL != 2).ToList();
            usuario.estatus = new  Estatus[] { new Estatus { NAME = "Activo", ID = 1}, new Estatus { NAME = "Inactivo", ID = 0 }};
            ViewBag.usuario = usuario;
            return PartialView();
        }
    }
}