using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Test;

public record ExecuteWorkerConfig
{
    public bool MessageWorker { get; set; }
    public bool QuartzWorker { get; set; }
    public bool ChannalWorker { get; set; }
}
