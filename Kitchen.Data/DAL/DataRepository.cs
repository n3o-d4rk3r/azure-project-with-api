using Kitchen.Data.DataContexts;
using Kitchen.Data.Models;
using Kitchen.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Data.DAL
{

    public class DataRepository<TEntity> where TEntity : class
    {
        protected static KitchenContext _context;
        protected static IMongoCollection<TEntity> DbSet;
        //protected static  IMongoCollection<BsonDocument> DbSetRaw;

        public DataRepository(KitchenContext context)
        {
            _context = context;

            DbSet = _context.GetCollection<TEntity>($"{typeof(TEntity).Name}s");

            //DbSetRaw = _context.GetCollection<BsonDocument>("AuthNumModels");

        }

        public virtual Task Add(TEntity obj)
        {
            return _context.AddCommand(async () => await DbSet.InsertOneAsync(obj));
        }

        public virtual Task AddBulk(IEnumerable<TEntity> obj)
        {
            return _context.AddCommand(async () => await DbSet.InsertManyAsync(obj));
        }

        public virtual Task Update(UpdateDefinition<TEntity> obj, string field, string value)
        {

            return _context.AddCommand(async () =>
            {
                await DbSet.UpdateManyAsync(Builders<TEntity>.Filter.Eq(field, value), obj);

            });
        }


        public virtual async Task<TEntity> GetById(ObjectId id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq(" _id ", id));
            return data.FirstOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(FilterDefinition<TEntity> filter)
        {

            var all = await DbSet.FindAsync(filter);
            return all.ToList();
        }




        //public virtual async Task<IEnumerable<BsonDocument>> GetAllRaw(FilterDefinition<BsonDocument> filter)
        //{

        //    var all = await DbSetRaw.FindAsync(filter);
        //    return all.ToList();
        //}

        public virtual async Task<IEnumerable<TEntity>> GetAllPaged(FilterDefinition<TEntity> filter, int limit = 50, int offset = 1)
        {

            var data = await GetPagerAsync(offset, limit, DbSet, filter);
            return data;
        }


        public virtual async Task<Pager<TEntity>> GetPaged(FilterDefinition<TEntity> filter, int page = 1, int pageSize = 50)
        {
            var data = await GetPagerResultAsync(page, pageSize, DbSet, filter);
            return data;
        }


        public virtual async Task<long> GetCount(FilterDefinition<TEntity> filter)
        {
            var data = await GetCountResult(DbSet, filter);
            return data;
        }

        private static async Task<Pager<TEntity>> GetPagerResultAsync(int page, int pageSize, IMongoCollection<TEntity> collection, FilterDefinition<TEntity> filter)
        {
            // count facet, aggregation stage of count
            var countFacet = AggregateFacet.Create("countFacet",
                PipelineDefinition<TEntity, AggregateCountResult>.Create(new[]
                {
                PipelineStageDefinitionBuilder.Count<TEntity>()
                }));

            var dataFacet = AggregateFacet.Create("dataFacet",
                PipelineDefinition<TEntity, TEntity>.Create(new[]
                {
                PipelineStageDefinitionBuilder.Skip<TEntity>((page - 1) * pageSize),
                PipelineStageDefinitionBuilder.Limit<TEntity>(pageSize),
                }));

            var aggregation = await collection.Aggregate()
                .Match(filter)
                .Facet(countFacet, dataFacet)
                .ToListAsync();

            var count = aggregation.First()
                .Facets.First(x => x.Name == "countFacet")
                .Output<AggregateCountResult>()
                ?.FirstOrDefault()
                ?.Count ?? 0;

            var data = aggregation.First()
                .Facets.First(x => x.Name == "dataFacet")
                .Output<TEntity>();

            return new Pager<TEntity>()
            {
                Count = (int)count / pageSize,
                Size = pageSize,
                Page = page,
                Items = data
            };
        }
        private static async Task<IEnumerable<TEntity>> GetPagerAsync(int page, int pageSize, IMongoCollection<TEntity> collection, FilterDefinition<TEntity> filter)
        {


            var dataFacet = AggregateFacet.Create("dataFacet",
                PipelineDefinition<TEntity, TEntity>.Create(new[]
                {
                PipelineStageDefinitionBuilder.Skip<TEntity>((page - 1) * pageSize),
                PipelineStageDefinitionBuilder.Limit<TEntity>(pageSize),
                }));

            var aggregation = await collection.Aggregate()
                .Match(filter)
                .Facet(dataFacet)
                .ToListAsync();

            var data = aggregation.First()
                .Facets.First(x => x.Name == "dataFacet")
                .Output<TEntity>();
            return data;


        }

        private static async Task<long> GetCountResult(IMongoCollection<TEntity> collection, FilterDefinition<TEntity> filter)
        {
            // count facet, aggregation stage of count
            var countFacet = AggregateFacet.Create("countFacet",
                PipelineDefinition<TEntity, AggregateCountResult>.Create(new[]
                {
                PipelineStageDefinitionBuilder.Count<TEntity>()
                }));


            //var filter = Builders<TEntity>.Filter.Empty;
            var aggregation = await collection.Aggregate()
                .Match(filter)
                .Facet(countFacet)
                .ToListAsync();

            var count = aggregation.First()
                .Facets.First(x => x.Name == "countFacet")
                .Output<AggregateCountResult>()
                ?.FirstOrDefault()
                ?.Count ?? 0;

            return count;
        }

        public virtual async Task<TEntity> GetOne(FilterDefinition<TEntity> filter)
        {
            var data = await DbSet.Find(filter).FirstOrDefaultAsync();
            return data;
        }
        public virtual async Task<TEntity> GetById(string id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("ResidentID", id));
            return data.FirstOrDefault();
        }

        public virtual async Task Update(TEntity obj)
        {
            var filter = Builders<TEntity>.Filter.Eq("ResidentID", (obj as Resident).ResidentID);
            await DbSet.ReplaceOneAsync(filter, obj);
        }
        public async Task Delete(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("ResidentID", id);
            await DbSet.DeleteOneAsync(filter);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual async Task UpdateSingleAsync(string id, UpdateDefinition<TEntity> updateDefinition)
        {
            var filter = Builders<TEntity>.Filter.Eq("GroupID", id);
            await DbSet.UpdateOneAsync(filter, updateDefinition);
        }
        public async Task DeleteByGroupId(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("GroupID", id);
            await DbSet.DeleteOneAsync(filter);
        }

        public async Task<DeleteResult> DeleteMenuById(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("MenuCatalogID", id);
            return await DbSet.DeleteOneAsync(filter); // Returns the result of the delete operation
        }

    }
}
