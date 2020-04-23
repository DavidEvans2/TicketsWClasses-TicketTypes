using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TicketingSystem
{
    class TaskFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string filePath { get; set; }
        public List<TaskTicketing> TaskTicketing { get; set; }

        public TaskFile(string path)
        {
            TaskTicketing = new List<TaskTicketing>();
            filePath = path;

            try
            {
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    TaskTicketing ticket = new TaskTicketing();
                    string line = sr.ReadLine();

                    int idx = line.IndexOf('"');
                    if (idx == -1)
                    {
                        string[] ticketInfo = line.Split(',');

                        ticket.ticketID = (ticketInfo[0]);

                        ticket.summary = ticketInfo[1];

                        ticket.status = ticketInfo[2];

                        ticket.priorityLevel = ticketInfo[3];

                        ticket.submitter = ticketInfo[4];

                        ticket.assignee = ticketInfo[5];

                        ticket.watching = ticketInfo[6].Split('|').ToList();

                        ticket.projectName = ticketInfo[7];

                        ticket.dateDue = ticketInfo[8];
                    }
                    else
                    {
                        ticket.ticketID = (line.Substring(0, idx - 1));
                        line = line.Substring(idx + 1);
                        idx = line.IndexOf('"');
                        ticket.summary = line.Substring(0, idx);
                        line = line.Substring(idx + 2);
                        ticket.status = line.Substring(0, idx);
                        line = line.Substring(idx + 3);
                        ticket.priorityLevel = line.Substring(0, idx);
                        line = line.Substring(idx + 4);
                        ticket.submitter = line.Substring(0, idx);
                        line = line.Substring(idx + 5);
                        ticket.assignee = line.Substring(0, idx);
                        line = line.Substring(idx + 6);
                        ticket.watching= line.Split('|').ToList();
                        line = line.Substring(idx + 7);
                        ticket.projectName = line.Substring(0, idx);
                        line = line.Substring(idx + 8);
                        ticket.dateDue = line.Substring(0, idx);
                        line = line.Substring(idx + 9);
                    }
                    TaskTicketing.Add(ticket);
                }
                sr.Close();
                logger.Info("Tickets in file {Count}", TaskTicketing.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
        public void AddTicket(TaskTicketing ticket)
        {
            try
            {
                ticket.ticketID = TaskTicketing.Max(m => m.ticketID) + 1;
                string summary = ticket.summary;

                string status = ticket.status;

                string priority = ticket.priorityLevel;

                string submitter = ticket.submitter;

                string assignee = ticket.assignee;

                string project = ticket.projectName;

                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{ticket.ticketID},{summary},{status},{priority},{assignee},{submitter},{string.Join("|", ticket.watching)}," +
                    $"{project}, {ticket.dateDue}");
                sw.Close();
                TaskTicketing.Add(ticket);
                logger.Info("Ticket id {Id} added", ticket.ticketID);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
