using Microsoft.AspNetCore.Http;
using SQLite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakhCuba.Models
{
    public class Images
    {
        [Key]
        public int Id { get; set; }
        public string ImageTitle { get; set; }
        [DisplayName("Имя картинки")]
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Загрузить картинку")]
        public IFormFile ImageFile { get; set; }

        public int NewsId { get; set; }
        public News News { get; set; }
    }
}
