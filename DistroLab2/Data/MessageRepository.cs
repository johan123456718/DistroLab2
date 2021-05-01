using DistroLab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A repostitory that gets and manipulate the information of message database
/// </summary>
/// 
namespace DistroLab2.Data
{
    public class MessageRepository
    {
        private readonly MessageDbContext _context;

        public MessageRepository()
        {
            _context = new MessageDbContext();
        }

        public Message GetMessage(int? id)
        {
            return _context.Messages.Where(m => m.Id.Equals(id)).Single();
        }

        public List<Message> GetMessagesFromSenderToRecipant(String senderId, String recipantId)
        {
            return _context.Messages.Where(m => m.SenderId.Equals(senderId) && m.RecipantId.Equals(recipantId)&& !m.IsDeleted).OrderByDescending(m => m.TimeSent).ToList();
        }

        public void AddMessageToDb(Message message)
        {
            _context.Add(message);
            _context.SaveChanges();
        }

        public void DeleteMessageFromDb(int id)
        {
            var query = _context.Messages.Where(m => m.Id.Equals(id)).Single();
            query.IsDeleted = true;
            _context.SaveChanges();
            
        }

        public String GetSenderFromDb(int id)
        {
            return _context.Messages.Where(m => m.Id.Equals(id)).Single().SenderId;
        }

        public List<Message> GetMessagesToRecipant(String userId)
        {
            return _context.Messages.Where(m => m.RecipantId.Equals(userId)).OrderByDescending(m => m.TimeSent).ToList();
        }

        public void SetRead(int? id)
        {
            Message message = _context.Messages.Where(m => m.Id.Equals(id)).Single();
            message.IsRead = true;
            _context.SaveChanges();

        }
    }
}
