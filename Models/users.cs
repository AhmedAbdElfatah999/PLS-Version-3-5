using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PrivateLessons.Models
{
    public partial class users{

//id,username,password,f_name,l_name,email,type_id
[Key]
public int id { get; set; }
[Required(ErrorMessage="Required filled"),MinLength(5),MaxLength(20)]
public string username { get; set; }

[Required,MinLength(15),MaxLength(25),DataType(DataType.Password)]
public string password { get; set; }
[Required]
public string f_name { get; set; }
[Required]
public string l_name { get; set; }
[DataType(DataType.EmailAddress)]

public string email { get; set; }

public int type_id { get; set; }











    }


}