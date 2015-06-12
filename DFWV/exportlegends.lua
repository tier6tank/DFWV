-- Export everything from legends mode
-- Valid args:  "all", "info", "maps", "sites"

gui = require 'gui'
local args = {...}
local vs = dfhack.gui.getCurViewscreen()
local i = 1

local MAPS = {
    "Standard biome+site map",
    "Elevations including lake and ocean floors",
    "Elevations respecting water level",
    "Biome",
    "Hydrosphere",
    "Temperature",
    "Rainfall",
    "Drainage",
    "Savagery",
    "Volcanism",
    "Current vegetation",
    "Evil",
    "Salinity",
    "Structures/fields/roads/etc.",
    "Trade",
    "Nobility and Holdings",
    "Diplomacy",
}

function getItemSubTypeName(itemType, subType)
    if (dfhack.items.getSubtypeCount(itemType)) <= 0 then
        return tostring(-1)
    end
    local subtypename = dfhack.items.getSubtypeDef(itemType, subType)
    if (subtypename == nil) then
        return tostring(-1)
    else
        return tostring(subtypename.name):lower()
    end
end

function findEntity(id)
    for k,v in ipairs(df.global.world.entities.all) do
        if (v.id == id) then
            return v
        end
    end
    return nil
end

function table.contains(table, element)
    for _, value in pairs(table) do
        if value == element then
            return true
        end
    end
    return false
end

function table.containskey(table, key)
    for value, _ in pairs(table) do
        if value == key then
            return true
        end
    end
    return false
end

