# motion-database

## App Settings

See appsettings.json for configuration options. Please leave this as default.

Use the (.gitignored) appsettings.Development.json/Production.json for your configuration, this way keys are not committed. 

## Solution Overview

.NET Core backend with JWT authentication and a MySQL database. Provides a RESTful API.

Vue.js frontend consumes RESTful API.

## Configuration for development

In the backend folder, copy the appsettings and create a appsettings.Development.json (will be .gitignored) with your local MySQL configuration.

In the frontend folder, copy the .env.production file and create a .env.development.local with the location of the API endpoint that you're running.