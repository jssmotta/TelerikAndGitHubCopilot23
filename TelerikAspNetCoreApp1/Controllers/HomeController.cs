using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;
using System.Net.Mail;
using TelerikAspNetCoreApp1.Model;
using TelerikAspNetCoreApp1.Models;


namespace TelerikAspNetCoreApp1.Controllers;
public class HomeController : Controller
{

    [HttpGet]
    public IActionResult Admission()
    {
        return View();
    }

    private const string pEmailFrom = "fake@example.com";
    private const string pEmailTo = "destination@example.com";

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Admission(Admission admission)
    {
        if (ModelState.IsValid)
        {
            // Send email
            var mail = new MailMessage(pEmailFrom, pEmailTo)
            {
                Subject = admission.Subject,
                Body = $"Name: {admission.Name}\nDate Birthday: {admission.DateBirthday}\nEmail: {admission.Email}\nDescription: {admission.Description}\nReturn Date: {admission.ReturnDate}"
            };

            var smtpServer = new SmtpClient("smtp.example.com");
            smtpServer.Send(mail);

            return RedirectToAction("Index");
        }

        return View();
    }


    public IActionResult Success()
    {
        return View();
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!string.IsNullOrEmpty(context.HttpContext.Request.Query["culture"]))
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(context.HttpContext.Request.Query["culture"]);
        }
        base.OnActionExecuting(context);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Email(ContactFormModel model)
    {
        if (ModelState.IsValid)
        {
            // TODO: Send email using the data in the model
            // You might use a service like SendGrid or SMTP to send the email
            return RedirectToAction("Index");
        }
        return View(model);
    }


    public IActionResult Email()
    {
        var model = new ContactFormModel();
        return View(model);
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        ViewData["Message"] = "Your application description page.";

        return View();
    }

    public IActionResult Contact()
    {
        ViewData["Message"] = "Your contact page.";

        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}
