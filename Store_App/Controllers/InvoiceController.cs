using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store_App.Helpers;
using Newtonsoft.Json;
using Store_App.Models.InvoiceModel;
using Store_App.Exceptions;

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
            try
            {
                IHttpContextHelper helper = new HttpContextHelper();
                int accountId = helper.GetAccountId(HttpContext);
                IInvoiceProcessor invoiceCreator = new InvoiceProcessorCreator().GetInvoiceCreator();
                IInvoice invoice = invoiceCreator.CreateInvoice(accountId, creditCard, billingAddress, shippingAddress);
                return JsonConvert.SerializeObject(invoice);
            } 
            catch (InvalidInputException)
            {
                return new StatusCodeResult(400);
            }
            catch (AccountNotFoundException)
            {
                return new StatusCodeResult(401);
            }
            catch (UnauthorizedException)
            {
                return new StatusCodeResult(401);
            }
            catch (ProductNotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (InvoiceNotFoundException)
            {
                return new StatusCodeResult(404);
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
            try
            {
                IHttpContextHelper helper = new HttpContextHelper();
                int accountId = helper.GetAccountId(HttpContext);
                IInvoice invoice = new InvoiceCreator().GetInvoice();
                invoice.AccessInvoice(invoiceId, accountId);

                return JsonConvert.SerializeObject(invoice);
            }
            catch (AccountNotFoundException)
            {
                return new StatusCodeResult(401);
            }
            catch (UnauthorizedException)
            {
                return new StatusCodeResult(401);
            }
            catch (ProductNotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (InvoiceNotFoundException)
            {
                return new StatusCodeResult(404);
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
            
            try
            {
                IHttpContextHelper helper = new HttpContextHelper();
                int accountId = helper.GetAccountId(HttpContext);
                IInvoiceList list = new InvoiceListCreator().GetInvoiceList();
                list.AccessInvoiceList(accountId);

                return JsonConvert.SerializeObject(list);
            }
            catch (AccountNotFoundException)
            {
                return new StatusCodeResult(401);
            }
            catch (UnauthorizedException)
            {
                return new StatusCodeResult(401);
            }
            catch (ProductNotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (InvoiceNotFoundException)
            {
                return new StatusCodeResult(404);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        } 
    }
}
