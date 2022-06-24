using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace jtcWeather.Services
{
    public class DataRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        SQLiteAsyncConnection conn;

        private async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<Favourite>();
        }

        public DataRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<bool> AddAsync(Favourite favourite)
        {
            await Init();
            
            int result = 0;

            try
            {
                if (string.IsNullOrEmpty(favourite.Item))
                    throw new Exception("Valid Loation Required");

                if (favourite.Id > 0)
                {
                    //  Update Currrent Record
                    result = await conn.UpdateAsync(favourite);

                    StatusMessage = string.Format("{0} record(s) updated (Location: {1})", result, favourite.Item);
                }
                else
                {
                    //  Insert New Record
                    result = await conn.InsertAsync(favourite);

                    StatusMessage = string.Format("{0} record(s) added (Location: {1})", result, favourite.Item);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error {1}", favourite.Item, ex.Message);
            }
            

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Favourite favourite)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await Init();

            await conn.DeleteAsync<Favourite>(id);
            return await Task.FromResult(true);
        }

        public async Task<Favourite> GetAsync(int id)
        {
            await Init();

            return await conn.Table<Favourite>().Where(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Favourite>> GetAllAsync()
        {
            await Init();

            return await Task.FromResult(await conn.Table<Favourite>().ToListAsync());
        }

    }
}
