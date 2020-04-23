using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem
{
    class TaskTicketing
    {
        public string ticketID { get; set; }
        public string summary { get; set; }
        public string status { get; set; }
        public string priorityLevel { get; set; }
        public string assignee { get; set; }
        public string submitter { get; set; }
        public List<string> watching { get; set; }
        public string dateDue { get; set; }
        public string projectName { get; set; }

        public TaskTicketing()
        {
            watching = new List<string>();
        }
        public string Display()
        {
            return $"Id: { ticketID}\nSummary: {summary}\nStatus: {status}\nPriority: {priorityLevel}\n" +
                $"Assignee: {assignee}\nSubmitter: {submitter}\n Watchers: {string.Join(", ", watching)}" +
                $"\nProject Name: {projectName}\n" +
                $"Due Date: {dateDue}\n";
        }
        
    }
}
