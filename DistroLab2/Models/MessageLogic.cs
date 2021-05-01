using DistroLab2.Areas.Identity.Models;
using DistroLab2.Data;
using DistroLab2.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DistroLab2.Models
{
    /// <summary>
    /// Message logic for handling user's messages
    /// </summary>
    public class MessageLogic
    {
        readonly MessageRepository messageRepository;
        readonly UserManager<LoginUser> userManager;


        
        public MessageLogic(UserManager<LoginUser> userManager)
        {
            messageRepository = new MessageRepository();
            this.userManager = userManager;

        }

        /// <summary>
        /// Gets all messages that exist in the logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MessageViewModel GetMessage(int? id, String userId)
        {
            Message message = messageRepository.GetMessage(id);
            MessageViewModel messageViewModel = new MessageViewModel();
            if (message.RecipantId.Equals(userId)){
                messageViewModel.Id = message.Id;
                var userName = userManager.FindByIdAsync(message.SenderId).GetAwaiter().GetResult().UserName;
                messageViewModel.Sender = userName;
                messageViewModel.Recipant = message.RecipantId;
                messageViewModel.Title = message.Title;
                messageViewModel.Content = message.Content;
                messageViewModel.TimeSent = message.TimeSent;
                messageViewModel.IsRead = message.IsRead;
                messageViewModel.IsDeleted = message.IsDeleted;
                messageRepository.SetRead(id);
                return messageViewModel;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all messages that are send from sender to recipant
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="recipantName"></param>
        /// <returns></returns>
        public List<MessageViewModel> GetMessagesFromSenderToRecipant(String senderName, String recipantName)
        {

            var senderId = userManager.FindByNameAsync(senderName).GetAwaiter().GetResult().Id;
            var recipantId = userManager.FindByNameAsync(recipantName).GetAwaiter().GetResult().Id;
            List<Message> messageList = messageRepository.GetMessagesFromSenderToRecipant(senderId, recipantId);
            List<MessageViewModel> modelList= new List<MessageViewModel>();
            for(int i = 0; i< messageList.Count; i++){
                MessageViewModel model = new MessageViewModel();
                model.Id = messageList[i].Id;
                model.Sender = senderName;
                model.TimeSent = messageList[i].TimeSent;
                model.Title = messageList[i].Title;
                model.IsRead = messageList[i].IsRead;
                modelList.Add(model);
            }
            return modelList;
        }

        /// <summary>
        /// Creates the message when sended
        /// </summary>
        /// <param name="messageViewModel"></param>
        /// <param name="userId"></param>
        /// <param name="timeSent"></param>
        public void CreateMessage(MessageViewModel messageViewModel, String userId, DateTime timeSent)
        {
            var message = new Message();
            message.SenderId = userId;
            var recipantId = userManager.FindByNameAsync(messageViewModel.Recipant).GetAwaiter().GetResult().Id;
            message.RecipantId = recipantId;
            message.Content = messageViewModel.Content;
            message.Title = messageViewModel.Title;
            message.TimeSent = timeSent;
            message.IsDeleted = false;
            message.IsRead = false;
            messageRepository.AddMessageToDb(message);
        }

        /// <summary>
        /// Gets the sender
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public String GetSender(int id)
        {
            return messageRepository.GetSenderFromDb(id);
        }

        /// <summary>
        /// Deletes the selected message
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        public void Deletemessage(int id, String userId)
        {
            var recipantId = messageRepository.GetMessage(id).RecipantId;
            if (recipantId.Equals(userId))
            {
                messageRepository.DeleteMessageFromDb(id);
            }
        }

        /// <summary>
        /// Gets the inbox from the logged in user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ReadViewModel GetInbox(String userId)
        {
            ReadViewModel readViewModel = new ReadViewModel();
            readViewModel.UserList = new List<string>();
            var messages = messageRepository.GetMessagesToRecipant(userId);
            readViewModel.NrOfDeleted = 0;
            readViewModel.NrOfRead = 0;
            readViewModel.TotNrOfMessages = 0;
            foreach (var message in messages)
            {
                readViewModel.TotNrOfMessages++;
                var sender = userManager.FindByIdAsync(message.SenderId).GetAwaiter().GetResult().UserName;

                if (!message.IsDeleted)
                {
                    if (!readViewModel.UserList.Contains(sender))
                    {
                        readViewModel.UserList.Add(sender);
                    }
                    if (message.IsRead)
                    {
                        readViewModel.NrOfRead++;
                    }
                }
                else
                {
                    readViewModel.NrOfRead++;
                    readViewModel.NrOfDeleted++;
                }
            }
            return readViewModel;
        }



    }
}
