# Modular Weapons System - Game Design Document

## 1. Project Overview

### 1.1 Vision Statement
Create a comprehensive modular weapon system for Terraria that allows players to customize and upgrade their weapons throughout the entire game progression, replacing the traditional "find better weapon, discard old weapon" cycle with meaningful customization and long-term investment.

### 1.2 Core Principles
- **Meaningful Choice**: Every modifier combination should feel different and viable
- **Progression Integration**: Follow Terraria's established metal tier progression
- **Balance Through Constraint**: Point system prevents overpowered combinations
- **Player Investment**: Weapons grow with the player rather than being replaced
- **Intuitive Design**: Leverage existing Terraria knowledge (metal tiers, crafting patterns)

### 1.3 Target Audience
- Terraria players who enjoy customization and progression systems
- Players who want more variety in weapon mechanics
- Both casual players (simple upgrades) and min-maxers (complex optimization)

## 2. Core Systems

### 2.1 Modular Weapon Framework
Each modular weapon consists of:
- **Base Weapon**: Determines weapon type, base stats, and point budget
- **Four Modifier Slots**: Each slot has a specific category
- **Point Budget System**: Limits total modifier point cost
- **Upgrade Path**: Following Terraria's metal progression

### 2.2 Point Budget System
**Purpose**: Prevent overpowered combinations while allowing meaningful choice

**Example Point Budgets** (UPDATED):
```
Copper Tier: 8 points    (+2 to accommodate 4th slot)
Iron Tier: 11 points
Silver Tier: 14 points
Gold Tier: 17 points
Cobalt Tier: 20 points
Mythril Tier: 24 points
Adamantite Tier: 28 points
Hallowed Tier: 32 points
```

### 2.3 Modifier Categories (REVISED)

**Universal Categories** (apply to all weapons):
1. **Ammo Type**: Magic, Arrow, Bullet, Rocket
2. **Damage Type**: Fire, Water, Lightning, Earth, Wind, Slime  
3. **Shot Type**: Auto Fire, Burst Fire, Charge Fire
4. **Special Effects**: Piercing, Bouncing, Homing, Life Steal, Crit Boost *(Boss drops only)*

**Weapon-Specific Categories** (Future weapons):
- Swords: Blade Type (Slash, Pierce, Crush) 
- Bows: Arrow Enhancement (Standard, Multi, Explosive)
- Whips: Segment Type (Short, Medium, Long)

### 2.4 Modifier Specifications (REVISED)

#### 2.4.1 Ammo Type Modifiers (1-4 points)
- **Magic** (1pt): Infinite ammo, consumes mana instead
- **Arrow** (2pts): Uses arrow ammunition, cheap and plentiful
- **Bullet** (3pts): Uses bullet ammunition, mid-tier damage/cost
- **Rocket** (4pts): Uses rocket ammunition, high damage/cost

#### 2.4.2 Damage Type Modifiers (2-3 points)
- **Fire** (2pts): Applies "On Fire!" debuff
- **Water** (2pts): Applies "Slow" debuff  
- **Lightning** (3pts): Applies "Ichor" debuff (defense reduction)
- **Earth** (3pts): Applies "Bleeding" debuff
- **Wind** (2pts): +50% knockback bonus
- **Slime** (3pts): Applies "Poisoned" debuff

#### 2.4.3 Shot Type Modifiers (2-4 points)
- **Auto Fire** (4pts): Hold to continuously fire, reduced damage per shot
- **Burst Fire** (2pts): Single click fires multiple projectiles, balanced damage
- **Charge Fire** (3pts): Hold to charge, release for high damage shot

#### 2.4.4 Special Effects Modifiers (3-5 points, BOSS DROPS ONLY)
- **Piercing** (3pts): Projectiles penetrate 1 enemy - *King Slime drop*
- **Bouncing** (3pts): Projectiles ricochet once - *Brain of Cthulhu drop*
- **Homing** (4pts): Slight target seeking behavior - *Skeletron drop*
- **Life Steal** (4pts): 2% damage converted to health - *Wall of Flesh drop*
- **Crit Boost** (3pts): +10% critical strike chance - *Eye of Cthulhu drop*

## 3. Weapon Types

