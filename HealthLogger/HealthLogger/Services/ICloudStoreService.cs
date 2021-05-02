using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthLogger.Services
{
    public interface ICloudStoreService
    {
        Task Backup();
        Task Sync();
    }
}
