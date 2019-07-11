using log4net;
using System;
using System.Linq;

namespace Service
{
    public class SysConfigServie
    {
        private static ILog log = LogManager.GetLogger(typeof(SysConfigServie));
        public bool UpdateSysConfig(le_sysconfig Dto)
        {
            using (Entities ctx = new Entities())
            {
                var model = ctx.le_sysconfig.Where(s => s.ID == Dto.ID).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                model.CName = Dto.CName;
                model.Description = Dto.Description;
                model.ID = Dto.ID;
                model.Name = Dto.Name;
                model.Value = Dto.Value;
                ctx.Entry<le_sysconfig>(model).State = System.Data.Entity.EntityState.Modified;
                try
                {

                    if (ctx.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    return false;
                }
                return false;
            }
        }


    }
}
