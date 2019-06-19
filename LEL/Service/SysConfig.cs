using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class SysConfig
    {
        private static SysConfig m_SysConfig;
        public SortedSet<string> keys;
        public SortedList<string, le_sysconfig> values;

        public SysConfig()
        {

        }

        private static SysConfig Load()
        {
            using (Entities ctx = new Entities())
            {
                SysConfig sc = new SysConfig();
                sc.keys = new SortedSet<string>();
                sc.values = new SortedList<string, le_sysconfig>();
                var DbList = ctx.le_sysconfig.Where(s => true).OrderBy(s => s.ID);
                foreach (var model in DbList)
                {
                    sc.keys.Add(model.Name);
                    sc.values.Add(model.Name, model);
                }
                return sc;
            }
        }

        public static SysConfig Get()
        {
            if (m_SysConfig == null)
            {
                m_SysConfig = Load();
            }

            return m_SysConfig;
        }

        /// <summary>
        /// 强制刷新
        /// </summary>
        /// <returns></returns>
        public static SysConfig RefreshSysConfig()
        {
            m_SysConfig = Load();
            return m_SysConfig;
        }


    }
}
