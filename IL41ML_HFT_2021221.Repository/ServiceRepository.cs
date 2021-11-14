using IL41ML_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Repository
{
    public class ServiceRepository : ARepository<Service>, IServiceRepository
    {
        private DbContext ctx;
        public ServiceRepository(DbContext ctx)
            : base(ctx)
        {
            this.ctx = ctx ?? throw new System.ArgumentNullException(nameof(ctx));
        }
        public void ChangeName(int id, string newName)
        {
            var service = this.GetOne(id);
            if (service == null)
            {
                throw new InvalidOperationException("Service not found");
            }

            service.ServiceName = newName;
            this.ctx.SaveChanges();
        }
        public void ChangePhoneNumber(int id, string newPhoneNumber)
        {
            var service = this.GetOne(id);
            if (service == null)
            {
                throw new InvalidOperationException("Service not found");
            }

            service.PhoneNr = newPhoneNumber;
            this.ctx.SaveChanges();
        }
        public void ChangeWeb(int id, string newWeb)
        {
            var service = this.GetOne(id);
            if (service == null)
            {
                throw new InvalidOperationException("Service not found");
            }

            service.WebPage = newWeb;
            this.ctx.SaveChanges();
        }
        public override Service GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.Id == id);
        }
        public override void Remove(int id)
        {
            this.Remove(this.GetOne(id));
        }
    }
}
