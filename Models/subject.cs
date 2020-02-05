
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PrivateLessons.Models
{
   public partial class subject
{
 public int id { get; set; }
 
   public string name { get; set; }

 [DataType(DataType.Date)]
  public System.DateTime date { get; set; }


 public  double price { get; set; }
 public string place { get; set; }
 public string time {get; set;}

 //subject can have many students (list of students)
 public ICollection <student_subject_registration> students{ get; set; }

public ICollection <book> book{get; set;}
public ICollection <exam> exam{get; set;}
 




}}