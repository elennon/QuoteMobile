using QuotesMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuotesMobile.Data
{
    public class ReceiptsDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ReceiptsDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Receipt>().Wait();
        }

        public Task<List<Receipt>> GetReceiptsAsync()
        {
            //Get all Quote.
            return database.Table<Receipt>().ToListAsync();
        }

        public Task<Receipt> GetReceiptAsync(int id)
        {
            // Get a specific note.
            return database.Table<Receipt>()
                            .Where(i => i.id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveReceiptAsync(Receipt note)
        {
            if (note.id != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(note);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(note);
            }
        }

        public Task<int> DeleteReceiptAsync(Receipt note)
        {
            // Delete a note.
            return database.DeleteAsync(note);
        }
    }
}
