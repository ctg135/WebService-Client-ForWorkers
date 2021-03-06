﻿using Client.DataModels;
using Client.Models;
using Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Droid.Models
{
    public class MockClient : IClientModel
    {
        public string Session { get; set; }
        public string Server { get; set; }

        public Task<AuthorizationResult> Authorization(string Login, string Password)
        {
            System.Diagnostics.Debug.WriteLine($"Паролимся {Login}, {Password}");
            if (Login == Password) return Task.FromResult(AuthorizationResult.Ok);
            else return Task.FromResult(AuthorizationResult.Error);
        }

        public Task<AuthorizationResult> Authorization()
        {
            System.Diagnostics.Debug.WriteLine($"Неработящая авторизация");
            return Task.FromResult(AuthorizationResult.Error);
        }

        public Task<AuthorizationResult> CheckConnect()
        {
            System.Diagnostics.Debug.WriteLine($"Проверка подключения");
            return Task.FromResult(AuthorizationResult.Error);
        }

        public Task<StatusCode> GetLastStatusCode()
        {
            System.Diagnostics.Debug.WriteLine($"Получаем последний статус");
            return Task.FromResult(new StatusCode() { Code = "2", LastUpdate = DateTime.Now.ToString() });
        }

        public Task<List<Plan>> GetPlans(DateTime Start, DateTime End, PlanTypes[] Filter)
        {
            System.Diagnostics.Debug.WriteLine($"Выводим планы {Start} - {End}, {JsonConvert.SerializeObject(Filter)}");

            List<Plan> plans = new List<Plan>();
            List<Plan> templateplans = new List<Plan>()
            {
                new Plan()
                {
                    TypePlan = "1",
                    DateSet = Start.ToString("dd.MM.yyyy"),
                    StartDay = "8:30",
                    EndDay = "10:30"
                },
                new Plan()
                {
                    TypePlan = "2",
                    DateSet = Start.AddDays(1).ToString("dd.MM.yyyy")
                },
                new Plan()
                {
                    TypePlan = "3",
                    DateSet = Start.AddDays(2).ToString("dd.MM.yyyy")
                },
                new Plan()
                {
                    TypePlan = "4",
                    DateSet = Start.AddDays(3).ToString("dd.MM.yyyy")
                }
            };

            if (Filter.Length == 0)
            {
                plans = templateplans;
            }
            else 
            {
                foreach(var filter in Filter)
                {
                    if (filter == PlanTypes.DayOff)
                    {
                        plans.Add(templateplans[1]);
                    }
                    else if (filter == PlanTypes.Holiday)
                    {
                        plans.Add(templateplans[3]);
                    }
                    else if (filter == PlanTypes.Hospital)
                    {
                        plans.Add(templateplans[2]);
                    }
                    else if (filter == PlanTypes.Working)
                    {
                        plans.Add(templateplans[0]);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Неизвестная дрянь '{filter}'");
                    }
                }
            }
            return Task.FromResult(plans);
        }

        public Task<List<Status>> GetStatuses()
        {
            System.Diagnostics.Debug.WriteLine($"Получаем типы статусов");
            List<Status> statuses = new List<Status>() 
            {
                new Status()
                {
                    Code = "1",
                    Title = "Не установлен"
                },
                new Status()
                {
                    Code = "2",
                    Title = "На работе"
                },
                new Status()
                {
                    Code = "3",
                    Title = "На перерыве"
                },
                new Status()
                {
                    Code = "4",
                    Title = "В отпуске"
                },
                new Status()
                {
                    Code = "5",
                    Title = "Рабочий день закончен"
                },
                new Status()
                {
                    Code = "6",
                    Title = "На больничном"
                },
                new Status()
                {
                    Code = "7",
                    Title = "На выходном"
                },
            };

            return Task.FromResult(statuses);
        }

        public Task<Plan> GetTodayPlan()
        {
            System.Diagnostics.Debug.WriteLine($"План на сегодня");
            return Task.FromResult(new Plan() { DateSet = DateTime.Now.ToString("dd.MM.yyyy"), StartDay = "8:00", EndDay = "16:00", TypePlan = "1" });
        }

        public Task<List<PlanType>> GetPlanTypes()
        {
            System.Diagnostics.Debug.WriteLine($"Типы планов");

            List<PlanType> planTypes = new List<PlanType>() 
            {
                new PlanType()
                {
                    Code = "1",
                    Title = "Рабочий день"
                },
                new PlanType()
                {
                    Code = "2",
                    Title = "Выходной"
                },
                new PlanType()
                {
                    Code = "3",
                    Title = "Больничный"
                },
                new PlanType()
                {
                    Code = "4",
                    Title = "Отпуск"
                }
            };

            return Task.FromResult(planTypes);
        }

        public Task<Worker> GetWorkerInfo()
        {
            System.Diagnostics.Debug.WriteLine($"Инфо о работнике");

            return Task.FromResult(new Worker() { Name = "Имя", Patronymic = "Отчество", Surname = "Фамилия",  Department ="Программистический", Position = "Программист" });
        }

        public Task<bool> IsSetStatusClientError(string ErrorMessage)
        {
            System.Diagnostics.Debug.WriteLine($"Ошибка ли в установке статуса?");

            throw new NotImplementedException();
        }

        public Task SetStatus(string Code)
        {
            System.Diagnostics.Debug.WriteLine($"Установка нового статуса ${Code}");

            throw new NotImplementedException();
        }

        public Task<List<Tasks.Task>> GetTasks(DateTime StartDate, TaskStages[] Filter)
        {
            System.Diagnostics.Debug.WriteLine($"Получение задач по {Filter} ПОКА БЕЗ ДАТЫ!1!");

            var tasks = new List<Tasks.Task>();
            if(Filter.Length == 0)
            {
                tasks.Add(new Tasks.Task()
                {
                    Id = "1",
                    Stage = "1",
                    Boss = "Сам я",
                    DateSetted = DateTime.Now.ToString("dd.MM.yyyy"),
                    Description = "Копать картоху"
                });
                tasks.Add(new Tasks.Task()
                {
                    Id = "2",
                    Stage = "2",
                    Boss = "Сам я",
                    DateSetted = DateTime.Now.ToString("dd.MM.yyyy"),
                    Description = "Деплом"
                });
                tasks.Add(new Tasks.Task()
                {
                    Id = "3",
                    Stage = "3",
                    Boss = "Сам я",
                    DateSetted = DateTime.Now.ToString("dd.MM.yyyy"),
                    DateFinished = DateTime.Now.ToString("dd.MM.yyyy"),
                    Description = "Курсач"
                });
            }

            foreach (var stage in Filter )
            {
                if (stage == TaskStages.NotAccepted)
                {
                    tasks.Add(new Tasks.Task()
                    {
                        Id = "1",
                        Stage = "1",
                        Boss = "Сам я",
                        DateSetted = DateTime.Now.ToString("dd.MM.yyyy"),
                        Description = "Копать картоху"
                    });
                }
                if (stage == TaskStages.Processing)
                {
                    tasks.Add(new Tasks.Task()
                    {
                        Id = "2",
                        Stage = "2",
                        Boss = "Сам я",
                        DateSetted = DateTime.Now.ToString("dd.MM.yyyy"),
                        Description = "Деплом"
                    });
                    tasks.Add(new Tasks.Task()
                    {
                        Id = "6",
                        Stage = "2",
                        Boss = "Сам я",
                        DateSetted = DateTime.Now.ToString("dd.MM.yyyy"),
                        Description = "Пипец какой длинный текст тута Пипец какой длинный текст тута\n Пипец какой длинный текст тута"
                    });
                    tasks.Add(new Tasks.Task()
                    {
                        Id = "7",
                        Stage = "2",
                        Boss = "Сам я",
                        DateSetted = DateTime.Now.ToString("dd.MM.yyyy"),
                        Description = "Текст короче "
                    });
                    tasks.Add(new Tasks.Task()
                    {
                        Id = "2",
                        Stage = "2",
                        Boss = "Сам я",
                        DateSetted = DateTime.Now.ToString("dd.MM.yyyy"),
                        Description = "Кусь"
                    });
                }
                if (stage == TaskStages.Completed)
                {
                    tasks.Add(new Tasks.Task()
                    {
                        Id = "3",
                        Stage = "3",
                        Boss = "Сам я",
                        DateSetted = DateTime.Now.ToString("dd.MM.yyyy"),
                        DateFinished = DateTime.Now.ToString("dd.MM.yyyy"),
                        Description = "Курсач"
                    });
                }
            }

            return Task.FromResult(tasks);
        }

        public Task<List<TaskStage>> GetTaskStages()
        {
            System.Diagnostics.Debug.WriteLine($"Список готовых стадий");

            return Task.FromResult(new List<TaskStage>() 
            {  
                new TaskStage()
                {
                    Code = "1",
                    Title = "Ожидает принятия"
                },
                new TaskStage()
                {
                    Code = "2",
                    Title = "Принято к выполнению"
                },
                new TaskStage()
                {
                    Code = "3",
                    Title = "Выполнено"
                },
            });
        }

        public Task AcceptTask(string TaskId)
        {
            System.Diagnostics.Debug.WriteLine($"Задача №{TaskId} принята");
            return Task.CompletedTask;
        }

        public Task CompleteTask(string TaskId)
        {
            System.Diagnostics.Debug.WriteLine($"Задача №{TaskId} завершена");
            return Task.CompletedTask;
        }
    }
}