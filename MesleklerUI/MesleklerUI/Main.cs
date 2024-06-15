using Rocket.API;
using Rocket.Core;
using Rocket.Core.Commands;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MesleklerUI
{
    public class Main : RocketPlugin<Configuration>
    {
        public static Main Instance { get; private set; }
        protected override void Load()
        {
            Instance = this;
            Console.WriteLine("JobsUI connected", Console.ForegroundColor = ConsoleColor.Cyan);
            EffectManager.onEffectButtonClicked += OnButtonClicked;
        }
        protected override void Unload()
        {
            Instance = this;
            Console.WriteLine("JobsUI disconnected!");
            EffectManager.onEffectButtonClicked -= OnButtonClicked;
        }


        [RocketCommand("jobs","Shows jobs","/jobs", AllowedCaller.Player)]
        [RocketCommandPermission("pdw.jobs")]
        public void MesleklerOpen(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = caller as UnturnedPlayer;
            EffectManager.sendUIEffect(31355, 31355, player.SteamPlayer().transportConnection, true);
            setModal(player, true);
            for (int i = 99; i >= 0; i--)
            {
                try
                {
                    var job = Configuration.Instance.jobs[i];
                    if (job == null)
                    {
                        EffectManager.sendUIEffectVisibility(31355, player.Player.channel.owner.transportConnection, true, $"JobContent ({i})", false);
                        continue;
                    }
                    EffectManager.sendUIEffectVisibility(31355, player.Player.channel.owner.transportConnection, true, $"JobContent ({i})", true);
                    EffectManager.sendUIEffectImageURL(31355, player.Player.channel.owner.transportConnection, true, $"JobsImage_{i}", job.jobImage);
                    EffectManager.sendUIEffectText(31355, player.Player.channel.owner.transportConnection, true, $"JobsName_{i}", job.jobName);
                    EffectManager.sendUIEffectText(31355, player.Player.channel.owner.transportConnection, true, $"JobsCount_{i}", R.Permissions.GetGroup(job.jobId).Members.Count().ToString());
                }
                catch
                {
                    EffectManager.sendUIEffectVisibility(31355, player.Player.channel.owner.transportConnection, true, $"JobContent ({i})", false);
                }
            }
            setModal(player, true);
        }
        [RocketCommand("addjob", "You can add job with this command!", "/addjob <name> <id> <imageUrl>", AllowedCaller.Player)]
        [RocketCommandPermission("pdw.addjob")]
        public void MeslekEkle(IRocketPlayer caller, string[] arg)
        {
            UnturnedPlayer player = caller as UnturnedPlayer;
            if(arg.Length < 3)
            {
                UnturnedChat.Say(player, "/addjob <name> <id> <imageUrl>", Color.red);
                return;
            }
            Configuration.Instance.jobs.Add(new Job
            {
                jobName = arg[0],
                jobId = arg[1],
                jobImage = arg[2],
            });
            Configuration.Save();
            UnturnedChat.Say(player, $"{arg[0]} succesfully added!", Color.cyan);
        }
        private void OnButtonClicked(Player Player, string buttonName)
        {
            if (buttonName == "JobsUIClose")
            {
                UnturnedPlayer player = UnturnedPlayer.FromPlayer(Player);
                EffectManager.askEffectClearByID(31355, player.SteamPlayer().transportConnection);
                setModal(player, false);
            }
        }

        private void setModal(UnturnedPlayer player, bool state)
        {
            player.Player.setPluginWidgetFlag(EPluginWidgetFlags.Modal, state);
        }

    }
}
