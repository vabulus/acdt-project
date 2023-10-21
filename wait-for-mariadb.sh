#!/bin/bash
set -e

host="$1"
shift
cmd="$@"
export PATH="$PATH:/root/.dotnet/tools"

until mariadb -h "$host" -u "incident-user" -p"password" -e 'SELECT 1'; do
  >&2 echo "MariaDB is unavailable - sleeping"
  sleep 1
done

>&2 echo "MariaDB is up - executing EF migrations"

>&2 echo "Executing command"

echo "Executing command $cmd"
exec $cmd
