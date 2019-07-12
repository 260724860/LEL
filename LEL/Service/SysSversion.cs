using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SysSversionService
    {
        public bool CreateOrUpdate(int? id, string MajorVersion, string ViversionNumber, string Description, string LeftoverBug, string Remarks,out string msg)
        {
            using (Entities ctx = new Entities())
            {
                if (id == 0 || id == null)
                {
                    le_sysversion model = new le_sysversion();
                    model.MajorVersion = MajorVersion;
                    model.ViversionNumber = ViversionNumber;
                    model.Description = Description;
                    model.LeftoverBug = LeftoverBug;
                    model.Remarks = Remarks;
                    model.CreateTime = DateTime.Now;
                        ctx.le_sysversion.Add(model);
                    
                }
                else
                {
                    var model = ctx.le_sysversion.Where(s => s.ID == id).FirstOrDefault();
                    model.MajorVersion = MajorVersion;
                    model.ViversionNumber = ViversionNumber;
                    model.Description = Description;
                    model.LeftoverBug = LeftoverBug;
                    model.Remarks = Remarks;
                    ctx.Entry<le_sysversion>(model).State = System.Data.Entity.EntityState.Modified;

                }
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    return false;
                }
                msg = "未知错误";
                return false;
            }

        }

        public List<le_sysversion> GetSysVersionList()
        {
            using (Entities ctx=new Entities())
            {
                var reuslt = ctx.le_sysversion.Where(s => true).ToList();
                return reuslt;
            }
        }
    }
}