### 3.1 Priority Order (Development)
1. **Modular Gun** ✅ (Complete-ish)
2. **Modular Sword** (Next)
3. **Modular Bow**
4. **Modular Whip**
5. **Modular Spear**
6. **Modular Yoyo**
7. **Modular Flail**
8. **Modular Boomerang**
9. **Modular Dart Weapon**

### 3.2 Weapon Specifications

#### 3.2.1 Modular Sword
**Modifier Categories**:
- **Blade Type**: Slash (wide arc), Pierce (forward thrust), Crush (overhead slam)
- **Damage Type**: Fire, Water, Lightning, Earth, Arcane
- **Attack Rate**: Fast, Normal, Heavy

**Unique Mechanics**:
- Blade type affects swing pattern and hitbox
- Combo potential with different attack rates
- Visual effects change based on damage type

#### 3.2.2 Modular Bow
**Modifier Categories**:
- **Arrow Type**: Standard, Multi-shot, Piercing, Explosive
- **Damage Type**: Fire, Water, Lightning, Earth, Arcane
- **Draw Type**: Quick, Normal, Heavy

**Unique Mechanics**:
- Arcane modifier removes ammo requirement, uses mana
- Draw type affects damage and speed
- Arrow type determines projectile behavior

#### 3.2.3 Modular Whip
**Modifier Categories**:
- **Segment Type**: Short (fast, low range), Medium (balanced), Long (slow, high range)
- **Damage Type**: Fire, Water, Lightning, Earth, Arcane
- **Tip Type**: Standard, Barbed, Weighted

**Unique Mechanics**:
- Summoner weapon that benefits minions
- Segment type affects range and attack speed
- Tip type affects effects on hit

### 3.3 Cross-Weapon Synergies
- **Damage Type consistency**: Fire mods work similarly across all weapons
- **Shared crafting materials**: Efficiency in resource gathering
- **Set bonuses**: Potential future feature for using multiple modular weapons

### 4. Progression System

### 4.1 Modifier Acquisition

**Base Modifiers** (Ammo/Damage/Shot):
- Crafted using standard materials
- Available from game start
- Follow metal tier progression for upgrades

**Special Effects Modifiers**:
- **BOSS DROPS ONLY** - rare drops (5-10% chance)
- Gated behind boss progression
- Cannot be crafted, only found
- Optional enhancement to core 3-slot system

**Boss Drop Schedule**:
```
Eye of Cthulhu → Crit Boost Modifier
King Slime → Piercing Modifier  
Brain of Cthulhu → Bouncing Modifier
Skeletron → Homing Modifier
Wall of Flesh → Life Steal Modifier
```

### 4.2 Progression Philosophy (REVISED)

**Early Game (3-slot focus)**:
- Players learn Ammo + Damage + Shot combinations
- Point budgets force meaningful choices
- No overwhelming special effects

**Mid Game (Boss progression)**:
- Special effects unlock as optional enhancements
- Players can choose to pursue boss drops or ignore them
- 4th slot remains empty until boss defeated

**Late Game (Full customization)**:
- All modifier categories available
- Complex builds possible with higher point budgets
- Special effects enable unique playstyles

### 4.3 Material Requirements
**Standard Materials**:
- **Modular Components** (universal upgrade material from enemy drops)
- Metal bars (tier-appropriate upgrade material)
- Gems (for damage type modifiers)
- Boss materials (for high-tier upgrades)
- Special items (for unique effects)

**Example Recipes**:
```
Iron Fire Damage Modifier:
- Copper Fire Damage Modifier
- Iron Bar x3
- Basic Modular Components x5
- Ruby x2

Hallowed Lightning Modifier:
- Adamantite Lightning Modifier  
- Hallowed Bar x5
- Hallowed Modular Components x15
- Soul of Light x10
- Crystal Shard x3
```

**Acquisition Methods**:
- **Base Modifiers**: Loot drops from appropriate enemies/biomes
- **Upgrade Materials**: Crafted using base modifier + metal bars + modular components
- **Modular Components**: Universal drops from all enemies (tiered by progression)

## 5. User Interface Design

### 5.1 Modifier Station
**Core Features**:
- **Weapon Slot**: Place modular weapon to modify
- **Three Modifier Slots**: Visual representation of categories
- **Point Display**: Current/maximum points used
- **Auto-Install/Extract**: Streamlined modifier management
- **Modifier Retention**: Removed modifiers are returned to player (supports upgrade path)

