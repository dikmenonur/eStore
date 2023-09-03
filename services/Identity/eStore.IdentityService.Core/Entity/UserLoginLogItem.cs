using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.IdentityService.Core.Entity
{
    [Table("UserLoginLogItem")]
    public class UserLoginLogItem
    {
        public long Id { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long UserId { get; set; }

        public bool IsSuccess { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string RemoteHost { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Stamp { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
