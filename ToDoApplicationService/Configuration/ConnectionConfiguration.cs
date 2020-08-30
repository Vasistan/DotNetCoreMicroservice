using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApplicationService.Configuration
{
    public class ConnectionConfiguration
    {
        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string Database { get; set; }

        public int RetryAttemptsOnTimeout { get; set; } = 1;

        public int RetryWaitSeconds { get; set; } = 5;
    }
}