**Quality of Life**:
- Drag and drop functionality
- Distance-based auto-close
- Visual feedback for valid/invalid combinations
- Real-time stat preview through weapon tooltips
- **Storage**: Players use existing chest system for modifier storage

### 5.2 Tooltip System
**Information Display**:
- Current modifiers installed
- Point budget usage
- Stat modifications with real-time preview
- Special effects description
- Clear indication when weapon is incomplete

**Visual Design**:
- Color coding for modifier tiers
- **Simple Visual Progression**: Base modifier sprites with metal-tier tinting
- Clear point cost display
- Warning indicators for incomplete weapons


## 6. Balancing Framework (UPDATED)

### 6.1 Point Cost Guidelines (REVISED)

**Basic Tier (1-2 points)**:
- Magic ammo (unlimited but mana-gated)
- Basic damage types (Fire, Water, Wind)

**Standard Tier (2-3 points)**:
- Arrow/Bullet ammo types
- Burst/Charge shot types
- Advanced damage types (Lightning, Earth, Slime)

**Premium Tier (3-4 points)**:
- Rocket ammo (expensive ammunition)
- Auto Fire (high DPS potential)
- Special effects (Piercing, Bouncing, Crit Boost)

**Elite Tier (4-5 points)**:
- Advanced special effects (Homing, Life Steal)

### 6.2 Balance Considerations

**Boss-Gated Special Effects**:
- Prevent early game power spikes
- Reward boss progression
- Optional complexity (players can ignore 4th slot)
- Higher point costs limit stacking with other powerful mods

**Ammo Type Balance**:
- Magic: Infinite but mana-limited
- Arrow: Cheap, plentiful ammo
- Bullet: Moderate cost/damage
- Rocket: Expensive but powerful

## 7. Technical Implementation

### 7.1 Code Architecture
**Core Components**:
- `ModularWeapon` base class
- `WeaponModifier` system
- `ModifierStation` tile and UI
- Save/load data management
- Recipe generation system

**Design Patterns**:
- Factory pattern for modifier creation
- Observer pattern for UI updates
- Strategy pattern for weapon behaviors
- Command pattern for upgrade operations

### 7.2 Data Management
**Save Data Structure**:
```csharp
public class ModularWeaponData
{
    public int weaponTier;
    public int[] modifierIDs;
    public int[] modifierTiers;
    public int pointBudget;
    public Dictionary<string, object> customData;
}
```

**Persistence Requirements**:
- Individual weapon state
- Modifier configurations
- Upgrade progress
- Player preferences

### 7.3 Performance Considerations
- Efficient modifier lookup systems
- Minimal garbage collection impact
- Optimized UI rendering
- Reduced network sync requirements

## 8. Development Priorities

### 8.1 MVP (Core Features)
**Phase 1: Core 3-Slot System**
1. Implement Ammo/Damage/Shot type framework
2. Update point budget system (+2 points per tier)
3. Create basic modifier items and recipes
4. Test balance with 3-slot combinations

**Phase 2: Special Effects Integration**  
1. Add 4th slot to UI and weapon framework
2. Implement boss drop system for special modifiers
3. Create special effect items (no recipes)
4. Balance point costs for 4-slot combinations

**Phase 3: Content Expansion**
1. Additional weapon types
2. Higher tier modifiers
3. Advanced special effects
4. Polish and optimization

**Priority Weapon Types**:
1. Modular Gun ✅ (Complete)
2. Modular Sword (Core melee test)
3. Modular Bow (Test arcane conversion)
4. Modular Whip (Summoner support)

### 8.2 Push Goals (Future Features)
**Polish & Enhancement** (Nice-to-have for later versions):
- Build saving/loading system
- Advanced weapon visual changes based on modifiers
- Modifier codex/discovery tracking
- Tutorial quest chains
- End-game retention systems (prestige, challenges)
- Multiplayer-specific features (trading, visual display)
- Advanced audio design
- Legendary modifier effects
- Cross-weapon synergy bonuses

**Additional Weapon Types**:
- Modular Spear, Yoyo, Flail, Boomerang, Dart Weapons

## 9. Content Expansion Plan

### 9.1 Phase 1: MVP Foundation
- ✅ Modular Gun (Complete)
- Modular Sword implementation
- Point system balancing
- Metal progression crafting
- Modular components system
- Basic particle effects

