﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Client.DataModels;

namespace Client.Models
{
    /// <summary>
    /// Результаты авторизации
    /// </summary>
    enum AuthorizationResult
    {
        Ok,
        Error
    }
    /// <summary>
    /// Интерфейс для получения данных от сервера
    /// </summary>
    interface IClientModel
    {
        /// <summary>
        /// Установка сессии по паролю и логину
        /// </summary>
        /// <param name="Login">Логин клиента</param>
        /// <param name="Password">Пароль клиента</param>
        /// <returns>Резуьтат аутентфикации</returns>
        Task<AuthorizationResult> Authorization(string Login, string Password);
        /// <summary>
        /// Авторизация по сессии
        /// </summary>
        /// <returns>Резуьтат аутентфикации</returns>
        Task<AuthorizationResult> Authorization();
        /// <summary>
        /// Сессия пользователя
        /// </summary>
        string Session { get; set; }
        /// <summary>
        /// Получение коллекции всех статусов
        /// </summary>
        /// <returns>Колеекция статусов</returns>
        Task<List<Status>>  GetStatuses();
        /// <summary>
        /// Получает информацию о работнике
        /// </summary>
        /// <returns></returns>
        Task<Worker> GetWorkerInfo();
        /// <summary>
        /// Получение информации о последнем установленном статусе
        /// </summary>
        /// <returns></returns>
        Task<StatusCode> GetLastStatusCode();
        /// <summary>
        /// Установка статуса работника
        /// </summary>
        /// <param name="Code">Код статуса</param>
        /// <returns></returns>
        Task SetStatus(string Code);
        /// <summary>
        /// Получение планов сотрудника по датам
        /// </summary>
        /// <param name="Start">Начальная дата</param>
        /// <param name="End">Конечная дата</param>
        /// <returns></returns>
        Task<List<Plan>> GetPlans(DateTime Start, DateTime End);
        /// <summary>
        /// Получение плана на сегодня
        /// </summary>
        /// <returns></returns>
        Task<Plan> GetTodayPlan();
    }
}