FROM mcr.microsoft.com/mssql/server:2022-latest

USER root

RUN mkdir /usr/config

COPY ["GLeadGenerator.Database/entrypoint.sh", "/usr/config/entrypoint.sh"]
COPY ["GLeadGenerator.Database/mssql-customize.sh", "/usr/config/mssql-customize.sh"]
COPY ["GLeadGenerator.Database/setup.sql", "/usr/config/setup.sql"]

WORKDIR /usr/config

RUN chmod +x entrypoint.sh
RUN chmod +x mssql-customize.sh

ENTRYPOINT ["./entrypoint.sh"]