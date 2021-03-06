﻿using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace pmcenter.Commands
{
    internal class SwitchNotificationCommand : ICommand
    {
        public bool OwnerOnly => true;

        public string Prefix => "switchnf";

        public async Task<bool> ExecuteAsync(TelegramBotClient botClient, Update update)
        {
            var IsDisabledNow = Conf.SwitchNotifications();
            _ = await Conf.SaveConf(false, true).ConfigureAwait(false);
            _ = await botClient.SendTextMessageAsync(update.Message.From.Id,
                IsDisabledNow ?
                    Vars.CurrentLang.Message_NotificationsOff :
                    Vars.CurrentLang.Message_NotificationsOn,
                ParseMode.Markdown,
                false,
                Vars.CurrentConf.DisableNotifications,
                update.Message.MessageId).ConfigureAwait(false);

            return true;
        }
    }
}
