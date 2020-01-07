using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GRINTSYS.SAPRestApi.Models;
using GRINTSYS.SAPRestApi.Persistence.Repositories;

namespace GRINTSYS.SAPRestApi.Domain.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> GetAsync(int id)
        {
            return await _paymentRepository.GetAsync(id);
        }
        public Task UpdateAsync(Payment payment)
        {
            return _paymentRepository.UpdateAsync(payment);
        }
    }
}