using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppRecrutement.Models
{
    //[Table("Departements")]
    public class Departement
    {

        public Departement()
        {
        }


        [Key]
        public Guid DepartementID { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }

        [JsonIgnore]

        public virtual ICollection<Personnel> Personnels { get; set; }

    }
}
