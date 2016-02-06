-- Export everything from legends mode
--[[=begin

exportlegends
=============
Controls legends mode to export data - especially useful to set-and-forget large
worlds, or when you want a map of every site when there are several hundred.

The 'info' option exports more data than is possible in vanilla, to a
:file:`region-date-legends_plus.xml` file developed to extend
:forums:`World Viewer <128932>` and other legends utilities.

Options:

:info:  Exports the world/gen info, the legends XML, and a custom XML with more information
:sites: Exports all available site maps
:maps:  Exports all seventeen detailed maps
:all:   Equivalent to calling all of the above, in that order

=end]]

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

function tablelength(T)
  local count = 0
  for _ in pairs(T) do count = count + 1 end
  return count
end

function string.starts(String,Start)
   return string.sub(String,1,string.len(Start))==Start
end

function export_geo_biomes()
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
end
function export_armies()
	io.write ("<armies>".."\n")
    for armyK, armyV in ipairs(df.global.world.armies.all) do
		io.write ("\t".."<army>".."\n")
		io.write ("\t\t".."<id>"..armyV.id.."</id>".."\n")
		io.write ("\t\t".."<coords>"..armyV.pos.x..","..armyV.pos.y.."</coords>".."\n")
		io.write ("\t\t".."<item>"..getItemSubTypeName(armyV.item_type,armyV.item_subtype).."</item>".."\n")
		
		io.write ("\t\t".."<item_type>"..tostring(df.item_type[armyV.item_type]):lower().."</item_type>".."\n")
		if (armyV.item_subtype ~= -1) then
			io.write ("\t\t".."<item_subtype>"..armyV.item_subtype.."</item_subtype>".."\n")
		end
		
		io.write ("\t\t".."<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(armyV.mat_type, armyV.mat_index)).."</mat>".."\n")
		io.write ("\t".."</army>".."\n")
    end
    io.write ("</armies>".."\n")
end
function export_units()
	io.write ("<units>".."\n")
    for unitK, unitV in ipairs(df.global.world.units.all) do
		io.write ("\t".."<unit>".."\n")
		io.write ("\t\t".."<id>"..unitV.id.."</id>".."\n")
		flagValues = ""
		for k,v in pairs(unitV) do
			if (k == "pos") then
				io.write ("\t\t".."<coords>")
					io.write (v.x..","..v.y..","..v.z)
				io.write ("</coords>\n")
			elseif (k == "name") then
				io.write ("\t\t".."<name>"..dfhack.df2utf(dfhack.TranslateName(v,0)).."</name>".."\n")
				io.write ("\t\t".."<name2>"..dfhack.df2utf(dfhack.TranslateName(v,1)).."</name2>".."\n")
			elseif ( k == "sex" or k == "civ_id" or k == "population_id" 
				 or k == "mood" or k == "hist_figure_id"  or k == "hist_figure_id2") then
				io.write ("\t\t".."<"..k..">"..v.."</"..k..">".."\n")
			elseif (k == "race") then
				io.write ("\t\t".."<race>"..(df.global.world.raws.creatures.all[v].creature_id).."</race>".."\n")
				io.write ("\t\t".."<caste>"..(df.global.world.raws.creatures.all[v].caste[unitV.caste].caste_id).."</caste>".."\n")
			elseif (k == "profession") then
				io.write ("\t\t".."<"..k..">"..df.profession[v]:lower().."</"..k..">".."\n")			
			elseif (k == "military" and v.squad_id ~= "-1") then
				io.write ("\t\t".."<squad_id>"..v.squad_id.."</squad_id>".."\n")
			elseif (k == "flags1" or k == "flags2" or k == "flags3") then
				for flagK, flagV in pairs(v) do
					if (flagV) then
						flagValues = flagValues..flagK..","
					end
				end
			elseif (k == "status") then
				io.write ("\t\t".."<labors>")
				for kLabor, vLabor in pairs(v.labors) do
					if (vLabor) then
						io.write (kLabor..",")
					end
				end
				io.write ("</labors>\n")			
			elseif (k == "opponent" and v.unit_id ~= "-1") then
				io.write ("\t\t".."<opponent_id>"..v.unit_id.."</opponent_id>".."\n")
			elseif (string.starts(k,"unk") or  k == "profession2") then -- Ignore
			
			elseif (type(v) == "userdata") then
				if (k == "patrol_route" or k == "burrows" or k == "counters" or k == "enemy" or k == "recuperation" or k == "syndromes" or k == "status2" or k == "unknown7" or k == "counters2" or k == "curse" or k == "idle_area" or k == "job" or k == "corpse_parts" or k == "flags4" or k == "path" or k == "last_hit" or k == "meeting" or k == "animal" or k == "activities" 
					or string.starts(k,"anon")) then -- Ignore, unimportant
				
				elseif (k == "relations") then -- Covered elsewhere
				
				elseif (k == "reports" or k == "body" or k == "appearance" or k == "actions") then -- Too much data
				
				elseif (k == "inventory") then
					printout = ""
					for itemk, itemv in ipairs(v) do
						printout = printout..("\t\t\t".."<item>".."\n")
						printout = printout..("\t\t\t\t".."<id>"..itemv.item.id.."</id>".."\n")
						printout = printout..("\t\t\t\t".."<mode>"..itemv.mode.."</mode>".."\n")
						if (itemv.body_part_id ~= -1) then
							printout = printout..("\t\t\t\t".."<body_part>"..unitV.body.body_plan.body_parts[itemv.body_part_id].name_singular[0].value.."</body_part>".."\n")
						end
						printout = printout..("\t\t\t".."</item>".."\n")
					end
					if (printout ~= "") then
						io.write ("\t\t".."<inventory>".."\n")
						io.write (printout)
						io.write ("\t\t".."</inventory>".."\n")
					end
				elseif (k == "owned_buildings" or k == "used_items") then
					printout = ""
					for ownedK, ownedV in ipairs(v) do
						printout = printout..ownedV.id..","
					end
					if (printout ~= "") then
						io.write ("\t\t".."<"..k..">"..printout.."</"..k..">".."\n")
					end
				elseif (k == "owned_items" or k == "traded_items") then -- ID List
					printout = ""
					for itemk, itemv in ipairs(v) do
						printout = printout..itemv..","
					end
					if (printout ~= "") then
						io.write ("\t\t".."<"..k..">"..printout.."</"..k..">".."\n")
					end
				elseif (k == "health") then
					printout = ""
					for healthk, healthv in pairs(v.flags) do
						if (healthk == 11) then
							break
						end
						if (healthv) then
							printout = printout..healthk..","
						end
					end
					if (printout ~= "") then
						io.write ("\t\t".."<"..k..">"..printout.."</"..k..">".."\n")
					end
				elseif (k == "general_refs") then
					for refK, refV in pairs(unitV.general_refs) do
						if (df.general_ref_type[refV:getType()] == "IS_NEMESIS") then
							io.write ("\t\t".."<nemesis_id>"..refV.nemesis_id.."</nemesis_id>".."\n")
						elseif (df.general_ref_type[refV:getType()] == "BUILDING_CIVZONE_ASSIGNED") then
							io.write ("\t\t".."<civzone_id>"..refV.building_id.."</civzone_id>".."\n")
						elseif (df.general_ref_type[refV:getType()] == "BUILDING_NEST_BOX") then
							io.write ("\t\t".."<nestbox_id>"..refV.building_id.."</nestbox_id>".."\n")
						elseif (df.general_ref_type[refV:getType()] == "CONTAINED_IN_ITEM") then 
							io.write ("\t\t".."<in_item_id>"..refV.item_id.."</in_item_id>".."\n")
						else
							if table.contains(df.general_ref_type, refV:getType()) then
								io.write ("\t\t".."<general_ref>"..refV.getType()..":"..df.general_ref_type[refV:getType()].."</general_ref>".."\n")
							end
						end
					end
				elseif (k == "specific_refs") then -- No specific refs found yet
					

				else
					io.write ("\t\t".."<"..k..">".."userdata".."</"..k..">".."\n")
				end
			end
		end
		io.write("\t\t".."<flags>"..flagValues.."</flags>".."\n")
		

		for relationK, relationV in pairs(unitV.relations) do
			if (relationK == "ghost_info" or relationK == "pregnancy_genes") then 
				if (relationV ~= nil) then
					--Todo: something with ghost info
					--[[
					type                     = 9
					type2                    = 9
					goal                     = 5
					target                   = <unit_ghost_info.T_target: 0x35ddacd8> (unit, item, building)
					misplace_pos             = <coord: 0x35ddacdc>
					action_timer             = 403200
					unk_18                   = 3
					flags                    = <unit_ghost_info.T_flags: 0x35ddacec> (announced, was_at_rest)
					death_x                  = 33425
					death_y                  = 47169
					death_z                  = 142
					--]]
				end
			elseif (relationK == "following") then
				if (relationV ~= nil) then
					io.write ("\t\t".."<following_unit>"..relationV.id.."</following_unit>".."\n")
				end
			elseif (relationV ~= -1 and relationV ~= nil and not string.starts(relationK,"unk")) then
				io.write ("\t\t".."<"..relationK..">"..relationV.."</"..relationK..">".."\n")
			end
		end
		
		io.write ("\t".."</unit>".."\n")
    end
    io.write ("</units>".."\n")
end
function export_engravings()
	io.write ("<engravings>".."\n")
    for engravingK, engravingV in ipairs(df.global.world.engravings) do
		io.write ("\t".."<engraving>".."\n")
		io.write ("\t\t".."<id>"..engravingK.."</id>".."\n")
		io.write ("\t\t".."<artist>"..engravingV.artist.."</artist>".."\n")
		if (engravingV.masterpiece_event ~= -1) then
			io.write ("\t\t".."<masterpiece_event>"..engravingV.masterpiece_event.."</masterpiece_event>".."\n")
		end
		io.write ("\t\t".."<skill_rating>"..engravingV.skill_rating.."</skill_rating>".."\n")
		io.write ("\t\t".."<coords>"..engravingV.pos.x..","..engravingV.pos.y..","..engravingV.pos.z.."</coords>".."\n")
		io.write ("\t\t".."<tile>"..engravingV.tile.."</tile>".."\n")
		io.write ("\t\t".."<art_id>"..engravingV.art_id.."</art_id>".."\n")
		io.write ("\t\t".."<art_subid>"..engravingV.art_subid.."</art_subid>".."\n")
		io.write ("\t\t".."<quality>"..engravingV.tile.."</quality>".."\n")
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
		io.write ("\t\t".."<location>"..engravinglocation.."</location>".."\n")
		if (engravingV.flags.hidden == true) then
			io.write ("\t\t".."<hidden/>".."\n")
		end
		io.write ("\t".."</engraving>".."\n")
    end
    io.write ("</engravings>".."\n")
end
function export_reports()
	io.write ("<reports>".."\n")
    for reportK, reportV in ipairs(df.global.world.status.reports) do
		io.write ("\t".."<report>".."\n")
		io.write ("\t\t".."<id>"..reportV.id.."</id>".."\n")
		io.write ("\t\t".."<text>"..dfhack.utf2df(reportV.text).."</text>".."\n")
		io.write ("\t\t".."<type>"..df.announcement_type[reportV.type]:lower().."</type>".."\n")
		io.write ("\t\t".."<year>"..reportV.year.."</year>".."\n")
		io.write ("\t\t".."<time>"..reportV.time.."</time>".."\n")
		if (reportV.flags.continuation) then
			io.write ("\t\t".."<continuation/>".."\n")
		end
		if (reportV.flags.announcement) then
			io.write ("\t\t".."<announcement/>".."\n")
		end
		io.write ("\t".."</report>".."\n")
    end
    io.write ("</reports>".."\n")
end
function export_buildings()
io.write ("<buildings>".."\n")
    for buildingK, buildingV in ipairs(df.global.world.buildings.all) do
		io.write ("\t".."<building>".."\n")
		io.write ("\t\t".."<id>"..buildingV.id.."</id>".."\n")
		io.write ("\t\t".."<name>"..buildingV.name.."</name>".."\n")
		io.write ("\t\t".."<coords1>"..buildingV.x1..","..buildingV.y1..","..buildingV.z.."</coords1>".."\n")
		io.write ("\t\t".."<coordscenter>"..buildingV.centerx..","..buildingV.centery..","..buildingV.z.."</coordscenter>".."\n")
		io.write ("\t\t".."<coords2>"..buildingV.x2..","..buildingV.y2..","..buildingV.z.."</coords2>".."\n")
		io.write ("\t\t".."<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(buildingV.mat_type, buildingV.mat_index)).."</mat>".."\n")
		print (buildingK)
		io.write ("\t\t".."<race>"..(df.global.world.raws.creatures.all[buildingV.race].creature_id).."</race>".."\n")
		buildingType = df.building_type[buildingV:getType()]:lower()
		io.write ("\t\t".."<type>"..buildingType.."</type>".."\n")
		if (buildingType == "workshop") then
			io.write ("\t\t".."<subtype>"..df.workshop_type[buildingV.type]:lower().."</subtype>".."\n")
		elseif (buildingType == "bed") then
			if (buildingV.owner ~= nil) then
				io.write ("\t\t".."<owner_unit_id>"..buildingV.owner.id.."</owner_unit_id>".."\n")
			end
		elseif (buildingType == "construction") then
			io.write ("\t\t".."<subtype>"..df.construction_type[buildingV.type]:lower().."</subtype>".."\n")
		elseif (buildingType == "furnace") then
			io.write ("\t\t".."<subtype>"..df.furnace_type[buildingV.type]:lower().."</subtype>".."\n")
		elseif (buildingType == "civzone") then
			io.write ("\t\t".."<subtype>"..df.civzone_type[buildingV.type]:lower().."</subtype>".."\n")
			io.write ("\t\t".."<zone_flags>")
			for kFlag, vFlag in pairs(buildingV.zone_flags) do
				if (vFlag) then
					io.write (kFlag..",")
				end
			end
			io.write ("</zone_flags>\n")					
		elseif (buildingType == "trap") then
			io.write ("\t\t".."<subtype>"..df.trap_type[buildingV.trap_type]:lower().."</subtype>".."\n")
		elseif (buildingType == "coffin") then
			for kContainedItem, vContainedItem in pairs(buildingV.contained_items) do
				if (vContainedItem.item:getType() == 23) then -- corpse
					io.write ("\t\t".."<corpse_hf>"..vContainedItem.item.hist_figure_id.."</corpse_hf>".."\n")
					io.write ("\t\t".."<corpse_unit>"..vContainedItem.item.unit_id.."</corpse_unit>".."\n")
					break
				end
			end
		elseif (buildingType == "bridge") then
			io.write ("\t\t".."<direction>"..buildingV.direction.."</direction>".."\n")
		elseif (buildingType == "nestbox") then
			io.write ("\t\t".."<claimed_by>"..buildingV.claimed_by.."</claimed_by>".."\n")
		elseif (buildingType == "armorstand" or buildingType == "weaponrack" or buildingType == "cabinet"
			 or buildingType == "box") then
			for kSquad, vSquad in pairs(buildingV.squads) do
				io.write ("\t\t".."<squad>"..vSquad.squad_id.."</squad>".."\n")
			end
		elseif (buildingType == "") then
		else -- stockpile, door, table, chair, statue, cage, farmplot, barsfloor, barsvertical, chain, support, tradedepot

		end
		io.write ("\t".."</building>".."\n")
    end
    io.write ("</buildings>".."\n")
end
function export_constructions()
	io.write ("<constructions>".."\n")
    for constructionK, constructionV in ipairs(df.global.world.constructions) do
		io.write ("\t".."<construction>".."\n")
		io.write ("\t\t".."<id>"..constructionK.."</id>".."\n")
		io.write ("\t\t".."<coords>"..constructionV.pos.x..","..constructionV.pos.y..","..constructionV.pos.z.."</coords>".."\n")
		io.write ("\t\t".."<item_type>"..tostring(df.item_type[constructionV.item_type]):lower().."</item_type>".."\n")
		if (constructionV.item_subtype ~= -1) then
			io.write ("\t\t".."<item_subtype>"..constructionV.item_subtype.."</item_subtype>".."\n")
		end
		io.write ("\t\t".."<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(constructionV.mat_type, constructionV.mat_index)).."</mat>".."\n")

		io.write ("\t".."</construction>".."\n")
    end
    io.write ("</constructions>".."\n")
end
function export_items()
	io.write ("<items>".."\n")
	for itemK, itemV in ipairs(df.global.world.items.all) do
		io.write ("\t".."<item>".."\n")
		io.write ("\t\t".."<id>"..itemV.id.."</id>".."\n")
		if (itemV:getType() ~= -1) then
			io.write ("\t\t".."<type>"..tostring(df.item_type[itemV:getType()]):lower().."</type>".."\n")
			if (itemV:getSubtype() ~= -1) then
				if (type(itemV.subtype) == "number") then
					io.write ("\t\t".."<subtype>"..itemV.subtype.."</subtype>".."\n")
				else
					io.write ("\t\t".."<subtype>"..itemV.subtype.name.."</subtype>".."\n")
				end
			end
		end
		if (itemV:getMaterial() ~= -1 and itemV:getMaterialIndex() ~= -1) then
			io.write ("\t\t".."<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(itemV:getMaterial(), itemV:getMaterialIndex())).."</mat>".."\n")
		end
		for k,v in pairs(itemV) do
			--io.write (k.."\t"..type(v).."\t"..itemV:getType().."\t"..itemK.."\n")
			
			if type(v) == "number" then
				--elseif (k == "subtype" or k == "age" or k == "anon_1" or k == "base_uniform_score" or k == "boiling_point" or k == "colddam_point" or k == "engraving_type" or k == "fixed_temp" or k == "heatdam_point" or k == "id" or k == "ignite_point" or k == "maker" or k == "maker_race" or k == "masterpiece_event" or k == "mat_index" or k == "mat_type" or k == "melting_point" or k == "quality" or k == "sharpness" or k == "skill_used" or k == "spec_heat" or k == "stack_size" or k == "stockpile_countdown" or k == "stockpile_delay" or k == "temp_updated_frame" or k == "topic" or k == "unk2" or k == "vehicle_id" or k == "walkable_id" or k == "wear" or k == "wear_timer" or k == "weight" or k == "weight_fraction" or k == "world_data_id" or k == "world_data_subid") then
				if (string.starts(k, "anon_") or string.starts(k,"unk") or k == "item_plant_growthst.anon_1" or k == "item_body_component.anon_1" or k == "item_sheetst.anon_1") then -- Ignore, not known
				
				elseif (k == "subtype" or k == "curse_year" or k == "curse_time" or k == "birth_year" or k == "birth_time" or k == "planting_skill" or k == "death_year" or k == "death_time" or k == "race" or k == "race2" or k == "caste" or k == "caste2" or k == "mat_index" or k == "mat_type" or k == "id" or k == "dye_mat_index" or k == "sex" ) then -- Ignore, covered elsewhere
				
				elseif (k == "base_uniform_score" or k == "maker_race" or k == "dimension" or k == "sharpness" or k == "rot_timer" or k == "blood_count" or k == "stored_fat" or k == "birth_year_bias" or k == "birth_time_bias" or k == "grow_counter"  or k == "walkable_id" or k == "fixed_temp"  or k == "spec_heat" or k == "ignite_point" or k == "colddam_point" or k == "boiling_point" or k == "temperature" or k == "stack_size" or k == "melting_point" or k == "heatdam_point" or k == "temp_updated_frame"  or k == "wear_timer" or k == "stockpile_countdown" or k == "unit_id2"  or k == "hist_figure_id2"  or k == "bone2"  ) then -- Not important enough
				
				elseif (k == "dye_mat_type") then
					if (v ~= -1 and itemV.dye_mat_index ~= -1) then
						io.write ("\t\t".."<dye_mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(v, itemV.dye_mat_index)).."</dye_mat>".."\n")
					end
				elseif (k == "quality" or k == "dye_quality" or k == "age" or k == "skill_used"or k == "wear"or k =="weight" or k == "weight_fraction"  or k == "id") then -- Only print if non-zero
					if (v ~= 0) then
						io.write ("\t\t".."<"..k..">"..v.."</"..k..">".."\n")
					end
				elseif (k == "maker" or k == "shape" or k == "topic" or k == "engraving_type" or k == "hist_figure_id" or k == "unit_id" or k == "hist_figure_id2" or k == "unit_id2" or k == "recipe_id" or k == "entity" or k == "vehicle_id" or k =="masterpiece_event" or k == "world_data_id" or k == "world_data_subid" or k == "temp_updated_frame") then -- Only print if not -1
					if (v ~= -1) then
						io.write ("\t\t".."<"..k..">"..v.."</"..k..">".."\n")
					end
				else
					 --Unexpected item property
					io.write ("\t\t".."<"..k..">"..v.."</"..k..">".."\n")
				end
			
			elseif type(v) == "string" then
				--k == "description" or k == "title"
				io.write ("\t\t".."<"..k..">"..v:lower().."</"..k..">".."\n")
				
			elseif type(v) == nil then
				--k == "magic" and others
			
			elseif type(v) == "userdata" then
				--k == "flags" or k == "flags2" or k == "general_refs" or k == "improvements" or k == "pos" or k == "specific_refs" or k == "stockpile"or k == "temperature"
				if (k == "subtype") then -- Ignore, handled elsewhere
				
				elseif (k == "pos") then
					if (itemV.pos.x ~= -30000) then
						io.write ("\t\t".."<coords>"..itemV.pos.x..","..itemV.pos.y..","..itemV.pos.z.."</coords>".."\n")
					end
				elseif (k == "temperature") then
					io.write ("\t\t".."<temperature>"..v.whole.."."..v.fraction.."</temperature>".."\n")
				elseif (k == "general_refs") then
					io.write ("\t\t".."<"..k..">".."\n")
					for refK, refV in ipairs(v) do
						if df.general_ref_type[refV:getType()] == "IS_ARTIFACT" then
							io.write("\t\t\t".."<artifact_id>"..refV.artifact_id.."</artifact_id>".."\n")
						elseif df.general_ref_type[refV:getType()] == "CONTAINED_IN_ITEM" then
							io.write("\t\t\t".."<container_item_id>"..refV.item_id.."</container_item_id>".."\n")
						elseif df.general_ref_type[refV:getType()] == "BUILDING_HOLDER" then
							io.write("\t\t\t".."<container_building_id>"..refV.building_id.."</container_building_id>".."\n")
						elseif df.general_ref_type[refV:getType()] == "CONTAINS_ITEM" then
							io.write("\t\t\t".."<contains_item_id>"..refV.item_id.."</contains_item_id>".."\n")
						elseif df.general_ref_type[refV:getType()] == "UNIT_HOLDER" then
							io.write("\t\t\t".."<holding_unit_id>"..refV.unit_id.."</holding_unit_id>".."\n")
						elseif df.general_ref_type[refV:getType()] == "UNIT_ITEMOWNER" then
							io.write("\t\t\t".."<owner_unit_id>"..refV.unit_id.."</owner_unit_id>".."\n")
						elseif df.general_ref_type[refV:getType()] == "UNIT_TRADEBRINGER" then
							io.write("\t\t\t".."<trader_unit_id>"..refV.unit_id.."</trader_unit_id>".."\n")
						elseif df.general_ref_type[refV:getType()] == "CONTAINS_UNIT" then
							io.write("\t\t\t".."<contains_unit_id>"..refV.unit_id.."</contains_unit_id>".."\n")
						elseif df.general_ref_type[refV:getType()] == "BUILDING_TRIGGER" then
							io.write("\t\t\t".."<trigger_building_id>"..refV.building_id.."</trigger_building_id>".."\n")
						elseif df.general_ref_type[refV:getType()] == "BUILDING_TRIGGERTARGET" then
							io.write("\t\t\t".."<triggertarget_building_id>"..refV.building_id.."</triggertarget_building_id>".."\n")
						elseif  df.general_ref_type[refV:getType()] == "ACTIVITY_EVENT" then -- Ignore, unknown
						
						else
							print (df.general_ref_type[refV:getType()])
							printall(refV)
							io.write("\t\t\t".."<"..refK..">"..df.general_ref_type[refV:getType()].."</"..refK..">".."\n")
						end
					end
					io.write ("\t\t".."</"..k..">".."\n")
				elseif (k == "specific_refs" or k == "flags2" or k == "corpse_flags" or k == "contaminants" or k == "material_amount" or k == "body" or k == "appearance") then -- Ignore, not important

				elseif (k == "ingredients") then
					io.write ("\t\t".."<"..k..">".."\n")
					for ingredientK, ingredientV in ipairs(v) do
						io.write ("\t\t\t".."<ingredient>".."\n")
						if (ingredientV.item_type ~= -1) then		
							io.write("\t\t\t\t".."<item_type>"..df.item_type[ingredientV.item_type]:lower().."</item_type>".."\n")
						end
						if (ingredientV.mat_type ~= -1) then
							io.write("\t\t\t\t".."<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(ingredientV.mat_type, ingredientV.mat_index)).."</mat>".."\n")
						end
						if (ingredientV.maker ~= -1) then
							io.write("\t\t\t\t".."<maker>"..ingredientV.maker.."</maker>".."\n")
						end 
						io.write ("\t\t\t".."</ingredient>".."\n")
					end
					io.write ("\t\t".."</"..k..">".."\n")
				elseif (k == "history_info") then
					if (not v.value.kills == nil) then
						io.write("\t\t\t\t".."<kills>"..v.value.kills.."</kills>".."\n")
					end
				elseif (k == "improvements") then
					io.write ("\t\t".."<"..k..">".."\n")
					for refK, refV in ipairs(v) do
						io.write ("\t\t\t".."<improvement>".."\n")
						io.write ("\t\t\t\t".."<improvement_type>"..df.improvement_type[refV:getType()]:lower().."</improvement_type>".."\n")
						for improvementK, improvementV in pairs(refV) do
							if (string.find(improvementK, "anon_1") ~= nil) then -- Ignore, anon
								
							elseif (type(improvementV) == "userdata") then
								if (improvementK == "cover_flags") then -- Ignore, not important
								elseif (improvementK == "image") then
									io.write("\t\t\t\t".."<image>".."\n")
									io.write("\t\t\t\t\t".."<id>"..improvementV.id.."</id>".."\n")
									io.write("\t\t\t\t\t".."<subid>"..improvementV.subid.."</subid>".."\n")
									io.write("\t\t\t\t\t".."<civ_id>"..improvementV.civ_id.."</civ_id>".."\n")
									io.write("\t\t\t\t\t".."<site_id>"..improvementV.site_id.."</site_id>".."\n")
									io.write("\t\t\t\t".."</image>".."\n")
								elseif (improvementK == "dye") then
									printout = ""
									if (improvementV.mat_type ~= -1) then
										printout = printout..("\t\t\t\t\t".."<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(improvementV.mat_type, improvementV.mat_index)).."</mat>".."\n")
									end
									if (improvementV.dyer ~= -1) then
										printout = printout..("\t\t\t\t\t".."<dyer>"..improvementV.dyer.."</dyer>".."\n")
									end
									if (improvementV.quality ~= 0) then
										printout = printout..("\t\t\t\t\t".."<quality>"..improvementV.quality.."</quality>".."\n")
									end
									if (improvementV.skill_rating ~= 0) then
										printout = printout..("\t\t\t\t\t".."<skill_rating>"..improvementV.skill_rating.."</skill_rating>".."\n")
									end
									if (printout ~= "") then
										io.write("\t\t\t\t".."<"..improvementK..">".."\n")
										io.write(printout)
										io.write("\t\t\t\t".."</"..improvementK..">".."\n")
									else
										io.write("\t\t\t\t".."<"..improvementK.."/>".."\n")
									end
								elseif (improvementK == "contents") then
									io.write("\t\t\t\t".."<"..improvementK..">"..improvementV[0].."</"..improvementK..">".."\n")
								else
									io.write("\t\t\t\t".."<"..improvementK..">".."userdata".."</"..improvementK..">".."\n")
								end 
							else
								if (type(improvementV) == "number" and improvementV == -1) then -- Ignore, -1
								
								elseif ((improvementK == "quality" or improvementK == "skill_rating") and improvementV == 0) then -- Ignore, 0
								
								elseif (improvementK == "mat_index" or improvementK == "anon_1") then -- Ignore, handled elsewhere
								
								elseif (improvementK == "mat_type") then
									io.write("\t\t\t\t".."<mat>"..dfhack.matinfo.toString(dfhack.matinfo.decode(improvementV, refV.mat_index)).."</mat>".."\n")
								else
									io.write("\t\t\t\t".."<"..improvementK..">"..improvementV.."</"..improvementK..">".."\n")
								end 
							end
						end
						io.write ("\t\t\t".."</improvement>".."\n")
					end
					io.write ("\t\t".."</"..k..">".."\n")
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
						io.write ("\t\t".."<"..k..">"..printout.."</"..k..">".."\n")
					end				
				elseif (string.starts(k,"bone")) then
					io.write ("\t\t".."<"..k..">"..dfhack.matinfo.toString(dfhack.matinfo.decode(v.mat_type, v.mat_index)).."</"..k..">".."\n")

				elseif (k ~= "stockpile" or v.id ~= -1) then -- If stockpile print if not -1, print all others
					printout = ""
					for tableK, tableV in pairs(v) do
						if (type(k) == "number") then  -- Can't have just numbers for tags
						
						elseif (type(tableV) == "number" or type(tableV) == "string") then
							printout = printout..("\t\t\t".."<"..tableK..">"..tableV.."</"..tableK..">".."\n")
						elseif (type(tableV) == "boolean") then
							if (tableV == true) then 
								printout = printout..("\t\t\t".."<"..tableK.."/>".."\n")
							end 
						else
							printout = printout..("\t\t\t".."<"..tableK..">"..type(tableV).."</"..tableK..">".."\n")
						end 
					end
					if (printout ~= "") then
						io.write ("\t\t".."<"..k..">".."\n")
						io.write (printout)
						io.write ("\t\t".."</"..k..">".."\n")
					end
					
				end
			end
		end
		io.write ("\t".."</item>".."\n")
	end
	io.write ("</items>".."\n")
end
function export_plants()
	io.write ("<plants>".."\n")
    for plantK, plantV in ipairs(df.global.world.plants.all) do
		io.write ("\t".."<plant>".."\n")
		io.write ("\t\t".."<id>"..plantK.."</id>".."\n")
		io.write ("\t\t".."<material>"..df.global.world.raws.plants.all[plantV.material].name.."</material>".."\n")
		io.write ("\t\t".."<coords>"..plantV.pos.x..","..plantV.pos.y..","..plantV.pos.z.."</coords>".."\n")
		io.write ("\t".."</plant>".."\n")
    end
    io.write ("</plants>".."\n")
end
function export_squads()
	io.write ("<squads>".."\n")
    for squadK, squadV in ipairs(df.global.world.squads.all) do
		io.write ("\t".."<squad>".."\n")
		io.write ("\t\t".."<id>"..squadV.id.."</id>".."\n")
		io.write ("\t\t".."<name>"..dfhack.df2utf(dfhack.TranslateName(squadV.name)).."</name>".."\n")
		io.write ("\t\t".."<altname>"..dfhack.df2utf(dfhack.TranslateName(squadV.name,1)).."</altname>".."\n")
		io.write ("\t\t".."<entity_id>"..squadV.entity_id.."</entity_id>".."\n")
		for positionK, positionV in pairs(squadV.positions) do
			if (positionV.occupant ~= -1) then
				io.write ("\t\t".."<member>"..positionV.occupant.."</member>".."\n")
			end
		end
		io.write ("\t".."</squad>".."\n")
    end
    io.write ("</squads>".."\n")
end
function export_races()
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
		flagsString = ""
		for flagK, flagV in pairs(raceV.flags) do
			if (string.find(flagK, "unk_") or type(flagK) == "number" or flagV == false) then 
				
			else
				flagsString = flagsString..flagK..","
			end
		end
		if (flagsString ~= "") then
			io.write ("\t\t".."<flags>"..flagsString:lower().."</flags>".."\n")
		end
		io.write ("\t".."</race>".."\n")
    end
    io.write ("</races>".."\n")
end
function export_written_contents()
	io.write ("<written_contents>".."\n")
    for writtencontentK, writtencontentV in ipairs(df.global.world.written_contents.all) do
		io.write ("\t".."<written_content>".."\n")
		io.write ("\t\t".."<id>"..writtencontentV.id.."</id>".."\n")
		io.write ("\t\t".."<title>"..writtencontentV.title.."</title>".."\n")
		io.write ("\t\t".."<pages>"..writtencontentV.page_start.."-"..writtencontentV.page_end.."</pages>".."\n")
		io.write ("\t\t".."<author>"..writtencontentV.author.."</author>".."\n")
		io.write ("\t".."</written_content>".."\n")
    end
    io.write ("</written_contents>".."\n")
end
function export_poetic_forms()
	io.write ("<poetic_forms>".."\n")
    for poeticformK, poeticformV in ipairs(df.global.world.poetic_forms.all) do
		io.write ("\t".."<poetic_form>".."\n")
		io.write ("\t\t".."<id>"..poeticformV.id.."</id>".."\n")
		io.write ("\t\t".."<name>"..dfhack.df2utf(dfhack.TranslateName(poeticformV.name)).."</name>".."\n")
		io.write ("\t\t".."<altname>"..dfhack.df2utf(dfhack.TranslateName(poeticformV.name,1)).."</altname>".."\n")
		io.write ("\t".."</poetic_form>".."\n")
    end
    io.write ("</poetic_forms>".."\n")
end
function export_musical_forms()
	io.write ("<musical_forms>".."\n")
    for musicalformK, musicalformV in ipairs(df.global.world.musical_forms.all) do
		io.write ("\t".."<musical_form>".."\n")
		io.write ("\t\t".."<id>"..musicalformV.id.."</id>".."\n")
		io.write ("\t\t".."<name>"..dfhack.df2utf(dfhack.TranslateName(musicalformV.name)).."</name>".."\n")
		io.write ("\t\t".."<altname>"..dfhack.df2utf(dfhack.TranslateName(musicalformV.name,1)).."</altname>".."\n")
		io.write ("\t".."</musical_form>".."\n")
    end
    io.write ("</musical_forms>".."\n")
end
function export_dance_forms()
	io.write ("<dance_forms>".."\n")
    for danceformK, danceformV in ipairs(df.global.world.dance_forms.all) do
		io.write ("\t".."<dance_form>".."\n")
		io.write ("\t\t".."<id>"..danceformV.id.."</id>".."\n")
		io.write ("\t\t".."<name>"..dfhack.df2utf(dfhack.TranslateName(danceformV.name)).."</name>".."\n")
		io.write ("\t\t".."<altname>"..dfhack.df2utf(dfhack.TranslateName(danceformV.name,1)).."</altname>".."\n")
		io.write ("\t".."</dance_form>".."\n")
    end
    io.write ("</dance_forms>".."\n")
end
function export_mountains()
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
end
function export_rivers()
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
end
function export_regions()
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
end
function export_underground_regions()
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
end
function export_sites()
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
end
function export_world_constructions()
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
end
function export_artifacts()
    io.write ("<artifacts>".."\n")
    for artifactK, artifactV in ipairs(df.global.world.artifacts.all) do
        io.write ("\t".."<artifact>".."\n")
        io.write ("\t\t".."<id>"..artifactV.id.."</id>".."\n")
		io.write ("\t\t".."<item_id>"..artifactV.id.."</item_id>".."\n")
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
end
function export_historical_figures()
    io.write ("<historical_figures>".."\n".."</historical_figures>".."\n")
end
function export_entity_populations()
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
end
function export_entities()
    io.write ("<entities>".."\n")
    for entityK, entityV in ipairs(df.global.world.entities.all) do
        io.write ("\t".."<entity>".."\n")
        io.write ("\t\t".."<id>"..entityV.id.."</id>".."\n")
		if (entityV.race ~= -1) then
			io.write ("\t\t".."<race>"..(df.global.world.raws.creatures.all[entityV.race].creature_id):lower().."</race>".."\n")
		end
		if (df.historical_entity_type[entityV.type] ~= nil) then
			io.write ("\t\t".."<type>"..(df.historical_entity_type[entityV.type]):lower().."</type>".."\n")
			--[[
			if (df.historical_entity_type[entityV.type]):lower() == "religion" then -- Get worshipped figure
                if (entityV.unknown1b ~= nil and entityV.unknown1b.worship ~= nill and
                    #entityV.unknown1b.worship == 1) then
                    io.write ("\t\t".."<worship_id>"..entityV.unknown1b.worship[0].."</worship_id>".."\n")
                else
					print(entityK)
                    print(entityV.unknown1b, entityV.unknown1b.worship, #entityV.unknown1b.worship)
                end
			end
			--]]
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
end
function export_historical_events()
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
					if (df.histfig_hf_link_type[v] ~= nil) then
						io.write ("\t\t".."<link_type>"..df.histfig_hf_link_type[v]:lower().."</link_type>".."\n")
					end
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
                    if (table.contains(df.global.world.identities.all,v)) then
                        if (df.global.world.identities.all[v].histfig_id == -1) then
                            local thisIdentity = df.global.world.identities.all[v]
                            io.write ("\t\t".."<identity_name>"..thisIdentity.name.first_name.."</identity_name>".."\n")
                            io.write ("\t\t".."<identity_race>"..(df.global.world.raws.creatures.all[thisIdentity.race].creature_id):lower().."</identity_race>".."\n")
                            io.write ("\t\t".."<identity_caste>"..(df.global.world.raws.creatures.all[thisIdentity.race].caste[thisIdentity.caste].caste_id):lower().."</identity_caste>".."\n")
                        else
                            io.write ("\t\t".."<identity_hf>"..df.global.world.identities.all[v].histfig_id.."</identity_hf>".."\n")
                        end
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
		--[[
		elseif (event:getType() == df.history_event_type.CHANGE_HF_STATE or
			    event:getType() == df.history_event_type.CREATED_SITE or
			    event:getType() == df.history_event_type.AGREEMENT_FORMED or
			    event:getType() == df.history_event_type.ARTIFACT_STORED or
			    event:getType() == df.history_event_type.HIST_FIGURE_SIMPLE_BATTLE_EVENT or
			    event:getType() == df.history_event_type.HF_ATTACKED_SITE or
			    event:getType() == df.history_event_type.HF_DESTROYED_SITE or
			    event:getType() == df.history_event_type.HIST_FIGURE_TRAVEL or
			    event:getType() == df.history_event_type.SITE_DISPUTE or
			    event:getType() == df.history_event_type.WAR_FIELD_BATTLE or
			    event:getType() == df.history_event_type.WAR_ATTACKED_SITE or
			    event:getType() == df.history_event_type.WAR_PLUNDERED_SITE or
			    event:getType() == df.history_event_type.HIST_FIGURE_ABDUCTED or
			    event:getType() == df.history_event_type.HF_GAINS_SECRET_GOAL or
			    event:getType() == df.history_event_type.HIST_FIGURE_REUNION or
			    event:getType() == df.history_event_type.ARTIFACT_POSSESSED or
			    event:getType() == df.history_event_type.CHANGE_CREATURE_TYPE or
			    event:getType() == df.history_event_type.RECLAIM_SITE or
			    event:getType() == df.history_event_type.ENTITY_CREATED or
			    event:getType() == df.history_event_type.ARTIFACT_LOST or
			    event:getType() == df.history_event_type.CHANGE_HF_BODY_STATE or
			    event:getType() == df.history_event_type.CREATED_WORLD_CONSTRUCTION or
			    event:getType() == df.history_event_type.ENTITY_RAZED_BUILDING or
			    event:getType() == df.history_event_type.HF_CONFRONTED or
			    event:getType() == df.history_event_type.WAR_SITE_NEW_LEADER or
			    event:getType() == df.history_event_type.WAR_DESTROYED_SITE or
			    event:getType() == df.history_event_type.ENTITY_LAW or
			    event:getType() == df.history_event_type.WAR_SITE_TAKEN_OVER or
			    event:getType() == df.history_event_type.CEREMONY or
			    event:getType() == df.history_event_type.PROCESSION or
			    event:getType() == df.history_event_type.PERFORMANCE or
			    event:getType() == df.history_event_type.COMPETITION or
			    event:getType() == df.history_event_type.WRITTEN_CONTENT_COMPOSED or
			    event:getType() == df.history_event_type.KNOWLEDGE_DISCOVERED or
			    event:getType() == df.history_event_type.REGIONPOP_INCORPORATED_INTO_ENTITY or
			    event:getType() == df.history_event_type.HF_RELATIONSHIP_DENIED or
			    event:getType() == df.history_event_type.WAR_SITE_TRIBUTE_FORCED or
				event:getType() == df.history_event_type.MUSICAL_FORM_CREATED or
			    event:getType() == df.history_event_type.POETIC_FORM_CREATED or
			    event:getType() == df.history_event_type.DANCE_FORM_CREATED or
			    event:getType() == df.history_event_type.ARTIFACT_TRANSFORMED or
			    event:getType() == df.history_event_type.ARTIFACT_DESTROYED) then
			--]]
        end
    end
    io.write ("</historical_events>".."\n")
end
function export_historical_event_collections()
    io.write ("<historical_event_collections>".."\n")
    io.write ("</historical_event_collections>".."\n")
end
function export_historical_eras()
    io.write ("<historical_eras>".."\n")
    io.write ("</historical_eras>".."\n")
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

--- presses 'd' for detailed maps
function wait_for_legends_vs()
    local vs = dfhack.gui.getCurViewscreen()
    if i <= #MAPS then
        if df.viewscreen_legendsst:is_instance(vs.parent) then
            vs = vs.parent
        end
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
        --export_site_maps()
        --wait_for_legends_vs()
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
