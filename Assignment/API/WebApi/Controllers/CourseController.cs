using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("Api/Batch")]
    public class CourseController : ApiController
    {
        private Students_DBEntities3 objEntity = new Students_DBEntities3();

        [HttpGet]
        [Route("AllBatches")]
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
        [Route("GetBatchesById/{BatcheID}")]
        public IHttpActionResult GetBatchById(string BatcheID)
        {
            Batch objDep = new Batch();
            int ID = Convert.ToInt32(BatcheID);
            try
            {
                objDep = objEntity.Batches.Find(ID);
                if (objDep == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(objDep);
        }

        [HttpPost]
        [Route("InsertBatches")]
        public IHttpActionResult PostBatch(Batch data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                objEntity.Batches.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateBatches")]
        public IHttpActionResult PutBatch(Batch batch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Batch objDep = new Batch();
                objDep = objEntity.Batches.Find(batch.BatchID);
                if (objDep != null)
                {
                    objDep.BatchName = batch.BatchName;
                }
                int i = this.objEntity.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(batch);
        }

        [HttpDelete]
        [Route("DeleteBatches")]
        public IHttpActionResult DeleteBatch(int id)
        {
            //int empId = Convert.ToInt32(id);  
            Batch batch = objEntity.Batches.Find(id);
            if (batch == null)
            {
                return NotFound();
            }

            objEntity.Batches.Remove(batch);
            objEntity.SaveChanges();

            return Ok(batch);
        }
    }
}
