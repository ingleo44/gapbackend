Instrucciones para la implementacion:

- Debe tener una base de datos sql server
- modificar el archivo gapbackend/medAppointments/appsettings.json en la llave 
 
 ```
  "ConnectionStrings": {
    "DefaultConnection": "Server=dbserver;Database=medAppointmentsDB;User Id=appuser;Password=@Wearegap1;"
```
 cambiar el valor del default connection por el valor de la cadena de conexion de su base de datos

- Para publicar las migraciones en su base de datos desde el command prompt debe navegar hacia la carpeta dal del proyecto, en la carpeta dal ejecutar el siguiente comando:

 ```
 dotnet ef database update --startup-project ..\medAppointments\medAppointments.csproj

```

Ya teniendo lista la base de datos podra ejecutar el API al ejecutar el proyecto aparecera una ventana de swagger con todos los metodos disponibles en la API

para autenticarse debe consumir el metodo login de la seccion authentication, el usuario y password por defecto es 
username : prueba
password: password

al autenticarse correctamente usted recibira el token que le permitira usar las api
