-- Export everything from legends mode
--[====[

exportlegends
=============
Controls legends mode to export data - especially useful to set-and-forget large
worlds, or when you want a map of every site when there are several hundred.

The 'info' option exports more data than is possible in vanilla, to a
:file:`region-date-legends_plus.xml` file developed to extend
:forums:`World Viewer <128932>` and other legends utilities.

Options:

:info:   Exports the world/gen info, the legends XML, and a custom XML with more information
:custom: Exports a custom XML with more information
:sites:  Exports all available site maps
:maps:   Exports all seventeen detailed maps
:all:    Equivalent to calling all of the above, in that order

]====]

gui = require 'gui'
script = require 'gui.script'
local args = {...}
local vs = dfhack.gui.getCurViewscreen()

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

function tablelength(T)
  local count = 0
  for _ in pairs(T) do count = count + 1 end
  return count
end

function string.starts(String,Start)
   return string.sub(String,1,string.len(Start))==Start
end
-- wrapper that returns "unknown N" for df.enum_type[BAD_VALUE],
-- instead of returning nil or causing an error
df_enums = {}
setmetatable(df_enums, {
    __index = function(self, enum)
        if not df[enum] or df[enum]._kind ~= 'enum-type' then
            error('invalid enum: ' .. enum)
        end
        local t = {}
        setmetatable(t, {
            __index = function(self, k)
                return df[enum][k] or 'unknown ' .. k
            end
        })
        return t
    end,
    __newindex = function() error('read-only') end
})

function export_references(table)
	for refK, refV in pairs(table) do
		file:write("\t\t<reference>\n")
		file:write("\t\t\t<type>"..df.general_ref_type[refV:getType()].."</type>\n")
		if refV:getType() == df.general_ref_type.ARTIFACT then file:write("\t\t\t<id>"..refV.artifact_id.."</id>\n") -- artifact
		elseif refV:getType() == df.general_ref_type.ENTITY then file:write("\t\t\t<id>"..refV.entity_id.."</id>\n") -- entity
		elseif refV:getType() == df.general_ref_type.HISTORICAL_EVENT then file:write("\t\t\t<id>"..refV.event_id.."</id>\n") -- event
		elseif refV:getType() == df.general_ref_type.SITE then file:write("\t\t\t<id>"..refV.site_id.."</id>\n") -- site
		elseif refV:getType() == df.general_ref_type.SUBREGION then file:write("\t\t\t<id>"..refV.region_id.."</id>\n") -- region
		elseif refV:getType() == df.general_ref_type.HISTORICAL_FIGURE then file:write("\t\t\t<id>"..refV.hist_figure_id.."</id>\n") -- hist figure
		elseif refV:getType() == df.general_ref_type.WRITTEN_CONTENT then -- TODO WRITTEN_CONTENTs
		elseif refV:getType() == df.general_ref_type.POETIC_FORM then file:write("\t\t\t<id>"..refV.poetic_form_id.."</id>\n") -- poetic form
		elseif refV:getType() == df.general_ref_type.MUSICAL_FORM then file:write("\t\t\t<id>"..refV.musical_form_id.."</id>\n") -- musical form
		elseif refV:getType() == df.general_ref_type.DANCE_FORM then file:write("\t\t\t<id>"..refV.dance_form_id.."</id>\n") -- dance form
		elseif refV:getType() == df.general_ref_type.KNOWLEDGE_SCHOLAR_FLAG then -- TODO KNOWLEDGE_SCHOLAR_FLAG
		elseif refV:getType() == df.general_ref_type.ABSTRACT_BUILDING then -- abstract building
			file:write("\t\t\t<id>"..refV.building_id.."</id>\n")
			file:write("\t\t\t<site_id>"..refV.site_id.."</site_id>\n")
		elseif refV:getType() == df.general_ref_type.CONTAINED_IN_ITEM then file:write("\t\t\t<id>"..refV.item_id.."</id>\n") -- item
		elseif refV:getType() == df.general_ref_type.CONTAINS_ITEM then file:write("\t\t\t<id>"..refV.item_id.."</id>\n") -- item
		elseif refV:getType() == df.general_ref_type.IS_ARTIFACT then file:write("\t\t\t<id>"..refV.artifact_id.."</id>\n") -- artifact
		elseif refV:getType() == df.general_ref_type.UNIT_ITEMOWNER then file:write("\t\t\t<id>"..refV.unit_id.."</id>\n") -- unit
		elseif refV:getType() == df.general_ref_type.UNIT_HOLDER then file:write("\t\t\t<id>"..refV.unit_id.."</id>\n") -- unit
		elseif refV:getType() == df.general_ref_type.UNIT_TRADEBRINGER then file:write("\t\t\t<id>"..refV.unit_id.."</id>\n") -- unit
		elseif refV:getType() == df.general_ref_type.CONTAINS_UNIT then file:write("\t\t\t<id>"..refV.unit_id.."</id>\n") -- unit
		elseif refV:getType() == df.general_ref_type.IS_NEMESIS then file:write("\t\t\t<id>"..refV.nemesis_id.."</id>\n") -- unit
		elseif refV:getType() == df.general_ref_type.BUILDING_HOLDER then file:write("\t\t\t<id>"..refV.building_id.."</id>\n") -- building
		elseif refV:getType() == df.general_ref_type.BUILDING_NEST_BOX then file:write("\t\t\t<id>"..refV.building_id.."</id>\n") -- building
		elseif refV:getType() == df.general_ref_type.BUILDING_CIVZONE_ASSIGNED then file:write("\t\t\t<id>"..refV.building_id.."</id>\n") -- building
		elseif refV:getType() == df.general_ref_type.BUILDING_CHAIN then file:write("\t\t\t<id>"..refV.building_id.."</id>\n") -- building
		elseif refV:getType() == df.general_ref_type.BUILDING_TRIGGER then file:write("\t\t\t<id>"..refV.building_id.."</id>\n") -- building
		elseif refV:getType() == df.general_ref_type.BUILDING_TRIGGERTARGET then file:write("\t\t\t<id>"..refV.building_id.."</id>\n") -- building
		elseif refV:getType() == df.general_ref_type.ACTIVITY_EVENT then file:write("\t\t\t<id>"..refV.anon_1.."</id>\n") -- event
		elseif refV:getType() == df.general_ref_type.INTERACTION then -- TODO INTERACTION
		elseif refV:getType() == df.general_ref_type.VALUE_LEVEL then -- TODO VALUE_LEVEL
		elseif refV:getType() == df.general_ref_type.LANGUAGE then -- TODO LANGUAGE
		else
			print("unknown reference",refV:getType(),df.general_ref_type[refV:getType()])
			for k,v in pairs(refV) do print(k,v) end
		end
		file:write("\t\t</reference>\n")
	end
end

