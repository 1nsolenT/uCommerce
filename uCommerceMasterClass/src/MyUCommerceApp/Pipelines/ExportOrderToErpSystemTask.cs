using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UCommerce;
using UCommerce.EntitiesV2;
using UCommerce.Pipelines;

namespace MyUCommerceApp.BusinessLogic.Pipelines
{
    public class ExportOrderToErpSystemTask : IPipelineTask<PurchaseOrder>, IUndoablePipelineTask<PurchaseOrder>
    {
        public PipelineExecutionResult Execute(PurchaseOrder subject)
        {
            string oderInformation = string.Format("{0}:{1} customer: {2}\r\n",
                subject.OrderNumber,
                new Money(subject.OrderTotal.GetValueOrDefault(), subject.BillingCurrency).ToString(),
                subject.Customer.EmailAddress);
            File.AppendAllText("C:\\Test\\Erp.txt", oderInformation);

            return PipelineExecutionResult.Success;
        }

        public PipelineExecutionResult Undo(PurchaseOrder subject)
        {
            throw new NotImplementedException();
        }
    }
}
