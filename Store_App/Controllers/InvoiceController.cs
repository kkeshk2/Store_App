using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store_App.Helpers;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Store_App.Models.CartModel;
using Store_App.Models.InvoiceModel;

namespace Store_App.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        [HttpGet("checkout")]
        [Authorize("ValidUser")]
        public ActionResult<string> Test(string creditCard, string billingAddress, string shippingAddress)
        {
            int? userId = HttpContextHelper.GetUserId(HttpContext);
            if (userId == null) return new StatusCodeResult(401);
            try
            {
                ICreditCardValidator validator = new CreditCardValidator();
                string last4 = validator.GetLast4(creditCard);
                IAddress billingAddressObject = new Address();
                billingAddressObject.AddAddress(billingAddress);
                IAddress shippingAddressObject = new Address();
                shippingAddressObject.AddAddress(shippingAddress);
                IInvoice invoice = new Invoice();
                invoice.CreateInvoice((int) userId, last4, billingAddressObject, shippingAddressObject);
                return JsonConvert.SerializeObject(invoice);
            }
            catch (ArgumentException)
            {
                return new StatusCodeResult(400);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(400);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("accessinvoice")]
        [Authorize("ValidUser")]
        public ActionResult<string> GetInvoice(int invoiceId)
        {
            int? userId = HttpContextHelper.GetUserId(HttpContext);
            if (userId == null) return new StatusCodeResult(401);

            try
            {
                IInvoice invoice = new Invoice();
                invoice.AccessInvoice(invoiceId);

                return JsonConvert.SerializeObject(invoice);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(400);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("accessinvoicelist")]
        [Authorize("ValidUser")]
        public ActionResult<string> GetInvoiceList()
        {
            int? userId = HttpContextHelper.GetUserId(HttpContext);
            if (userId == null) return new StatusCodeResult(401);

            try
            {
                IInvoiceList list = new InvoiceList();
                list.AccessInvoiceList((int) userId);

                return JsonConvert.SerializeObject(list);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(400);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }


        }

        [HttpGet("accesslastinvoice")]
        [Authorize("ValidUser")]
        public ActionResult<string> GetInvoiceLast()
        {
            int? userId = HttpContextHelper.GetUserId(HttpContext);
            if (userId == null) return new StatusCodeResult(401);

            try
            {
                IInvoice invoice = new Invoice();
                invoice.AccessLastInvoice((int) userId);

                return JsonConvert.SerializeObject(invoice);
            }
            catch (InvalidOperationException)
            {
                return new StatusCodeResult(400);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }   
    }
}
