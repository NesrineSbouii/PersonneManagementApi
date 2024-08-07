using System;
namespace PersonManagementApi.Entities
{
	public class Personne
	{
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public ICollection<Emploi> Emplois { get; set; }
    }
}