--create an extra legends xml with extra data, by Mason11987 for World Viewer
function export_more_legends_xml()
    local julian_day = math.floor(df.global.cur_year_tick / 1200) + 1
    local month = math.floor(julian_day / 28) + 1 --days and months are 1-indexed
    local day = julian_day % 28 + 1
    local year_str = string.format('%0'..math.max(5, string.len(''..df.global.cur_year))..'d', df.global.cur_year)
    local date_str = year_str..string.format('-%02d-%02d', month, day)

    io.output(tostring(df.global.world.cur_savegame.save_dir).."-"..date_str.."-legends_plus.xml")

    io.write ("<?xml version=\"1.0\" encoding='UTF-8'?>".."\n")
    io.write ("<df_world>".."\n")
    io.write ("<name>"..dfhack.df2utf(dfhack.TranslateName(df.global.world.world_data.name)).."</name>".."\n")
    io.write ("<altname>"..dfhack.df2utf(dfhack.TranslateName(df.global.world.world_data.name,1)).."</altname>".."\n")

	--[[  geo_biomes can't be easily associated with map tiles or biomes without embarking, I think.
	io.write ("<geo_biomes>".."\n")
    for geobiomeK, geobiomeV in ipairs(df.global.world.world_data.geo_biomes) do
		io.write ("\t".."<geo_biome>".."\n")
		io.write ("\t\t\t".."<id>"..geobiomeK.."</id>".."\n")
		for layerK, layerV in ipairs(geobiomeV.layers) do
			io.write ("\t\t".."<layer>".."\n")
			io.write ("\t\t\t".."<id>"..layerK.."</id>".."\n")
			io.write ("\t\t\t".."<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(0, layerV.mat_index))..":"..layerV.type.."-"..layerV.mat_index.."</mat>".."\n")
			for k, v in ipairs(layerV.vein_mat) do
				io.write ("\t\t\t".."<vein>"..dfhack.matinfo.toString(dfhack.matinfo.decode(0, v))..":"..layerV.vein_type[k].."-"..v.."</vein>".."\n")
		
			end
			for k, v in ipairs(layerV.vein_nested_in) do
				if (v ~= -1) then
					io.write ("\t\t\t".."<vein_nested_in>"..k..","..v.."</vein_nested_in>".."\n")
				end
			end
			io.write ("\t\t\t".."<top_height>"..layerV.top_height.."</top_height>".."\n")
			io.write ("\t\t\t".."<bottom_height>"..layerV.bottom_height.."</bottom_height>".."\n")
			io.write ("\t\t".."</layer>".."\n")
		end
		io.write ("\t".."</geo_biome>".."\n")
    end
    io.write ("</geo_biomes>".."\n")
	--]]
	
	io.write ("<races>".."\n")
    for raceK, raceV in ipairs(df.global.world.raws.creatures.all) do
		io.write ("\t".."<race>".."\n")
		io.write ("\t\t".."<id>"..raceK.."</id>".."\n")
		io.write ("\t\t".."<key>"..raceV.creature_id.."</key>".."\n")
		io.write ("\t\t".."<nameS>"..raceV.name[0].."</nameS>".."\n")
		io.write ("\t\t".."<nameP>"..raceV.name[1].."</nameP>".."\n")
		for casteK, casteV in ipairs(raceV.caste) do
			io.write ("\t\t".."<caste>".."\n")
			io.write ("\t\t\t".."<id>"..casteK.."</id>".."\n")
			io.write ("\t\t\t".."<name>"..casteV.caste_id.."</name>".."\n")
			io.write ("\t\t\t".."<gender>"..casteV.gender.."</gender>".."\n")
			io.write ("\t\t\t".."<description>"..casteV.description.."</description>".."\n")
			io.write ("\t\t".."</caste>".."\n")
		end
		io.write ("\t".."</race>".."\n")
    end
    io.write ("</races>".."\n")
	
	io.write ("<mountains>".."\n")
    for mountainK, mountainV in ipairs(df.global.world.world_data.mountain_peaks) do
		io.write ("\t".."<mountain>".."\n")
		io.write ("\t\t".."<id>"..mountainK.."</id>".."\n")
		for k,v in pairs(mountainV) do
			if (k == "height") then
				io.write ("\t\t".."<"..k..">"..tostring(v).."</"..k..">".."\n")
			elseif (k == "pos") then
				io.write ("\t\t".."<coords>")
					io.write (v.x..","..v.y)
				io.write ("</coords>\n")
			elseif (k == "name") then
				io.write ("\t\t".."<name>"..dfhack.df2utf(dfhack.TranslateName(v,0)).."</name>".."\n")
				io.write ("\t\t".."<name2>"..dfhack.df2utf(dfhack.TranslateName(v,1)).."</name2>".."\n")
			end
		end
		io.write ("\t".."</mountain>".."\n")
    end
    io.write ("</mountains>".."\n")
	
	io.write ("<rivers>".."\n")
    for riverK, riverV in ipairs(df.global.world.world_data.rivers) do
		io.write ("\t".."<river>".."\n")
		io.write ("\t\t".."<id>"..riverK.."</id>".."\n")
		for k,v in pairs(riverV) do
			if (k == "height") then
				io.write ("\t\t".."<"..k..">"..tostring(v).."</"..k..">".."\n")
			elseif (k == "path") then
				io.write ("\t\t".."<coords>")
				for xK, xVal in ipairs(riverV.path.x) do
					io.write (xVal..","..riverV.path.y[xK].."|")
				end
				io.write (riverV.end_pos.x..","..riverV.end_pos.y)
				io.write ("</coords>\n")
			elseif (k == "elevation") then
				io.write ("\t\t".."<elevation>")
				for xK, xVal in ipairs(riverV.elevation) do
					io.write (xVal.."|")
				end
				io.write ("</elevation>\n")
			elseif (k == "name") then
				io.write ("\t\t".."<name>"..dfhack.df2utf(dfhack.TranslateName(v,0)).."</name>".."\n")
				io.write ("\t\t".."<name2>"..dfhack.df2utf(dfhack.TranslateName(v,1)).."</name2>".."\n")
			end
		end
		io.write ("\t".."</river>".."\n")
    end
    io.write ("</rivers>".."\n")
	
    io.write ("<regions>".."\n")
    for regionK, regionV in ipairs(df.global.world.world_data.regions) do
        io.write ("\t".."<region>".."\n")
        io.write ("\t\t".."<id>"..regionV.index.."</id>".."\n")
        io.write ("\t\t".."<coords>")
            for xK, xVal in ipairs(regionV.region_coords.x) do
                io.write (xVal..","..regionV.region_coords.y[xK].."|")
            end
        io.write ("</coords>\n")
        io.write ("\t\t".."<population>")
            for popK, popV in ipairs(regionV.population) do
				if (popV.type ~= 7 and popV.type ~= 6 and popV.type ~= 5) then
					io.write (popV.race..","..popV.count_min..","..popV.type.."|")
				end
            end
        io.write ("</population>\n")		
        io.write ("\t".."</region>".."\n")
    end
    io.write ("</regions>".."\n")

    io.write ("<underground_regions>".."\n")
    for regionK, regionV in ipairs(df.global.world.world_data.underground_regions) do
        io.write ("\t".."<underground_region>".."\n")
        io.write ("\t\t".."<id>"..regionV.index.."</id>".."\n")
        io.write ("\t\t".."<coords>")
            for xK, xVal in ipairs(regionV.region_coords.x) do
                io.write (xVal..","..regionV.region_coords.y[xK].."|")
            end
        io.write ("</coords>\n")
        io.write ("\t\t".."<population>")
            for popK, popV in ipairs(regionV.feature_init.feature.population) do
				if (popV.type ~= 7 and popV.type ~= 6 and popV.type ~= 5) then
					io.write (popV.race..","..popV.count_min..","..popV.type.."|")
				end
            end
        io.write ("</population>\n")				
        io.write ("\t".."</underground_region>".."\n")
    end
    io.write ("</underground_regions>".."\n")

    io.write ("<sites>".."\n")
    for siteK, siteV in ipairs(df.global.world.world_data.sites) do
        if (#siteV.buildings > 0) then
            io.write ("\t".."<site>".."\n")
            for k,v in pairs(siteV) do
                if (k == "id") then
                    io.write ("\t\t".."<"..k..">"..tostring(v).."</"..k..">".."\n")
                elseif (k == "buildings") then
                    io.write ("\t\t".."<structures>".."\n")
                    for buildingK, buildingV in ipairs(siteV.buildings) do
                        io.write ("\t\t\t".."<structure>".."\n")
                        io.write ("\t\t\t\t".."<id>"..buildingV.id.."</id>".."\n")
                        io.write ("\t\t\t\t".."<type>"..df.abstract_building_type[buildingV:getType()]:lower().."</type>".."\n")
                        if (df.abstract_building_type[buildingV:getType()]:lower() ~= "underworld_spire") then
                            io.write ("\t\t\t\t".."<name>"..dfhack.df2utf(dfhack.TranslateName(buildingV.name, 1)).."</name>".."\n")
                            io.write ("\t\t\t\t".."<name2>"..dfhack.df2utf(dfhack.TranslateName(buildingV.name)).."</name2>".."\n")
                        end
                        io.write ("\t\t\t".."</structure>".."\n")
                    end
                    io.write ("\t\t".."</structures>".."\n")
                end
            end
            io.write ("\t".."</site>".."\n")
        end
    end
    io.write ("</sites>".."\n")

    io.write ("<world_constructions>".."\n")
    for wcK, wcV in ipairs(df.global.world.world_data.constructions.list) do
        io.write ("\t".."<world_construction>".."\n")
        io.write ("\t\t".."<id>"..wcV.id.."</id>".."\n")
        io.write ("\t\t".."<name>"..dfhack.df2utf(dfhack.TranslateName(wcV.name,1)).."</name>".."\n")
        io.write ("\t\t".."<type>"..(df.world_construction_type[wcV:getType()]):lower().."</type>".."\n")
        io.write ("\t\t".."<coords>")
        for xK, xVal in ipairs(wcV.square_pos.x) do
            io.write (xVal..","..wcV.square_pos.y[xK].."|")
        end
        io.write ("</coords>\n")
        io.write ("\t".."</world_construction>".."\n")
    end
    io.write ("</world_constructions>".."\n")

    io.write ("<artifacts>".."\n")
    for artifactK, artifactV in ipairs(df.global.world.artifacts.all) do
        io.write ("\t".."<artifact>".."\n")
        io.write ("\t\t".."<id>"..artifactV.id.."</id>".."\n")
        if (artifactV.item:getType() ~= -1) then
            io.write ("\t\t".."<item_type>"..tostring(df.item_type[artifactV.item:getType()]):lower().."</item_type>".."\n")
            if (artifactV.item:getSubtype() ~= -1) then
                io.write ("\t\t".."<item_subtype>"..artifactV.item.subtype.name.."</item_subtype>".."\n")
            end
        end
        if (table.containskey(artifactV.item,"description")) then
            io.write ("\t\t".."<item_description>"..artifactV.item.description:lower().."</item_description>".."\n")
        end
        if (artifactV.item:getMaterial() ~= -1 and artifactV.item:getMaterialIndex() ~= -1) then
            io.write ("\t\t".."<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(artifactV.item:getMaterial(), artifactV.item:getMaterialIndex())).."</mat>".."\n")
        end
        io.write ("\t".."</artifact>".."\n")
    end
    io.write ("</artifacts>".."\n")

    io.write ("<historical_figures>".."\n".."</historical_figures>".."\n")

    io.write ("<entity_populations>".."\n")
    for entityPopK, entityPopV in ipairs(df.global.world.entity_populations) do
        io.write ("\t".."<entity_population>".."\n")
        io.write ("\t\t".."<id>"..entityPopV.id.."</id>".."\n")
        for raceK, raceV in ipairs(entityPopV.races) do
            local raceName = (df.global.world.raws.creatures.all[raceV].creature_id):lower()
            io.write ("\t\t".."<race>"..raceName..":"..entityPopV.counts[raceK].."</race>".."\n")
        end
        io.write ("\t\t".."<civ_id>"..entityPopV.civ_id.."</civ_id>".."\n")
        io.write ("\t".."</entity_population>".."\n")
    end
    io.write ("</entity_populations>".."\n")

    io.write ("<entities>".."\n")
    for entityK, entityV in ipairs(df.global.world.entities.all) do
        io.write ("\t".."<entity>".."\n")
        io.write ("\t\t".."<id>"..entityV.id.."</id>".."\n")
        io.write ("\t\t".."<race>"..(df.global.world.raws.creatures.all[entityV.race].creature_id):lower().."</race>".."\n")
        io.write ("\t\t".."<type>"..(df.historical_entity_type[entityV.type]):lower().."</type>".."\n")
        if (df.historical_entity_type[entityV.type]):lower() == "religion" then -- Get worshipped figure
            if (entityV.unknown1b ~= nil and entityV.unknown1b.worship ~= nill and
                #entityV.unknown1b.worship == 1) then
                io.write ("\t\t".."<worship_id>"..entityV.unknown1b.worship[0].."</worship_id>".."\n")
            else
                print(entityV.unknown1b, entityV.unknown1b.worship, #entityV.unknown1b.worship)
            end
        end
        for id, link in pairs(entityV.entity_links) do
            io.write ("\t\t".."<entity_link>".."\n")
                for k, v in pairs(link) do
                    if (k == "type") then
                        io.write ("\t\t\t".."<"..k..">"..tostring(df.entity_entity_link_type[v]).."</"..k..">".."\n")
                    else
                        io.write ("\t\t\t".."<"..k..">"..v.."</"..k..">".."\n")
                    end
                end
            io.write ("\t\t".."</entity_link>".."\n")
        end
        for id, link in ipairs(entityV.children) do
            io.write ("\t\t".."<child>"..link.."</child>".."\n")
        end
        io.write ("\t".."</entity>".."\n")
    end
    io.write ("</entities>".."\n")

    io.write ("<historical_events>".."\n")
    for ID, event in ipairs(df.global.world.history.events) do
        if event:getType() == df.history_event_type.ADD_HF_ENTITY_LINK
              or event:getType() == df.history_event_type.ADD_HF_SITE_LINK
              or event:getType() == df.history_event_type.ADD_HF_HF_LINK
              or event:getType() == df.history_event_type.ADD_HF_ENTITY_LINK
              or event:getType() == df.history_event_type.TOPICAGREEMENT_CONCLUDED
              or event:getType() == df.history_event_type.TOPICAGREEMENT_REJECTED
              or event:getType() == df.history_event_type.TOPICAGREEMENT_MADE
              or event:getType() == df.history_event_type.BODY_ABUSED
              or event:getType() == df.history_event_type.CHANGE_HF_JOB
              or event:getType() == df.history_event_type.CREATED_BUILDING
              or event:getType() == df.history_event_type.CREATURE_DEVOURED
              or event:getType() == df.history_event_type.HF_DOES_INTERACTION
              or event:getType() == df.history_event_type.HF_LEARNS_SECRET
              or event:getType() == df.history_event_type.HIST_FIGURE_NEW_PET
              or event:getType() == df.history_event_type.HIST_FIGURE_REACH_SUMMIT
              or event:getType() == df.history_event_type.ITEM_STOLEN
              or event:getType() == df.history_event_type.REMOVE_HF_ENTITY_LINK
              or event:getType() == df.history_event_type.REMOVE_HF_SITE_LINK
              or event:getType() == df.history_event_type.REPLACED_BUILDING
              or event:getType() == df.history_event_type.MASTERPIECE_CREATED_ARCH_DESIGN
              or event:getType() == df.history_event_type.MASTERPIECE_CREATED_DYE_ITEM
              or event:getType() == df.history_event_type.MASTERPIECE_CREATED_ARCH_CONSTRUCT
              or event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM
              or event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT
              or event:getType() == df.history_event_type.MASTERPIECE_CREATED_FOOD -- Missing item subtype
              or event:getType() == df.history_event_type.MASTERPIECE_CREATED_ENGRAVING
              or event:getType() == df.history_event_type.MASTERPIECE_LOST
              or event:getType() == df.history_event_type.ENTITY_ACTION
              or event:getType() == df.history_event_type.HF_ACT_ON_BUILDING
              or event:getType() == df.history_event_type.ARTIFACT_CREATED
              or event:getType() == df.history_event_type.ASSUME_IDENTITY
              or event:getType() == df.history_event_type.CREATE_ENTITY_POSITION
              or event:getType() == df.history_event_type.DIPLOMAT_LOST
              or event:getType() == df.history_event_type.MERCHANT
              or event:getType() == df.history_event_type.WAR_PEACE_ACCEPTED
              or event:getType() == df.history_event_type.WAR_PEACE_REJECTED
              or event:getType() == df.history_event_type.HIST_FIGURE_WOUNDED
              or event:getType() == df.history_event_type.HIST_FIGURE_DIED
                then
            io.write ("\t".."<historical_event>".."\n")
            io.write ("\t\t".."<id>"..event.id.."</id>".."\n")
            io.write ("\t\t".."<type>"..tostring(df.history_event_type[event:getType()]):lower().."</type>".."\n")
            for k,v in pairs(event) do
                if k == "year" or k == "seconds" or k == "flags" or k == "id"
                    or (k == "region" and event:getType() ~= df.history_event_type.HF_DOES_INTERACTION)
                    or k == "region_pos" or k == "layer" or k == "feature_layer" or k == "subregion"
                    or k == "anon_1" or k == "anon_2" or k == "flags2" or k == "unk1" then

                elseif event:getType() == df.history_event_type.ADD_HF_ENTITY_LINK and k == "link_type" then
                    io.write ("\t\t".."<"..k..">"..df.histfig_entity_link_type[v]:lower().."</"..k..">".."\n")
                elseif event:getType() == df.history_event_type.ADD_HF_ENTITY_LINK and k == "position_id" then
                    local entity = findEntity(event.civ)
                    if (entity ~= nil and event.civ > -1 and v > -1) then
                        for entitypositionsK, entityPositionsV in ipairs(entity.positions.own) do
                            if entityPositionsV.id == v then
                                io.write ("\t\t".."<position>"..tostring(entityPositionsV.name[0]):lower().."</position>".."\n")
                                break
                            end
                        end
                    else
                        io.write ("\t\t".."<position>-1</position>".."\n")
                    end
                elseif event:getType() == df.history_event_type.CREATE_ENTITY_POSITION and k == "position" then
                    local entity = findEntity(event.site_civ)
                    if (entity ~= nil and v > -1) then
                        for entitypositionsK, entityPositionsV in ipairs(entity.positions.own) do
                            if entityPositionsV.id == v then
                                io.write ("\t\t".."<position>"..tostring(entityPositionsV.name[0]):lower().."</position>".."\n")
                                break
                            end
                        end
                    else
                        io.write ("\t\t".."<position>-1</position>".."\n")
                    end
                elseif event:getType() == df.history_event_type.REMOVE_HF_ENTITY_LINK and k == "link_type" then
                    io.write ("\t\t".."<"..k..">"..df.histfig_entity_link_type[v]:lower().."</"..k..">".."\n")
                elseif event:getType() == df.history_event_type.REMOVE_HF_ENTITY_LINK and k == "position_id" then
                    local entity = findEntity(event.civ)
                    if (entity ~= nil and event.civ > -1 and v > -1) then
                        for entitypositionsK, entityPositionsV in ipairs(entity.positions.own) do
                            if entityPositionsV.id == v then
                                io.write ("\t\t".."<position>"..tostring(entityPositionsV.name[0]):lower().."</position>".."\n")
                                break
                            end
                        end
                    else
                        io.write ("\t\t".."<position>-1</position>".."\n")
                    end
                elseif event:getType() == df.history_event_type.ADD_HF_HF_LINK and k == "type" then
                    io.write ("\t\t".."<link_type>"..df.histfig_hf_link_type[v]:lower().."</link_type>".."\n")
                elseif event:getType() == df.history_event_type.ADD_HF_SITE_LINK and k == "type" then
                    io.write ("\t\t".."<link_type>"..df.histfig_site_link_type[v]:lower().."</link_type>".."\n")
                elseif event:getType() == df.history_event_type.REMOVE_HF_SITE_LINK and k == "type" then
                    io.write ("\t\t".."<link_type>"..df.histfig_site_link_type[v]:lower().."</link_type>".."\n")
                elseif (event:getType() == df.history_event_type.ITEM_STOLEN or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT
                        ) and k == "item_type" then
                    io.write ("\t\t".."<item_type>"..df.item_type[v]:lower().."</item_type>".."\n")
                elseif (event:getType() == df.history_event_type.ITEM_STOLEN or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT
                        ) and k == "item_subtype" then
                    --if event.item_type > -1 and v > -1 then
                        io.write ("\t\t".."<"..k..">"..getItemSubTypeName(event.item_type,v).."</"..k..">".."\n")
                    --end
                elseif event:getType() == df.history_event_type.ITEM_STOLEN and k == "mattype" then
                    if (v > -1) then
                        if (dfhack.matinfo.decode(event.mattype, event.matindex) == nil) then
                            io.write ("\t\t".."<mattype>"..event.mattype.."</mattype>".."\n")
                            io.write ("\t\t".."<matindex>"..event.matindex.."</matindex>".."\n")
                        else
                            io.write ("\t\t".."<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.mattype, event.matindex)).."</mat>".."\n")
                        end
                    end
                elseif (event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT
                        ) and k == "mat_type" then
                    if (v > -1) then
                        if (dfhack.matinfo.decode(event.mat_type, event.mat_index) == nil) then
                            io.write ("\t\t".."<mat_type>"..event.mat_type.."</mat_type>".."\n")
                            io.write ("\t\t".."<mat_index>"..event.mat_index.."</mat_index>".."\n")
                        else
                            io.write ("\t\t".."<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.mat_type, event.mat_index)).."</mat>".."\n")
                        end
                    end
                elseif event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT and k == "imp_mat_type" then
                    if (v > -1) then
                        if (dfhack.matinfo.decode(event.imp_mat_type, event.imp_mat_index) == nil) then
                            io.write ("\t\t".."<imp_mat_type>"..event.imp_mat_type.."</imp_mat_type>".."\n")
                            io.write ("\t\t".."<imp_mat_index>"..event.imp_mat_index.."</imp_mat_index>".."\n")
                        else
                            io.write ("\t\t".."<imp_mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.imp_mat_type, event.imp_mat_index)).."</imp_mat>".."\n")
                        end
                    end

                elseif event:getType() == df.history_event_type.ITEM_STOLEN and k == "matindex" then
                    --skip
                elseif event:getType() == df.history_event_type.ITEM_STOLEN and k == "item" and v == -1 then
                    --skip
                elseif (event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT
                        ) and k == "mat_index" then
                    --skip
                elseif event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT and k == "imp_mat_index" then
                    --skip
                elseif (event:getType() == df.history_event_type.WAR_PEACE_ACCEPTED or
                        event:getType() == df.history_event_type.WAR_PEACE_REJECTED or
                        event:getType() == df.history_event_type.TOPICAGREEMENT_CONCLUDED or
                        event:getType() == df.history_event_type.TOPICAGREEMENT_REJECTED or
                        event:getType() == df.history_event_type.TOPICAGREEMENT_MADE
                        ) and k == "topic" then
                    io.write ("\t\t".."<topic>"..tostring(df.meeting_topic[v]):lower().."</topic>".."\n")
                elseif event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT and k == "improvement_type" then
                    io.write ("\t\t".."<improvement_type>"..df.improvement_type[v]:lower().."</improvement_type>".."\n")
                elseif ((event:getType() == df.history_event_type.HIST_FIGURE_REACH_SUMMIT and k == "figures") or
                        (event:getType() == df.history_event_type.HIST_FIGURE_NEW_PET and k == "group")
                     or (event:getType() == df.history_event_type.BODY_ABUSED and k == "bodies")) then
                    for detailK,detailV in pairs(v) do
                        io.write ("\t\t".."<"..k..">"..detailV.."</"..k..">".."\n")
                    end
                elseif  event:getType() == df.history_event_type.HIST_FIGURE_NEW_PET and k == "pets" then
                    for detailK,detailV in pairs(v) do
                        io.write ("\t\t".."<"..k..">"..(df.global.world.raws.creatures.all[detailV].creature_id):lower().."</"..k..">".."\n")
                    end
                elseif event:getType() == df.history_event_type.BODY_ABUSED and (k == "props") then
                    io.write ("\t\t".."<"..k.."_item_type"..">"..tostring(df.item_type[event.props.item.item_type]):lower().."</"..k.."_item_type"..">".."\n")
                    io.write ("\t\t".."<"..k.."_item_subtype"..">"..getItemSubTypeName(event.props.item.item_type,event.props.item.item_subtype).."</"..k.."_item_subtype"..">".."\n")
                    if (event.props.item.mat_type > -1) then
                        if (dfhack.matinfo.decode(event.props.item.mat_type, event.props.item.mat_index) == nil) then
                            io.write ("\t\t".."<props_item_mat_type>"..event.props.item.mat_type.."</props_item_mat_type>".."\n")
                            io.write ("\t\t".."<props_item_mat_index>"..event.props.item.mat_index.."</props_item_mat_index>".."\n")
                        else
                            io.write ("\t\t".."<props_item_mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.props.item.mat_type, event.props.item.mat_index)).."</props_item_mat>".."\n")
                        end
                    end
                    --io.write ("\t\t".."<"..k.."_item_mat_type"..">"..tostring(event.props.item.mat_type).."</"..k.."_item_mat_index"..">".."\n")
                    --io.write ("\t\t".."<"..k.."_item_mat_index"..">"..tostring(event.props.item.mat_index).."</"..k.."_item_mat_index"..">".."\n")
                    io.write ("\t\t".."<"..k.."_pile_type"..">"..tostring(event.props.pile_type).."</"..k.."_pile_type"..">".."\n")
                elseif event:getType() == df.history_event_type.ASSUME_IDENTITY and k == "identity" then
                    if (df.global.world.assumed_identities.all[v].histfig_id == -1) then
                        local thisIdentity = df.global.world.assumed_identities.all[v]
                        io.write ("\t\t".."<identity_name>"..thisIdentity.name.first_name.."</identity_name>".."\n")
                        io.write ("\t\t".."<identity_race>"..(df.global.world.raws.creatures.all[thisIdentity.race].creature_id):lower().."</identity_race>".."\n")
                        io.write ("\t\t".."<identity_caste>"..(df.global.world.raws.creatures.all[thisIdentity.race].caste[thisIdentity.caste].caste_id):lower().."</identity_caste>".."\n")
                    else
                        io.write ("\t\t".."<identity_hf>"..df.global.world.assumed_identities.all[v].histfig_id.."</identity_hf>".."\n")
                    end
                elseif event:getType() == df.history_event_type.MASTERPIECE_CREATED_ARCH_CONSTRUCT and k == "building_type" then
                    io.write ("\t\t".."<building_type>"..df.building_type[v]:lower().."</building_type>".."\n")
                elseif event:getType() == df.history_event_type.MASTERPIECE_CREATED_ARCH_CONSTRUCT and k == "building_subtype" then
                    if (df.building_type[event.building_type]:lower() == "furnace") then
                        io.write ("\t\t".."<building_subtype>"..df.furnace_type[v]:lower().."</building_subtype>".."\n")
                    elseif v > -1 then
                        io.write ("\t\t".."<building_subtype>"..tostring(v).."</building_subtype>".."\n")
                    end
                elseif k == "race" then
                    if v > -1 then
                        io.write ("\t\t".."<race>"..(df.global.world.raws.creatures.all[v].creature_id):lower().."</race>".."\n")
                    end
                elseif k == "caste" then
                    if v > -1 then
                        io.write ("\t\t".."<caste>"..(df.global.world.raws.creatures.all[event.race].caste[v].caste_id):lower().."</caste>".."\n")
                    end
                elseif k == "interaction" and event:getType() == df.history_event_type.HF_DOES_INTERACTION then
                    io.write ("\t\t".."<interaction_action>"..df.global.world.raws.interactions[v].str[3].value.."</interaction_action>".."\n")
                    io.write ("\t\t".."<interaction_string>"..df.global.world.raws.interactions[v].str[4].value.."</interaction_string>".."\n")
                elseif k == "interaction" and event:getType() == df.history_event_type.HF_LEARNS_SECRET then
                    io.write ("\t\t".."<secret_text>"..df.global.world.raws.interactions[v].str[2].value.."</secret_text>".."\n")
                elseif event:getType() == df.history_event_type.HIST_FIGURE_DIED and k == "weapon" then
                    for detailK,detailV in pairs(v) do
                        if (detailK == "item") then
                            if detailV > -1 then
                                io.write ("\t\t".."<"..detailK..">"..detailV.."</"..detailK..">".."\n")
                                local thisItem = df.item.find(detailV)
                                if (thisItem ~= nil) then
                                    if (thisItem.flags.artifact == true) then
                                        for refk,refv in pairs(thisItem.general_refs) do
                                            if (refv:getType() == 1) then
                                                io.write ("\t\t".."<artifact_id>"..refv.artifact_id.."</artifact_id>".."\n")
                                                break
                                            end
                                        end
                                    end
                                end

                            end
                        elseif (detailK == "item_type") then
                            if event.weapon.item > -1 then
                                io.write ("\t\t".."<"..detailK..">"..tostring(df.item_type[detailV]):lower().."</"..detailK..">".."\n")
                            end
                        elseif (detailK == "item_subtype") then
                            if event.weapon.item > -1 and detailV > -1 then
                                io.write ("\t\t".."<"..detailK..">"..getItemSubTypeName(event.weapon.item_type,detailV).."</"..detailK..">".."\n")
                            end
                        elseif (detailK == "mattype") then
                            if (detailV > -1) then
                                io.write ("\t\t".."<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.weapon.mattype, event.weapon.matindex)).."</mat>".."\n")
                            end
                        elseif (detailK == "matindex") then

                        elseif (detailK == "shooter_item") then
                            if detailV > -1 then
                                io.write ("\t\t".."<"..detailK..">"..detailV.."</"..detailK..">".."\n")
                                local thisItem = df.item.find(detailV)
                                if  thisItem ~= nil then
                                    if (thisItem.flags.artifact == true) then
                                        for refk,refv in pairs(thisItem.general_refs) do
                                            if (refv:getType() == 1) then
                                                io.write ("\t\t".."<shooter_artifact_id>"..refv.artifact_id.."</shooter_artifact_id>".."\n")
                                                break
                                            end
                                        end
                                    end
                                end
                            end
                        elseif (detailK == "shooter_item_type") then
                            if event.weapon.shooter_item > -1 then
                                io.write ("\t\t".."<"..detailK..">"..tostring(df.item_type[detailV]):lower().."</"..detailK..">".."\n")
                            end
                        elseif (detailK == "shooter_item_subtype") then
                            if event.weapon.shooter_item > -1 and detailV > -1 then
                                io.write ("\t\t".."<"..detailK..">"..getItemSubTypeName(event.weapon.shooter_item_type,detailV).."</"..detailK..">".."\n")
                            end
                        elseif (detailK == "shooter_mattype") then
                            if (detailV > -1) then
                                io.write ("\t\t".."<shooter_mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.weapon.shooter_mattype, event.weapon.shooter_matindex)).."</shooter_mat>".."\n")
                            end
                        elseif (detailK == "shooter_matindex") then
                            --skip
                        elseif detailK == "slayer_race" or detailK == "slayer_caste" then
                            --skip
                        else
                            io.write ("\t\t".."<"..detailK..">"..detailV.."</"..detailK..">".."\n")
                        end
                    end
                elseif event:getType() == df.history_event_type.HIST_FIGURE_DIED and k == "death_cause" then
                    io.write ("\t\t".."<"..k..">"..df.death_type[v]:lower().."</"..k..">".."\n")
                elseif event:getType() == df.history_event_type.CHANGE_HF_JOB and (k == "new_job" or k == "old_job") then
                    io.write ("\t\t".."<"..k..">"..df.profession[v]:lower().."</"..k..">".."\n")
                else
                    io.write ("\t\t".."<"..k..">"..tostring(v).."</"..k..">".."\n")
                end
            end
            io.write ("\t".."</historical_event>".."\n")
        end
    end
    io.write ("</historical_events>".."\n")
    io.write ("<historical_event_collections>".."\n")
    io.write ("</historical_event_collections>".."\n")
    io.write ("<historical_eras>".."\n")
    io.write ("</historical_eras>".."\n")
    io.write ("</df_world>".."\n")
    io.close()
end

-- export information and XML ('p, x')
function export_legends_info()
    print('    Exporting:  World map/gen info')
    gui.simulateInput(vs, 'LEGENDS_EXPORT_MAP')
    print('    Exporting:  Legends xml')
    gui.simulateInput(vs, 'LEGENDS_EXPORT_XML')
    print("    Exporting:  Extra legends_plus xml")
    export_more_legends_xml()
end

-- presses 'd' for detailed maps
function wait_for_legends_vs()
    local vs = dfhack.gui.getCurViewscreen()
    if i <= #MAPS then
        if df.viewscreen_legendsst:is_instance(vs) then
            gui.simulateInput(vs, 'LEGENDS_EXPORT_DETAILED_MAP')
            dfhack.timeout(10,'frames',wait_for_export_maps_vs)
        else
            dfhack.timeout(10,'frames',wait_for_legends_vs)
        end
    end
end

-- selects detailed map and export it
function wait_for_export_maps_vs()
    local vs = dfhack.gui.getCurViewscreen()
    if dfhack.gui.getCurFocus() == "export_graphical_map" then
        vs.sel_idx = i-1
        print('    Exporting:  '..MAPS[i]..' map')
        gui.simulateInput(vs, 'SELECT')
        i = i + 1
        dfhack.timeout(10,'frames',wait_for_legends_vs)
    else
        dfhack.timeout(10,'frames',wait_for_export_maps_vs)
    end
end

-- export site maps
function export_site_maps()
    local vs = dfhack.gui.getCurViewscreen()
    print('    Exporting:  All possible site maps')
    vs.main_cursor = 1
    gui.simulateInput(vs, 'SELECT')
    for i=1, #vs.sites do
        gui.simulateInput(vs, 'LEGENDS_EXPORT_MAP')
        gui.simulateInput(vs, 'STANDARDSCROLL_DOWN')
    end
    gui.simulateInput(vs, 'LEAVESCREEN')
end

-- main()
if dfhack.gui.getCurFocus() == "legends" then
    if args[1] == "all" then
        export_legends_info()
        export_site_maps()
        wait_for_legends_vs()
    elseif args[1] == "info" then
        export_legends_info()
    elseif args[1] == "maps" then
        wait_for_legends_vs()
    elseif args[1] == "sites" then
        export_site_maps()
    else dfhack.printerr('Valid arguments are "all", "info", "maps" or "sites"')
    end
elseif args[1] == "maps" and
        dfhack.gui.getCurFocus() == "export_graphical_map" then
    wait_for_export_maps_vs()
else
    dfhack.printerr('Exportlegends must be run from the main legends view')
end
