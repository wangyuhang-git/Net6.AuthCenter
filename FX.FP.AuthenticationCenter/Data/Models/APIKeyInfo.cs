using FX.FP.AuthenticationCenter.Data.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FX.FP.AuthenticationCenter.Data.Models
{
    public class APIKeyInfo : IEntity
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar(36)")]
        public string GUID { get; set; }

        [Required,Column(TypeName = "varchar(50)")]
        public string AppID { get; set; }

        [Required, Column(TypeName = "varchar(36)")]
        public string AppSecret { get; set; }

        [Required, Column(TypeName = "nvarchar(30)")]
        public string Area { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? PlatformName { get; set; }

        [Column(TypeName = "nvarchar(25)")]
        public string? LinkMan { get; set; }

        [Column(TypeName = "varchar(40)")]
        public string? LinkPhone { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ValidityDate { get; set; }

        public int? Status { get; set; }

        [MaxLength(220)]
        public string? BackReason { get; set; }

        public int? OrderNum { get; set; }

        [Column(TypeName = "varchar(120)")]
        public string AllowIps { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string ModifiedBy { get; set; }
    }
}
