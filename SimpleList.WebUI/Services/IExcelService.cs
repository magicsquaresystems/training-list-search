using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// this is an interface for excel service 
namespace SimpleList.WebUI.Services
{
    public interface IExcelService
    {
        IEnumerable<T> ImportFromExcel<T>(byte[] excelData);
    }
}
