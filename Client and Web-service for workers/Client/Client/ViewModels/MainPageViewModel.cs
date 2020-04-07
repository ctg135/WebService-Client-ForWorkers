﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Client.DataModels;
using Client.Models;
using Client.Views;

namespace Client.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        private IClientModel Client { get; set; }
        public ICommand Exit { get; private set; }
        public ICommand SetNewStatus { get; private set; }
        public ICommand UpdateData { get; private set; }
        /// <summary>
        /// Статус работника
        /// </summary>
        public string Status
        {
            get
            {
                if (Globals.WorkerStatus.Code == StatusCode.Empty().Code) return "-";
                return Globals.StatusCodes[Globals.WorkerStatus.Code].Title;
            }
        }
        /// <summary>
        /// Обновление даты статуса
        /// </summary>
        public string LastUpdate
        {
            get
            {
                return Globals.WorkerStatus.LastUpdate;
            }
        }
        /// <summary>
        /// График на сегодня
        /// </summary>
        public Plan PlanToday { get; set; }
        public MainPageViewModel()
        {
            Exit = new Command(UnAutho);
            SetNewStatus = new Command(SetStatus);
            UpdateData = new Command(UpdateProps);
            
            Client = new ClientMock(); // ТЯВА
            Client.Session = Globals.Config.GetItem("Session");

            PlanToday = Plan.Empty();
            Globals.WorkerStatus = StatusCode.Empty();
        }
        /// <summary>
        /// Комманда выхода
        /// </summary>
        private async void UnAutho()
        {
            Globals.Clear();
            Application.Current.MainPage = new AuthoPage();
        }
        /// <summary>
        /// Команда установки статуса
        /// </summary>
        private async void SetStatus()
        {
            List<string> buts = new List<string>(Globals.Statuses.Keys);
            string butCancel = "Отмена";
            
            var res = await Application.Current.MainPage.DisplayActionSheet("Выбор статуса", butCancel, null, buts.ToArray());

            if(res == butCancel)
            {
                return;
            }
            try
            {
                await Client.SetStatus(Globals.Statuses[res].Code);
                Globals.WorkerStatus = await Client.GetLastStatusCode();
            }
            catch (Exception exc)
            {
                await FatalError(exc.Message);
                return;
            }
            
            NotifyPropertyChanged(nameof(Status));
            NotifyPropertyChanged(nameof(LastUpdate));
        }
        /// <summary>
        /// Команда обноления планов и статуса на форме
        /// </summary>
        public async void UpdateProps()
        {
            try
            {
                PlanToday = await Client.GetTodayPlan();
                Globals.WorkerStatus = await Client.GetLastStatusCode();
            }
            catch (Exception exc)
            {
                await FatalError(exc.Message);
                return;
            }

            NotifyPropertyChanged(nameof(Status));
            NotifyPropertyChanged(nameof(LastUpdate));
            NotifyPropertyChanged(nameof(PlanToday));
        }
    }
}