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
            Content.Systems.ModifierData.InitializeModifierCosts();
            ModContent.GetInstance<TestMod>().Logger.Info("Modifier point costs initialized");
        }

	}
}
