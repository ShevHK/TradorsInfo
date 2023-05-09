using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TradorsInformation.DB.Entities
{
    public class TradorInfo
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [NotNull]
        public string Name { get; set; }

        [StringLength(50)]
        [NotNull]
        public string Surname { get; set; }

        [StringLength(50)]
        [NotNull]
        public string Email { get; set; }

        [StringLength(50)]
        [NotNull]
        public string Phone { get; set; }
    }
}
