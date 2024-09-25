# 🌟 Proyecto CRUD con Web API en ASP.NET y Entity Framework 🌟

¡Bienvenido al proyecto CRUD desarrollado con C#, Web API en ASP.NET y Entity Framework! Este proyecto utiliza SQL Server como base de datos y sigue las mejores prácticas de desarrollo para ofrecer una arquitectura limpia y mantenible.

## 📋 Descripción del Proyecto

Este proyecto tiene como objetivo implementar una API CRUD que permite realizar operaciones básicas (Crear, Leer, Actualizar, Eliminar) sobre una entidad de ejemplo, utilizando:

- **ASP.NET Web API**: Para exponer y consumir servicios HTTP.
- **Entity Framework Core**: Para interactuar con la base de datos de una manera eficiente y fácil de mantener.
- **SQL Server**: Como base de datos para almacenar la información.

  ## 🌐 Endpoints de la Web API
  ### ProfesorController 🌐
- **POST** `/api/autenticacion`: Abre sesion dando paso al dashborad.
  
  ### AlumnoController 🌐
- **GET** `/api/alumnoProfesor`: Obtiene los alumnos del profesor logueado.
- **GET** `/api/datosAlumno`: Obtiene los datos del alumno seleccionado.
- **PUT** `/api/actualizarAlumno`: Actualiza los datos del alumno seleccionado.
- **POST** `/api/insertarYmatricular`: Inserta un alumno y agrega una matricula.
- **DELETE** `/api/eliminarAlumno`: Elimina al alumno seleccionado.

 ### CalificacionesController 🌐
 - **GET** `/api/getCalificaciones`: Obtiene las calificaciones de un alumno
- **POST** `/api/postCalificacion`: Inserta una nueva calificacion.
- **DELETE** `/api/deleteCalificacion`: Elimina una calificacion.

## 🗄️ Creando la Base de Datos en SQL Server
### 1️⃣ Creación de la Base de Datos

```sql
CREATE DATABASE UdemySchool;
GO
```

### 2️⃣ script para crear las entidades

