# WebService-Client-ForWorkers
Программный продукт решает проблему распределения рабочего времени кадров.
Он позволяет просмотреть количество отработанных часов и выполненных работником заданий для отслеживания эффективности его работы.
Также, таким образом можно более точно определить график работы сотрудника, так как каждый сотрудник работает эффективнее в разное время.

Программа даёт возможность давать задания своим подопечным. 
Сами работники при помощи мобильного клиента отмечают о выполнении заданий.

## Состав программного обеспечения

### База данных

Хранит в себе информацию о:
* Работниках
* Рабочих графиках работников
* Заданиях работников
* Списке событий о статусе работника (логи статусов)

Информацию о структуре базы данных можно узнать в [дампе базы данных](https://github.com/ctg135/WebService-Client-ForWorkers/blob/DataBase/База%20данных/workers.sql) 
или по [ветви](https://github.com/ctg135/WebService-Client-ForWorkers/tree/DataBase) `DataBase` этого репозитория.

Разработка базы данных ведется в СУБД `MySQL 5.5` и `phpMyAdmin 3.5.1`

### Программа для администрирования

Используется управляющими работниками. Существует два типа: главный управляющий и управляющий отделом.

Главынй имеет доступ к информации всех работников, а также может выдать задание любому работнику. Может составлять график, а также создавать новых работников.

Управляющий отделом имеет меньший доступ: он умеет также может выдавать задния и просматривать активность, но только в отношении подопечных своего отдела. Состовляет график работы.

Разработка этой части ПО в этом репозитории не отображена :(

### Клиент

Представляет собой мобильное приложение на устройстве работника.

Выполняет роль аутентификатора для работников. При помощи него происходит сбор событий статуса работника (о его деятельности), происходит просмотр рабочего графика, а также выполнения заданий.

Приложение разрабатывается на платформе `Xamarin` `Framework 4.8`. На данном этапе разбиты зависимости, проведены при помощи `Autofac` и `Autofac.CommonServiceLocator`.
На данном этапе разработки готово приложение только для ОС Android и ориентировано на версию ОС `4.4`

### Веб-сервис

Выполняет роль безопасной связи клиента и базы данных.

Представляет собой `Rest API` для построения клиентов.

Для доступа к ресурсам необходима аутентификация при помощи сессии.

Ведет журнал логов для обработанных сообщений логгером `log4net`

Построен на основе `ASP.NET`, а контроллеры на `ApiController`. Развертывается при помощи локального хоста `IIS`