### 9.2 Phase 2: Core Weapon Variety  
- Modular Bow with arcane system
- Modular Whip
- Advanced modifier effects
- Balance refinement
- Core system polish

### 9.3 Phase 3: Expansion
- Remaining weapon types
- Push goal features implementation
- Advanced visual systems
- Community feedback integration

### 9.4 Phase 4: Advanced Content
- Post-Moon Lord progression
- Legendary modifier system
- Challenge modes
- Integration with mod ecosystem

## 10. Success Metrics

### 10.1 MVP Success Criteria
**Core System Adoption**:
- Players actively use modifier stations
- Weapon upgrades feel rewarding and impactful
- Point budget creates meaningful choices
- Modular components provide steady progression

**Mechanical Balance**:
- No single "best" build dominance
- All weapon types feel viable and distinct
- Progression feels natural alongside Terraria's metal tiers
- Resource costs feel fair for power gained

**Technical Stability**:
- Save/load system works reliably
- UI performs smoothly
- Particle effects don't impact performance
- Modifier system integrates seamlessly

### 10.2 Community Engagement
**Player Behavior**:
- **Modifier Station Usage**: Regular weapon customization
- **Upgrade Frequency**: Active progression through metal tiers
- **Build Experimentation**: Players trying different combinations
- **Retention**: Keeping modular weapons across progression

**Feedback Quality**:
- Constructive balance suggestions
- Feature requests align with push goals
- Bug reports help improve stability
- Community sharing of builds and strategies

### 10.3 Long-term Health (Post-MVP)
**System Expansion**:
- Additional weapon types integrate smoothly
- Push goal features enhance rather than complicate
- Mod compatibility maintained
- Community contribution opportunities

## 11. Risk Assessment (UPDATED)

### 11.1 New Risks

**Boss Drop Dependency**:
- Players may not engage with special effects if boss fights are avoided
- Balancing drop rates vs player frustration
- **Mitigation**: Make special effects optional, not required for weapon function

**4th Slot Complexity**:
- 360 total combinations vs original 72
- Exponential balance testing requirements  
- **Mitigation**: Boss-gate the complexity, phase implementation

**Point Budget Inflation**:
- Higher point budgets may make early restrictions meaningless
- **Mitigation**: Conservative point increases, thorough testing

### 11.2 Success Metrics (REVISED)

**Core System Health**:
- Players actively use 3-slot combinations before pursuing bosses
- Boss drops feel rewarding, not mandatory
- Special effects enhance rather than dominate gameplay

**Progression Flow**:
- Early game: Focus on basic 3-slot mastery
- Mid game: Boss hunting for special modifiers
- Late game: Complex 4-slot optimization

## 12. Future Vision

### 12.1 MVP Completion Goals
**Immediate Success Targets**:
- **4 Core Weapon Types**: Gun, Sword, Bow, Whip fully functional
- **Balanced Progression**: Point system creates meaningful choices
- **Natural Integration**: Feels native to Terraria's gameplay
- **Community Adoption**: Players actively engage with the system

### 12.2 Post-MVP Expansion
**Phase 1 Extensions**:
- Remaining weapon types (Spear, Yoyo, Flail, Boomerang, Dart)
- Enhanced visual effects and weapon appearance changes
- Quality of life improvements from community feedback

**Phase 2 Advanced Features**:
- Build saving/loading system
- Legendary modifiers with unique mechanics
- Cross-weapon synergy systems
- Challenge modes and end-game retention

### 12.3 Long-term Vision
**System Evolution**:
- **Complete Weapon Coverage**: All major weapon types modular
- **Seamless Integration**: Indistinguishable from core Terraria content
- **Community Standard**: Widely adopted as essential mod
- **Mod Ecosystem Foundation**: Other mods building on this framework

**Potential Expansions** (Far Future):
- **Armor Modularity**: Extend system to armor pieces
- **Tool Integration**: Modular pickaxes, drills, etc.
- **Building Components**: Modular furniture and decorations
- **Magic System**: Modular spell crafting

### 12.4 Community Features
**Sharing & Discovery**:
- Export/import weapon configurations
- Community build databases
- Educational content and tutorials
- Competitive PvP modifier sets

---

*Document Version: 2.1*  
*Last Updated: June 16, 2025*  
*Major Changes: Revised modifier system from 3 to 4 categories, boss-gated special effects, updated point budgets, ammo type system*