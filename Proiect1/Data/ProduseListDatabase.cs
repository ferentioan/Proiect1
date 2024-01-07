using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Threading.Tasks;
using Proiect1.Models;

namespace Proiect1.Data
{
    public class ProduseListDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public ProduseListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Produse>().Wait();
            _database.CreateTableAsync<Transport>().Wait();
            _database.CreateTableAsync<ListTransport>().Wait();
            _database.CreateTableAsync<Warehouse>().Wait();
        }
        public Task<List<Warehouse>> GetWarehousesAsync()
        {
            return _database.Table<Warehouse>().ToListAsync();
        }
        public Task<int> SaveWarehouseAsync(Warehouse shop)
        {
            if (shop.ID != 0)
            {
                return _database.UpdateAsync(shop);
            }
            else
            {
                return _database.InsertAsync(shop);
            }
        }
        public Task<List<Produse>> GetProdusesAsync()
        {
            return _database.Table<Produse>().ToListAsync();
        }
        public Task<Produse> GetProduseAsync(int id)
        {
            return _database.Table<Produse>()
            .Where(i => i.ID == id)
           .FirstOrDefaultAsync();
        }
        public Task<int> SaveProduseAsync(Produse slist)
        {
            if (slist.ID != 0)
            {
                return _database.UpdateAsync(slist);
            }
            else
            {
                return _database.InsertAsync(slist);
            }
        }
        public Task<int> DeleteProduseAsync(Produse slist)
        {
            return _database.DeleteAsync(slist);
        }
        public Task<int> SaveTransportAsync(Transport transport)
        {
            if (transport.ID != 0)
            {
                return _database.UpdateAsync(transport);
            }
            else
            {
                return _database.InsertAsync(transport);
            }
        }
        public Task<int> DeleteTransportAsync(Transport transport)
        {
            return _database.DeleteAsync(transport);
        }
        public Task<List<Transport>> GetTransportsAsync()
        {
            return _database.Table<Transport>().ToListAsync();
        }
        public Task<int> SaveListTransportAsync(ListTransport listp)
        {
            if (listp.ID != 0)
            {
                return _database.UpdateAsync(listp);
            }
            else
            {
                return _database.InsertAsync(listp);
            }
        }
        public Task<List<Transport>> GetListTransportsAsync(int produseid)
        {
            return _database.QueryAsync<Transport>(
            "select P.ID, P.Description from Transport P"
            + " inner join ListTransport LP"
            + " on P.ID = LP.TransportID where LP.ProduseID = ?",
            produseid);
        }
        public Task<int> DeleteListTransportAsync(int produseID, int transportID)
        {
            return _database.ExecuteAsync("DELETE FROM ListTransport WHERE ProduseID = ? AND TransportID = ?", produseID, transportID);
        }
        public Task<int> DeleteWarehouseAsync(Warehouse warehouse)
        {
            return _database.DeleteAsync(warehouse);
        }
    }
}
