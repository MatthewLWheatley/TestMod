using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TestMod
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class TestMod : Mod
	{
		public override void PostSetupContent()
        {
            // Initialize the modifier point costs
            Content.Systems.ModifierData.InitializeModifierCosts();
            
            // Debug log to confirm initialization
            ModContent.GetInstance<TestMod>().Logger.Info("Modifier point costs initialized");
        }
        
        // Alternative: Initialize in Mod.Load if PostSetupContent doesn't work
        public override void Load()
        {
            // Backup initialization
        }
	}
}
