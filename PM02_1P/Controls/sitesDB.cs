using PM02_1P.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PM02_1P.Controller
{
    public class SitesDB
    {
        readonly SQLiteAsyncConnection dbase;  
        
        public SitesDB(string dbpath)
        {
            dbase = new SQLiteAsyncConnection(dbpath);            
            dbase.CreateTableAsync<Sites>();             
        }    
        
        public Task<int> SaveSite(Sites site)
        {
            if (site.Id != 0)
            {
                return dbase.UpdateAsync(site);
            }
            else
            {
                return dbase.InsertAsync(site);
            }
        }
       
        public Task<List<Sites>> GetSiteList()
        {
            return dbase.Table<Sites>().ToListAsync();
        }

        public Task<Sites> GetSite(int sId)
        {
            return dbase.Table<Sites>()
                .Where(i => i.Id == sId)
                .FirstOrDefaultAsync();
        }
       
        public Task<int> DeleteSites(Sites site)
        {
            return dbase.DeleteAsync(site);
        }              
    }
}
