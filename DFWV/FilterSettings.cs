using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DFWV.WorldClasses;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using System.Linq.Dynamic;

namespace DFWV
{
    /// <summary>
    /// This class, a property of World, stores settings for how objects are currently being filtered.
    /// It also stores details on what sort of fields should be filterable/sortable.
    /// </summary>
    public class FilterSettings
    {
        private readonly Dictionary<Type, Filter> Filters = new Dictionary<Type, Filter>();

        public readonly Dictionary<Type, Dictionary<string, Type>> Fields =
            new Dictionary<Type, Dictionary<string, Type>>();

        public readonly Dictionary<Type, Dictionary<string, IEnumerable<string>>> Options =
            new Dictionary<Type, Dictionary<string, IEnumerable<string>>>();

        private readonly World World;

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
            Fields[typeof (Artifact)] = new Dictionary<string, Type>
            {
                {"Type", typeof (string)},
                {"Material", typeof (string)},
                {"Lost", typeof (bool)},
                {"Value", typeof (int)}
            };
            Fields[typeof (Civilization)] = new Dictionary<string, Type>
            {
                {"isFull", typeof (bool)}
            };
            Fields[typeof (Entity)] = new Dictionary<string, Type>
            {
                {"isPlayerControlled", typeof (bool)},
                {"MemberCount", typeof (int)},
                {"EventCount", typeof (int)}
            };
            Fields[typeof (EntityPopulation)] = new Dictionary<string, Type>
            {
                {"MemberCount", typeof (int)},
                {"Battles", typeof (int)}
            };
            Fields[typeof (God)] = new Dictionary<string, Type>
            {
                {"CivilizationCount", typeof (int)},
                {"SphereCount", typeof (int)},
                {"LeaderCount", typeof (int)}
            };
            Fields[typeof (HistoricalEra)] = new Dictionary<string, Type>
            {
                {"StartYear", typeof (int)}
            };
            Fields[typeof (HistoricalEvent)] = new Dictionary<string, Type>
            {
                {"Year", typeof (int)},
                {"InCollection", typeof (bool)}
            };
            Fields[typeof (HistoricalEventCollection)] = new Dictionary<string, Type>
            {
                {"StartYear", typeof (int)},
                {"Combatants", typeof (int)},
                {"Casualties", typeof (int)},
                {"Battles", typeof (int)}
            };
            Fields[typeof (HistoricalFigure)] = new Dictionary<string, Type>
            {
                {"BirthYear", typeof (int)},
                {"PlayerControlled", typeof (bool)},
                {"EntPopID", typeof (int)},
                {"Deity", typeof (bool)},
                {"Force", typeof (bool)},
                {"Ghost", typeof (bool)},
                {"Dead", typeof (bool)},
                {"Animated", typeof (bool)},
                {"inEntPop", typeof (bool)},
                {"isLeader", typeof (bool)},
                {"isGod", typeof (bool)},
                {"CreatedArtifactCount", typeof (int)},
                {"CreatedMasterpieceCount", typeof (int)},
                {"ChildrenCount", typeof (int)},
                {"Kills", typeof (int)},
                {"Battles", typeof (int)},
                {"DescendentCount", typeof (int)},
                {"AncestorCount", typeof (int)},
                {"DescendentGenerations", typeof (int)},
                {"Flags", typeof (int)},
                {"EventCount", typeof (int)}
            };