function export_geo_biomes()
	--[[  geo_biomes can't be easily associated with map tiles or biomes without embarking, I think.
	file:write("<geo_biomes>\n")
    for geobiomeK, geobiomeV in ipairs(df.global.world.world_data.geo_biomes) do
		file:write("\t<geo_biome>\n")
		file:write("\t\t\t<id>"..geobiomeK.."</id>\n")
		for layerK, layerV in ipairs(geobiomeV.layers) do
			file:write("\t\t<layer>\n")
			file:write("\t\t\t<id>"..layerK.."</id>\n")
			file:write("\t\t\t<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(0, layerV.mat_index))..":"..layerV.type.."-"..layerV.mat_index.."</mat>\n")
			for k, v in ipairs(layerV.vein_mat) do
				file:write("\t\t\t<vein>"..dfhack.matinfo.toString(dfhack.matinfo.decode(0, v))..":"..layerV.vein_type[k].."-"..v.."</vein>\n")
		
			end
			for k, v in ipairs(layerV.vein_nested_in) do
				if (v ~= -1) then
					file:write("\t\t\t<vein_nested_in>"..k..","..v.."</vein_nested_in>\n")
				end
			end
			file:write("\t\t\t<top_height>"..layerV.top_height.."</top_height>\n")
			file:write("\t\t\t<bottom_height>"..layerV.bottom_height.."</bottom_height>\n")
			file:write("\t\t</layer>\n")
		end
		file:write("\t</geo_biome>\n")
    end
    file:write("</geo_biomes>\n")
	--]]
end
function export_armies()
	file:write("<armies>\n")
    for armyK, armyV in ipairs(df.global.world.armies.all) do
		file:write("\t<army>\n")
		file:write("\t\t<id>"..armyV.id.."</id>\n")
		file:write("\t\t<coords>"..armyV.pos.x..","..armyV.pos.y.."</coords>\n")
		file:write("\t\t<item_type>"..tostring(df.item_type[armyV.item_type]):lower().."</item_type>\n")
		if (armyV.item_subtype ~= -1) then
			file:write("\t\t<item_subtype>"..armyV.item_subtype.."</item_subtype>\n")
		end
		
		file:write("\t\t<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(armyV.mat_type, armyV.mat_index)).."</mat>\n")
		file:write("\t</army>\n")
    end
    file:write("</armies>\n")
end
function export_units()
	file:write("<units>\n")
    for unitK, unitV in ipairs(df.global.world.units.all) do
		file:write("\t<unit>\n")
		file:write("\t\t<id>"..unitV.id.."</id>\n")
		file:write("\t\t<name>"..dfhack.df2utf(dfhack.TranslateName(unitV.name)).."</name>\n")
		file:write("\t\t<name2>"..dfhack.df2utf(dfhack.TranslateName(unitV.name,1)).."</name2>\n")
		file:write("\t\t<profession>"..df_enums.profession[unitV.profession]:lower().."</profession>\n")
		file:write("\t\t<profession2>"..df_enums.profession[unitV.profession2]:lower().."</profession2>\n")
		file:write("\t\t<race>"..(df.global.world.raws.creatures.all[unitV.race].creature_id).."</race>\n")
		file:write("\t\t<coords>"..unitV.pos.x..","..unitV.pos.y..","..unitV.pos.z.."</coords>\n")
		file:write("\t\t<caste>"..(df.global.world.raws.creatures.all[unitV.race].caste[unitV.caste].caste_id):lower().."</caste>\n")
		file:write("\t\t<sex>"..unitV.sex.."</sex>\n")
		if (unitV.civ_id ~= -1) then
			file:write("\t\t<civ_id>"..unitV.civ_id.."</civ_id>\n")
		end
		if (unitV.population_id ~= -1) then
			file:write("\t\t<population_id>"..unitV.population_id.."</population_id>\n")
		end
		file:write("\t\t<birth_year>"..unitV.birth_year.."</birth_year>\n")
		file:write("\t\t<birth_time>"..unitV.birth_time.."</birth_time>\n")
		if (unitV.hist_figure_id ~= -1) then
			file:write("\t\t<hist_figure_id>"..unitV.hist_figure_id.."</hist_figure_id>\n")
		end
		file:write("\t\t<inventory>\n")
		for k,itemV in pairs(unitV.inventory) do
			file:write("\t\t\t<unit_inventory_item>\n")
			file:write("\t\t\t\t<id>"..itemV.item.id.."</id>\n")
			file:write("\t\t\t\t<mode>"..itemV.mode.."</mode>\n")
			if (itemV.body_part_id ~= -1) then
				file:write("\t\t\t\t".."<body_part>"..unitV.body.body_plan.body_parts[itemV.body_part_id].name_singular[0].value.."</body_part>".."\n")
			end
			file:write("\t\t\t</unit_inventory_item>\n")
		end
		file:write("\t\t</inventory>\n")
		
		-- Ignore flags, not interesting
		local ignoreFlags = {"calculated_nerves", "calculated_bodyparts", "calculated_insulation",
							 "vision_good", "breathing_good", "body_part_relsize_computed",
							 "size_modifier_computed", "stuck_weapon_computed", "body_temp_in_range",
							 "move_state", "can_swap", "calculated_inventory", "unk1", "unk2", "unk3",
							 "roaming_wilderness_population_source","roaming_wilderness_population_source_not_a_map_feature"}
		file:write("\t\t<flags>")		
		for flagName,flagValue in pairs(unitV.flags1) do
			if (flagValue == true and not table.contains(ignoreFlags, flagName)) then
				file:write(flagName..",")
			end
		end
		for flagName,flagValue in pairs(unitV.flags2) do
			if (flagValue == true and not table.contains(ignoreFlags, flagName)) then
				file:write(flagName..",")
			end
		end
		for flagName,flagValue in pairs(unitV.flags3) do
			if (flagValue == true and not table.contains(ignoreFlags, flagName)) then
				file:write(flagName..",")
			end
		end
		for flagName,flagValue in pairs(unitV.flags4) do
			if (flagValue == true and not table.contains(ignoreFlags, flagName)) then
				file:write(flagName..",")
			end
		end
		file:write("</flags>\n")
		export_references(unitV.general_refs)
		file:write("\t</unit>\n")
    end
    file:write("</units>\n")
end

function export_engravings()
	file:write("<engravings>\n")
    for engravingK, engravingV in ipairs(df.global.world.engravings) do
		file:write("\t<engraving>\n")
		file:write("\t\t<id>"..engravingK.."</id>\n")
		file:write("\t\t<artist>"..engravingV.artist.."</artist>\n")
		if (engravingV.masterpiece_event ~= -1) then
			file:write("\t\t<masterpiece_event>"..engravingV.masterpiece_event.."</masterpiece_event>\n")
		end
		file:write("\t\t<skill_rating>"..engravingV.skill_rating.."</skill_rating>\n")
		file:write("\t\t<coords>"..engravingV.pos.x..","..engravingV.pos.y..","..engravingV.pos.z.."</coords>\n")
		file:write("\t\t<tile>"..engravingV.tile.."</tile>\n")
		file:write("\t\t<art_id>"..engravingV.art_id.."</art_id>\n")
		file:write("\t\t<art_subid>"..engravingV.art_subid.."</art_subid>\n")
		file:write("\t\t<quality>"..engravingV.tile.."</quality>\n")
		engravinglocation = "floor"
		if (engravingV.flags.west) then
			engravinglocation = "west"
		elseif (engravingV.flags.east) then
			engravinglocation = "east"
		elseif (engravingV.flags.north) then
			engravinglocation = "north"
		elseif (engravingV.flags.south) then
			engravinglocation = "south"
		elseif (engravingV.flags.northwest) then
			engravinglocation = "northwest"
		elseif (engravingV.flags.northeast) then
			engravinglocation = "northeast"
		elseif (engravingV.flags.southwest) then
			engravinglocation = "southwest"
		elseif (engravingV.flags.southeast) then
			engravinglocation = "southeast"
		end
		file:write("\t\t<location>"..engravinglocation.."</location>\n")
		if (engravingV.flags.hidden == true) then
			file:write("\t\t<hidden/>\n")
		end
		file:write("\t</engraving>\n")
    end
    file:write("</engravings>\n")
end
function export_reports()
	file:write("<reports>\n")
    for reportK, reportV in ipairs(df.global.world.status.reports) do
		file:write("\t<report>\n")
		file:write("\t\t<id>"..reportV.id.."</id>\n")
		file:write("\t\t<text>"..dfhack.utf2df(reportV.text).."</text>\n")
		file:write("\t\t<type>"..df.announcement_type[reportV.type]:lower().."</type>\n")
		file:write("\t\t<year>"..reportV.year.."</year>\n")
		file:write("\t\t<time>"..reportV.time.."</time>\n")
		if (reportV.flags.continuation) then
			file:write("\t\t<continuation/>\n")
		end
		if (reportV.flags.announcement) then
			file:write("\t\t<announcement/>\n")
		end
		file:write("\t</report>\n")
    end
    file:write("</reports>\n")
end
function export_buildings()
file:write("<buildings>\n")
    for buildingK, buildingV in ipairs(df.global.world.buildings.all) do
		file:write("\t<building>\n")
		file:write("\t\t<id>"..buildingV.id.."</id>\n")
		file:write("\t\t<name>"..buildingV.name.."</name>\n")
		file:write("\t\t<coords1>"..buildingV.x1..","..buildingV.y1..","..buildingV.z.."</coords1>\n")
		file:write("\t\t<coordscenter>"..buildingV.centerx..","..buildingV.centery..","..buildingV.z.."</coordscenter>\n")
		file:write("\t\t<coords2>"..buildingV.x2..","..buildingV.y2..","..buildingV.z.."</coords2>\n")
		file:write("\t\t<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(buildingV.mat_type, buildingV.mat_index)).."</mat>\n")
		if (buildingV.race >= 0) then
			file:write("\t\t<race>"..(df.global.world.raws.creatures.all[buildingV.race].creature_id).."</race>\n")
		end
		buildingType = df.building_type[buildingV:getType()]:lower()
		file:write("\t\t<type>"..buildingType.."</type>\n")
		if (buildingType == "workshop") then

			file:write("\t\t<subtype>"..df.workshop_type[buildingV.type]:lower().."</subtype>\n")
		elseif (buildingType == "bed") then
			if (buildingV.owner ~= nil) then
				file:write("\t\t<owner_unit_id>"..buildingV.owner.id.."</owner_unit_id>\n")
			end
		elseif (buildingType == "construction") then
			file:write("\t\t<subtype>"..df.construction_type[buildingV.type]:lower().."</subtype>\n")
		elseif (buildingType == "furnace") then
			file:write("\t\t<subtype>"..df.furnace_type[buildingV.type]:lower().."</subtype>\n")
		elseif (buildingType == "civzone") then
			file:write("\t\t<subtype>"..df.civzone_type[buildingV.type]:lower().."</subtype>\n")
			file:write("\t\t<zone_flags>")
			for kFlag, vFlag in pairs(buildingV.zone_flags) do
				if (vFlag) then
					file:write(kFlag..",")
				end
			end
			file:write("</zone_flags>\n")					
		elseif (buildingType == "trap") then
			file:write("\t\t<subtype>"..df.trap_type[buildingV.trap_type]:lower().."</subtype>\n")
		elseif (buildingType == "coffin") then
			for kContainedItem, vContainedItem in pairs(buildingV.contained_items) do
				if (vContainedItem.item:getType() == 23) then -- corpse
					file:write("\t\t<corpse_hf>"..vContainedItem.item.hist_figure_id.."</corpse_hf>\n")
					file:write("\t\t<corpse_unit>"..vContainedItem.item.unit_id.."</corpse_unit>\n")
					break
				end
			end
		elseif (buildingType == "bridge") then
			file:write("\t\t<direction>"..buildingV.direction.."</direction>\n")
		elseif (buildingType == "nestbox") then
			file:write("\t\t<claimed_by>"..buildingV.claimed_by.."</claimed_by>\n")
		elseif (buildingType == "armorstand" or buildingType == "weaponrack" or buildingType == "cabinet"
			 or buildingType == "box") then
			for kSquad, vSquad in pairs(buildingV.squads) do
				file:write("\t\t<squad>"..vSquad.squad_id.."</squad>\n")
			end
		elseif (buildingType == "") then
		else -- stockpile, door, table, chair, statue, cage, farmplot, barsfloor, barsvertical, chain, support, tradedepot

		end
		export_references(buildingV.general_refs)
		file:write("\t</building>\n")
    end
    file:write("</buildings>\n")
end
function export_constructions()
	file:write("<constructions>\n")
    for constructionK, constructionV in ipairs(df.global.world.constructions) do
		file:write("\t<construction>\n")
		file:write("\t\t<id>"..constructionK.."</id>\n")
		file:write("\t\t<coords>"..constructionV.pos.x..","..constructionV.pos.y..","..constructionV.pos.z.."</coords>\n")
		file:write("\t\t<item_type>"..tostring(df.item_type[constructionV.item_type]):lower().."</item_type>\n")
		if (constructionV.item_subtype ~= -1) then
			file:write("\t\t<item_subtype>"..constructionV.item_subtype.."</item_subtype>\n")
		end
		file:write("\t\t<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(constructionV.mat_type, constructionV.mat_index)).."</mat>\n")

		file:write("\t</construction>\n")
    end
    file:write("</constructions>\n")
end
function export_items()
	file:write("<items>\n")
	for itemK, itemV in ipairs(df.global.world.items.all) do
		file:write("\t<item>\n")
		file:write("\t\t<id>"..itemV.id.."</id>\n")
		if (itemV:getType() ~= -1) then
			file:write("\t\t<type>"..tostring(df.item_type[itemV:getType()]):lower().."</type>\n")
			if (itemV:getSubtype() ~= -1) then
				if (type(itemV.subtype) == "number") then
					file:write("\t\t<subtype>"..itemV.subtype.."</subtype>\n")
				else
					file:write("\t\t<subtype>"..itemV.subtype.name.."</subtype>\n")
				end
			end
		end
		if (itemV:getMaterial() ~= -1 and itemV:getMaterialIndex() ~= -1) then
			file:write("\t\t<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(itemV:getMaterial(), itemV:getMaterialIndex())).."</mat>\n")
		end
		for k,v in pairs(itemV) do
			--file:write(k.."\t"..type(v).."\t"..itemV:getType().."\t"..itemK.."\n")
			
			if type(v) == "number" then
				--elseif (k == "subtype" or k == "age" or k == "anon_1" or k == "base_uniform_score" or k == "boiling_point" or k == "colddam_point" or k == "engraving_type" or k == "fixed_temp" or k == "heatdam_point" or k == "id" or k == "ignite_point" or k == "maker" or k == "maker_race" or k == "masterpiece_event" or k == "mat_index" or k == "mat_type" or k == "melting_point" or k == "quality" or k == "sharpness" or k == "skill_used" or k == "spec_heat" or k == "stack_size" or k == "stockpile_countdown" or k == "stockpile_delay" or k == "temp_updated_frame" or k == "topic" or k == "unk2" or k == "vehicle_id" or k == "walkable_id" or k == "wear" or k == "wear_timer" or k == "weight" or k == "weight_fraction" or k == "world_data_id" or k == "world_data_subid") then
				if (string.starts(k, "anon_") or string.starts(k,"unk") or k == "item_plant_growthst.anon_1" or k == "item_body_component.anon_1" or k == "item_sheetst.anon_1") then -- Ignore, not known
				
				elseif (k == "subtype" or k == "curse_year" or k == "curse_time" or k == "birth_year" or k == "birth_time" or k == "planting_skill" or k == "death_year" or k == "death_time" or k == "race" or k == "race2" or k == "caste" or k == "caste2" or k == "mat_index" or k == "mat_type" or k == "id" or k == "dye_mat_index" or k == "sex" ) then -- Ignore, covered elsewhere
				
				elseif (k == "base_uniform_score" or k == "maker_race" or k == "dimension" or k == "sharpness" or k == "rot_timer" or k == "blood_count" or k == "stored_fat" or k == "birth_year_bias" or k == "birth_time_bias" or k == "grow_counter"  or k == "walkable_id" or k == "fixed_temp"  or k == "spec_heat" or k == "ignite_point" or k == "colddam_point" or k == "boiling_point" or k == "temperature" or k == "stack_size" or k == "melting_point" or k == "heatdam_point" or k == "temp_updated_frame"  or k == "wear_timer" or k == "stockpile_countdown" or k == "unit_id2"  or k == "hist_figure_id2"  or k == "bone2" or k == "stockpile_delay" or k == "weight_fraction"  ) then -- Not important enough
				
				elseif (k == "dye_mat_type") then
					if (v ~= -1 and itemV.dye_mat_index ~= -1) then
						file:write("\t\t<dye_mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(v, itemV.dye_mat_index)).."</dye_mat>\n")
					end
				elseif (k == "quality" or k == "dye_quality" or k == "age" or k == "skill_used"or k == "wear"or k =="weight" or k == "weight_fraction"  or k == "id") then -- Only print if non-zero
					if (v ~= 0) then
						file:write("\t\t<"..k..">"..v.."</"..k..">\n")
					end
				elseif (k == "maker" or k == "shape" or k == "topic" or k == "engraving_type" or k == "hist_figure_id" or k == "unit_id" or k == "hist_figure_id2" or k == "unit_id2" or k == "recipe_id" or k == "entity" or k == "vehicle_id" or k =="masterpiece_event" or k == "world_data_id" or k == "world_data_subid" or k == "temp_updated_frame") then -- Only print if not -1
					if (v ~= -1) then
						file:write("\t\t<"..k..">"..v.."</"..k..">\n")
					end
				else
					 --Unexpected item property
					file:write("\t\t<"..k..">"..v.."</"..k..">\n")
				end
			
			elseif type(v) == "string" then
				--k == "description" or k == "title"
				file:write("\t\t<"..k..">"..v:lower().."</"..k..">\n")
				
			elseif type(v) == nil then
				--k == "magic" and others
			
			elseif type(v) == "userdata" then
				--k == "flags" or k == "flags2" or k == "general_refs" or k == "improvements" or k == "pos" or k == "specific_refs" or k == "stockpile"or k == "temperature"
				if (k == "subtype") then -- Ignore, handled elsewhere
				
				elseif (k == "pos") then
					if (itemV.pos.x ~= -30000) then
						file:write("\t\t<coords>"..itemV.pos.x..","..itemV.pos.y..","..itemV.pos.z.."</coords>\n")
					end
				elseif (k == "temperature") then
					file:write("\t\t<temperature>"..v.whole.."."..v.fraction.."</temperature>\n")
				elseif (k == "general_refs") then --Handled elsewhere

				elseif (k == "specific_refs" or k == "flags2" or k == "corpse_flags" or k == "contaminants" or k == "material_amount" or k == "body" or k == "appearance") then -- Ignore, not important

				elseif (k == "ingredients") then
					file:write("\t\t<"..k..">\n")
					for ingredientK, ingredientV in ipairs(v) do
						file:write("\t\t\t<ingredient>\n")
						if (ingredientV.item_type ~= -1) then		
							file:write("\t\t\t\t<item_type>"..df.item_type[ingredientV.item_type]:lower().."</item_type>\n")
						end
						if (ingredientV.mat_type ~= -1) then
							file:write("\t\t\t\t<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(ingredientV.mat_type, ingredientV.mat_index)).."</mat>\n")
						end
						if (ingredientV.maker ~= -1) then
							file:write("\t\t\t\t<maker>"..ingredientV.maker.."</maker>\n")
						end 
						file:write("\t\t\t</ingredient>\n")
					end
					file:write("\t\t</"..k..">\n")
				elseif (k == "history_info") then
					if (not v.value.kills == nil) then
						file:write("\t\t\t\t<kills>"..v.value.kills.."</kills>\n")
					end
				elseif (k == "improvements") then
					file:write("\t\t<"..k..">\n")
					for refK, refV in ipairs(v) do
						file:write("\t\t\t<improvement>\n")
						file:write("\t\t\t\t<improvement_type>"..df.improvement_type[refV:getType()]:lower().."</improvement_type>\n")
						for improvementK, improvementV in pairs(refV) do
							if (string.find(improvementK, "anon_1") ~= nil) then -- Ignore, anon
								
							elseif (type(improvementV) == "userdata") then
								if (improvementK == "cover_flags") then -- Ignore, not important
								elseif (improvementK == "image") then
									file:write("\t\t\t\t<image>\n")
									file:write("\t\t\t\t\t<id>"..improvementV.id.."</id>\n")
									file:write("\t\t\t\t\t<subid>"..improvementV.subid.."</subid>\n")
									file:write("\t\t\t\t\t<civ_id>"..improvementV.civ_id.."</civ_id>\n")
									file:write("\t\t\t\t\t<site_id>"..improvementV.site_id.."</site_id>\n")
									file:write("\t\t\t\t</image>\n")
								elseif (improvementK == "dye") then
									printout = ""
									if (improvementV.mat_type ~= -1) then
										printout = printout..("\t\t\t\t\t<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(improvementV.mat_type, improvementV.mat_index)).."</mat>\n")
									end
									if (improvementV.dyer ~= -1) then
										printout = printout..("\t\t\t\t\t<dyer>"..improvementV.dyer.."</dyer>\n")
									end
									if (improvementV.quality ~= 0) then
										printout = printout..("\t\t\t\t\t<quality>"..improvementV.quality.."</quality>\n")
									end
									if (improvementV.skill_rating ~= 0) then
										printout = printout..("\t\t\t\t\t<skill_rating>"..improvementV.skill_rating.."</skill_rating>\n")
									end
									if (printout ~= "") then
										file:write("\t\t\t\t<"..improvementK..">\n")
										file:write(printout)
										file:write("\t\t\t\t</"..improvementK..">\n")
									else
										file:write("\t\t\t\t<"..improvementK.."/>\n")
									end
								elseif (improvementK == "contents") then
									file:write("\t\t\t\t<"..improvementK..">"..improvementV[0].."</"..improvementK..">\n")
								else
									
								end 
							else
								if (type(improvementV) == "number" and improvementV == -1) then -- Ignore, -1
								
								elseif ((improvementK == "quality" or improvementK == "skill_rating") and improvementV == 0) then -- Ignore, 0
								
								elseif (improvementK == "mat_index" or improvementK == "anon_1") then -- Ignore, handled elsewhere
								
								elseif (improvementK == "mat_type") then
									file:write("\t\t\t\t<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(improvementV, refV.mat_index)).."</mat>\n")
								else
									file:write("\t\t\t\t<"..improvementK..">"..improvementV.."</"..improvementK..">\n")
								end 
							end
						end
						file:write("\t\t\t</improvement>\n")
					end
					file:write("\t\t</"..k..">\n")
				elseif (k == "handedness") then
					printout = ""
					for tableK, tableV in pairs(v) do
						if (type(tableV) == "boolean") then
							if (tableV == true) then 
								printout = printout..tableK..","
							end 
						end 
					end
					if (printout ~= "") then
						file:write("\t\t<"..k..">"..printout.."</"..k..">\n")
					end				
				elseif (string.starts(k,"bone")) then
					file:write("\t\t<"..k..">"..dfhack.matinfo.toString(dfhack.matinfo.decode(v.mat_type, v.mat_index)).."</"..k..">\n")

				elseif (k ~= "stockpile" or v.id ~= -1) then -- If stockpile print if not -1, print all others
					printout = ""
					for tableK, tableV in pairs(v) do
						if (type(k) == "number") then  -- Can't have just numbers for tags
						
						elseif (type(tableV) == "number" or type(tableV) == "string") then
							printout = printout..("\t\t\t<"..tableK..">"..tableV.."</"..tableK..">\n")
						elseif (type(tableV) == "boolean") then
							if (tableV == true) then 
								printout = printout..("\t\t\t<"..tableK.."/>\n")
							end 
						else
							printout = printout..("\t\t\t<"..tableK..">"..type(tableV).."</"..tableK..">\n")
						end 
					end
					if (printout ~= "") then
						file:write("\t\t<"..k..">\n")
						file:write(printout)
						file:write("\t\t</"..k..">\n")
					end
					
				end
			end
		end
		export_references(itemV.general_refs)
		file:write("\t</item>\n")
	end
	file:write("</items>\n")
end
function export_plants()
	file:write("<plants>\n")
    for plantK, plantV in ipairs(df.global.world.plants.all) do
		file:write("\t<plant>\n")
		file:write("\t\t<id>"..plantK.."</id>\n")
		file:write("\t\t<material>"..df.global.world.raws.plants.all[plantV.material].name.."</material>\n")
		file:write("\t\t<coords>"..plantV.pos.x..","..plantV.pos.y..","..plantV.pos.z.."</coords>\n")
		file:write("\t</plant>\n")
    end
    file:write("</plants>\n")
end
function export_squads()
	file:write("<squads>\n")
    for squadK, squadV in ipairs(df.global.world.squads.all) do
		file:write("\t<squad>\n")
		file:write("\t\t<id>"..squadV.id.."</id>\n")
		file:write("\t\t<name>"..dfhack.df2utf(dfhack.TranslateName(squadV.name)).."</name>\n")
		file:write("\t\t<altname>"..dfhack.df2utf(dfhack.TranslateName(squadV.name,1)).."</altname>\n")
		file:write("\t\t<entity_id>"..squadV.entity_id.."</entity_id>\n")
		for positionK, positionV in pairs(squadV.positions) do
			if (positionV.occupant ~= -1) then
				file:write("\t\t<member>"..positionV.occupant.."</member>\n")
			end
		end
		file:write("\t</squad>\n")
    end
    file:write("</squads>\n")
end
function export_races()
	file:write("<races>\n")
    for raceK, raceV in ipairs(df.global.world.raws.creatures.all) do
		file:write("\t<race>\n")
		file:write("\t\t<id>"..raceK.."</id>\n")
		file:write("\t\t<key>"..raceV.creature_id.."</key>\n")
		file:write("\t\t<nameS>"..raceV.name[0].."</nameS>\n")
		file:write("\t\t<nameP>"..raceV.name[1].."</nameP>\n")
		for casteK, casteV in ipairs(raceV.caste) do
			file:write("\t\t<caste>\n")
			file:write("\t\t\t<id>"..casteK.."</id>\n")
			file:write("\t\t\t<name>"..casteV.caste_id.."</name>\n")
			file:write("\t\t\t<gender>"..casteV.gender.."</gender>\n")
			file:write("\t\t\t<description>"..casteV.description.."</description>\n")
			file:write("\t\t</caste>\n")
		end
		flagsString = ""
		for flagK, flagV in pairs(raceV.flags) do
			if (string.find(flagK, "unk_") or type(flagK) == "number" or flagV == false) then 
				
			else
				flagsString = flagsString..flagK..","
			end
		end
		if (flagsString ~= "") then
			file:write("\t\t<flags>"..flagsString:lower().."</flags>\n")
		end
		for sphereK, sphereV in pairs(raceV.sphere) do
			file:write("\t\t<sphere>"..df.sphere_type[sphereV].."</sphere>\n")
		end
		file:write("\t</race>\n")
    end
    file:write("</races>\n")
end
function export_written_contents()
    file:write("<written_contents>\n")
    for wcK, wcV in ipairs(df.global.world.written_contents.all) do
		if (tablelength(wcV.refs) > 0) then
			file:write("\t<written_content>\n")
			file:write("\t\t<id>"..wcV.id.."</id>\n")
			export_references(wcV.refs)
			file:write("\t</written_content>\n")
		end
    end
    file:write("</written_contents>\n")
end
function export_poetic_forms()
    file:write("<poetic_forms>\n")
    for formK, formV in ipairs(df.global.world.poetic_forms.all) do
        file:write("\t<poetic_form>\n")
        file:write("\t\t<id>"..formV.id.."</id>\n")
        file:write("\t\t<name>"..dfhack.df2utf(dfhack.TranslateName(formV.name,1)).."</name>\n")
        file:write("\t\t<altname>"..dfhack.df2utf(dfhack.TranslateName(formV.name)).."</altname>\n")
        file:write("\t</poetic_form>\n")
    end
    file:write("</poetic_forms>\n")
end
function export_musical_forms()
	file:write("<musical_forms>\n")
    for formK, formV in ipairs(df.global.world.musical_forms.all) do
		file:write("\t<musical_form>\n")
		file:write("\t\t<id>"..formV.id.."</id>\n")
		file:write("\t\t<name>"..dfhack.df2utf(dfhack.TranslateName(formV.name, 1)).."</name>\n")
		file:write("\t\t<altname>"..dfhack.df2utf(dfhack.TranslateName(formV.name)).."</altname>\n")
		file:write("\t</musical_form>\n")
    end
    file:write("</musical_forms>\n")
end
function export_dance_forms()
	file:write("<dance_forms>\n")
    for formK, formV in ipairs(df.global.world.dance_forms.all) do
        file:write("\t<dance_form>\n")
        file:write("\t\t<id>"..formV.id.."</id>\n")
        file:write("\t\t<name>"..dfhack.df2utf(dfhack.TranslateName(formV.name,1)).."</name>\n")
		file:write("\t\t<altname>"..dfhack.df2utf(dfhack.TranslateName(formV.name)).."</altname>\n")
        file:write("\t</dance_form>\n")
    end
    file:write("</dance_forms>\n")
end
function export_landmasses()
    file:write("<landmasses>\n")
    for landmassK, landmassV in ipairs(df.global.world.world_data.landmasses) do
        file:write("\t<landmass>\n")
        file:write("\t\t<id>"..landmassV.index.."</id>\n")
        file:write("\t\t<name>"..dfhack.df2utf(dfhack.TranslateName(landmassV.name,1)).."</name>\n")
        file:write("\t\t<area>"..landmassV.area.."</area>\n")
        file:write("\t\t<coord_1>"..landmassV.min_x..","..landmassV.min_y.."</coord_1>\n")
        file:write("\t\t<coord_2>"..landmassV.max_x..","..landmassV.max_y.."</coord_2>\n")
        file:write("\t</landmass>\n")
    end
    file:write("</landmasses>\n")
end
function export_mountains()
    file:write("<mountain_peaks>\n")
    for mountainK, mountainV in ipairs(df.global.world.world_data.mountain_peaks) do
        file:write("\t<mountain_peak>\n")
        file:write("\t\t<id>"..mountainK.."</id>\n")
        file:write("\t\t<name>"..dfhack.df2utf(dfhack.TranslateName(mountainV.name,1)).."</name>\n")
        file:write("\t\t<coords>"..mountainV.pos.x..","..mountainV.pos.y.."</coords>\n")
        file:write("\t\t<height>"..mountainV.height.."</height>\n")
        file:write("\t</mountain_peak>\n")
    end
    file:write("</mountain_peaks>\n")
end
function export_rivers()
	file:write("<rivers>\n")
    for riverK, riverV in ipairs(df.global.world.world_data.rivers) do
		file:write("\t<river>\n")
		file:write("\t\t<id>"..riverK.."</id>\n")
		for k,v in pairs(riverV) do
			if (k == "height") then
				file:write("\t\t<"..k..">"..tostring(v).."</"..k..">\n")
			elseif (k == "path") then
				file:write("\t\t<coords>")
				for xK, xVal in ipairs(riverV.path.x) do
					file:write(xVal..","..riverV.path.y[xK]..","..riverV.elevation[xK].."|")
				end
				file:write(riverV.end_pos.x..","..riverV.end_pos.y..",-1")
				file:write("</coords>\n")
			elseif (k == "elevation") then -- Handled in coords

			elseif (k == "name") then
				file:write("\t\t<name>"..dfhack.df2utf(dfhack.TranslateName(v,0)).."</name>\n")
				file:write("\t\t<name2>"..dfhack.df2utf(dfhack.TranslateName(v,1)).."</name2>\n")
			end
		end
		file:write("\t</river>\n")
	end
	file:write("</rivers>\n")
end
function export_regions()
    file:write("<regions>\n")
    for regionK, regionV in ipairs(df.global.world.world_data.regions) do
        file:write("\t<region>\n")
        file:write("\t\t<id>"..regionV.index.."</id>\n")
        file:write("\t\t<coords>")
            for xK, xVal in ipairs(regionV.region_coords.x) do
                file:write(xVal..","..regionV.region_coords.y[xK].."|")
            end
        file:write("</coords>\n")
        file:write("\t\t".."<population>")
            for popK, popV in ipairs(regionV.population) do
				if (popV.type ~= 7 and popV.type ~= 6 and popV.type ~= 5) then
					file:write(popV.race..","..popV.count_min..","..popV.type.."|")
				end
            end
        file:write("</population>\n")		
        file:write("\t</region>\n")
    end
	file:write("</regions>\n")
end
function export_underground_regions()
    file:write("<underground_regions>\n")
    for regionK, regionV in ipairs(df.global.world.world_data.underground_regions) do
        file:write("\t<underground_region>\n")
        file:write("\t\t<id>"..regionV.index.."</id>\n")
        file:write("\t\t<coords>")
            for xK, xVal in ipairs(regionV.region_coords.x) do
                file:write(xVal..","..regionV.region_coords.y[xK].."|")
            end
        file:write("</coords>\n")
        file:write("\t\t".."<population>")
            for popK, popV in ipairs(regionV.feature_init.feature.population) do
				if (popV.type ~= 7 and popV.type ~= 6 and popV.type ~= 5) then
					file:write(popV.race..","..popV.count_min..","..popV.type.."|")
				end
            end
        file:write("</population>\n")				
        file:write("\t</underground_region>\n")
    end
end
function export_sites()
    file:write("</underground_regions>\n")

    file:write("<sites>\n")
    for siteK, siteV in ipairs(df.global.world.world_data.sites) do
        file:write("\t<site>\n")
		file:write("\t\t<id>"..siteV.id.."</id>\n")
        for k,v in pairs(siteV) do
            if ((k == "civ_id" or k == "cur_owner_id") and v ~= -1) then
                file:write("\t\t<"..k..">"..tostring(v).."</"..k..">\n")
            elseif (k == "buildings") then
                if (#siteV.buildings > 0) then
                    file:write("\t\t<structures>\n")
                    for buildingK, buildingV in ipairs(siteV.buildings) do
                        file:write("\t\t\t<structure>\n")
                        file:write("\t\t\t\t<id>"..buildingV.id.."</id>\n")
                        file:write("\t\t\t\t<type>"..df_enums.abstract_building_type[buildingV:getType()]:lower().."</type>\n")
                        if table.containskey(buildingV,"name") then
                            file:write("\t\t\t\t<name>"..dfhack.df2utf(dfhack.TranslateName(buildingV.name, 1)).."</name>\n")
                            file:write("\t\t\t\t<name2>"..dfhack.df2utf(dfhack.TranslateName(buildingV.name)).."</name2>\n")
                        end
                        if (buildingV:getType() == df.abstract_building_type.TEMPLE) then
                            file:write("\t\t\t\t<deity>"..buildingV.deity.."</deity>\n")
                            file:write("\t\t\t\t<religion>"..buildingV.religion.."</religion>\n")
                        end
                        if (buildingV:getType() == df.abstract_building_type.DUNGEON) then
                            file:write("\t\t\t\t<dungeon_type>"..buildingV.dungeon_type.."</dungeon_type>\n")
                        end
						
						if (buildingV.inhabitants ~= nil and tablelength(buildingV.inhabitants) > 0) then
							file:write("\t\t<inhabitants>")
							for inhabitabntK,inhabitantV in pairs(buildingV.inhabitants) do
								file:write(inhabitantV.histfig_id..",")
							end
							file:write("</inhabitants>\n")
						end
                        file:write("\t\t\t</structure>\n")
                    end
                    file:write("\t\t</structures>\n")
                end
            end
        end
        file:write("\t</site>\n")
    end
    file:write("</sites>\n")
end
function export_world_constructions()
    file:write("<world_constructions>\n")
    for wcK, wcV in ipairs(df.global.world.world_data.constructions.list) do
        file:write("\t<world_construction>\n")
        file:write("\t\t<id>"..wcV.id.."</id>\n")
        file:write("\t\t<name>"..dfhack.df2utf(dfhack.TranslateName(wcV.name,1)).."</name>\n")
        file:write("\t\t<type>"..(df_enums.world_construction_type[wcV:getType()]):lower().."</type>\n")
        file:write("\t\t<coords>")
        for xK, xVal in ipairs(wcV.square_pos.x) do
            file:write(xVal..","..wcV.square_pos.y[xK].."|")
        end
        file:write("</coords>\n")
        file:write("\t</world_construction>\n")
    end
	file:write("</world_constructions>\n")
end
function export_artifacts()
    file:write("<artifacts>\n")
    for artifactK, artifactV in ipairs(df.global.world.artifacts.all) do
        file:write("\t<artifact>\n")
        file:write("\t\t<id>"..artifactV.id.."</id>\n")
        if (artifactV.item:getType() ~= -1) then
            file:write("\t\t<item_type>"..tostring(df_enums.item_type[artifactV.item:getType()]):lower().."</item_type>\n")
            if (artifactV.item:getSubtype() ~= -1) then
                file:write("\t\t<item_subtype>"..artifactV.item.subtype.name.."</item_subtype>\n")
            end
            for improvementK,impovementV in pairs(artifactV.item.improvements) do
                if impovementV:getType() == df.improvement_type.WRITING then
                    for writingk,writingV in pairs(impovementV.contents) do
                        file:write("\t\t<writing>"..writingV.."</writing>\n")
                    end
                elseif impovementV:getType() == df.improvement_type.PAGES then
                    file:write("\t\t<page_count>"..impovementV.count.."</page_count>\n")
                    for writingk,writingV in pairs(impovementV.contents) do
                        file:write("\t\t<writing>"..writingV.."</writing>\n")
                    end
                end
            end
        end
        if (table.containskey(artifactV.item,"description")) then
            file:write("\t\t<item_description>"..dfhack.df2utf(artifactV.item.description:lower()).."</item_description>\n")
        end
        if artifactV.item:getMaterial() ~= -1 then
            file:write("\t\t<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(artifactV.item:getMaterial(), artifactV.item:getMaterialIndex())).."</mat>\n")
        end
        file:write("\t</artifact>\n")
    end
    file:write("</artifacts>\n")
end
function export_historical_figures()
    file:write("<historical_figures>\n")
    for hfK, hfV in ipairs(df.global.world.history.figures) do
        file:write("\t<historical_figure>\n")
        file:write("\t\t<id>"..hfV.id.."</id>\n")
        file:write("\t\t<sex>"..hfV.sex.."</sex>\n")
        if hfV.race >= 0 then 
			file:write("\t\t<race_id>"..hfV.race.."</race_id>\n") 
		end
        file:write("\t</historical_figure>\n")
    end
    file:write("</historical_figures>\n")
end
function export_entity_populations()
    file:write("<entity_populations>\n")
    for entityPopK, entityPopV in ipairs(df.global.world.entity_populations) do
        file:write("\t<entity_population>\n")
        file:write("\t\t<id>"..entityPopV.id.."</id>\n")
        for raceK, raceV in ipairs(entityPopV.races) do
            file:write("\t\t<race_count>"..raceK..":"..entityPopV.counts[raceK].."</race_count>\n")
        end
        file:write("\t\t<civ_id>"..entityPopV.civ_id.."</civ_id>\n")
        file:write("\t</entity_population>\n")
    end
    file:write("</entity_populations>".."\n")
end
function export_entities()
    file:write("<entities>\n")
    for entityK, entityV in ipairs(df.global.world.entities.all) do
        file:write("\t<entity>\n")
        file:write("\t\t<id>"..entityV.id.."</id>\n")
        if entityV.race >= 0 then
            file:write("\t\t<race_id>"..entityV.race.."</race_id>\n")
        end
        file:write("\t\t<type>"..(df_enums.historical_entity_type[entityV.type]):lower().."</type>\n")
        if entityV.type == df.historical_entity_type.Religion then -- Get worshipped figure
            if (entityV.unknown1b ~= nil and entityV.unknown1b.worship ~= nil) then
                for k,v in pairs(entityV.unknown1b.worship) do
                    file:write("\t\t<worship_id>"..v.."</worship_id>\n")
                end
            end
        end
        for id, link in pairs(entityV.entity_links) do
            file:write("\t\t<entity_link>\n")
                for k, v in pairs(link) do
                    if (k == "type") then
                        file:write("\t\t\t<"..k..">"..tostring(df_enums.entity_entity_link_type[v]).."</"..k..">\n")
                    else
                        file:write("\t\t\t<"..k..">"..v.."</"..k..">\n")
                    end
                end
            file:write("\t\t</entity_link>\n")
        end
        for positionK,positionV in pairs(entityV.positions.own) do
            file:write("\t\t<entity_position>\n")
            file:write("\t\t\t<id>"..positionV.id.."</id>\n")
            if positionV.name[0]          ~= "" then file:write("\t\t\t<name>"..positionV.name[0].."</name>\n") end
            if positionV.name_male[0]     ~= "" then file:write("\t\t\t<name_male>"..positionV.name_male[0].."</name_male>\n") end
            if positionV.name_female[0]   ~= "" then file:write("\t\t\t<name_female>"..positionV.name_female[0].."</name_female>\n") end
            if positionV.spouse[0]        ~= "" then file:write("\t\t\t<spouse>"..positionV.spouse[0].."</spouse>\n") end
            if positionV.spouse_male[0]   ~= "" then file:write("\t\t\t<spouse_male>"..positionV.spouse_male[0].."</spouse_male>\n") end
            if positionV.spouse_female[0] ~= "" then file:write("\t\t\t<spouse_female>"..positionV.spouse_female[0].."</spouse_female>\n") end
            file:write("\t\t</entity_position>\n")
        end
        for assignmentK,assignmentV in pairs(entityV.positions.assignments) do
            file:write("\t\t<entity_position_assignment>\n")
            for k, v in pairs(assignmentV) do
                if ((k == "id" or k == "histfig" or k == "position_id" or k == "squad_id") and v ~= -1) then
                    file:write("\t\t\t<"..k..">"..v.."</"..k..">\n")
                end
            end
            file:write("\t\t</entity_position_assignment>\n")
        end
		if (entityV.histfig_ids ~= nil and tablelength(entityV.histfig_ids) > 0) then
			file:write("\t\t<histfig_ids>")
			for idx,id in pairs(entityV.histfig_ids) do
				file:write(id..",")
			end
			file:write("</histfig_ids>\n")
		end
        if (table.containskey(entityV,"occasion_info") and entityV.occasion_info ~= nil) then
            for occasionK, occasionV in pairs(entityV.occasion_info.occasions) do
                file:write("\t\t<occasion>\n")
                file:write("\t\t\t<id>"..occasionV.id.."</id>\n")
                file:write("\t\t\t<name>"..dfhack.df2utf(dfhack.TranslateName(occasionV.name,1)).."</name>\n")
                file:write("\t\t\t<event>"..occasionV.event.."</event>\n")
                for scheduleK, scheduleV in pairs(occasionV.schedule) do
                    file:write("\t\t\t<schedule>\n")
                    file:write("\t\t\t\t<id>"..scheduleK.."</id>\n")
                    file:write("\t\t\t\t<type>"..df_enums.occasion_schedule_type[scheduleV.type]:lower().."</type>\n")
                    if(scheduleV.type == df.occasion_schedule_type.THROWING_COMPETITION) then
                        file:write("\t\t\t\t<item_type>"..df_enums.item_type[scheduleV.reference]:lower().."</item_type>\n")
                        file:write("\t\t\t\t<item_subtype>"..getItemSubTypeName(scheduleV.reference,scheduleV.reference2).."</item_subtype>\n")
                    else
						if (scheduleV.reference ~= -1) then
							file:write("\t\t\t\t<reference>"..scheduleV.reference.."</reference>\n")
						end
						if (scheduleV.reference2 ~= -1) then
							file:write("\t\t\t\t<reference2>"..scheduleV.reference2.."</reference2>\n")
						end
                    end
                    for featureK, featureV in pairs(scheduleV.features) do
                        file:write("\t\t\t\t<feature>\n")
                        if(df_enums.occasion_schedule_feature[featureV.feature] ~= nil) then
                            file:write("\t\t\t\t\t<type>"..df_enums.occasion_schedule_feature[featureV.feature]:lower().."</type>\n")
                        else
                            file:write("\t\t\t\t\t<type>"..featureV.feature.."</type>\n")
                        end
						if (featureV.reference ~= -1) then
							file:write("\t\t\t\t\t<reference>"..featureV.reference.."</reference>\n")
						end
                        file:write("\t\t\t\t</feature>\n")
                    end
                    file:write("\t\t\t</schedule>\n")
                end
                file:write("\t\t</occasion>\n")
            end
        end
        file:write("\t</entity>\n")
    end
    file:write("</entities>".."\n")
end
function export_historical_events()
    file:write("<historical_events>\n")
    for ID, event in ipairs(df.global.world.history.events) do
        if event:getType() == df.history_event_type.ADD_HF_ENTITY_LINK
              or event:getType() == df.history_event_type.ADD_HF_SITE_LINK
              or event:getType() == df.history_event_type.ADD_HF_HF_LINK
              or event:getType() == df.history_event_type.ADD_HF_ENTITY_LINK
              or event:getType() == df.history_event_type.BODY_ABUSED
              --or event:getType() == df.history_event_type.CHANGE_CREATURE_TYPE
              -- or event:getType() == df.history_event_type.CHANGE_HF_STATE
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
              or event:getType() == df.history_event_type.MASTERPIECE_CREATED_FOOD
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
              or event:getType() == df.history_event_type.TOPICAGREEMENT_CONCLUDED
              or event:getType() == df.history_event_type.TOPICAGREEMENT_REJECTED
              or event:getType() == df.history_event_type.TOPICAGREEMENT_MADE
              or event:getType() == df.history_event_type.CHANGE_HF_JOB
              or event:getType() == df.history_event_type.CREATED_BUILDING
              or event:getType() == df.history_event_type.CREATURE_DEVOURED
                then
            file:write("\t<historical_event>\n")
            file:write("\t\t<id>"..event.id.."</id>\n")
            file:write("\t\t<type>"..tostring(df_enums.history_event_type[event:getType()]):lower().."</type>\n")
            for k,v in pairs(event) do
                if k == "year" or k == "seconds" or k == "flags" or k == "id"
                    or (k == "region" and event:getType() ~= df.history_event_type.HF_DOES_INTERACTION)
                    or k == "region_pos" or k == "layer" or k == "feature_layer" or k == "subregion"
                    or k == "anon_1" or k == "anon_2" or k == "anon_3" or k == "anon_4" or k == "flags2" or k == "unk1"
					or k == "circumstance" or k == "circumstance_id" or k == "reason" or k == "reason_id" then

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
				elseif event:getType() == df.history_event_type.ADD_HF_SITE_LINK and k == "site" then
					--skip
				elseif event:getType() == df.history_event_type.ADD_HF_HF_LINK and (k == "hf" or k == "hf_target") then
					--skip
				elseif event:getType() == df.history_event_type.ADD_HF_ENTITY_LINK and k == "civ" then
					--skip
				elseif event:getType() == df.history_event_type.CREATURE_DEVOURED 
					and (k == "site" or (k == "victim" and v == -1)) then
					--skip
                elseif event:getType() == df.history_event_type.BODY_ABUSED and ((k == "civ" or k == "site") and v == -1) then
					--skip
                elseif event:getType() == df.history_event_type.ADD_HF_ENTITY_LINK and k == "link_type" then
                    file:write("\t\t<"..k..">"..df_enums.histfig_entity_link_type[v]:lower().."</"..k..">\n")
                elseif event:getType() == df.history_event_type.ADD_HF_ENTITY_LINK and k == "position_id" then
                    local entity = findEntity(event.civ)
                    if (entity ~= nil and event.civ > -1 and v > -1) then
                        for entitypositionsK, entityPositionsV in ipairs(entity.positions.own) do
                            if entityPositionsV.id == v then
                                file:write("\t\t<position>"..tostring(entityPositionsV.name[0]):lower().."</position>\n")
                                break
                            end
                        end
                    else
                        file:write("\t\t<position>-1</position>\n")
                    end
                elseif event:getType() == df.history_event_type.CREATE_ENTITY_POSITION and k == "position" then
                    local entity = findEntity(event.site_civ)
                    if (entity ~= nil and v > -1) then
                        for entitypositionsK, entityPositionsV in ipairs(entity.positions.own) do
                            if entityPositionsV.id == v then
                                file:write("\t\t<position>"..tostring(entityPositionsV.name[0]):lower().."</position>\n")
                                break
                            end
                        end
                    else
                        file:write("\t\t<position>-1</position>\n")
                    end
                elseif event:getType() == df.history_event_type.REMOVE_HF_ENTITY_LINK and k == "link_type" then
                    file:write("\t\t<"..k..">"..df_enums.histfig_entity_link_type[v]:lower().."</"..k..">\n")
                elseif event:getType() == df.history_event_type.REMOVE_HF_ENTITY_LINK and k == "position_id" then
                    local entity = findEntity(event.civ)
                    if (entity ~= nil and event.civ > -1 and v > -1) then
                        for entitypositionsK, entityPositionsV in ipairs(entity.positions.own) do
                            if entityPositionsV.id == v then
                                file:write("\t\t<position>"..tostring(entityPositionsV.name[0]):lower().."</position>\n")
                                break
                            end
                        end
                    else
                        file:write("\t\t<position>-1</position>\n")
                    end
                elseif event:getType() == df.history_event_type.ADD_HF_HF_LINK and k == "type" then
                    file:write("\t\t<link_type>"..df_enums.histfig_hf_link_type[v]:lower().."</link_type>\n")
                elseif event:getType() == df.history_event_type.ADD_HF_SITE_LINK and k == "type" then
                    file:write("\t\t<link_type>"..df_enums.histfig_site_link_type[v]:lower().."</link_type>\n")
                elseif event:getType() == df.history_event_type.REMOVE_HF_SITE_LINK and k == "type" then
                    file:write("\t\t<link_type>"..df_enums.histfig_site_link_type[v]:lower().."</link_type>\n")
                elseif (event:getType() == df.history_event_type.ITEM_STOLEN or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_DYE_ITEM
                        ) and k == "item_type" then
                    file:write("\t\t<item_type>"..df_enums.item_type[v]:lower().."</item_type>\n")
                elseif (event:getType() == df.history_event_type.ITEM_STOLEN or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_DYE_ITEM
                        ) and k == "item_subtype" then
                    --if event.item_type > -1 and v > -1 then
                        file:write("\t\t<"..k..">"..getItemSubTypeName(event.item_type,v).."</"..k..">\n")
                    --end
				elseif event:getType() == df.history_event_type.MASTERPIECE_CREATED_FOOD and k == "item_subtype" then
					--if event.item_type > -1 and v > -1 then
						file:write("\t\t<item_type>food</item_type>\n")
						file:write("\t\t<"..k..">"..getItemSubTypeName(df.item_type.FOOD,v).."</"..k..">\n")
					--end
                elseif event:getType() == df.history_event_type.ITEM_STOLEN and k == "mattype" then
                    if (v > -1) then
                        if (dfhack.matinfo.decode(event.mattype, event.matindex) == nil) then
                            file:write("\t\t<mattype>"..event.mattype.."</mattype>\n")
                            file:write("\t\t<matindex>"..event.matindex.."</matindex>\n")
                        else
                            file:write("\t\t<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.mattype, event.matindex)).."</mat>\n")
                        end
                    end
                elseif (event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_FOOD or
                        event:getType() == df.history_event_type.MASTERPIECE_CREATED_DYE_ITEM
                        ) and k == "mat_type" then
                    if (v > -1) then
                        if (dfhack.matinfo.decode(event.mat_type, event.mat_index) == nil) then
                            file:write("\t\t<mat_type>"..event.mat_type.."</mat_type>\n")
                            file:write("\t\t<mat_index>"..event.mat_index.."</mat_index>\n")
                        else
                            file:write("\t\t<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.mat_type, event.mat_index)).."</mat>\n")
                        end
                    end
                elseif event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT and k == "imp_mat_type" then
                    if (v > -1) then
                        if (dfhack.matinfo.decode(event.imp_mat_type, event.imp_mat_index) == nil) then
                            file:write("\t\t<imp_mat_type>"..event.imp_mat_type.."</imp_mat_type>\n")
                            file:write("\t\t<imp_mat_index>"..event.imp_mat_index.."</imp_mat_index>\n")
                        else
                            file:write("\t\t<imp_mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.imp_mat_type, event.imp_mat_index)).."</imp_mat>\n")
                        end
                    end
                elseif event:getType() == df.history_event_type.MASTERPIECE_CREATED_DYE_ITEM and k == "dye_mat_type" then
                    if (v > -1) then
                        if (dfhack.matinfo.decode(event.dye_mat_type, event.dye_mat_index) == nil) then
                            file:write("\t\t<dye_mat_type>"..event.dye_mat_type.."</dye_mat_type>\n")
                            file:write("\t\t<dye_mat_index>"..event.dye_mat_index.."</dye_mat_index>\n")
                        else
                            file:write("\t\t<dye_mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.dye_mat_type, event.dye_mat_index)).."</dye_mat>\n")
                        end
                    end
                elseif (event:getType() == df.history_event_type.WAR_PEACE_ACCEPTED or
                        event:getType() == df.history_event_type.WAR_PEACE_REJECTED or
                        event:getType() == df.history_event_type.TOPICAGREEMENT_CONCLUDED or
                        event:getType() == df.history_event_type.TOPICAGREEMENT_REJECTED or
                        event:getType() == df.history_event_type.TOPICAGREEMENT_MADE
                        ) and k == "topic" then
                    file:write("\t\t<topic>"..tostring(df_enums.meeting_topic[v]):lower().."</topic>\n")
                elseif event:getType() == df.history_event_type.MASTERPIECE_CREATED_ITEM_IMPROVEMENT and k == "improvement_type" then
                    file:write("\t\t<improvement_type>"..df_enums.improvement_type[v]:lower().."</improvement_type>\n")
                elseif ((event:getType() == df.history_event_type.HIST_FIGURE_REACH_SUMMIT and k == "group")
                     or (event:getType() == df.history_event_type.HIST_FIGURE_NEW_PET and k == "group")
                     or (event:getType() == df.history_event_type.BODY_ABUSED and k == "bodies")) then
                    for detailK,detailV in pairs(v) do
                        file:write("\t\t<"..k..">"..detailV.."</"..k..">\n")
                    end
                elseif  event:getType() == df.history_event_type.HIST_FIGURE_NEW_PET and k == "pets" then
                    for detailK,detailV in pairs(v) do
                        file:write("\t\t<"..k..">"..df.global.world.raws.creatures.all[detailV].name[0].."</"..k..">\n")
                    end
                elseif event:getType() == df.history_event_type.BODY_ABUSED and (k == "props") then
                    file:write("\t\t<props_item_type>"..tostring(df_enums.item_type[event.props.item.item_type]):lower().."</props_item_type>\n")
                    file:write("\t\t<props_item_subtype>"..getItemSubTypeName(event.props.item.item_type,event.props.item.item_subtype).."</props_item_subtype>\n")
                    if (event.props.item.mat_type > -1) then
                        if (dfhack.matinfo.decode(event.props.item.mat_type, event.props.item.mat_index) == nil) then
                            file:write("\t\t<props_item_mat_type>"..event.props.item.mat_type.."</props_item_mat_type>\n")
                            file:write("\t\t<props_item_mat_index>"..event.props.item.mat_index.."</props_item_mat_index>\n")
                        else
                            file:write("\t\t<props_item_mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.props.item.mat_type, event.props.item.mat_index)).."</props_item_mat>\n")
                        end
                    end
                    --file:write("\t\t<"..k.."_item_mat_type>"..tostring(event.props.item.mat_type).."</"..k.."_item_mat_index>\n")
                    --file:write("\t\t<"..k.."_item_mat_index>"..tostring(event.props.item.mat_index).."</"..k.."_item_mat_index>\n")
                    file:write("\t\t<"..k.."_pile_type>"..tostring(event.props.pile_type).."</"..k.."_pile_type>\n")
                elseif event:getType() == df.history_event_type.ASSUME_IDENTITY and k == "identity" then
                    if (table.contains(df.global.world.identities.all,v)) then
                        if (df.global.world.identities.all[v].histfig_id == -1) then
                            local thisIdentity = df.global.world.identities.all[v]
                            file:write("\t\t<identity_name>"..thisIdentity.name.first_name.."</identity_name>\n")
                            file:write("\t\t<identity_race>"..(df.global.world.raws.creatures.all[thisIdentity.race].creature_id):lower().."</identity_race>\n")
                            file:write("\t\t<identity_caste>"..(df.global.world.raws.creatures.all[thisIdentity.race].caste[thisIdentity.caste].caste_id):lower().."</identity_caste>\n")
                        else
                            file:write("\t\t<identity_hf>"..df.global.world.identities.all[v].histfig_id.."</identity_hf>\n")
                        end
                    end
                elseif event:getType() == df.history_event_type.MASTERPIECE_CREATED_ARCH_CONSTRUCT and k == "building_type" then
                    file:write("\t\t<building_type>"..df_enums.building_type[v]:lower().."</building_type>\n")
                elseif event:getType() == df.history_event_type.MASTERPIECE_CREATED_ARCH_CONSTRUCT and k == "building_subtype" then
                    if (df_enums.building_type[event.building_type]:lower() == "furnace") then
                        file:write("\t\t<building_subtype>"..df_enums.furnace_type[v]:lower().."</building_subtype>\n")
                    elseif v > -1 then
                        file:write("\t\t<building_subtype>"..tostring(v).."</building_subtype>\n")
                    end
                elseif k == "race" then
                    if v > -1 then
                        file:write("\t\t<race_id>"..v.."</race_id>\n")
                    end
                elseif k == "caste" then
                    if v > -1 then
                        file:write("\t\t<caste>"..(df.global.world.raws.creatures.all[event.race].caste[v].caste_id):lower().."</caste>\n")
                    end
                elseif k == "interaction" and event:getType() == df.history_event_type.HF_DOES_INTERACTION then
                    file:write("\t\t<interaction_action>"..df.global.world.raws.interactions[v].str[3].value.."</interaction_action>\n")
                    file:write("\t\t<interaction_string>"..df.global.world.raws.interactions[v].str[4].value.."</interaction_string>\n")
                elseif k == "interaction" and event:getType() == df.history_event_type.HF_LEARNS_SECRET then
                    file:write("\t\t<secret_text>"..df.global.world.raws.interactions[v].str[2].value.."</secret_text>\n")
                elseif event:getType() == df.history_event_type.HIST_FIGURE_DIED and k == "weapon" then
                    for detailK,detailV in pairs(v) do
                        if (detailK == "item") then
                            if detailV > -1 then
                                file:write("\t\t<"..detailK..">"..detailV.."</"..detailK..">\n")
                                local thisItem = df.item.find(detailV)
                                if (thisItem ~= nil) then
                                    if (thisItem.flags.artifact == true) then
                                        for refk,refv in pairs(thisItem.general_refs) do
                                            if (refv:getType() == df.general_ref_type.IS_ARTIFACT) then
                                                file:write("\t\t<artifact_id>"..refv.artifact_id.."</artifact_id>\n")
                                                break
                                            end
                                        end
                                    end
                                end

                            end
                        elseif (detailK == "item_type") then
                            if event.weapon.item > -1 then
                                file:write("\t\t<"..detailK..">"..tostring(df_enums.item_type[detailV]):lower().."</"..detailK..">\n")
                            end
                        elseif (detailK == "item_subtype") then
                            if event.weapon.item > -1 and detailV > -1 then
                                file:write("\t\t<"..detailK..">"..getItemSubTypeName(event.weapon.item_type,detailV).."</"..detailK..">\n")
                            end
                        elseif (detailK == "mattype") then
                            if (detailV > -1) then
                                file:write("\t\t<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.weapon.mattype, event.weapon.matindex)).."</mat>\n")
                            end
                        elseif (detailK == "matindex") then

                        elseif (detailK == "shooter_item") then
                            if detailV > -1 then
                                file:write("\t\t<"..detailK..">"..detailV.."</"..detailK..">\n")
                                local thisItem = df.item.find(detailV)
                                if  thisItem ~= nil then
                                    if (thisItem.flags.artifact == true) then
                                        for refk,refv in pairs(thisItem.general_refs) do
                                            if (refv:getType() == df.general_ref_type.IS_ARTIFACT) then
                                                file:write("\t\t<shooter_artifact_id>"..refv.artifact_id.."</shooter_artifact_id>\n")
                                                break
                                            end
                                        end
                                    end
                                end
                            end
                        elseif (detailK == "shooter_item_type") then
                            if event.weapon.shooter_item > -1 then
                                file:write("\t\t<"..detailK..">"..tostring(df_enums.item_type[detailV]):lower().."</"..detailK..">\n")
                            end
                        elseif (detailK == "shooter_item_subtype") then
                            if event.weapon.shooter_item > -1 and detailV > -1 then
                                file:write("\t\t<"..detailK..">"..getItemSubTypeName(event.weapon.shooter_item_type,detailV).."</"..detailK..">\n")
                            end
                        elseif (detailK == "shooter_mattype") then
                            if (detailV > -1) then
                                file:write("\t\t<shooter_mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(event.weapon.shooter_mattype, event.weapon.shooter_matindex)).."</shooter_mat>\n")
                            end
                        elseif (detailK == "shooter_matindex") then
                            --skip
                        elseif detailK == "slayer_race" or detailK == "slayer_caste" then
                            --skip
                        else
                            file:write("\t\t<"..detailK..">"..detailV.."</"..detailK..">\n")
                        end
                    end
                elseif event:getType() == df.history_event_type.HIST_FIGURE_DIED and k == "death_cause" then
                    file:write("\t\t<"..k..">"..df_enums.death_type[v]:lower().."</"..k..">\n")
                elseif event:getType() == df.history_event_type.CHANGE_HF_JOB and (k == "new_job" or k == "old_job") then
                    file:write("\t\t<"..k..">"..df_enums.profession[v]:lower().."</"..k..">\n")
                elseif event:getType() == df.history_event_type.CHANGE_CREATURE_TYPE and (k == "old_race" or k == "new_race")  and v >= 0 then
                    file:write("\t\t<"..k..">"..df.global.world.raws.creatures.all[v].name[0].."</"..k..">\n")
                else
                    file:write("\t\t<"..k..">"..tostring(v).."</"..k..">\n")
                end
            end
            file:write("\t</historical_event>\n")
        end
    end
    file:write("</historical_events>".."\n")
end
function export_historical_event_collections()
    file:write("<historical_event_collections>".."\n")
    file:write("</historical_event_collections>".."\n")
end
function export_historical_eras()
    file:write("<historical_eras>".."\n")
    file:write("</historical_eras>".."\n")
end

--create an extra legends xml with extra data, by Mason11987 for World Viewer
function export_more_legends_xml()
    local month = dfhack.world.ReadCurrentMonth() + 1 --days and months are 1-indexed
    local day = dfhack.world.ReadCurrentDay()
    local year_str = string.format('%0'..math.max(5, string.len(''..df.global.cur_year))..'d', df.global.cur_year)
    local date_str = year_str..string.format('-%02d-%02d', month, day)

    local filename = df.global.world.cur_savegame.save_dir.."-"..date_str.."-legends_plus.xml"
    file = io.open(filename, 'w')
    if not file then qerror("could not open file: " .. filename) end

    file:write("<?xml version=\"1.0\" encoding='UTF-8'?>".."\n")
    file:write("<df_world>".."\n")
    file:write("<name>"..dfhack.df2utf(dfhack.TranslateName(df.global.world.world_data.name)).."</name>".."\n")
    file:write("<altname>"..dfhack.df2utf(dfhack.TranslateName(df.global.world.world_data.name,1)).."</altname>".."\n")

	export_geo_biomes()
	export_armies()
	export_units()
	export_reports()
	export_buildings()
	export_constructions()
	export_items()
	export_plants()
	export_squads()
	export_written_contents()
	export_poetic_forms()
	export_musical_forms()
	export_dance_forms()
	export_races()
	export_landmasses()
	export_mountains()
	export_rivers()
	export_regions()
	export_underground_regions()
	export_sites()
	export_world_constructions()
	export_artifacts()
	export_historical_figures()
	export_entity_populations()
	export_entities()
	export_historical_events()
	export_historical_event_collections()
	export_historical_eras()
	
    file:write("</df_world>".."\n")
    file:close()
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

function export_detailed_maps()
    script.start(function()
        for i = 1, #MAPS do
            local vs = dfhack.gui.getViewscreenByType(df.viewscreen_export_graphical_mapst, 0)
            if not vs then
                local legends_vs = dfhack.gui.getViewscreenByType(df.viewscreen_legendsst, 0)
                    or qerror("Could not find legends screen")
                gui.simulateInput(legends_vs, 'LEGENDS_EXPORT_DETAILED_MAP')
            end

            vs = dfhack.gui.getViewscreenByType(df.viewscreen_export_graphical_mapst, 0)
                or qerror("Could not find map export screen")
            vs.sel_idx = i - 1
            print('    Exporting map: ' .. MAPS[i])
            gui.simulateInput(vs, 'SELECT')
            while dfhack.gui.getCurViewscreen() == vs do
                script.sleep(10, 'frames')
            end
        end
    end)
end

-- export site maps
function export_site_maps()
    local vs = dfhack.gui.getCurViewscreen()
    if ((dfhack.gui.getCurFocus() ~= "legends" ) and (not table.contains(vs, "main_cursor"))) then -- Using open-legends
        vs = vs.parent
    end
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
if dfhack.gui.getCurFocus() == "legends" or dfhack.gui.getCurFocus() == "dfhack/lua/legends" then
    -- either native legends mode, or using the open-legends.lua script
    if args[1] == "all" then
        export_legends_info()
        export_site_maps()
        export_detailed_maps()
    elseif args[1] == "info" then
        export_legends_info()
    elseif args[1] == "custom" then
	    print("    Exporting:  Extra legends_plus xml")
        export_more_legends_xml()
    elseif args[1] == "maps" then
        export_detailed_maps()
    elseif args[1] == "sites" then
        export_site_maps()
    else dfhack.printerr('Valid arguments are "all", "info", "maps" or "sites"')
    end
elseif args[1] == "maps" and dfhack.gui.getCurFocus() == "export_graphical_map" then
    export_detailed_maps()
else
    dfhack.printerr('Exportlegends must be run from the main legends view')
end
