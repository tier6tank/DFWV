using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using System.Windows.Forms;

namespace DFWV.WorldClasses
{
    /// <summary>
    /// This class, a property of World, stores settings for how objects are currently being filtered.
    /// It also stores details on what sort of fields should be filterable/sortable.
    /// </summary>
    class FilterSettings
    {
        Dictionary<Type, Filter> Filters = new Dictionary<Type, Filter>();
        public Dictionary<Type, Dictionary<string, Type>> Fields = new Dictionary<Type, Dictionary<string, Type>>();
        //public Dictionary<string, List<string>> Options = new Dictionary<string, List<string>>();
        public Dictionary<Type, Dictionary<string, IEnumerable<string>>> Options = new Dictionary<Type, Dictionary<string, IEnumerable<string>>>();
        private World World;

        public FilterSettings(World world)
        {
            World = world;
            LoadFields();
            LoadOptions();
            SetDefaultFilters();
        }

        /// <summary>
        /// Loads the set of all fields that can be filtered on/sorted on, based on World Object Type.
        /// They're stored as Dictionary<string, Type> because when displaying the options on the FilterForm different options 
        ///     should be possible for bools then strings (for example, no Contains() for bool)
        /// The string in the Dictionary is a string that is the same as a Property of the Type it's being added to.
        /// This is used with the Dynamic Query function to let you filter out all Artifacts that have a Name that Contains "dwarf" for example.
        /// </summary>
        private void LoadFields()
        {
            Fields[typeof(Artifact)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) }, 
                    { "Lost", typeof(bool) } };
            Fields[typeof(Civilization)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) },
                    };
            Fields[typeof(Entity)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) }, { "isPlayerControlled", typeof(bool) },
                    { "MemberCount", typeof(int) }};
            Fields[typeof(EntityPopulation)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) },
                    { "MemberCount", typeof(int) }, { "Battles", typeof(int) }};
            Fields[typeof(God)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string)}, 
                    { "CivilizationCount", typeof(int) }, { "SphereCount", typeof(int) }, { "LeaderCount", typeof(int) }};
            Fields[typeof(HistoricalEra)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) }, 
                    { "StartYear", typeof(int) } };
            Fields[typeof(HistoricalEvent)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) },
                    { "Year", typeof(int) }, { "InCollection", typeof(bool) } } ;
            Fields[typeof(HistoricalEventCollection)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) },
                    { "StartYear", typeof(int) }, { "Combatants", typeof(int) }, { "EventCollectionType", typeof(string) },
                    { "Casualties", typeof(int) }, { "Battles", typeof(int) } };
            Fields[typeof(HistoricalFigure)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) }, 
                    { "BirthYear", typeof(int) }, { "isPlayerControlled", typeof(bool) }, 
                    { "EntPopID", typeof(int) }, { "Deity", typeof(bool) }, { "Force", typeof(bool) }, { "Ghost", typeof(bool) },
                    { "Dead", typeof(bool) }, { "Animated", typeof(bool) }, 
                    { "inEntPop", typeof(bool) },  { "isLeader", typeof(bool) },  { "isGod", typeof(bool) }, 
                    { "CreatedArtifactCount", typeof(int) }, { "CreatedMasterpieceCount", typeof(int) }, { "ChildrenCount", typeof(int) }, 
                    { "Kills", typeof(int) }, { "Battles", typeof(int) }, { "DescendentCount", typeof(int) }, { "AncestorCount", typeof(int) }, 
                    { "DescendentGenerations", typeof(int) } };
               
            Fields[typeof(Leader)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) }, 
                    { "Inheritance", typeof(string) }, { "Married", typeof(bool) } };
            Fields[typeof(Parameter)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) } };
            Fields[typeof(Race)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) },
                    { "Population", typeof(int) }, { "CivCount", typeof(int) }, { "LeaderCount", typeof(int) }, 
                    { "HFCount", typeof(int) }, };
            Fields[typeof(Region)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) }, 
                    { "Battles", typeof(int) }, { "InhabitantCount", typeof(int) } };
            Fields[typeof(Site)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) }, 
                    { "AltName", typeof(string) }, { "isPlayerControlled", typeof(bool) }, { "Parent.Name", typeof(string) }, 
                    { "HFInhabitantCount", typeof(int) } , { "TotalPopulation", typeof(int) } };
            Fields[typeof(UndergroundRegion)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) } };
            Fields[typeof(WorldConstruction)] = new Dictionary<string, Type>() { { "ID", typeof(int) }, { "Name", typeof(string) } };
            Fields[typeof(Dynasty)] = new Dictionary<string, Type>() { { "Name", typeof(string) }, { "Duration", typeof(int) }, { "MemberCount", typeof(int) } };
        }


        /// <summary>
        /// TODO: Future expansion to allow a filter/sort type that is a list of strings, so you can: Select Historical Figures that have Caste, Dropdown pick "Male"
        /// </summary>
        private void LoadOptions()
        {
            Options[typeof(Civilization)] = new Dictionary<string, IEnumerable<string>>() { { "Race.Name", World.Races.Keys }};
            Options[typeof(Entity)] = new Dictionary<string, IEnumerable<string>>() { { "Race.Name", World.Races.Keys }, 
                    { "Type", new List<string>() {"Civ","Religion","Group","Other"} } };
            Options[typeof(EntityPopulation)] = new Dictionary<string, IEnumerable<string>>() { { "Race.Name", World.Races.Keys } };
            Options[typeof(God)] = new Dictionary<string, IEnumerable<string>>() { { "Race.Name", World.Races.Keys }, 
                    { "Type", new List<string>() {"deity","force"} } };
            Options[typeof(HistoricalEvent)] = new Dictionary<string, IEnumerable<string>>() { { "EventType", HistoricalEvent.Types } };

            Options[typeof(HistoricalEventCollection)] = new Dictionary<string, IEnumerable<string>>() { { "EventCollectionType", HistoricalEventCollection.Types} 
                        };
            Options[typeof(HistoricalFigure)] = new Dictionary<string, IEnumerable<string>>() { { "Job", HistoricalFigure.AssociatedTypes }, 
                        { "HFCaste", HistoricalFigure.Castes }, { "Race.Name", World.Races.Keys } };
            Options[typeof(Leader)] = new Dictionary<string, IEnumerable<string>>() { { "Race.Name", World.Races.Keys } };
            Options[typeof(Region)] = new Dictionary<string, IEnumerable<string>>() { { "RegionType", Region.Types } };
            Options[typeof(Site)] = new Dictionary<string, IEnumerable<string>>() { { "SiteType", Site.Types } };
            

            // Quick check to verify no option is concerning the same property as a Field
            foreach (var fieldSet in Fields)
            {
                foreach (var field in fieldSet.Value.Keys)
                {
                    if (Options.ContainsKey(fieldSet.Key) && Options[fieldSet.Key].ContainsKey(field))
                        Options[fieldSet.Key].Remove(field);
                }
            }
        }


        /// <summary>
        /// All the default filters, generally they are for sorting, but I also remove some details which almost no one will care about.  
        ///     Normally huge sets are limited to 50k items by default, to not kill an unsuspecting user who accidentally clicks the Historical Events tab.
        ///     Though this can be changed if desired using the FilterForm.
        /// </summary>
        public void SetDefaultFilters()
        {
            this[typeof(Artifact)] = new Filter("Name", null, -1);
            this[typeof(Civilization)] = new Filter("Name", "IsFull = true", -1);
            this[typeof(Entity)] = new Filter(new List<string>() { "Name", "Type" }, null, -1);
            this[typeof(EntityPopulation)] = new Filter(new List<string>() { "ID", "Race = null" }, null, -1);
            this[typeof(God)] = new Filter("Name", null, -1);
            this[typeof(HistoricalEra)] = new Filter("StartYear", null, -1);
            this[typeof(HistoricalEvent)] = new Filter("Year", null, 50000);
            this[typeof(HistoricalEventCollection)] = new Filter(new List<string>() { "StartYear" }, null, 50000);
            this[typeof(HistoricalFigure)] = new Filter("Name", null, 50000);
            this[typeof(Leader)] = new Filter("Name", null, -1);
            this[typeof(Parameter)] = new Filter();
            this[typeof(Race)] = new Filter(new List<string>() { "Name", "!isCivilized" }, null, -1);
            this[typeof(Region)] = new Filter("Name", null, -1);
            this[typeof(Site)] = new Filter("Name", null, -1);
            this[typeof(UndergroundRegion)] = new Filter(new List<string>() { "Depth", "Name" }, null, -1);
            this[typeof(WorldConstruction)] = new Filter();
            this[typeof(Dynasty)] = new Filter();

        }


        /// <summary>
        /// Indexer to add, change, or return filters.
        /// </summary>
        public Filter this[Type T]
        {
            get
            {
                return Filters[T];
            }
            set
            {
                if (!Filters.ContainsKey(T))
                    Filters.Add(T, value);
                else
                    Filters[T] = value;

            }
        }

    }

    /// <summary>
    /// There is one "Filter" for each Filterable World Object type within the FilterSettings Class.
    /// Filters have a set of "Where" conditions, which restrict what items will be returned when data is returned
    ///     A set of "OrderBy" conditions which control sorting
    ///     and a "Take" property which controls if there is a limit on items returned.
    /// </summary>
    class Filter
    {
        public List<string> Where = new List<string>();
        public List<string> OrderBy = new List<string>();

        public int Take { get; set; }


        /// <summary>
        /// Filters can be created with one, many, or zero orderby/where clauses.
        /// </summary>
        public Filter(string orderby, string where, int take)
        {
            if (orderby != null)
                OrderBy.Add(orderby);

            if (where != null)
                Where.Add(where);
            Take = take;
        }
        public Filter(List<string> orderby, List<string> where, int take)
        {
            if (orderby != null)
                OrderBy = orderby;
            if (where != null)
                Where = where;
            Take = take;
        }
        public Filter()
        {
            Take = -1;
        }
          

        /// <summary>
        /// Returns an array of "T" where T is a WorldObject.  
        ///     Given a list of objects this function creates a query on that list, adds all the appropriate clauses, then runs that query, returning the results.
        ///     If there is some issue with the query, all the clauses are wiped and the top 50k of that list is returned.
        /// </summary>
        public T[] Get<T>(List<T> list)
        {
            IQueryable<T> query = list.AsQueryable();

            try
            {
                foreach (string where in Where)
                    query = query.Where(where);
                foreach (string orderby in OrderBy)
                {
                    if (orderby.StartsWith("-"))
                        query = query.OrderBy(orderby.Substring(1) + " desc");
                    else
                        query = query.OrderBy(orderby);
                }
                if (Take > 0)
                    query = query.Take(Take);
                return query.ToArray();
            }
            catch (Exception)
            {
                string ErrorMessage = "Filter error, returning defaults" + Environment.NewLine +
                                    typeof(T).ToString() + Environment.NewLine;

                foreach (string where in Where)
                    ErrorMessage += "Where: " + where + Environment.NewLine;
                foreach (string orderby in OrderBy)
                    ErrorMessage += "OrderBy: " + orderby + Environment.NewLine;
                
                ErrorMessage += "Take: " + Take;

                MessageBox.Show(ErrorMessage, "DF World Viewer", MessageBoxButtons.OK);
                               
                Where.Clear();
                OrderBy.Clear();
                return list.AsQueryable().Take(50000).ToArray(); ;
            }

        }

    }
}
