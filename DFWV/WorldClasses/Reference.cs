using System;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;

namespace DFWV.WorldClasses
{
    public enum ReferenceType
    {
        // ReSharper disable InconsistentNaming
        UNKNOWN_REFERENCE_TYPE,
        ARTIFACT,
        IS_ARTIFACT,
        NEMESIS,
        IS_NEMESIS,
        ITEM,
        ITEM_TYPE,
        COINBATCH,
        MAPSQUARE,
        ENTITY_ART_IMAGE,
        CONTAINS_UNIT,
        CONTAINS_ITEM,
        CONTAINED_IN_ITEM,
        PROJECTILE,
        UNIT,
        UNIT_MILKEE,
        UNIT_TRAINEE,
        UNIT_ITEMOWNER,
        UNIT_TRADEBRINGER,
        UNIT_HOLDER,
        UNIT_WORKER,
        UNIT_CAGEE,
        UNIT_BEATEE,
        UNIT_FOODRECEIVER,
        UNIT_KIDNAPEE,
        UNIT_PATIENT,
        UNIT_INFANT,
        UNIT_SLAUGHTEREE,
        UNIT_SHEAREE,
        UNIT_SUCKEE,
        UNIT_REPORTEE,
        BUILDING,
        BUILDING_CIVZONE_ASSIGNED,
        BUILDING_TRIGGER,
        BUILDING_TRIGGERTARGET,
        BUILDING_CHAIN,
        BUILDING_CAGED,
        BUILDING_HOLDER,
        BUILDING_WELL_TAG,
        BUILDING_USE_TARGET_1,
        BUILDING_USE_TARGET_2,
        BUILDING_DESTINATION,
        BUILDING_NEST_BOX,
        ENTITY,
        ENTITY_STOLEN,
        ENTITY_OFFERED,
        ENTITY_ITEMOWNER,
        LOCATION,
        INTERACTION,
        ABSTRACT_BUILDING,
        HISTORICAL_EVENT,
        SPHERE,
        SITE,
        SUBREGION,
        FEATURE_LAYER,
        HISTORICAL_FIGURE,
        ENTITY_POP,
        CREATURE,
        UNIT_RIDER,
        UNIT_CLIMBER,
        UNIT_GELDEE,
        KNOWLEDGE_SCHOLAR_FLAG,
        ACTIVITY_EVENT,
        VALUE_LEVEL,
        LANGUAGE,
        WRITTEN_CONTENT,
        POETIC_FORM,
        MUSICAL_FORM,
        DANCE_FORM
        // ReSharper restore InconsistentNaming
    }

    public class Reference : XMLObject
    {
        public ReferenceType Type { get; set; }
        public int? Id { get; set; }
        public WorldObject refObject { get; set; }
        public int? SiteId { get; set; } //For Abstract Building reference
        public WorldObject Owner { get; set; }
        public override Point Location => Owner.Location;

        public Reference(XContainer data, WorldObject owner) : base(owner.World)
        {
            if (data.Element("id") != null)
                Id = Convert.ToInt32(data.Element("id").Value);
            if (data.Element("site_id") != null)
                SiteId = Convert.ToInt32(data.Element("site_id").Value);
            if (data.Element("type") != null)
            {
                var typeIndex = Enum.GetNames(typeof (ReferenceType)).ToList().IndexOf(data.Element("type").Value);
                if (typeIndex == -1)
                    Type = ReferenceType.UNKNOWN_REFERENCE_TYPE;
                else
                    Type = (ReferenceType) typeIndex;
            }
            Owner = owner;
        }


