using DFWV.WorldClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFWV.WorldClasses
{
    class Stats
    {
        World World;

        //Graph Data
        public Dictionary<int, int> HFAliveInYear = new Dictionary<int, int>();
        public Dictionary<int, int> SitesInYear = new Dictionary<int, int>();

        //Other Data
        public Dictionary<string, List<HistoricalFigure>> SphereAllegiance = new Dictionary<string, List<HistoricalFigure>>();
        public Dictionary<Race, WorldTime> AverageLifeSpan = new Dictionary<Race, WorldTime>();
        public Dictionary<Race, int> DeadHFs = new Dictionary<Race, int>();
        public Dictionary<Race, int> KilledHFs = new Dictionary<Race, int>();
        public Dictionary<Race, int> LivingHFs = new Dictionary<Race, int>();
        public Dictionary<Race, int> SitesPerRace = new Dictionary<Race, int>();
        public Dictionary<Race, int> CivsPerRace = new Dictionary<Race, int>();
        public Dictionary<string, int> EntitiesByType = new Dictionary<string, int>();
        public Dictionary<Race, int> EntGroupsPerRace = new Dictionary<Race, int>();
        public Dictionary<Race, int> EntReligionPerRace = new Dictionary<Race, int>();
        public Dictionary<Race, int> EntOtherPerRace = new Dictionary<Race, int>();
        public Dictionary<string, int> AssociatedTypesCount = new Dictionary<string, int>();
        public Dictionary<string, int> CastesCount = new Dictionary<string, int>();
        public Dictionary<string, int> InteractionCount = new Dictionary<string, int>();
        public Dictionary<string, int> PetCount = new Dictionary<string, int>();
        public Dictionary<string, int> GoalCount = new Dictionary<string, int>();

        public int DeityHFs;
        public int AnimatedHFs;
        public int GhostHFs;
        public int AdventurerHFs;
        public int ForceHFs;
        public int GodHFs;
        public int LeaderHFs;
        public int MaleHFs;
        public int FemaleHFs;
        public int NonGenderHFs;

        public int OverallDeadHFs = 0;
        public int OverallKilledHFs = 0;
        public int OverallLivingHFs = 0;
        public WorldTime OverallAverageLifeSpan = new WorldTime(0);

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
            World = world;
        }

        internal void Gather()
        {
            Dictionary<Race, WorldTime> TotalLifeSpan = new Dictionary<Race, WorldTime>();
            WorldTime OverallTotalLifeSpan = new WorldTime(0);

            foreach (string sphere in HistoricalFigure.Spheres)
                SphereAllegiance.Add(sphere, new List<HistoricalFigure>());

            foreach (Race race in World.Races.Values)
            {
                TotalLifeSpan.Add(race, new WorldTime(0));
                AverageLifeSpan.Add(race, new WorldTime(0));
                DeadHFs.Add(race,0);
                KilledHFs.Add(race, 0);
                LivingHFs.Add(race, 0);
                SitesPerRace.Add(race,0);
                CivsPerRace.Add(race, 0);
                EntGroupsPerRace.Add(race, 0);
                EntReligionPerRace.Add(race, 0);
                EntOtherPerRace.Add(race, 0);
            }

            foreach (string caste in HistoricalFigure.Castes)
                CastesCount.Add(caste, 0);

            foreach (string job in HistoricalFigure.AssociatedTypes)
                AssociatedTypesCount.Add(job, 0);

            foreach (string job in HistoricalFigure.Interactions)
                InteractionCount.Add(job, 0);

            foreach (string job in HistoricalFigure.JourneyPets)
                PetCount.Add(job, 0);

            foreach (string job in HistoricalFigure.Goals)
                GoalCount.Add(job, 0);


            foreach (HistoricalFigure HF in World.HistoricalFigures.Values)
            {

                if (HF.Deity)
                    DeityHFs++;
                if (HF.Animated)
                    AnimatedHFs++;
                if (HF.Ghost)
                    GhostHFs++;
                if (HF.Adventurer)
                    AdventurerHFs++;
                if (HF.Force)
                    ForceHFs++;
                if (HF.isGod)
                    GodHFs++;
                if (HF.isLeader)
                    LeaderHFs++;

                if (HF.isFemale)
                    FemaleHFs++;
                else if (HF.isMale)
                    MaleHFs++;
                else
                    NonGenderHFs++;

               
                if (HF.Caste.HasValue)
                    CastesCount[HistoricalFigure.Castes[HF.Caste.Value]]++;
                if (HF.AssociatedType.HasValue)
                    AssociatedTypesCount[HistoricalFigure.AssociatedTypes[HF.AssociatedType.Value]]++;
                if (HF.JourneyPet != null)
                {
                    foreach (int pet in HF.JourneyPet)
                        PetCount[HistoricalFigure.JourneyPets[pet]]++;
                }
                if (HF.InteractionKnowledge != null)
                {
                    foreach (int knowledge in HF.InteractionKnowledge)
                        InteractionCount[HistoricalFigure.Interactions[knowledge]]++;
                }
                if (HF.ActiveInteraction.HasValue)
                {
                    if (HF.InteractionKnowledge == null || !HF.InteractionKnowledge.Contains(HF.ActiveInteraction.Value))
                        InteractionCount[HistoricalFigure.Interactions[HF.ActiveInteraction.Value]]++;
                }
                if (HF.Goal.HasValue)
                    GoalCount[HistoricalFigure.Goals[HF.Goal.Value]]++;

                if (HF.Sphere != null)
                {
                    foreach (int sphere in HF.Sphere)
                        SphereAllegiance[HistoricalFigure.Spheres[sphere]].Add(HF);
                }
                if (HF.Dead)
                {
                    OverallDeadHFs++;
                    if (HF.Race != null)
                        DeadHFs[HF.Race]++;
                    
                    if (HF.DiedEvent.SlayerHF != null)
                    { 
                        OverallKilledHFs++;
                        if (HF.Race != null)
                            KilledHFs[HF.Race]++;
                    }
                    WorldTime lifeSpan = (HF.DiedEvent.Time - HF.Birth);
                    if (HF.Race != null)
                        TotalLifeSpan[HF.Race] += lifeSpan;
                    OverallTotalLifeSpan += lifeSpan;
                }
                else
                {
                    if (HF.Race != null)
                        LivingHFs[HF.Race]++;
                    OverallLivingHFs++;
                }
            }

            foreach (Site site in World.Sites.Values)
            {
                if (site.Owner != null && site.Owner.Race != null)
                    SitesPerRace[site.Owner.Race]++;
                else if (site.Parent != null && site.Parent.Race != null)
                    SitesPerRace[site.Owner.Race]++;
            }

            foreach (Civilization civ in World.Civilizations)
            {
                if (civ.Race != null)
                    CivsPerRace[civ.Race]++;
            }

            foreach (Entity ent in World.Entities.Values)
            {
                if (!EntitiesByType.ContainsKey(ent.Type))
                    EntitiesByType.Add(ent.Type, 0);
                EntitiesByType[ent.Type]++;
                if (ent.Race != null)
                {
                    if (ent.Type == "Group")
                        EntGroupsPerRace[ent.Race]++;
                    else if (ent.Type == "Religion")
                        EntReligionPerRace[ent.Race]++;
                    else if (ent.Type == "Other")
                        EntOtherPerRace[ent.Race]++;
                }
            }

            foreach (Race race in World.Races.Values)
            {
                if (DeadHFs[race] > 0)
                    AverageLifeSpan[race] = TotalLifeSpan[race] / DeadHFs[race];
                else
                {
                    AverageLifeSpan.Remove(race);
                    DeadHFs.Remove(race);
                    KilledHFs.Remove(race);
                    LivingHFs.Remove(race);
                }
            }
                

            OverallAverageLifeSpan = OverallTotalLifeSpan / OverallDeadHFs;

            CreateGraphData();

        }

        private void CreateGraphData()
        {
            CreateHFPopulationGraph();
            CreateSiteGraph();
        }

        private void CreateSiteGraph()
        {
            Dictionary<int, int> SitesBuiltInYear = new Dictionary<int, int>();
            Dictionary<int, int> SitesDestroyedInYear = new Dictionary<int, int>();

            int earliestSiteBuilt = 0;
            int earliestSiteDestroyed = 0;
            int latestSiteBuilt = 0;
            int latestSiteDestroyed = 0;

            foreach (Site site in World.Sites.Values)
            {

                int createdYear;
                if (site.CreatedEvent != null)
                    createdYear = site.CreatedEvent.Time.Year;
                else
                    createdYear = 0;

                if (!SitesBuiltInYear.ContainsKey(createdYear))
                    SitesBuiltInYear.Add(createdYear, 1);
                else
                    SitesBuiltInYear[createdYear]++;

                if (createdYear < earliestSiteBuilt)
                    earliestSiteBuilt = createdYear;
                else if (createdYear > latestSiteBuilt)
                    latestSiteBuilt = createdYear;

                if (site.ReclaimedEvents != null)
                {
                    foreach (HE_ReclaimSite evt in site.ReclaimedEvents)
                    {
                        if (!SitesBuiltInYear.ContainsKey(evt.Time.Year))
                            SitesBuiltInYear.Add(evt.Time.Year, 1);
                        else
                            SitesBuiltInYear[evt.Time.Year]++;

                        if (evt.Time.Year < earliestSiteBuilt)
                            earliestSiteBuilt = evt.Time.Year;
                        else if (evt.Time.Year > latestSiteBuilt)
                            latestSiteBuilt = evt.Time.Year;
                    }
                }

                if (site.DestroyedEvents != null)
                {
                    foreach (HE_DestroyedSite evt in site.DestroyedEvents)
                    {
                        if (!SitesDestroyedInYear.ContainsKey(evt.Time.Year))
                            SitesDestroyedInYear.Add(evt.Time.Year, 1);
                        else
                            SitesDestroyedInYear[evt.Time.Year]++;

                        if (evt.Time.Year < earliestSiteDestroyed)
                            earliestSiteDestroyed = evt.Time.Year;
                        else if (evt.Time.Year > latestSiteDestroyed)
                            latestSiteDestroyed = evt.Time.Year;
                    }
                }
            }

            SitesInYear.Clear();
            SitesInYear.Add(earliestSiteBuilt - 1, 0);

            for (int y = earliestSiteBuilt; y <= Math.Max(latestSiteBuilt, latestSiteDestroyed); y++)
            {
                SitesInYear.Add(y, SitesInYear[y - 1]);
                if (!SitesBuiltInYear.ContainsKey(y))
                    SitesBuiltInYear.Add(y, 0);
                if (!SitesDestroyedInYear.ContainsKey(y))
                    SitesDestroyedInYear.Add(y, 0);

                SitesInYear[y] += SitesBuiltInYear[y] - SitesDestroyedInYear[y];
            }

        }

        private void CreateHFPopulationGraph()
        {
            Dictionary<int, int> HFBirthsInYear = new Dictionary<int, int>();
            Dictionary<int, int> HFDeathsInYear = new Dictionary<int, int>();

            int earliestBirth = int.MaxValue;
            int earliestDeath = int.MaxValue;
            int latestBirth = int.MinValue;
            int latestDeath = int.MinValue;

            foreach (HistoricalFigure hf in World.HistoricalFigures.Values)
            {
                if (!HFBirthsInYear.ContainsKey(hf.Birth.Year))
                    HFBirthsInYear.Add(hf.Birth.Year, 1);
                else
                    HFBirthsInYear[hf.Birth.Year]++;

                if (hf.Birth.Year < earliestBirth)
                    earliestBirth = hf.Birth.Year;
                else if (hf.Birth.Year > latestBirth)
                    latestBirth = hf.Birth.Year;

                if (hf.Dead)
                { 
                    if (!HFDeathsInYear.ContainsKey(hf.Death.Year))
                        HFDeathsInYear.Add(hf.Death.Year, 1);
                    else
                        HFDeathsInYear[hf.Death.Year]++;

                    if (hf.Death.Year < earliestDeath)
                        earliestDeath = hf.Death.Year;
                    else if (hf.Death.Year > latestDeath)
                        latestDeath = hf.Death.Year;
                }
            }

            HFAliveInYear.Add(earliestBirth - 1, 0);

            for (int y = earliestBirth; y <= Math.Max(latestDeath, latestBirth); y++)
            {
                HFAliveInYear.Add(y, HFAliveInYear[y - 1]);
                if (!HFBirthsInYear.ContainsKey(y))
                    HFBirthsInYear.Add(y, 0);
                if (!HFDeathsInYear.ContainsKey(y))
                    HFDeathsInYear.Add(y, 0);

                HFAliveInYear[y] += HFBirthsInYear[y] - HFDeathsInYear[y];
            }
        }


    }
}
