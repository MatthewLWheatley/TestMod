using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ModularWeapons
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class ModularWeapons : Mod
    {
        public override void PostSetupContent()
        {
            Content.Systems.ModifierData.InitializeModifierCosts();
            ModContent.GetInstance<ModularWeapons>().Logger.Info("Modifier point costs initialized");
        }

	}
}
