using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TicketingSystem
{
    class EnhancementFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string filePath { get; set; }
        public List<EnhancementTicket> EnhancedTicket { get; set; }

        public EnhancementFile(string path)
        {
            EnhancedTicket = new List<EnhancementTicket>();
            filePath = path;

            try
            {
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    EnhancementTicket ticket = new EnhancementTicket();
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
                        ticket.software = ticketDetails[7];
                        ticket.ticketCost = double.Parse(ticketDetails[8]);
                        ticket.reason = ticketDetails[9];
                        ticket.ticketEstimate = ticketDetails[10];
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
                        line = line.Substring(idx + 7);
                        ticket.software = line.Substring(0, idx);
                        line = line.Substring(idx + 8);
                        ticket.ticketCost = double.Parse(line.Substring(0, idx));
                        line = line.Substring(idx + 9);
                        ticket.reason = line.Substring(0, idx);
                        line = line.Substring(0, idx);
                        ticket.ticketEstimate = line.Substring(0, idx);

                    }
                    EnhancedTicket.Add(ticket);
                }
                sr.Close();
                logger.Info("Tickets in file {Count}", EnhancedTicket.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
        public void AddTicket(EnhancementTicket ticket)
        {
            try
            {
                ticket.ticketID = EnhancedTicket.Max(m => m.ticketID) + 1;
                string summary = ticket.summary;

                string status = ticket.status;

                string priorityLevel = ticket.priorityLevel;

                string submitter = ticket.submitter;

                string assignee = ticket.assignee;

                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{ticket.ticketID},{summary},{status},{priorityLevel},{submitter},{assignee},{string.Join("|", ticket.watching)}," +
                    $"{ticket.software}, {ticket.ticketCost}, {ticket.reason}, {ticket.ticketEstimate}");
                sw.Close();
                EnhancedTicket.Add(ticket);
                logger.Info("Ticket id {Id} added", ticket.ticketID);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
