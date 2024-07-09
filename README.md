Blogging Platform API

How to run it:

Step 1) Clone repo:

* https://github.com/diegovita/Platform.git
* gh repo clone diegovita/Platform

Step 2)

Install Docker runtime (Docker Desktop), skip it if you already installed it.

Step 3)

Place yourself via cmd in root directory of cloned repository, where Platform.sln is located at. Execute "ls" command and check if Dockerfile and docker-compose .yml can be seen.
Execute "docker-compose up", notice this command will execute the PRODUCTION environment artifacts (WEB API - SQL SERVER - ELASTICSEARCH - KIBANA), Web API won't execute in Debug mode.
If you wan't to deploy Development environment, place yourself in Development/ and execute "docker-compose up" there (SQL SERVER - ELASTICSEARCH - KIBANA) and execute cloned Web API in visual studio 2022.

Elasticsearch along with Kibana will let you monitorize LOGS in real-time.

Production Environment Urls and Ports:

Web Api:
http://localhost:8082
Swagger:
http://localhost:8082/swagger/index.html
Elasticsearch: 
http://elasticsearch:9201
Kibana: 
http://elasticsearch:5602
SQL Server
localhost:1433
user= sa
password= blogging2024!

Development Environment Urls and Ports:

Elasticsearch: 
http://elasticsearch:9200
Kibana: 
http://elasticsearch:5601
SQL Server
localhost:1434
user= sa
password= blogging2024!


How to check logs:

Go to Kibana UI

http://elasticsearch:5602 (Production) or http://elasticsearch:5601 (Development)


Click on the button at top left corner, below Elastic logo, go all the way down to Stack Management. Then:

Stack Management > Index Management > Copy blogging-logs-2024- to clipboard.

Go top left corner below elasticsearch logo, and then click on Discover > Create DataView > paste blogging-logs-2024- and add * at the end in the index pattern textbox, like this: blogging-logs-2024-*, choose whatever name you like and create. You will be able to check logs on Discover tab.






  
