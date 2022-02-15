using System.ComponentModel;        
namespace VewMVC.Models
{
    public class PersonModel
    {
        public int Id{get; set;}
        public string LastName{get; set;} = "Default LastName";
        public string FirstName{get; set;} = "Default FisrtName";
        public string Address{get; set;} = "Default Address";
        public string Gender{get; set;} = "Default Gender";

    }
}