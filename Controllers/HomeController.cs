using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VewMVC.Models;

namespace VewMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public static List<PersonModel> people = new List<PersonModel>(){

           new PersonModel{
               Id = 1,
               FirstName = "Pham",
               LastName = "Quan",
               Address = " Ha Noi",
               Gender = "Male"
           },
           new PersonModel{
               Id = 2,
               FirstName = "Pham",
               LastName = "Phong",
               Address = " Thai Binh",
               Gender = "FeMale"
           },
           new PersonModel{
               Id= 3,
               FirstName = "Cho",
               LastName = "Trang",
               Address = " Ha Noi",
               Gender = "Male"
           }

    };
    public IActionResult Index()
    {
        Set("QuanDepTraiCookie", "this my first cookie", 10);
        HttpContext.Session.SetString("QuanSesson", "This Is My MVC Session");
        return View(people);
    }
    public IActionResult AddPeople()
    {
        return View();
    }
    public IActionResult SampleView()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        var sessionValue = HttpContext.Session.GetString("QuanSesson");
        var result = Get("QuanDepTrai");
        ViewBag.QuanDepTrai = result;
        ViewBag.Quan = "Quan Dep Trai";
        return View();
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {

        var person = people.Where(x => x.Id == id).FirstOrDefault();
        return View(person);

    }
    [HttpPost]
    public IActionResult Edit(PersonModel person)
    {
        var EditPerson = (from person1 in people
                          where person1.Id == person.Id
                          select person1).FirstOrDefault();
        EditPerson.FirstName = person.FirstName;
        EditPerson.LastName = person.LastName;
        EditPerson.Gender = person.Gender;
        EditPerson.Address = person.Address;
        return RedirectToAction("Index");
    }
    public IActionResult Delete(int? id)
    {

        var person = people.Where(person => person.Id == id).FirstOrDefault();
        people.Remove(person);
        return RedirectToAction("Index");

    }

    [HttpPost]
    public IActionResult AddPeople(PersonModel model)
    {
        people.Add(model);
        return RedirectToAction("Index");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    private void Set(string key, string value, int? expireTime)
    {
        CookieOptions options = new CookieOptions();
        if (expireTime.HasValue)
        {
            options.Expires = DateTime.Now.AddMinutes(expireTime.Value);

        }
        else
        {
            options.Expires = DateTime.Now.AddSeconds(30);

        }
        Response.Cookies.Append(key, value, options);
    }
    private string Get(string key)
    {
        return Request.Cookies[key];
    }
    private void Remove(string key)
    {
        Response.Cookies.Delete(key);
    }
}
