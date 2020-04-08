﻿using System;
using System.Collections.Generic;
using System.Text;
using Client.Models;
using Client.DataModels;

namespace Client
{
    public static class Globals
    {
        /// <summary>
        /// Экхепляр для работы с конфигурацией приложения
        /// </summary>
        public static IConfigManager Config { get; set; }
        /// <summary>
        /// Информация о работнике
        /// </summary>
        public static Worker WorkerInfo { get; set; }
        /// <summary>
        /// Активный статус работника
        /// </summary>
        public static StatusCode WorkerStatus { get; set; }
        /// <summary>
        /// {Название статуса - Статус}
        /// </summary>
        public static Dictionary<string, Status> Statuses { get; set; }
        /// <summary>
        /// {Код статуса - Статус}
        /// </summary>
        public static Dictionary<string, Status> StatusCodes { get; set; }
        /// <summary>
        /// Очистка всех данных
        /// </summary>
        public static void Clear()
        {
            StatusCodes = null;
            Statuses = null;
            WorkerInfo = null;
            WorkerStatus = null;
            Config.SetItem("Session", "");
        }
    }
}
