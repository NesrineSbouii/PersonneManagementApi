﻿using System;
namespace PersonManagementApi.Entities
{
	public class JobDto
	{
        public int Id { get; set; }
        public int PersonneId { get; set; }
        public required string NomEntreprise { get; set; }
        public required string PosteOccupe { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
    }
}

