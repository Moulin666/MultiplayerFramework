using System;
using System.Collections.Generic;
using System.Linq;
using MGF.Domain;

namespace MGF.Mappers
{
    public class StatMapper : MapperBase<Stat>
    {
        protected override Stat Delete(Stat domainObject)
        {
            if (null == domainObject)
                throw new ArgumentNullException(nameof(domainObject));

            // Immediately call delete now and returns the object.
            DeleteNow(domainObject.Id);
            return domainObject;
        }

        protected override void DeleteNow(int id)
        {
            using (MGFContext entities = new MGFContext())
            {
                DataEntities.Stat entity = new DataEntities.Stat { StatId = id };

                // Gets the character list and attaches the entity to the contain (makes this object exists in the list of objects).
                entities.Stats.Attach(entity);
                // Remove the character from the container.
                entities.Stats.Remove(entity);
                entities.SaveChanges();
            }
        }

        protected override IList<Stat> Fetch()
        {
            using (MGFContext entities = new MGFContext())
            {
                return entities.Stats
                    // Don't cash the entitiesin EF
                    .AsNoTracking()
                    // Order the entities by ID
                    .OrderBy(statEntity => statEntity.StatId)
                    // Execute the querry and return a list
                    .ToList()
                    // Using the list of entities, create new DomainBase Stat
                    .Select(statEntity => new Stat(statEntity.StatId, statEntity.Name, statEntity.Value))
                    // Return a List<Stat> of stats
                    .ToList();
            }
        }

        protected override Stat Fetch(int id)
        {
            Stat statObject = null;

            using (MGFContext entities = new MGFContext())
            {
                DataEntities.Stat entity = entities.Stats
                    // Eagerly grab this entities linkeg object - Stats.
                    //.Include(characterEntity => characterEntity.Stats)
                    .FirstOrDefault(statEntity => statEntity.StatId == id);

                if (entity != null)
                {
                    // Load data and extra data, such as linked objects or XML data etc.
                    statObject = new Stat(entity.StatId, entity.Name, entity.Value);
                }
            }

            return statObject;
        }

        protected override Stat Insert(Stat domainObject)
        {
            using (MGFContext entities = new MGFContext())
            {
                DataEntities.Stat entity = new DataEntities.Stat();
                Map(domainObject, entity);
                entities.Stats.Add(entity);
                domainObject = SaveChanges(entities, entity);
            }

            return domainObject;
        }

        protected override void Map(Stat domainObject, object entity)
        {
            DataEntities.Stat statEntity = entity as DataEntities.Stat;

            if (null == domainObject)
                throw new ArgumentNullException(nameof(domainObject));

            if (null == entity)
                throw new ArgumentNullException(nameof(entity));

            if (null == statEntity)
                throw new ArgumentOutOfRangeException(nameof(entity));

            // Map all fields from the domain object to the entity except the ID if it isn't allow to change 
            // (most IDs should never be changed).
            //statEntity.Id = domainObject.Id;

            statEntity.Name = domainObject.Name;
            statEntity.Value = domainObject.Value;
        }

        public void MapStat(Stat domainObject, object entity)
        {
            Map(domainObject, entity);
        }

        protected override Stat Update(Stat domainObject)
        {
            // Pull out the id because we'll be using it in a lambda that might be deferred when calling and the thread
            // may not have access to the domain object's context
            // (yay multithreading)
            int id;

            if (null == domainObject)
                throw new ArgumentNullException(nameof(domainObject));

            id = domainObject.Id;

            using (MGFContext entities = new MGFContext())
            {
                DataEntities.Stat entity = entities.Stats
                    .FirstOrDefault(statEntity => statEntity.StatId == id);

                if (entity != null)
                {
                    Map(domainObject, entity);
                    domainObject = SaveChanges(entities, entity);
                }
            }

            return domainObject;
        }

        private Stat SaveChanges(MGFContext entities, DataEntities.Stat entity)
        {
            // Save everything in the context (unit of work means it should only be this entity and anything it contains).
            entities.SaveChanges();

            // reload what the database has based on the ID that we modified.
            return Fetch(entity.StatId);
        }
    }
}
