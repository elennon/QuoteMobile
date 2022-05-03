using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using QuotesMobile.Models;
using QuotesMobile.Views;

namespace QuotesMobile.Data
{
    public class QuotesDatabase
    {
        readonly SQLiteAsyncConnection database;

        public QuotesDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Quote>().Wait();
            database.CreateTableAsync<Receipt>().Wait();
        }

        public Task<List<Quote>> GetQuotesAsync()
        {
            //Get all Quote.
            return database.Table<Quote>().ToListAsync();
        }

        public Task<Quote> GetQuoteAsync(int id)
        {
            // Get a specific note.
            return database.Table<Quote>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<List<Quote>> GetQuotesAgreedAsync()
        {
            // Get a specific note.
            return database.Table<Quote>()
                            .Where(i => i.Agreed == true && i.AgreedDate != null && i.Finished == false)
                            .ToListAsync();
        }

        public Task<List<Quote>> GetJobsDoneAsync()
        {
            // Get a specific note.
            return database.Table<Quote>()
                            .Where(x => x.Finished == true)
                            .ToListAsync();
        }

        public Task<int> SaveQuoteAsync(Quote note)
        {
            if (note.ID != 0)
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

        public Task<int> DeleteQuoteAsync(Quote note)
        {
            // Delete a note.
            return database.DeleteAsync(note);
        }

        public Task<List<Receipt>> GetReceiptsAsync()
        {
            //Get all Quote.
            return database.Table<Receipt>().ToListAsync();
        }

        public Task<List<Receipt>> GetSpentByMonthAsync(Months m)
        {
            return database.Table<Receipt>().ToListAsync();
            //return database.Table<Receipt>()
            //                .Where(x => x.dateBought.Value.Month == m.Monthh)
            //                .ToListAsync();
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