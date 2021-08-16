using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalog.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The game's name must be between 3 and 100 characters.")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The producer's name must be between 3 and 100 characters.")]
        public string Produtora { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "The price must be between 1 BRL and 1000 BRL.")]
        public double Preco { get; set; }
    }
}
