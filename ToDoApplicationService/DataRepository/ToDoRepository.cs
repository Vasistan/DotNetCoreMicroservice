using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApplicationService.Database;
using ToDoApplicationService.Models;

namespace ToDoApplicationService.DataRepository
{
    public interface IToDoRepository
    {
        Task<ToDoItemDetail> AddNew(ToDoItemDetail todoItem);
        Task<List<ToDoItemDetail>> GetAll();
        Task<ToDoItemDetail> GetById(string id);
        Task<ToDoItemDetail> MarkAsDone(ToDoItemDetail todoItem);
    }

    public class ToDoRepository : IToDoRepository
    {
        private readonly IMongoDbConnection _connection;
        private readonly IMongoCollection<ToDoItemDetail> _collection;

        public ToDoRepository(IMongoDbConnection connection)
        {
            this._connection = connection;
            _collection = _connection.Database.GetCollection<ToDoItemDetail>("TodoItems");
        }
        public async Task<List<ToDoItemDetail>> GetAll()
        {
            return (await _collection.FindAsync(data => true)).ToList();
        }

        public async Task<ToDoItemDetail> GetById(string id)
        {
            return await (await _collection.FindAsync(data => data.Id==id)).FirstOrDefaultAsync();
        }

        public async Task<ToDoItemDetail> AddNew(ToDoItemDetail todoItem)
        {
            todoItem.Id = Guid.NewGuid().ToString();
            todoItem.ModifiedDate = todoItem.CreatedDate = DateTime.Now;

            await _collection.InsertOneAsync(todoItem);
            return todoItem;
        }

        public async Task<ToDoItemDetail> MarkAsDone(ToDoItemDetail todoItem)
        {
            todoItem.ModifiedDate = DateTime.Now;
            todoItem.IsDone = true;

            await _collection.ReplaceOneAsync(items => items.Id == todoItem.Id, todoItem);
            return todoItem;
        }
    }
}
