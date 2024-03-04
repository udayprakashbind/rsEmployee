using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace rsEmployee.Models
{
    public class DataServices
    {
        public SqlConnection con=new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        public bool addemployee( rs_employee obj)
        {
            SqlCommand cmd = new SqlCommand("sp_empmanagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@mobile", obj.mobile);
            cmd.Parameters.AddWithValue("@dob", obj.dob);
            cmd.Parameters.AddWithValue("@gender", obj.gender);
            cmd.Parameters.AddWithValue("@address", obj.address);
            cmd.Parameters.AddWithValue("@dept_id", obj.dept_id);
            cmd.Parameters.AddWithValue("@isActive", obj.isActive);

            cmd.Parameters.AddWithValue("@Id", obj.id);
            if (obj.id != null && obj.id != "")
            {
                cmd.Parameters.AddWithValue("@Action", "updateemployee");
            }
            else
            {

                cmd.Parameters.AddWithValue("@Action", "insertEmp");
            }

            if (con.State==ConnectionState.Closed) 
                con.Open();
            int i=cmd.ExecuteNonQuery();
            con.Close();
            if(i>0)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }

        public List<rs_employee> GetEmployeeList()
        {
            List<rs_employee> emplist=new List<rs_employee>();
            SqlCommand cmd = new SqlCommand("sp_empmanagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "selectemplist");
            con.Open();
            SqlDataReader sdr= cmd.ExecuteReader();
            rs_employee emp = new rs_employee();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    emp=new rs_employee();
                    emp.id = sdr["id"].ToString();
                    emp.name = sdr["Name"].ToString();
                    emp.department = sdr["department"].ToString();
                    emp.mobile = sdr["mobile"].ToString() ;
                    emp.address = sdr["address"].ToString();
                    emp.dob = sdr["dob"].ToString();
                    emp.gender = sdr["gender"].ToString();
                    emplist.Add(emp);
                }
            }
            sdr.Close();
            con.Close() ;
            return emplist;
        }
        public rs_employee GetEmployeebyId(string id)
        {
         
            SqlCommand cmd = new SqlCommand("sp_empmanagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getempbyId");
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            rs_employee emp = new rs_employee();
            if (sdr.HasRows)
            {             
                    emp = new rs_employee();
                    emp.id = sdr["id"].ToString();
                    emp.name = sdr["Name"].ToString();
                    emp.department = sdr["department"].ToString();
                    emp.mobile = sdr["mobile"].ToString();
                    emp.address = sdr["address"].ToString();
                    emp.dob = sdr["dob"].ToString();
                    emp.gender = sdr["gender"].ToString();
              
            }
            sdr.Close();
            con.Close();
            return emp;
        }

        public bool DeletEmp (string id)
        {
            SqlCommand cmd = new SqlCommand("sp_empmanagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@Action", "deleteEmp");
            if(con.State==ConnectionState.Closed)
                con.Open() ;
         int i=   cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Deletdept(string id)
        {
            SqlCommand cmd = new SqlCommand("sp_empmanagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@Action", "deleDept");
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public rs_employee getlastemp()
        {
            SqlCommand cmd = new SqlCommand("sp_empmanagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action","getLastemp");
            con.Open();
            SqlDataReader sdr= cmd.ExecuteReader();
            rs_employee emp=new rs_employee();
            if (sdr.HasRows)
            {
                sdr.Read();
                emp=new rs_employee();
                emp.id = sdr["Id"].ToString();
         
            }
            sdr.Close();
            con.Close( );
            return emp;
        }
       

        public bool adddepartment(rs_department obj)
        {
            SqlCommand cmd = new SqlCommand("sp_empmanagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@department", obj.department);
           
            cmd.Parameters.AddWithValue("@Id", obj.id);
                if(obj.id!=null && obj.id!="") 
            {
                cmd.Parameters.AddWithValue("@Action", "updatedepartment");
            }
            else
            {

                cmd.Parameters.AddWithValue("@Action", "insertdept");
            }
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       


        public List<rs_department> getdeptlist()
        {
            List<rs_department> deptlist= new List<rs_department>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_empmanagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "selectdept");
            SqlDataReader sdr= cmd.ExecuteReader();
            rs_department dept=new rs_department();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dept= new rs_department();
                    dept.id = sdr["Id"].ToString();
                    dept.department = sdr["department"].ToString();
                    deptlist.Add(dept);
                }
            }
            sdr.Close();
            con.Close();
            return deptlist;
        }


        public bool addOrupdateEmpDept(emp_deptManage obj)
        {
            SqlCommand cmd = new SqlCommand("sp_empmanagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@emp_id", obj.emp_id);
            cmd.Parameters.AddWithValue("@dept_id", obj.dept_id);

            cmd.Parameters.AddWithValue("@Id", obj.id);
            if (obj.id != null && obj.id != "")
            {
                cmd.Parameters.AddWithValue("@Action", "updateEmpDept");
            }
            else
            {

                cmd.Parameters.AddWithValue("@Action", "insertEmpDept");
            }
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}