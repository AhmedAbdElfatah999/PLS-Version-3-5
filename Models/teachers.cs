
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PrivateLessons.Models
{
   public partial class teachers
{
               [Key]
public int id { get; set; }
 public int user_id { get; set; }
 public double salary { get; set; }=1000f;
 

 //public ICollection <subject> subject{get; set;}


}
}