            Fields[typeof (Leader)] = new Dictionary<string, Type>
            {
                {"Inherited", typeof (bool)},
                {"Married", typeof (bool)}
            };
            Fields[typeof (Parameter)] = new Dictionary<string, Type>();
            Fields[typeof (Race)] = new Dictionary<string, Type>
            {
                {"Population", typeof (int)},
                {"CivCount", typeof (int)},
                {"LeaderCount", typeof (int)},
                {"HFCount", typeof (int)}
            };
            Fields[typeof (Region)] = new Dictionary<string, Type>
            {
                {"Battles", typeof (int)},
                {"InhabitantCount", typeof (int)}
            };
            Fields[typeof (Site)] = new Dictionary<string, Type>
            {
                {"AltName", typeof (string)},
                {"isPlayerControlled", typeof (bool)},
                {"Parent.Name", typeof (string)},
                {"HFInhabitantCount", typeof (int)},
                {"TotalPopulation", typeof (int)},
                {"CreatedArtifactCount", typeof (int)},
                {"EventCount", typeof (int)}
            };
            Fields[typeof (Structure)] = new Dictionary<string, Type>
            {
                {"isRazed", typeof (bool)},
                {"Tomb", typeof (bool)}
            };
            Fields[typeof (UndergroundRegion)] = new Dictionary<string, Type>();
            Fields[typeof (WorldConstruction)] = new Dictionary<string, Type>();
            Fields[typeof(Dynasty)] = new Dictionary<string, Type>
            {
                {"Duration", typeof (int)},
                {"MemberCount", typeof (int)}
            };
            Fields[typeof(River)] = new Dictionary<string, Type>();
            Fields[typeof(Mountain)] = new Dictionary<string, Type>
            {
                {"Height", typeof (int)},
            };

            foreach (var type in new List<Type>
            {
                typeof(Artifact), typeof(Civilization), typeof(Entity), typeof(EntityPopulation), typeof(God), typeof(HistoricalEra), typeof(HistoricalEvent),
                typeof(HistoricalEventCollection), typeof(HistoricalFigure), typeof(Leader), typeof(Parameter), typeof(Race), typeof(Region), typeof(Site),
                typeof(Structure), typeof(UndergroundRegion), typeof(WorldConstruction), typeof(Dynasty), typeof(River), typeof(Mountain)
            })
            {
                Fields[type].Add("Name", typeof (string));
                if (!type.IsSubclassOf(typeof (XMLObject))) continue;
                Fields[type].Add("ID", typeof(int));
                Fields[type].Add("Notability", typeof(int));
            }
        }


        /// <summary>
        /// Loads the set of all options that can be filtered on.
        /// They're stored as Dictionary<string, IEnumerable<string>> 
        /// This is used with the Dynamic Query function to let you filter out all Events that match type "hf died".
        /// </summary>
        private void LoadOptions()
        {
            Options[typeof (Civilization)] = new Dictionary<string, IEnumerable<string>>
            {
                {"RaceName", World.Races.Values.Select(x=>x.Key)}
            };
            Options[typeof (Entity)] = new Dictionary<string, IEnumerable<string>>
            {
                {"RaceName", World.Races.Values.Select(x=>x.Key)},
                {"Type", new List<string> {"Civilization", "Religion", "Group", "Site Government", "Migrating Group", "Nomadic Group", "Outcast", "Other" }}
            };
            Options[typeof (EntityPopulation)] = new Dictionary<string, IEnumerable<string>>
            {
                {"RaceName", World.Races.Values.Select(x=>x.Key)}
            };
            Options[typeof(WorldConstruction)] = new Dictionary<string, IEnumerable<string>>
            {
                {"ConstructionType", WorldConstruction.Types}
            };
            Options[typeof(God)] = new Dictionary<string, IEnumerable<string>>
            {
                {"RaceName", World.Races.Values.Select(x=>x.Key)},
                {"GodType", new List<string> {"deity", "force"}}
            };
            Options[typeof (HistoricalEvent)] = new Dictionary<string, IEnumerable<string>>
            {
                {"EventType", HistoricalEvent.Types}
            };

            Options[typeof (HistoricalEventCollection)] = new Dictionary<string, IEnumerable<string>>
            {
                {"EventCollectionType", HistoricalEventCollection.Types}
            };
            Options[typeof (HistoricalFigure)] = new Dictionary<string, IEnumerable<string>>
            {
                {"Job", HistoricalFigure.AssociatedTypes},
                {"HFCaste", HistoricalFigure.Castes},
                {"RaceName", World.Races.Values.Select(x=>x.Key)}
            };
            Options[typeof(Leader)] = new Dictionary<string, IEnumerable<string>> { { "RaceName", World.Races.Values.Select(x => x.Key) } };
            Options[typeof (Region)] = new Dictionary<string, IEnumerable<string>> {{"RegionType", Region.Types}};
            Options[typeof(Site)] = new Dictionary<string, IEnumerable<string>> { { "SiteType", Site.Types } };
            Options[typeof(Structure)] = new Dictionary<string, IEnumerable<string>> { { "StructureType", Structure.Types } };


            // Quick check to verify no option is concerning the same property as a Field
            foreach (var fieldSet in Fields)
            {
                foreach (
                    var field in
                        fieldSet.Value.Keys.Where(
                            field => Options.ContainsKey(fieldSet.Key) && Options[fieldSet.Key].ContainsKey(field)))
                {
                    Options[fieldSet.Key].Remove(field);
                }
            }
        }


