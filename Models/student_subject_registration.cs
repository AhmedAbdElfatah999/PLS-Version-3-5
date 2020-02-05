
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace PrivateLessons.Models
{
    public partial class student_subject_registration{
            [Key]
       public int id { get; set; }
       public int student_id { get; set; }
       public int subject_id { get; set; }
       public int teacher_id { get; set; }
       public double grade { get; set; }
       [DataType(DataType.Date)]
       public System.DateTime date { get; set; }

       [ForeignKey(nameof(student_id))]
       public students students { get; set; }

       [ForeignKey(nameof(subject_id))]
       public subject subject { get; set; }

        [ForeignKey(nameof(teacher_id))]
       public teachers teachers { get; set; }


    }}