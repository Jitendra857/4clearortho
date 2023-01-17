using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;
namespace _4eOrtho.BAL
{
    public class PaymentFailureEntity : BaseEntity
    {
        /// <summary>
        ///  Method to create instance of new Payment
        /// </summary>
        /// <returns></returns>
        public PaymentFailure Create()
        {
            return orthoEntities.PaymentFailures.CreateObject();
        }

        /// <summary>
        ///  Method to Insert new Payment
        /// </summary>
        /// <param name="newPayment"></param>
        public void Save(PaymentFailure newPayment)
        {
            if (newPayment.EntityState == System.Data.EntityState.Detached)
            {
                orthoEntities.AddToPaymentFailures(newPayment);
            }
            orthoEntities.SaveChanges();
        }

        /// <summary>
        ///  Method to Insert new Payment
        /// </summary>
        /// <param name="newPayment"></param>
        public List<PaymentFailure> GetPaymentFailureListByWebURL()
        {
            return orthoEntities.PaymentFailures.ToList();
        }
    }
}
