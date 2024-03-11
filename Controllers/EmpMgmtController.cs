using rsEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rsEmployee.Controllers
{
    public class EmpMgmtController : Controller
    {
        // GET: EmployeeManagement
        DataServices db= new DataServices();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult addDepartment(string id)
        {

            return View();
        }
        [HttpPost]
        public ActionResult insertDepartment(rs_department obj)
        {
            db.adddepartment(obj);
            return RedirectToAction("addDepartment");
        }

        public ActionResult CreatEmp(string id)
        {

            ViewBag.deptlist= new SelectList(db.getdeptlist(),"Id","department");
            rs_employee emp=new rs_employee();
            if(id!=null && id != "")
            {
                emp=db.GetEmployeebyId(id);
            }
            return View(emp);
        }
        [HttpPost]
        public ActionResult insertEmp( rs_employee obj)
        {
            if (obj.isActive == "on")
            {
                obj.isActive = "1";
            }
            else
            {
                obj.isActive="0";   
            }
            db.addemployee(obj);
            rs_employee emp= new rs_employee();
            emp=db.getlastemp();
            
            emp_deptManage empdept=new emp_deptManage();
            empdept.dept_id = obj.dept_id;
            empdept.emp_id = emp.id;
            db.addOrupdateEmpDept(empdept);
            
            return RedirectToAction("CreatEmp");
        }

        public ActionResult view_emp(string filterdate)
        {
            List<rs_employee> empList = new List<rs_employee>();
            if (filterdate != null)
            {
                empList = db.GetEmployeeList(filterdate);
            }
            else
            {
                empList = db.GetEmployeeList();

            }
            return View(empList);
        }

        public ActionResult filterdataBydob(string filterdate)
        {
            List<rs_employee> empList = new List<rs_employee>();
            if (filterdate != null)
            {
                empList = db.GetEmployeeList(filterdate);
            }
            else
            {
                empList = db.GetEmployeeList();

            }
            return Json(empList,JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteEmp(string id)
        {
            db.DeletEmp(id);

            return RedirectToAction("view_emp");
        }
        public ActionResult DeleteDept(string id)
        {
            db.Deletdept(id);

            return View();
        }
    }
}