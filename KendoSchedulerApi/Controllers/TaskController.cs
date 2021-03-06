using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoSchedulerApi.Helpers;
using KendoSchedulerApi.Models;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
namespace KendoSchedulerApi.Controllers
{
    public class TaskController : ApiController
    {
        SampleEntities db = new SampleEntities();

        public DataSourceResult GetTask([ModelBinder(typeof(KendoSchedulerApi.ModelBinders.DataSourceRequestModelBinder))] DataSourceRequest request)
        {
            //var data = db.Tasks.ToList().Select(task => new TaskViewModel()
            //{
            //    TaskID = task.TaskID,
            //    Title = task.Title,
            //    Description = task.Description,
            //    Start = DateTime.SpecifyKind(task.Start, DateTimeKind.Utc),
            //    End = DateTime.SpecifyKind(task.End, DateTimeKind.Utc),
            //    IsAllDay = task.IsAllDay,
            //    RecurrenceID = task.RecurrenceID,
            //    RecurrenceRule = task.RecurrenceRule,
            //    RecurrenceException = task.RecurrenceException,
            //    StartTimezone = task.StartTimezone,
            //    EndTimezone = task.EndTimezone,
            //    OwnerID = task.OwnerID
            //});

            var result = SRBinderHelper.RetrunListOfOrders().ToList();

            var data = result.Select(task => new TaskViewModel()
            {
                TaskID = task.TaskID,
                Title = task.Title,
                Description = task.Description,
                Start = DateTime.SpecifyKind(task.Start, DateTimeKind.Utc),
                End = DateTime.SpecifyKind(task.End, DateTimeKind.Utc),
                IsAllDay = task.IsAllDay,
                RecurrenceID = task.RecurrenceID,
                RecurrenceRule = task.RecurrenceRule,
                RecurrenceException = task.RecurrenceException,
                StartTimezone = task.StartTimezone,
                EndTimezone = task.EndTimezone,
                OwnerID = task.OwnerID
            });
            //return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            return data.ToDataSourceResult(request);
        }

        // GET api/Task/5
        public SRViewModel GetTask(int id)
        {
            var result = SRBinderHelper.RetrunListOfOrders().ToList();
            var task = result.Where(p => p.TaskID == id).FirstOrDefault();
            
            if (task == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return task;
        }

        // PUT api/Task/5
        [HttpPut]
        public HttpResponseMessage PutTask(int id, [FromBody] TaskViewModel task)
        {
            if (ModelState.IsValid && id == task.TaskID)
            {
                var entity = new SRViewModel
                {
                    TaskID = task.TaskID,
                    Title = task.Title,
                    Start = task.Start,
                    StartTimezone = task.StartTimezone,
                    End = task.End,
                    EndTimezone = task.EndTimezone,
                    Description = task.Description,
                    RecurrenceRule = task.RecurrenceRule,
                    RecurrenceException = task.RecurrenceException,
                    RecurrenceID = task.RecurrenceID,
                    IsAllDay = task.IsAllDay,
                    OwnerID = task.OwnerID
                };

                //db.Tasks.Attach(entity);
                //db.Entry(entity).State = EntityState.Modified;

                //try
                //{
                //    db.SaveChanges();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    return Request.CreateResponse(HttpStatusCode.NotFound);
                //}
                SRBinderHelper.UpdateOrder(entity);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Task
        [HttpPost]
        public HttpResponseMessage PostTask(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                var entity = new SRViewModel
                {
                    TaskID = task.TaskID,
                    Title = task.Title,
                    Start = task.Start,
                    StartTimezone = task.StartTimezone,
                    End = task.End,
                    EndTimezone = task.EndTimezone,
                    Description = task.Description,
                    RecurrenceRule = task.RecurrenceRule,
                    RecurrenceException = task.RecurrenceException,
                    RecurrenceID = task.RecurrenceID,
                    IsAllDay = task.IsAllDay,
                    OwnerID = task.OwnerID
                };

                //db.Tasks.Add(entity);
                //db.SaveChanges();
                SRBinderHelper.AddOrder(entity);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, new { Data = new[] { task }, Total = 1 });
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = task.TaskID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Task/5
        public HttpResponseMessage DeleteTask(int id)
        {
            Task task = db.Tasks.Single(p => p.TaskID == id);
            if (task == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }



            try
            {
                SRBinderHelper.DeleteOrder(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, task);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
