using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TicketingSystem
{
    class ticketFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string fileDirectory { get; set; }
        public List<Ticket> Ticket { get; set; }

        public ticketFile(string path)
        {
            Ticket = new List<Ticket>();
            fileDirectory = path;

            try
            {
                StreamReader sr = new StreamReader(fileDirectory);
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    Ticket ticket = new Ticket();
                    string line = sr.ReadLine();

                    int idx = line.IndexOf('"');
                    if (idx == -1)
                    {
                        string[] ticketDetails = line.Split(',');
                        ticket.ticketID = (ticketDetails[0]);
                        ticket.summary = ticketDetails[1];
                        ticket.status = ticketDetails[2];
                        ticket.priorityLevel = ticketDetails[3];
                        ticket.submitter = ticketDetails[4];
                        ticket.assignee = ticketDetails[5];
                        ticket.watching = ticketDetails[6].Split('|').ToList();
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
                        ticket.watching = line.Split('|').ToList();
                    }
                    TicketingSystem.Ticket.Add(ticket);
                }
                sr.Close();
                logger.Info("Movie File Number: ", Ticket.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
        public void AddTicket(Ticket ticket)
        {
            try
            {
                ticket.ticketID = Ticket.Max(m => m.ticketID) + 1;
                string summary = ticket.summary;
                string status = ticket.status;
                string priorityLevel = ticket.priorityLevel;
                string submitter = ticket.submitter;
                string assignee = ticket.assignee;
                StreamWriter sw = new StreamWriter(fileDirectory, true);
                sw.WriteLine($"{ticket.ticketID},{summary},{status},{priorityLevel},{submitter},{assignee},{string.Join("|", ticket.watching)}");
                sw.Close();
                TicketingSystem.Ticket.Add(ticket);
                logger.Info("TicketID added", ticket.ticketID);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
