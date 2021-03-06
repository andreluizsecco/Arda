﻿using Arda.Kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Arda.Kanban.Models.Repositories;
using Arda.Kanban.ViewModels;

namespace Arda.Kanban.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        KanbanContext _context;

        public AppointmentRepository(KanbanContext context)
        {
            _context = context;
        }

        public bool AddNewAppointment(AppointmentViewModel appointment)
        {
            // Load Workload object to save.
            var workload = _context.WorkloadBacklogs.FirstOrDefault(wb => wb.WBID == appointment._AppointmentWorkloadWBID);

            // Load UserKanban object to save.
            var user = _context.Users.FirstOrDefault(u => u.UniqueName == appointment._AppointmentUserUniqueName);

            var appointmentToBeSaved = new Appointment()
            {
                AppointmentID = appointment._AppointmentID,
                AppointmentWorkload = workload,
                AppointmentDate = appointment._AppointmentDate,
                AppointmentHoursDispensed = appointment._AppointmentHoursDispensed,
                AppointmentTE = appointment._AppointmentTE,
                AppointmentUser = user,
                AppointmentComment = appointment._AppointmentComment
            };

            _context.Appointments.Add(appointmentToBeSaved);

            var response = _context.SaveChanges();

            // setting the last appointment inside the repository
            SetLastAppointment(appointment._AppointmentWorkloadWBID, appointment._AppointmentID);

            if (response > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void SetLastAppointment(Guid workloadId, Guid appointmentId)
        {
            // Load Workload object to save.
            var workload = _context.WorkloadBacklogs.First(wb => wb.WBID == workloadId);
            workload.LastAppointmentId = appointmentId;

            _context.SaveChanges();            
        }

        public List<AppointmentViewModel> GetAllAppointmentsSimple()
        {
            var response = (from a in _context.Appointments
                            join w in _context.WorkloadBacklogs on a.AppointmentWorkload.WBID equals w.WBID
                            orderby a.AppointmentDate descending
                            select new AppointmentViewModel
                            {
                                _AppointmentID = a.AppointmentID,
                                _AppointmentWorkloadWBID = a.AppointmentWorkload.WBID,
                                _WorkloadTitle = w.WBTitle,
                                _AppointmentDate = a.AppointmentDate,
                                _AppointmentHoursDispensed = a.AppointmentHoursDispensed,
                                _AppointmentUserUniqueName = a.AppointmentUser.UniqueName,
                                // By design : it does not return data
                                _AppointmentComment = null,
                                _AppointmentTE = 0,
                                _AppointmentUserName = ""
                            }).ToList();

            return response;
        }

        public List<AppointmentViewModel> GetAllAppointmentsSimple(string user)
        {
            var response = (from a in _context.Appointments
                            join w in _context.WorkloadBacklogs on a.AppointmentWorkload.WBID equals w.WBID
                            where a.AppointmentUser.UniqueName == user
                            orderby a.AppointmentDate descending
                            select new AppointmentViewModel
                            {
                                _AppointmentID = a.AppointmentID,
                                _AppointmentWorkloadWBID = a.AppointmentWorkload.WBID,
                                _WorkloadTitle = w.WBTitle,
                                _AppointmentDate = a.AppointmentDate,
                                _AppointmentHoursDispensed = a.AppointmentHoursDispensed,
                                _AppointmentUserUniqueName = a.AppointmentUser.UniqueName,
                                // By design : it does not return data
                                _AppointmentComment = null,
                                _AppointmentTE = 0,
                                _AppointmentUserName = ""
                            }).ToList();

            return response;
        }

        public List<AppointmentViewModel> GetAllAppointmentsWorkload(Guid wbid)
        {
            var response = (from a in _context.Appointments
                            where a.AppointmentWorkloadWBID == wbid
                            orderby a.AppointmentDate descending
                            select new AppointmentViewModel
                            {
                                // _AppointmentWorkloadWBID and _WorkloadTitle: does not return the workload title -- it is obvious from wbid
                                _AppointmentID = a.AppointmentID,
                                _AppointmentDate = a.AppointmentDate,
                                _AppointmentUserUniqueName = a.AppointmentUserUniqueName,
                                _AppointmentHoursDispensed = a.AppointmentHoursDispensed,
                                _AppointmentComment = a.AppointmentComment
                            }).ToList();

            return response;
        }

        public AppointmentViewModel GetAppointmentByID(Guid id)
        {
            var appointment = (from a in _context.Appointments
                               join w in _context.WorkloadBacklogs on a.AppointmentWorkload.WBID equals w.WBID
                               join u in _context.Users on a.AppointmentUser.UniqueName equals u.UniqueName
                               where a.AppointmentID == id
                               select new AppointmentViewModel
                               {
                                   _AppointmentID = a.AppointmentID,
                                   _AppointmentUserUniqueName = u.UniqueName,
                                   _AppointmentDate = a.AppointmentDate,
                                   _AppointmentHoursDispensed = a.AppointmentHoursDispensed,
                                   _AppointmentTE = a.AppointmentTE,
                                   _AppointmentWorkloadWBID = w.WBID,
                                   _WorkloadTitle = w.WBTitle,
                                   _AppointmentComment = a.AppointmentComment
                               }).First();

            return appointment;
        }

        public bool DeleteAppointmentByID(Guid id)
        {
            var appointmentToBeDeleted = _context.Appointments.First(a => a.AppointmentID == id);

            if (appointmentToBeDeleted != null)
            {
                var response = _context.Remove(appointmentToBeDeleted);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditAppointment(AppointmentViewModel appointment)
        {
            var appointmentToBeUpdated = (from a in _context.Appointments
                                          where a.AppointmentID == appointment._AppointmentID
                                          select a).First();

            //var user = (from u in _context.Users
            //            where u.UniqueName == appointment._AppointmentUserName
            //            select u).First();

            //var workload = (from wb in _context.WorkloadBacklogs
            //              where wb.WBID == appointment._AppointmentWorkloadWBID
            //              select wb).First();

            if (appointmentToBeUpdated != null)
            {
                // Update informations of object
                appointmentToBeUpdated.AppointmentComment = appointment._AppointmentComment;
                appointmentToBeUpdated.AppointmentDate = appointment._AppointmentDate;
                appointmentToBeUpdated.AppointmentHoursDispensed = appointment._AppointmentHoursDispensed;
                appointmentToBeUpdated.AppointmentTE = appointment._AppointmentTE;
                //appointmentToBeUpdated.AppointmentUser = user;
                //appointmentToBeUpdated.AppointmentWorkload = workload;
                //appointmentToBeUpdated.AppointmentWorkloadWBID = appointment._AppointmentWorkloadWBID;

                var response = _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
