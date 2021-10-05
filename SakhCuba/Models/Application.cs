using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SakhCuba.Models
{
    public class Application
    {
        public int Id { get; set; }
        [Display(Name="Ваше имя")]
        [Required (ErrorMessage = "Необходимо ваше имя")]
        public string Name { get; set; }

        [Display(Name = "Ник в игре")]
        [Required(ErrorMessage = "Без ника мы не будем вас добавлять на сервер")]
        public string Nickname { get; set; }

        [Display(Name = "Имя в дискорде в формате Вася#1234")]
        [RegularExpression(@"\w+#\d+", ErrorMessage ="Ваш ник не подходит по формату")]
        public string DiscordName { get; set; }

        [Display(Name = "Ваш возраст")]
        [Required(ErrorMessage = "Необходим ваш возраст")]
        [Range(1, 150, ErrorMessage = "Недопустимый возраст")]
        public float Age { get; set; }

        [Display(Name = "Расскажите о себе")]
        [Required(ErrorMessage = "Распишите хотя бы что-нибудь о себе")]
        [StringLength(10000)]
        [DataType(DataType.MultilineText)]
        public string About { get; set; }

        [Display(Name = "Ваша любимая книга")]
        [Required(ErrorMessage = "Если у вас нет любимой книги, напишите любой другой предмет искусства")]
        public string FavoriteBook { get; set; }

        [Display(Name = "Почему вы хотите играть на нашем сервере?")]
        [Required(ErrorMessage = "Заполните пожалуйста")]
        [DataType(DataType.MultilineText)]
        public string ReasonToPlay { get; set; }

        [Display(Name = "Прочитали ли вы правила?")]
        [Required(ErrorMessage = "Укажите, что вы прочитали правила")]
        public string Rules { get; set; }

        public DateTime Date { get; set; }

        public string IP { get; set; }
        
        public int? DecisionId { get; set; }
        public Decision Decision { get; set; }
    }
}
