using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PrivateLessons.Models
{
    public partial class book{
    public int id { get; set; }

    public string name { get; set; }
    public int subject_id { get; set; }

//public subject subject{get; set;}


        
    }}