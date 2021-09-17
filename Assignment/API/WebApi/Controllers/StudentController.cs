using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/Student")]
    public class StudentController : ApiController
    {
        private Students_DBEntities3 objEntity = new Students_DBEntities3();

        [HttpGet]
        [Route("AllStudents")]
        public IQueryable<Student> GetStudent()
        {
            try
            {
                return objEntity.Students;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("Batch")]
        public IQueryable<Batch> GetBatch()
        {
            try
            {
                return objEntity.Batches;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetStudentsById/{StdID}")]
        public IHttpActionResult GetStudenteById(string StdID)
        {
            Student objEmp = new Student();
            int ID = Convert.ToInt32(StdID);
            try
            {
                objEmp = objEntity.Students.Find(ID);
                if (objEmp == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(objEmp);
        }

        [HttpPost]
        [Route("InsertStudents")]
        public IHttpActionResult PostStudent(Student data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                objEntity.Students.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateStudents")]
        public IHttpActionResult PutStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Student objEmp = new Student();
                objEmp = objEntity.Students.Find(student.StdID);
                if (objEmp != null)
                {
                    objEmp.Name = student.Name;
                    objEmp.Batch = student.Batch;
                    objEmp.Phone = student.Phone;
                    objEmp.Address = student.Address;
                    objEmp.Age = student.Age;

                }
                int i = this.objEntity.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(student);
        }

        [HttpDelete]
        [Route("DeleteStudents")]
        public IHttpActionResult DeleteStudent(int id)
        {
            //int empId = Convert.ToInt32(id);  
            Student student = objEntity.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            objEntity.Students.Remove(student);
            objEntity.SaveChanges();

            return Ok(student);
        }
    }
}
