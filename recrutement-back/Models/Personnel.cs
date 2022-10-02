using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace AppRecrutement.Models
{
    public class Personnel
    {

        public Personnel()
        {
        }
        [Key]
        public Guid idPersonnel { get; set; }

        [Column]
        public string FullName { get; set; }

        [Column]
        public string email { get; set; }

        [Column]
        public string Pays { get; set; }

        [Column]
        public string Ville { get; set; }

        [Column]
        [Display(Name = "Niveau d'étude")]
        public string diplome { get; set; }



        [Column]
        public string Specialite { get; set; }


        [Column]
        [Display(Name = "Nombre d'années d'expérience")]
        public int Nb_annees_experience { get; set; }

        [Column]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de naissance")]
        public string Date_naissance { get; set; }

        public Guid DepartementId { get; set; }

        [ForeignKey("DepartementId")]
        public Departement departement { get; set; }
    }
}
