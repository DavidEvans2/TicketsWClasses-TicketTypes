using System;
using System.IO;
using TicketingSystem;

namespace TicketAssignment
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            string file = "tickets.csv";
            string enhancefile = "nhancement.csv";
            string taskfile = "task.csv";
            logger.Info("Prgram Started");


            ticketFile ticketfile = new ticketFile(file);
            EnhancementFile enhancement = new EnhancementFile(enhancefile);
            TaskFile task = new TaskFile(taskfile);

            string choice;
            do
            {
                //ask question
                Console.WriteLine("1) Make a Bug Ticket file.");
                Console.WriteLine("2) Read Bug Ticket data.");
                Console.WriteLine("3) Make an Enhanced Ticket File.");
                Console.WriteLine("4) Read Enhanced Ticket data.");
                Console.WriteLine("5) Make a Task Ticket file.");
                Console.WriteLine("6) Read Task Ticket Data.");
                Console.WriteLine("Press the any key to exit");

                choice = Console.ReadLine();
                logger.Info("User choice: ", choice);

                if (choice == "1")
                {
                    Ticket ticket = new Ticket();
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine("Wanna enter a ticket (Y/N)?");
                        string reply = Console.ReadLine();
                        logger.Info("User Reply: ", reply);
                        if (reply != "Y") { break; }

                        Console.WriteLine("Please enter the TicketID: ");
                        ticket.ticketID = Console.ReadLine();

                        Console.WriteLine("Please enter the Ticket Summary: ");
                        ticket.summary = Console.ReadLine();

                        Console.WriteLine("Please enter the Ticket Status");
                        ticket.status = Console.ReadLine();

                        Console.WriteLine("What is the Level of Priority?");
                        ticket.priorityLevel = Console.ReadLine();

                        Console.WriteLine("Who was assigned to this ticket?");
                        ticket.assignee = Console.ReadLine();

                        Console.WriteLine("Who submitted the ticket?");
                        ticket.submitter = Console.ReadLine();
                        string input;

                        do
                        {
                            Console.WriteLine("Whos is watching? (Enter 'exit' to quit program): ");
                            input = Console.ReadLine();
                            if (input != "exit" && input.Length > 0)
                            {
                                ticket.watching.Add(input);
                            }

                        } while (input != "exit");
                        if (ticket.watching.Count == 0)
                        {
                            ticket.watching.Add("(Nobody's watching the ticket!)");
                        }
                        ticketfile.AddTicket(ticket);
                    }
                }
                else if (choice == "2")
                {
                    foreach (Ticket ti in ticketfile.Ticket)
                    {
                        Console.WriteLine(ti.Display());
                    }
                }
                else if (choice == "3")
                {
                    EnhancementTicket enhancementTicket = new EnhancementTicket();
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine("Wanna enter a ticket (Y/N)?");
                        string reply = Console.ReadLine().ToUpper();
                        logger.Info("User Reply: ", reply);
                        if (reply != "Y") { break; }

                        Console.WriteLine("Please enter the TicketID");
                        enhancementTicket.ticketID = Console.ReadLine();

                        Console.WriteLine("Please enter the Ticket Summary: ");
                        enhancementTicket.summary = Console.ReadLine();

                        Console.WriteLine("Please enter the Ticket Status");
                        enhancementTicket.status = Console.ReadLine();

                        Console.WriteLine("What is the Level of Priority?");
                        enhancementTicket.priorityLevel = Console.ReadLine();

                        Console.WriteLine("Who was assigned to this ticket?");
                        enhancementTicket.assignee = Console.ReadLine();

                        Console.WriteLine("Who submitted the ticket?");
                        enhancementTicket.submitter = Console.ReadLine();

                        Console.WriteLine("What's the reason for the ticket?");
                        enhancementTicket.reason = Console.ReadLine();

                        Console.WriteLine("What kind of software for the ticket?");
                        enhancementTicket.software = Console.ReadLine();

                        Console.WriteLine("How much does the ticket cost?");
                        enhancementTicket.ticketCost = double.Parse(Console.ReadLine());

                        Console.WriteLine("What the ticket estimate?");
                        enhancementTicket.ticketEstimate = Console.ReadLine();
                        string input;

                        do
                        {
                            Console.WriteLine("Who's watching? (Enter 'exit' to quit the program): ");
                            input = Console.ReadLine();
                            if (input != "exit" && input.Length > 0)
                            {
                                enhancementTicket.watching.Add(input);
                            }
                        } while (input != "done");
                        if (enhancementTicket.watching.Count == 0)
                        {
                            enhancementTicket.watching.Add("Nobody's watchin the ticket!");
                        }
                        enhancement.AddTicket(enhancementTicket);
                    }
                }

                else if (choice == "4")
                {
                    foreach (EnhancementTicket et in enhancement.EnhancedTicket)
                    {
                        Console.WriteLine(et.Display());
                    }
                }

                else if (choice == "5")
                {
                    TaskTicketing taskTickets = new TaskTicketing();
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine("Wanna enter a ticket (Y/N)?: ");
                        string reply = Console.ReadLine().ToUpper();
                        logger.Info("User reply: ", reply);
                        if (reply != "Y") { break; }

                        Console.WriteLine("Please enter the TicketID: ");
                        taskTickets.ticketID = Console.ReadLine();

                        Console.WriteLine("Please enter the Ticket Summary");
                        taskTickets.summary = Console.ReadLine();

                        Console.WriteLine("What's the Ticket Status?");
                        taskTickets.status = Console.ReadLine();

                        Console.WriteLine("What's the Level of Priority?: ");
                        taskTickets.priorityLevel = Console.ReadLine();

                        Console.WriteLine("Who's assigned to this ticket?: ");
                        taskTickets.assignee = Console.ReadLine();

                        Console.WriteLine("Who submitted the ticket?: ");
                        taskTickets.submitter = Console.ReadLine();

                        Console.WriteLine("What's the due date for this task?");
                        taskTickets.dateDue = Console.ReadLine();

                        Console.WriteLine("What is the name of the project?");
                        taskTickets.projectName = Console.ReadLine();
                        string input;

                        do
                        {
                            Console.WriteLine("Who's watching? ( Enter 'exit' to quit the program): ");
                            input = Console.ReadLine();
                            if (input != "exit" && input.Length > 0)
                            {
                                taskTickets.watching.Add(input);
                            }
                        } while (input != "exit");
                        if (taskTickets.watching.Count == 0)
                        {
                            taskTickets.watching.Add("Nobody's watching the ticket!");
                        }
                        task.AddTicket(taskTickets);
                    }
                }
                else if (choice == "6")
                {
                    foreach (TaskTicketing tt in task.TaskTicketing)
                    {
                        Console.WriteLine(tt.Display());
                    }
                }
            } while (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6");
            logger.Info("End of Program");
        }
    }
}