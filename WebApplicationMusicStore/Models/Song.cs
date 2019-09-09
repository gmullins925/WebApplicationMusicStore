using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationMusicStore.Models
{
	public class Song
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Artist { get; set; }
		public string Album { get; set; }
		[Display(Name="Release Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime ReleaseDate { get; set; }
		public string Genre { get; set; }
		public string ImagePath { get; set; }
		public decimal Price { get; set; }
	}
}