        /// <summary>
        /// All the default filters, generally they are for sorting, but I also remove some details which almost no one will care about.  
        ///     Normally huge sets are limited to 50k items by default, to not kill an unsuspecting user who accidentally clicks the Historical Events tab.
        ///     Though this can be changed if desired using the FilterForm.
        /// </summary>
        private void SetDefaultFilters()
        {
            this[typeof(Artifact)] = new Filter("Name", null, null, -1);
            this[typeof(Civilization)] = new Filter("Name", "IsFull = true", "Race.Name", -1);
            this[typeof(Entity)] = new Filter(new List<string> {"Name", "Type"}, null, null, -1);
            this[typeof(EntityPopulation)] = new Filter(new List<string> {"ID", "Race = null"}, null, null, -1);
            this[typeof(God)] = new Filter("Name", null, null, -1);
            this[typeof(HistoricalEra)] = new Filter("StartYear", null, null, -1);
            this[typeof(HistoricalEvent)] = new Filter("Year", null, null, 50000);
            this[typeof(HistoricalEventCollection)] = new Filter("StartYear", null, null, 50000);
            this[typeof(HistoricalFigure)] = new Filter("Name", null, null, 50000);
            this[typeof(Leader)] = new Filter("Name", null, null, -1);
            this[typeof(Parameter)] = new Filter();
            this[typeof(Race)] = new Filter(new List<string> {"Name", "!isCivilized"}, null, null, -1);
            this[typeof(Region)] = new Filter("Name", null, null, -1);
            this[typeof(Site)] = new Filter("Name", null, null, -1);
            this[typeof(Structure)] = new Filter("Name", null, null, -1);
            this[typeof(UndergroundRegion)] = new Filter(new List<string> {"Depth", "Name"}, null, null, -1);
            this[typeof(WorldConstruction)] = new Filter();
            this[typeof(Dynasty)] = new Filter("Name", null, null, -1);
            this[typeof(River)] = new Filter("Name", null, null, -1);
            this[typeof(Mountain)] = new Filter("Name", null, null, -1);


        }


