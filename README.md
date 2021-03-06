# FstrApi .NET 6.0
## Проект WebApi для Хакатона [*SkillFactory*](https://skillfactory.ru/) + [*Федерации спортивного туризма России*](https://tssr.ru/).

### Информация:
- Ссылка на сервис (REST API): http://31.132.166.125:33222
- Примеры запросов для API: http://31.132.166.125:33222/swagger/index.html

### Билд проекта:
```sh
dotnet build FstrApi.sln
```

### Деплой на хостинг:
Публичный Docker-образ: https://hub.docker.com/r/doctorikari/fstrapi
Создание Docker-контейнера из образа и запуск:
```sh
docker run --name fstrapi -e FSTR_DB_HOST=192.168.1.3 -e FSTR_DB_PORT=4321 -e FSTR_DB_LOGIN=username -e FSTR_DB_PASS=12345 -d -p 33222:80 doctorikari/fstrapi
```

### Набор методов:
#### MVP1
- POST /submitData	-	Добавление информации о новом маршруте

#### MVP2
- GET /submitData	-	Получение всех маршрутов пользователя
- GET /submitData/{id}	-	Получение маршрута пользователя по id
- GET /submitData/{id}/status	-	Получение статуса маршрута пользователя по id
- PUT /submitData/{id}	-	Изменение данных маршрута