```sql
CREATE TABLE alumno (
  id int IDENTITY(1,1) primary key,
  dni varchar(8) NOT NULL,
  nombre varchar(255) NOT NULL,
  direccion varchar(255) NOT NULL,
  edad int NOT NULL,
  email varchar(100) NOT NULL
);

CREATE TABLE profesor (
	usuario varchar(255) primary key,
	pass varchar(255) NOT NULL,
	nombre varchar(255) NOT NULL,
	email varchar(255) NOT NULL
);

CREATE TABLE asignatura (
  id int IDENTITY(1,1) primary key,
  nombre varchar(255) NOT NULL,
  creditos int DEFAULT 0 NOT NULL,
  profesor varchar(255),
  FOREIGN KEY (profesor) REFERENCES profesor(usuario)
);


CREATE TABLE matricula (
  id int IDENTITY(1,1) primary key,
  alumnoId int NOT NULL,
  asignaturaId int NOT NULL,
  FOREIGN KEY (alumnoId) REFERENCES alumno(id),
  FOREIGN KEY (asignaturaId) REFERENCES asignatura(id)
);

CREATE TABLE calificacion (
  id int IDENTITY(1,1) primary key,
  descripcion varchar(255) NOT NULL,
  nota REAL NOT NULL,
  porcentaje int NOT NULL,
  matriculaId int NOT NULL,
  FOREIGN KEY (matriculaId) REFERENCES matricula(id)
) ;
```
### 3️⃣ Script para inseccion de registros
```sql
/*Tabla alumno*/
INSERT INTO [dbo].[alumno] ([dni],[nombre],[direccion],[edad],[email]) VALUES('4570012T','Miguel Alba Muñoz','C/Catadores 6',21,'miguel@gmail.com');
INSERT INTO [dbo].[alumno] ([dni],[nombre],[direccion],[edad],[email]) VALUES('5540004H','Jesus Rosado Pérez','C/Espronceda 57',21,'jesus@gmail.com');
INSERT INTO [dbo].[alumno] ([dni],[nombre],[direccion],[edad],[email]) VALUES('3714623B','Ana Martínez Segura','C/Ave 1',19,'ana@gmail.com');
INSERT INTO [dbo].[alumno] ([dni],[nombre],[direccion],[edad],[email]) VALUES('8172446X','Naiara Gómez Lucero','C/Rafael Alberti 5',20,'naiara@hotmail.com');
INSERT INTO [dbo].[alumno] ([dni],[nombre],[direccion],[edad],[email]) VALUES('6623895J','Pedro Giraldo Sánchez','C/Cerro Águila 122',21,'pedro@gmail.com');
INSERT INTO [dbo].[alumno] ([dni],[nombre],[direccion],[edad],[email]) VALUES('1465252W','María Pérez López','C/Cielo S/N',18,'maria@gmail.com');
INSERT INTO [dbo].[alumno] ([dni],[nombre],[direccion],[edad],[email]) VALUES('1012618H','Luis Rodríguez Lances','C/Ceuta 41',19,'luis@gmail.com');
INSERT INTO [dbo].[alumno] ([dni],[nombre],[direccion],[edad],[email]) VALUES('4142276Q','Rocío Ruiz Ruiz','C/Benítez 3',20,'rocio@gmail.com');
INSERT INTO [dbo].[alumno] ([dni],[nombre],[direccion],[edad],[email]) VALUES('3116151Y','Diego Manzorro Rodríguez','C/Isla 5',20,'diego@gmail.com');
INSERT INTO [dbo].[alumno] ([dni],[nombre],[direccion],[edad],[email]) VALUES('3970711M','Antonio Cobelo Sánchez','C/Muniera 7',21,'antonio@gmail.com');

/*Tabla profesores*/
INSERT INTO [dbo].[profesor]([usuario],[pass],[nombre],[email]) VALUES ('rocio','1234','Rocio Sánchez Jiménez','rocio@gmail.com');
INSERT INTO [dbo].[profesor]([usuario],[pass],[nombre],[email]) VALUES ('julio','1234','Julio Cerro Garcés','julio@gmail.com');
INSERT INTO [dbo].[profesor]([usuario],[pass],[nombre],[email]) VALUES ('ivan','1234','Ivan Martínez Recio','ivan@gmail.com');

/*Tabla asignaturas*/
INSERT INTO [dbo].[asignatura]([nombre],[creditos],[profesor]) VALUES('Matemáticas',6,'rocio');
INSERT INTO [dbo].[asignatura]([nombre],[creditos],[profesor]) VALUES('Informática',4,'rocio');
INSERT INTO [dbo].[asignatura]([nombre],[creditos],[profesor]) VALUES('Inglés',5,'julio');
INSERT INTO [dbo].[asignatura]([nombre],[creditos],[profesor]) VALUES('Lengua',6,'ivan');

/*Tabla matricula*/
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(1,1);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(1,2);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(1,3);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(2,2);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(3,3);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(3,4);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(4,1);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(5,2);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(5,3);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(6,1);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(7,4);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(8,3);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(8,4);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(9,2);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(9,3);
INSERT INTO [dbo].[matricula]([alumnoId],[asignaturaId]) VALUES(10,4);
```
## 🛠️ Tecnologías y Herramientas Utilizadas

- **C# / .NET Core**
- **Entity Framework Core**
- **SQL Server**
- **ASP.NET** (para la desarrollo de la API)
- **Postam** (para la documentacion de la API)

## 🚀 Instrucciones de Configuración

- Clona este repositorio.
- Configura la cadena de conexión en el archivo de configuración de la API.
- Ejecuta el script SQL para crear la base de datos y las tablas.
- Compila y ejecuta la API.

## 🤝 Contribuciones

¡Las contribuciones son bienvenidas! Si deseas mejorar este proyecto, sigue estos pasos:

- Haz un fork del repositorio.
- Crea una nueva rama (`git checkout -b feature/nuevaCaracteristica`).
- Realiza tus cambios.
- Haz un commit de tus cambios (`git commit -am 'Añadir nueva característica'`).
- Empuja la rama (`git push origin feature/nuevaCaracteristica`).
- Abre un Pull Request.

## 🌐 Repositorio del Front-End

Para ver la interfaz de usuario del proyecto, visita el siguiente repositorio de GitHub:

- [Repositorio Front-End](https://github.com/JonaDevnet/ReactJs-Crud-Web)


## 📝 Licencia

Este proyecto está bajo la Licencia MIT - mira el archivo [LICENSE](./LICENSE) para más detalles.


  
  
