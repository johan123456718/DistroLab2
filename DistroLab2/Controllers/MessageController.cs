using DistroLab2.Areas.Identity.Models;
using DistroLab2.Models;
using DistroLab2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistroLab2.Controllers
{
    /// <summary>
    /// Controller used for the application
    /// </summary>
    [Authorize]
    public class MessageController : Controller
    {
        
        private readonly UserManager<LoginUser> _userManager;
        private readonly MessageLogic _messageLogic;
        private readonly UserLogic _userLogic;

        public MessageController( UserManager<LoginUser> userManager)
        {
            _userManager = userManager;
            _messageLogic = new MessageLogic(userManager);
            _userLogic = new UserLogic(userManager);
        }
        /// <summary>
        /// Returns the home page with login info for the logged in user
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            String userId = _userManager.GetUserId(User);
            IndexViewModel indexViewModel = _userLogic.GetLoginDetails(userId);
            return View(indexViewModel);
        }

        /// <summary>
        /// Returns the list of messages from a given sender to the logged in user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        // GET: Message
        public IActionResult MessageList(String userName)
        {
            var recipantName = _userManager.GetUserName(User);
            List<MessageViewModel> messages = _messageLogic.GetMessagesFromSenderToRecipant(userName, recipantName);
            return View(messages);
        }
        /// <summary>
        /// Returns all the info for a given message, given that it is sent to the logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Message/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            String userId = _userManager.GetUserId(User);
            var message = _messageLogic.GetMessage(id, userId);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        /// <summary>
        /// Returns the view used to create and send a message
        /// </summary>
        /// <returns></returns>
        // GET: Message/Send
        public IActionResult Send()
        {    
            SendFormViewModel model = new SendFormViewModel();
            model.Users = _userLogic.GetAllUsers();
            return View(model);
        }
        /// <summary>
        /// Sends the message to the given recipant
        /// </summary>
        /// <param name="messageViewModel"></param>
        /// <returns></returns>
        // POST: Message/Send
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send([Bind("Recipant,Title,Content")] MessageViewModel messageViewModel)
        {
 
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var timeSent = DateTime.Now;
                _messageLogic.CreateMessage(messageViewModel, userId, timeSent);
                TempData["confirmData"] = "The message has been sent to " + messageViewModel.Recipant + ", " + timeSent;
                return RedirectToAction(nameof(Send));
            }
            TempData["confirmData"] = "";
            return RedirectToAction(nameof(MessageList));
        }
        /// <summary>
        /// Returns delete window with message info, given that the message is sent to the logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Message/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            String userId = _userManager.GetUserId(User);
            var message = _messageLogic.GetMessage(id, userId);

            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        /// <summary>
        /// Deletes the given message
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            String userId = _userManager.GetUserId(User);
            var message = _messageLogic.GetMessage(id, userId);
            _messageLogic.Deletemessage(id, userId);
            
            return RedirectToAction(nameof(MessageList), new { userName = message.Sender});
        }

        /// <summary>
        /// Returns the view of the inbox with user that has sent messages to the logged in user
        /// </summary>
        /// <returns></returns>
        // GET: Inbox
        public async Task<IActionResult> Inbox()
        {
            var userId = _userManager.GetUserId(User);
            ReadViewModel readViewModel = _messageLogic.GetInbox(userId);
            return View(readViewModel);
        }
    }
}
