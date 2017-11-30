using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL.Interface
{
   public interface IPartialPage
    {
       int AddPartialPage(PartialPageModel obj);
       int UpdatePartialPage(PartialPageModel obj);
       PartialPageModel GetPartialPage(long? PartialPageId);
       PartialPageModel GetPartialByPageCode(string PageCode);
       List<PartialPageModel> GetAllPartialPage();
       void DeletePartialPage(long PartialPageId);
    }
}
