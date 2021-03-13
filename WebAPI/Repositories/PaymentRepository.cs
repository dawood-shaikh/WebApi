using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.Repositories
{
    public class PaymentRepository : IRepository<PaymentStatustbl>
    {
        readonly EntityDBContext _entityContext;
        public PaymentRepository(EntityDBContext context)
        {
            _entityContext = context;
        }

        public IEnumerable<PaymentStatustbl> GetAll()
        {
            return _entityContext.paymentStatustbls.ToList();
        }

        public PaymentStatustbl Get(int id)
        {
            return _entityContext.paymentStatustbls.FirstOrDefault(x => x.Id == id);
        }

        public void Add(PaymentStatustbl entity)
        {
            _entityContext.paymentStatustbls.Add(entity);
            _entityContext.SaveChanges();
        }

        public void Update(PaymentStatustbl  paymentStatustbl, PaymentStatustbl entity)
        {
            paymentStatustbl.Status = entity.Status;
            paymentStatustbl.CreditCardNumber = entity.CreditCardNumber;
            _entityContext.SaveChanges();
        }

        public void Delete(PaymentStatustbl  paymentStatustbl)
        {
            _entityContext.paymentStatustbls.Remove(paymentStatustbl);
            _entityContext.SaveChanges();
        }
    }
}
