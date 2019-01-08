using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.BAL.RestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.Interfaces
{
    public interface IShipmentsService
    {
        ShipmentsRestModel GetShipmentByInvoiceNumber(string invoiceNumber);
        IEnumerable<ShipmentsRestModel> GetAllShipments();
        bool PostShipment(ShipmentModel model);
        //bool IsShipmentAllowed(IEnumerable<ShipmentLinesModel> model, out string message);
        bool IsInvoiceExisted(string invoiceNumber);
    }
}
