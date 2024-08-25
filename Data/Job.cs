using System;
using System.Text.Json.Serialization;
using PersonManagementApi.Entities;

namespace PersonManagementApi.Data
{
	public class Job
	{
        public int Id { get; set; }
        public required int PersonneId { get; set; }
        public required Person Personne{ get; set; }
        public required string NomEntreprise { get; set; }
        public required string PosteOccupe { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
    }
}