        internal override void Link()
        {
            switch (Type)
            {
                case ReferenceType.UNKNOWN_REFERENCE_TYPE:
                    break;
                case ReferenceType.UNIT:
                case ReferenceType.UNIT_MILKEE:
                case ReferenceType.UNIT_TRAINEE:
                case ReferenceType.UNIT_ITEMOWNER:
                case ReferenceType.UNIT_TRADEBRINGER:
                case ReferenceType.UNIT_HOLDER:
                case ReferenceType.UNIT_WORKER:
                case ReferenceType.UNIT_CAGEE:
                case ReferenceType.UNIT_BEATEE:
                case ReferenceType.UNIT_FOODRECEIVER:
                case ReferenceType.UNIT_KIDNAPEE:
                case ReferenceType.UNIT_PATIENT:
                case ReferenceType.UNIT_INFANT:
                case ReferenceType.UNIT_SLAUGHTEREE:
                case ReferenceType.UNIT_SHEAREE:
                case ReferenceType.UNIT_SUCKEE:
                case ReferenceType.UNIT_REPORTEE:
                case ReferenceType.UNIT_RIDER:
                case ReferenceType.UNIT_CLIMBER:
                case ReferenceType.UNIT_GELDEE:
                case ReferenceType.CONTAINS_UNIT:
                case ReferenceType.IS_NEMESIS:
                    if (Id.HasValue && World.Units.ContainsKey(Id.Value))
                        refObject = World.Units[Id.Value];
                    break;
                case ReferenceType.BUILDING:
                case ReferenceType.BUILDING_CIVZONE_ASSIGNED:
                case ReferenceType.BUILDING_TRIGGER:
                case ReferenceType.BUILDING_TRIGGERTARGET:
                case ReferenceType.BUILDING_CHAIN:
                case ReferenceType.BUILDING_CAGED:
                case ReferenceType.BUILDING_HOLDER:
                case ReferenceType.BUILDING_WELL_TAG:
                case ReferenceType.BUILDING_USE_TARGET_1:
                case ReferenceType.BUILDING_USE_TARGET_2:
                case ReferenceType.BUILDING_DESTINATION:
                case ReferenceType.BUILDING_NEST_BOX:
                case ReferenceType.ABSTRACT_BUILDING:
                    if (Id.HasValue && World.Buildings.ContainsKey(Id.Value))
                        refObject = World.Buildings[Id.Value];
                    break;
                case ReferenceType.ENTITY:
                case ReferenceType.ENTITY_STOLEN:
                case ReferenceType.ENTITY_OFFERED:
                case ReferenceType.ENTITY_ITEMOWNER:
                    if (Id.HasValue && World.Entities.ContainsKey(Id.Value))
                        refObject = World.Entities[Id.Value];
                    break;
                case ReferenceType.ARTIFACT:
                case ReferenceType.IS_ARTIFACT:
                    if (Id.HasValue && World.Artifacts.ContainsKey(Id.Value))
                        refObject = World.Artifacts[Id.Value];
                    break;
                case ReferenceType.ITEM:
                case ReferenceType.CONTAINS_ITEM:
                case ReferenceType.CONTAINED_IN_ITEM:
                    if (Id.HasValue && World.Items.ContainsKey(Id.Value))
                        refObject = World.Items[Id.Value];
                    break;
                case ReferenceType.HISTORICAL_EVENT:
                case ReferenceType.ACTIVITY_EVENT:
                    if (Id.HasValue && World.HistoricalEvents.ContainsKey(Id.Value))
                        refObject = World.HistoricalEvents[Id.Value];
                    break;
                case ReferenceType.SITE:
                    if (Id.HasValue && World.Sites.ContainsKey(Id.Value))
                        refObject = World.Sites[Id.Value];
                    break;
                case ReferenceType.SUBREGION:
                    if (Id.HasValue && World.Regions.ContainsKey(Id.Value))
                        refObject = World.Regions[Id.Value];
                    break;
                case ReferenceType.HISTORICAL_FIGURE:
                    if (Id.HasValue && World.HistoricalFigures.ContainsKey(Id.Value))
                        refObject = World.HistoricalFigures[Id.Value];
                    break;
                case ReferenceType.ENTITY_POP:
                    if (Id.HasValue && World.EntityPopulations.ContainsKey(Id.Value))
                        refObject = World.EntityPopulations[Id.Value];
                    break;
                case ReferenceType.WRITTEN_CONTENT:
                    if (Id.HasValue && World.WrittenContents.ContainsKey(Id.Value))
                        refObject = World.WrittenContents[Id.Value];
                    break;
                case ReferenceType.POETIC_FORM:
                    if (Id.HasValue && World.PoeticForms.ContainsKey(Id.Value))
                        refObject = World.PoeticForms[Id.Value];
                    break;
                case ReferenceType.MUSICAL_FORM:
                    if (Id.HasValue && World.MusicalForms.ContainsKey(Id.Value))
                        refObject = World.MusicalForms[Id.Value];
                    break;
                case ReferenceType.DANCE_FORM:
                    if (Id.HasValue && World.DanceForms.ContainsKey(Id.Value))
                        refObject = World.DanceForms[Id.Value];
                    break;
                case ReferenceType.NEMESIS:
                    break;
                case ReferenceType.ITEM_TYPE:
                    break;
                case ReferenceType.COINBATCH:
                    break;
                case ReferenceType.MAPSQUARE:
                    break;
                case ReferenceType.ENTITY_ART_IMAGE:
                    break;
                case ReferenceType.PROJECTILE:
                    break;
                case ReferenceType.LOCATION:
                    break;
                case ReferenceType.INTERACTION:
                    break;
                case ReferenceType.SPHERE:
                    break;
                case ReferenceType.FEATURE_LAYER:
                    break;
                case ReferenceType.CREATURE:
                    break;
                case ReferenceType.KNOWLEDGE_SCHOLAR_FLAG: //TODO Check this out
                    break;
                case ReferenceType.VALUE_LEVEL: //TODO Nothing exported
                    break;
                case ReferenceType.LANGUAGE: //TODO Nothing exported
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        internal override void Process()
        {
            
        }
        internal override void Plus(XDocument xdoc)
        {
            
        }
        internal override void Export(string table)
        {
            
        }

        public override void Select(MainForm frm)
        {
            refObject?.Select(frm);
        }

        public override string ToString()
        {
            var prefixText = "Reference: ";
            switch (Type)
            {
                case ReferenceType.UNKNOWN_REFERENCE_TYPE:
                    break;
                case ReferenceType.UNIT_TRADEBRINGER:
                    prefixText = @"Being traded by Unit: ";
                    break;
                case ReferenceType.UNIT_HOLDER:
                    prefixText = @"Held by Unit: ";
                    break;
                case ReferenceType.UNIT_ITEMOWNER:
                    prefixText = @"Owned by Unit: ";
                    break;
                case ReferenceType.IS_NEMESIS:
                    prefixText = @"Is Nemesis of Unit: ";
                    break;
                case ReferenceType.UNIT_MILKEE:
                case ReferenceType.UNIT_TRAINEE:
                case ReferenceType.UNIT_WORKER:
                case ReferenceType.UNIT_CAGEE:
                case ReferenceType.UNIT_BEATEE:
                case ReferenceType.UNIT_FOODRECEIVER:
                case ReferenceType.UNIT_KIDNAPEE:
                case ReferenceType.UNIT_PATIENT:
                case ReferenceType.UNIT_INFANT:
                case ReferenceType.UNIT_SLAUGHTEREE:
                case ReferenceType.UNIT_SHEAREE:
                case ReferenceType.UNIT_SUCKEE:
                case ReferenceType.UNIT_REPORTEE:
                case ReferenceType.UNIT_RIDER:
                case ReferenceType.UNIT_CLIMBER:
                case ReferenceType.UNIT_GELDEE:

                    break;
                case ReferenceType.BUILDING_HOLDER:
                    prefixText = @"In Building: ";
                    break;
                case ReferenceType.BUILDING_TRIGGER:
                    prefixText = @"Triggered by Building: ";
                    break;
                case ReferenceType.BUILDING_TRIGGERTARGET:
                    prefixText = @"Triggers Building: ";
                    break;
                case ReferenceType.BUILDING_CHAIN:
                    prefixText = @"Chained to Building: ";
                    break;
                case ReferenceType.BUILDING_CIVZONE_ASSIGNED:
                    prefixText = @"Assigned to Zone: ";
                    break;
                case ReferenceType.BUILDING_CAGED:
                case ReferenceType.BUILDING_WELL_TAG:
                case ReferenceType.BUILDING_USE_TARGET_1:
                case ReferenceType.BUILDING_USE_TARGET_2:
                case ReferenceType.BUILDING_DESTINATION:
                case ReferenceType.BUILDING_NEST_BOX:
                case ReferenceType.ABSTRACT_BUILDING:

                     break;
                case ReferenceType.ENTITY_STOLEN:
                case ReferenceType.ENTITY_OFFERED:
                case ReferenceType.ENTITY_ITEMOWNER:

                    break;

                case ReferenceType.IS_ARTIFACT:
                    prefixText = @"Is Artifact: ";
                    break;
                case ReferenceType.ACTIVITY_EVENT:
                    prefixText = @"Related to event: ";
                    break;
                case ReferenceType.CONTAINS_UNIT:
                case ReferenceType.CONTAINS_ITEM:
                case ReferenceType.CONTAINED_IN_ITEM:
                case ReferenceType.UNIT:
                case ReferenceType.BUILDING:
                case ReferenceType.ENTITY:
                case ReferenceType.ARTIFACT:
                case ReferenceType.ITEM:
                case ReferenceType.HISTORICAL_EVENT:
                case ReferenceType.WRITTEN_CONTENT:
                case ReferenceType.SITE:
                case ReferenceType.SUBREGION:
                case ReferenceType.HISTORICAL_FIGURE:
                case ReferenceType.POETIC_FORM:
                case ReferenceType.MUSICAL_FORM:
                case ReferenceType.DANCE_FORM:
                    prefixText = $"{Enum.GetName(typeof(ReferenceType), Type).Replace("_", " ").ToLower().ToTitleCase()}: ";
                    break;
                case ReferenceType.ENTITY_POP:
                    break;
                case ReferenceType.NEMESIS:
                    break;
                case ReferenceType.ITEM_TYPE:
                    break;
                case ReferenceType.COINBATCH:
                    break;
                case ReferenceType.MAPSQUARE:
                    break;
                case ReferenceType.ENTITY_ART_IMAGE:
                    break;
                case ReferenceType.PROJECTILE:
                    break;
                case ReferenceType.LOCATION:
                    break;
                case ReferenceType.INTERACTION:
                    break;
                case ReferenceType.SPHERE:
                    break;
                case ReferenceType.FEATURE_LAYER:
                    break;
                case ReferenceType.CREATURE:
                    break;
                case ReferenceType.KNOWLEDGE_SCHOLAR_FLAG: //TODO Check this out
                    break;
                case ReferenceType.VALUE_LEVEL: //TODO Nothing exported
                    break;
                case ReferenceType.LANGUAGE: //TODO Nothing exported
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (refObject != null)
                return prefixText + refObject;
            if (prefixText != "Reference: ")
                return prefixText + Id;
            return $"{Enum.GetName(typeof (ReferenceType), Type).Replace("_", " ").ToLower().ToTitleCase()} reference " + Id;
        }
    }
}
