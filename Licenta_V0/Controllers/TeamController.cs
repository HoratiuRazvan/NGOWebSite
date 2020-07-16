using Licenta_V0.Models;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Licenta_V0.Controllers
{
    public class TeamController : Controller
    {
        ApplicationDbContext context;
        public TeamController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Team
        public ActionResult Index()
        {
            var viewModel = context.Teams.Include(p => p.TeamLeader).ToList();
            return View(viewModel);
        }
        public ActionResult Show(int id)
        {
            var team = context.Teams.SingleOrDefault(m => m.Id == id);
            if (team == null)
                return HttpNotFound();
            List<MemberModels> membersOfTeam = context.Members.Include(m => m.Member).Where(m => m.TeamId == id).ToList();

            var viewModel = new ShowTeamViewModel
            {
                Team = team,
                Members = membersOfTeam,
                IsOwner = false,
                IsAdmin = false
            };
            var userId = User.Identity.GetUserId();

            if (userId == team.TeamLeaderId)
                viewModel.IsOwner = true;

            if (User.IsInRole("Admin"))
                viewModel.IsAdmin = true;
            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            List<ApplicationUser> usersInDb = context.Users.ToList();
            var index = usersInDb.FindIndex(m => m.UserName == "admin@admin.com");
            usersInDb.RemoveAt(index);

            var viewModel = new NewTeamViewModel
            {
                Team = new TeamModels
                {
                    Id = 0
                },
                Users = usersInDb
            };

            return View("TeamForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            List<ApplicationUser> usersInDb = context.Users.ToList();
            var index = usersInDb.FindIndex(m => m.UserName == "admin@admin.com");
            usersInDb.RemoveAt(index);

            var team = context.Teams.SingleOrDefault(m => m.Id == id);
            if (team == null)
                return HttpNotFound();


            var viewModel = new NewTeamViewModel
            {
                Team = team,
                Users = usersInDb
            };

            return View("TeamForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TeamModels team)
        {
            if (!ModelState.IsValid)
            {
                List<ApplicationUser> usersInDb = context.Users.ToList();
                var index = usersInDb.FindIndex(m => m.UserName == "admin@admin.com");
                usersInDb.RemoveAt(index);
                var viewModel = new NewTeamViewModel
                {
                    Team = team,
                    Users = usersInDb
                };
                return View("TeamForm", viewModel);
            }
            if (team.Id == 0)
            {
                context.Teams.Add(team);
            }
            else
            {
                var dbTeam = context.Teams.SingleOrDefault(s => s.Id == team.Id);
                if (dbTeam == null)
                    return HttpNotFound();

                dbTeam.Title = team.Title;
                dbTeam.TeamLeaderId = team.TeamLeaderId;
            }
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AddMember(int id)
        {
            var teamInDb = context.Teams.SingleOrDefault(m => m.Id == id);
            if (teamInDb == null)
                return HttpNotFound();
            List<ApplicationUser> usersInDb = context.Users.ToList();
            List<ApplicationUser> usersForViewModel = new List<ApplicationUser>();
            List<ApplicationUser> usersAlreadyMembers = context.Members.Where(m => m.TeamId == teamInDb.Id).Select(m => m.Member).ToList();
            var index = usersInDb.FindIndex(m => m.UserName == "admin@admin.com");
            usersInDb.RemoveAt(index);
            index = usersInDb.FindIndex(m => m.Id == teamInDb.TeamLeaderId);
            usersInDb.RemoveAt(index);
            foreach (var user in usersInDb)
            {
                if (usersAlreadyMembers.FindIndex(p => p.Id == user.Id) == -1)
                    usersForViewModel.Add(user);
            }

            var viewModel = new AddMemberViewModel
            {
                Member = new MemberModels
                {
                    Id = 0,
                    TeamId = teamInDb.Id,
                    Team = teamInDb
                },
                Users = usersForViewModel
            };
            return View("AddMember", viewModel);
        }
        [HttpPost]
        public ActionResult SaveMember(MemberModels member)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddMember", new { id = member.TeamId });
            }
            context.Members.Add(member);
            context.SaveChanges();

            return RedirectToAction("Show", new { id = member.TeamId });
        }
    }
}