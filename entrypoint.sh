
set -e

/opt/mssql/bin/sqlservr &


sleep 30s


/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -i /usr/src/app/init.sql

# Keep the container running
tail -f /dev/null
