using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QrCodeGenerator.DataStorage
{
    internal class QrCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Code { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}