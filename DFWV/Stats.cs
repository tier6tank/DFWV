using System;
using System.Collections.Generic;
using System.Linq;
using DFWV.WorldClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV
{
    public class Stats
    {
        readonly World _world;

        //Graph Data
        public readonly Dictionary<int, int> HfAliveInYear = new Dictionary<int, int>();
        public readonly Dictionary<int, int> SitesInYear = new Dictionary<int, int>();

        //Other Data
        private readonly Dictionary<string, List<HistoricalFigure>> _sphereAllegiance = new Dictionary<string, List<HistoricalFigure>>();
        private readonly Dictionary<Race, WorldTime> _averageLifeSpan = new Dictionary<Race, WorldTime>();
        private readonly Dictionary<Race, int> _deadHFs = new Dictionary<Race, int>();
        private readonly Dictionary<Race, int> _killedHFs = new Dictionary<Race, int>();
        private readonly Dictionary<Race, int> _livingHFs = new Dictionary<Race, int>();
        private readonly Dictionary<Race, int> _sitesPerRace = new Dictionary<Race, int>();
        private readonly Dictionary<Race, int> _civsPerRace = new Dictionary<Race, int>();
        private readonly Dictionary<string, int> _entitiesByType = new Dictionary<string, int>();
        private readonly Dictionary<Race, int> _entGroupsPerRace = new Dictionary<Race, int>();
        private readonly Dictionary<Race, int> _entReligionPerRace = new Dictionary<Race, int>();
        private readonly Dictionary<Race, int> _entOtherPerRace = new Dictionary<Race, int>();
        private readonly Dictionary<string, int> _associatedTypesCount = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _castesCount = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _interactionCount = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _petCount = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _goalCount = new Dictionary<string, int>();


        private int _deityHFs;
        private int _animatedHFs;
        private int _ghostHFs;
        private int _adventurerHFs;
        private int _forceHFs;
        private int _godHFs;
        private int _leaderHFs;
        private int _maleHFs;
        private int _femaleHFs;
        private int _nonGenderHFs;

        private int _overallDeadHFs;
        private int _overallKilledHFs;
        private int _overallLivingHFs;
        private WorldTime _overallAverageLifeSpan = new WorldTime(0);

        #region Remove later


        //public int? CurrentIdentityID { get; set; }
        //public int? UsedIdentityID { get; set; }
        //public string HoldsArtifact { get; set; }

        //public int CreatedArtifactCount { get { return CreatedArtifacts == null ? 0 : CreatedArtifacts.Count; } }
        //public int CreatedMasterpieceCount { get { return Events == null ? 0 : Events.Where(x => HistoricalEvent.Types[x.Type].Contains("masterpiece")).Count(); } }
        //public int ChildrenCount { get { return Children == null ? 0 : Children.Count; } }
        //public int Kills { get { return SlayingEvents == null ? 0 : SlayingEvents.Count; } }
        //public int Battles { get { return BattleEventCollections == null ? 0 : BattleEventCollections.Count; } }
        //public string DispNameLower { get { return ToString().ToLower(); } }
        //public int EntPopID { get { return EntPop == null ? -1 : EntPop.ID; } }
        //public int DescendentCount { get; set; }
        //public int AncestorCount { get; set; }
        //public int DescendentGenerations { get; set; }
        //public bool PlayerControlled { get; set; }
        //public string Job { get { return AssociatedType.HasValue ? AssociatedTypes[AssociatedType.Value] : ""; } }
        //public string HFCaste { get { return Caste.HasValue ? Castes[Caste.Value] : ""; } }

        #endregion


        public Stats(World world)
        {
            _world = world;
        }

        internal void Gather()
        {
            var totalLifeSpan = new Dictionary<Race, WorldTime>();
            var overallTotalLifeSpan = new WorldTime(0);

            foreach (var sphere in HistoricalFigure.Spheres)
                _sphereAllegiance.Add(sphere, new List<HistoricalFigure>());

            foreach (var race in _world.Races.Values)
            {
                totalLifeSpan.Add(race, new WorldTime(0));
                _averageLifeSpan.Add(race, new WorldTime(0));
                _deadHFs.Add(race,0);
                _killedHFs.Add(race, 0);
                _livingHFs.Add(race, 0);
                _sitesPerRace.Add(race,0);
                _civsPerRace.Add(race, 0);
                _entGroupsPerRace.Add(race, 0);
                _entReligionPerRace.Add(race, 0);
                _entOtherPerRace.Add(race, 0);
            }

            foreach (var caste in HistoricalFigure.Castes)
                _castesCount.Add(caste, 0);

            foreach (var job in HistoricalFigure.AssociatedTypes)
                _associatedTypesCount.Add(job, 0);

            foreach (var job in HistoricalFigure.Interactions)
                _interactionCount.Add(job, 0);

            foreach (var job in HistoricalFigure.JourneyPets)
                _petCount.Add(job, 0);

            foreach (var job in HistoricalFigure.Goals)
                _goalCount.Add(job, 0);


            foreach (var hf in _world.HistoricalFigures.Values)
            {

                if (hf.Deity)
                    _deityHFs++;
                if (hf.Animated)
                    _animatedHFs++;
                if (hf.Ghost)
                    _ghostHFs++;
                if (hf.Adventurer)
                    _adventurerHFs++;
                if (hf.Force)
                    _forceHFs++;
                if (hf.IsGod)
                    _godHFs++;
                if (hf.IsLeader)
                    _leaderHFs++;

                if (hf.IsFemale)
                    _femaleHFs++;
                else if (hf.IsMale)
                    _maleHFs++;
                else
                    _nonGenderHFs++;

               
                if (hf.Caste.HasValue)
                    _castesCount[HistoricalFigure.Castes[hf.Caste.Value]]++;
                if (hf.AssociatedType.HasValue)
                    _associatedTypesCount[HistoricalFigure.AssociatedTypes[hf.AssociatedType.Value]]++;
                if (hf.JourneyPet != null)
                {
                    foreach (var pet in hf.JourneyPet)
                        _petCount[HistoricalFigure.JourneyPets[pet]]++;
                }
                if (hf.InteractionKnowledge != null)
                {
                    foreach (var knowledge in hf.InteractionKnowledge)
                        _interactionCount[HistoricalFigure.Interactions[knowledge]]++;
                }
                if (hf.ActiveInteraction.HasValue)
                {
                    if (hf.InteractionKnowledge == null || !hf.InteractionKnowledge.Contains(hf.ActiveInteraction.Value))
                        _interactionCount[HistoricalFigure.Interactions[hf.ActiveInteraction.Value]]++;
                }
                if (hf.Goal.HasValue)
                    _goalCount[HistoricalFigure.Goals[hf.Goal.Value]]++;

                if (hf.Sphere != null)
                {
                    foreach (var sphere in hf.Sphere)
                        _sphereAllegiance[HistoricalFigure.Spheres[sphere]].Add(hf);
                }
                if (hf.Dead)
                {
                    _overallDeadHFs++;
                    if (hf.Race != null)
                        _deadHFs[hf.Race]++;

                    if (hf.DiedEvent?.SlayerHf != null)
                    { 
                        _overallKilledHFs++;
                        if (hf.Race != null)
                            _killedHFs[hf.Race]++;
                    }
                    if (hf.DiedEvent == null) continue;
                    var lifeSpan = (hf.DiedEvent.Time - hf.Birth);
                    if (hf.Race != null)
                        totalLifeSpan[hf.Race] += lifeSpan;
                    overallTotalLifeSpan += lifeSpan;
                }
                else
                {
                    if (hf.Race != null)
                        _livingHFs[hf.Race]++;
                    _overallLivingHFs++;
                }
            }

            foreach (var site in _world.Sites.Values)
            {
                if (site.Owner?.Race != null)
                    _sitesPerRace[site.Owner.Race]++;
                else if (site.Parent?.Race != null)
                    _sitesPerRace[site.Owner.Race]++;
            }

            foreach (var civ in _world.Civilizations.Where(civ => civ.Race != null))
            {
                _civsPerRace[civ.Race]++;
            }

            foreach (var ent in _world.Entities.Values)
            {
                if (!_entitiesByType.ContainsKey(ent.Type))
                    _entitiesByType.Add(ent.Type, 0);
                _entitiesByType[ent.Type]++;
                if (ent.Race == null) continue;
                switch (ent.Type)
                {
                    case "Group":
                        _entGroupsPerRace[ent.Race]++;
                        break;
                    case "Religion":
                        _entReligionPerRace[ent.Race]++;
                        break;
                    case "Other":
                        _entOtherPerRace[ent.Race]++;
                        break;
                }
            }

            foreach (var race in _world.Races.Values)
            {
                if (_deadHFs[race] > 0)
                    _averageLifeSpan[race] = totalLifeSpan[race] / _deadHFs[race];
                else
                {
                    _averageLifeSpan.Remove(race);
                    _deadHFs.Remove(race);
                    _killedHFs.Remove(race);
                    _livingHFs.Remove(race);
                }
            }
                

            _overallAverageLifeSpan = overallTotalLifeSpan / _overallDeadHFs;

            CreateGraphData();

        }

        private void CreateGraphData()
        {
            CreateHfPopulationGraph();
            CreateSiteGraph();
        }

        private void CreateSiteGraph()
        {
            var sitesBuiltInYear = new Dictionary<int, int>();
            var sitesDestroyedInYear = new Dictionary<int, int>();

            var earliestSiteBuilt = 0;
            var earliestSiteDestroyed = 0;
            var latestSiteBuilt = 0;
            var latestSiteDestroyed = 0;

            foreach (var site in _world.Sites.Values)
            {
                var createdYear = site.CreatedEvent?.Time.Year ?? 0;

                if (!sitesBuiltInYear.ContainsKey(createdYear))
                    sitesBuiltInYear.Add(createdYear, 1);
                else
                    sitesBuiltInYear[createdYear]++;

                if (createdYear < earliestSiteBuilt)
                    earliestSiteBuilt = createdYear;
                else if (createdYear > latestSiteBuilt)
                    latestSiteBuilt = createdYear;

                var cachedSiteEvents = site.Events;

                foreach (var evt in cachedSiteEvents.OfType<HE_ReclaimSite>())
                {
                    if (!sitesBuiltInYear.ContainsKey(evt.Time.Year))
                        sitesBuiltInYear.Add(evt.Time.Year, 1);
                    else
                        sitesBuiltInYear[evt.Time.Year]++;

                    if (evt.Time.Year < earliestSiteBuilt)
                        earliestSiteBuilt = evt.Time.Year;
                    else if (evt.Time.Year > latestSiteBuilt)
                        latestSiteBuilt = evt.Time.Year;
                }

                foreach (var evt in cachedSiteEvents.OfType<HE_DestroyedSite>())
                {
                    if (!sitesDestroyedInYear.ContainsKey(evt.Time.Year))
                        sitesDestroyedInYear.Add(evt.Time.Year, 1);
                    else
                        sitesDestroyedInYear[evt.Time.Year]++;

                    if (evt.Time.Year < earliestSiteDestroyed)
                        earliestSiteDestroyed = evt.Time.Year;
                    else if (evt.Time.Year > latestSiteDestroyed)
                        latestSiteDestroyed = evt.Time.Year;
                }
            }

            SitesInYear.Clear();
            SitesInYear.Add(earliestSiteBuilt - 1, 0);

            for (var y = earliestSiteBuilt; y <= Math.Max(latestSiteBuilt, latestSiteDestroyed); y++)
            {
                SitesInYear.Add(y, SitesInYear[y - 1]);
                if (!sitesBuiltInYear.ContainsKey(y))
                    sitesBuiltInYear.Add(y, 0);
                if (!sitesDestroyedInYear.ContainsKey(y))
                    sitesDestroyedInYear.Add(y, 0);

                SitesInYear[y] += sitesBuiltInYear[y] - sitesDestroyedInYear[y];
            }

        }

        private void CreateHfPopulationGraph()
        {
            var hfBirthsInYear = new Dictionary<int, int>();
            var hfDeathsInYear = new Dictionary<int, int>();

            var earliestBirth = int.MaxValue;
            var earliestDeath = int.MaxValue;
            var latestBirth = int.MinValue;
            var latestDeath = int.MinValue;

            foreach (var hf in _world.HistoricalFigures.Values)
            {
                if (!hfBirthsInYear.ContainsKey(hf.Birth.Year))
                    hfBirthsInYear.Add(hf.Birth.Year, 1);
                else
                    hfBirthsInYear[hf.Birth.Year]++;

                if (hf.Birth.Year < earliestBirth)
                    earliestBirth = hf.Birth.Year;
                else if (hf.Birth.Year > latestBirth)
                    latestBirth = hf.Birth.Year;

                if (!hf.Dead) continue;
                if (!hfDeathsInYear.ContainsKey(hf.Death.Year))
                    hfDeathsInYear.Add(hf.Death.Year, 1);
                else
                    hfDeathsInYear[hf.Death.Year]++;

                if (hf.Death.Year < earliestDeath)
                    earliestDeath = hf.Death.Year;
                else if (hf.Death.Year > latestDeath)
                    latestDeath = hf.Death.Year;
            }

            HfAliveInYear.Add(earliestBirth - 1, 0);

            for (var y = earliestBirth; y <= Math.Max(latestDeath, latestBirth); y++)
            {
                HfAliveInYear.Add(y, HfAliveInYear[y - 1]);
                if (!hfBirthsInYear.ContainsKey(y))
                    hfBirthsInYear.Add(y, 0);
                if (!hfDeathsInYear.ContainsKey(y))
                    hfDeathsInYear.Add(y, 0);

                HfAliveInYear[y] += hfBirthsInYear[y] - hfDeathsInYear[y];
            }
        }


    }
}
