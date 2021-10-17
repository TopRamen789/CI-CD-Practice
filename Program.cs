using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using LibGit2Sharp;
using Newtonsoft.Json;

namespace GitTools
{
    public class Ticket {
        [JsonProperty("Ticket")]
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string StoryPoints { get; set; }
    }

    static class TicketQueryHelper {
        public static Func<Ticket, bool> GetBugTickets = ticket => ticket.Name.Contains("bug");
        public static Func<Ticket, bool> GetFeatureTickets = ticket => ticket.Name.Contains("task");
    }

    class Program {
        static string GetDataFromFile() {
            string fileData = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\data.json");
            return fileData;
        }

        static List<Ticket> GetTicketData() {
            string fileData = GetDataFromFile();
            return JsonConvert.DeserializeObject<List<Ticket>>(fileData);
        }

        static Repository GetRepository() {
            return new Repository($"{Directory.GetCurrentDirectory()}\\GitRepoTest");
        }

        static ILookup<string, IEnumerable<string>> MapBranches(Repository repository, List<Ticket> tickets) {
            var ticketNames = tickets.Select(t => t.Name);
            return repository.Branches.ToLookup(b => b.FriendlyName, b => ticketNames.Where(t => b.FriendlyName.Contains(t)));
        }

        static void Main(string[] args) {
            List<Ticket> tickets = GetTicketData();
            List<Ticket> bugTickets = tickets
                .Where(ticket => TicketQueryHelper.GetBugTickets(ticket))
                .ToList();
            List<Ticket> featureTickets = tickets
                .Where(ticket => TicketQueryHelper.GetFeatureTickets(ticket))
                .ToList();
            var test = GetRepository();
            var mappedBranches = MapBranches(test, tickets);
        }
    }
}