        /// <summary>
        /// Indexer to add, change, or return filters.
        /// </summary>
        public Filter this[Type T]
        {
            get { return Filters[T]; }
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
    public class Filter
    {
        public readonly List<string> Where = new List<string>();
        public readonly List<string> OrderBy = new List<string>();
        public readonly List<string> GroupBy = new List<string>();

        public int Take { get; private set; }


        /// <summary>
        /// Filters can be created with one, many, or zero orderby/where clauses.
        /// </summary>
        public Filter(string orderby, string where, string group, int take)
        {
            if (orderby != null)
                OrderBy.Add(orderby);

            if (where != null)
                Where.Add(where);

            if (group != null)
                GroupBy.Add(group);

            Take = take;
        }

        public Filter(List<string> orderby, List<string> where, List<string> group, int take)
        {
            if (orderby != null)
                OrderBy = orderby;
            if (where != null)
                Where = where;
            if (group != null)
                GroupBy = group;

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
        public object[] Get<T>(List<T> list)
        {
            var query = list.AsQueryable();

            try
            {
                query = Where.Aggregate(query, (current, w) => current.Where(w));
                query = OrderBy.Aggregate(query, (current, o) => o.StartsWith("-") ? current.OrderBy(o.Substring(1) + " desc") : current.OrderBy(o));

                //query = Where.Aggregate(query, (current, @where) => current.Where(@where));
                //query = OrderBy.Aggregate(query, (current, @orderby) => @orderby.StartsWith("-") ? current.OrderBy(@orderby.Substring(1) + " desc") : current.OrderBy(@orderby));

                if (GroupBy.Count > 0)
                {
                    var returnList = new List<object>();

                    var newquery = query.GroupBy("new (" + String.Join(", ", GroupBy) + ")", "it");

                    var eq = newquery.Select("new(it.Key as Key, it as Items)");

                    var keyEmplist = (from dynamic dat in eq select dat).ToList();

                    foreach (var group in keyEmplist)
                    {
                        string key = group.Key.ToString().Substring(1, group.Key.ToString().Length - 2);
                        var keyDisplay = "";


                        foreach (var keyPart in key.Split(','))
                        {
                            if (keyPart.Split('=').Count() == 2)
                                keyDisplay += keyPart.Split('=')[1] + " ";
                            else
                                keyDisplay += "Unknown ";
                        }

                        var elist = group.Items;



                        returnList.Add("    " + keyDisplay.ToTitleCase());
                        foreach (var emp in elist)
                        {
                            returnList.Add(emp);
                            if (Take != -1 && returnList.Count > Take)
                                return returnList.ToArray();
                        }
                    }

                    return returnList.ToArray();
                }

                //var newquery = GroupBy.Aggregate(query, (current, @groupby) => current.GroupBy("new (" + @groupby + ")", "it"));


                if (Take > 0)
                    query = query.Take(Take);
                return query.ToArray() as object[];
            }
            catch (Exception e)
            {
                var ErrorMessage = string.Format("Filter error, returning defaults{0}{1}{0}", Environment.NewLine,
                    typeof (T));

                ErrorMessage = Where.Aggregate(ErrorMessage,
                    (current, @where) => current + ("Where: " + @where + Environment.NewLine));
                ErrorMessage = OrderBy.Aggregate(ErrorMessage,
                    (current, @orderby) => current + ("OrderBy: " + @orderby + Environment.NewLine));
                ErrorMessage = GroupBy.Aggregate(ErrorMessage,
                    (current, @groupby) => current + ("GroupBy: " + @groupby + Environment.NewLine));


                ErrorMessage += "Take: " + Take + Environment.NewLine;
                ErrorMessage += e.Message;

                MessageBox.Show(ErrorMessage, @"DF World Viewer", MessageBoxButtons.OK);

                Where.Clear();
                OrderBy.Clear();
                GroupBy.Clear();

                return list.AsQueryable().Take(50000).ToArray() as object[];
            }

        }
    }

    /// <summary>
    /// When an object is added to the Summary Treeview with MainForm.AddSummaryItem() a NavigationFilter object might be associated with it.
    /// The Navigation Filter allows navigation from the Summary Treeview to an individual world object with associated filters.
    /// </summary>
    internal class NavigationFilter
    {
        public Type Type;
        public Filter Filter;
        public NavigationFilter(Type type, Filter filter)
        {
            Type = type;
            Filter = filter;
        }

        public NavigationFilter(Type type)
        {
            Type = type;
            Filter = new Filter();
        }

        public void Select(MainForm frm)
        {
            frm.World.Filters[Type] = Filter;

            switch (Type.Name)
            {
                case "Civilization":
                    frm.FillList(frm.lstCivilization, frm.World.Civilizations, frm.tabCivilization);
                    Program.MakeSelected(frm.tabCivilization, frm.lstCivilization);
                    break;
                case "Dynasty":
                    frm.FillList(frm.lstDynasty, frm.World.Dynasties, frm.tabDynasty);
                    Program.MakeSelected(frm.tabDynasty, frm.lstDynasty);
                    break;
                case "God":
                    frm.FillList(frm.lstGod, frm.World.Gods, frm.tabGod);
                    Program.MakeSelected(frm.tabGod, frm.lstGod);
                    break;
                case "Race":
                    frm.FillList(frm.lstRace, frm.World.Races, frm.tabRace);
                    Program.MakeSelected(frm.tabRace, frm.lstRace);
                    break;
                case "Leader":
                    frm.FillList(frm.lstLeader, frm.World.Leaders, frm.tabLeader);
                    Program.MakeSelected(frm.tabLeader, frm.lstLeader);
                    break;

                case "Artifact":
                    frm.FillList(frm.lstArtifact, frm.World.Artifacts, frm.tabArtifact);
                    Program.MakeSelected(frm.tabArtifact, frm.lstArtifact);
                    break;
                case "Entity":
                    frm.FillList(frm.lstEntity, frm.World.Entities, frm.tabEntity);
                    Program.MakeSelected(frm.tabEntity, frm.lstEntity);
                    break;
                case "EntityPopulation":
                    frm.FillList(frm.lstEntityPopulation, frm.World.EntityPopulations, frm.tabEntityPopulation);
                    Program.MakeSelected(frm.tabEntityPopulation, frm.lstEntityPopulation);
                    break;
                case "Site":
                    frm.FillList(frm.lstSite, frm.World.Sites, frm.tabSite);
                    Program.MakeSelected(frm.tabSite, frm.lstSite);
                    break;
                case "Structure":
                    frm.FillList(frm.lstStructure, frm.World.Structures, frm.tabStructure);
                    Program.MakeSelected(frm.tabStructure, frm.lstStructure);
                    break;
                case "Region":
                    frm.FillList(frm.lstRegion, frm.World.Regions, frm.tabRegion);
                    Program.MakeSelected(frm.tabRegion, frm.lstRegion);
                    break;
                case "UndergroundRegion":
                    frm.FillList(frm.lstUndergroundRegion, frm.World.UndergroundRegions, frm.tabUndergroundRegion);
                    Program.MakeSelected(frm.tabUndergroundRegion, frm.lstUndergroundRegion);
                    break;
                case "WorldConstruction":
                    frm.FillList(frm.lstWorldConstruction, frm.World.WorldConstructions, frm.tabWorldConstruction);
                    Program.MakeSelected(frm.tabWorldConstruction, frm.lstWorldConstruction);
                    break;
                case "HistoricalEvent":
                    frm.FillList(frm.lstHistoricalEvent, frm.World.HistoricalEvents, frm.tabHistoricalEvent);
                    Program.MakeSelected(frm.tabHistoricalEvent, frm.lstHistoricalEvent);
                    break;
                case "HistoricalEventCollection":
                    frm.FillList(frm.lstHistoricalEventCollection, frm.World.HistoricalEventCollections, frm.tabHistoricalEventCollection);
                    Program.MakeSelected(frm.tabHistoricalEventCollection, frm.lstHistoricalEventCollection);
                    break;
                case "HistoricalFigure":
                    frm.FillList(frm.lstHistoricalFigure, frm.World.HistoricalFigures, frm.tabHistoricalFigure);
                    Program.MakeSelected(frm.tabHistoricalFigure, frm.lstHistoricalFigure);
                    break;
                case "HistoricalEra":
                    frm.FillList(frm.lstHistoricalEra, frm.World.HistoricalEras, frm.tabHistoricalEra);
                    Program.MakeSelected(frm.tabHistoricalEra, frm.lstHistoricalEra);
                    break;
            }
        }

    }

}