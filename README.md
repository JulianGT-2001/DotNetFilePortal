# 📁 Dotnet File Portal

**Dotnet File Portal** es un sistema de **gestión documental empresarial** construido con **.NET 8**, **PostgreSQL**, **Clean Architecture** y completamente contenerizado con **Docker**.

Permite **subir, almacenar, consultar, descargar y eliminar archivos**, guardando metadatos en una base de datos PostgreSQL y gestionando los archivos de forma segura en el servidor.

---

## ✅ **Características principales**

- 📂 Subida de múltiples archivos (`IFormFile`).
- 🔒 Almacenamiento físico local (soportable para migrar a S3).
- 📄 Metadatos persistidos en PostgreSQL.
- 🔗 API REST construida con **ASP.NET Core**.
- 🧱 Patrón **Clean Architecture** (Core, Infrastructure, WebApi).
- 🐳 Contenerización con **Docker** y orquestación con **docker-compose**.
- 🔀 Versionado estructurado con **Git** (`main`, `Core`, `Infrastructure`, `WebApi`, `Docker`).

---

## 🗂️ **Estructura del proyecto**

```
/FileUpload/
 ├── Core/           # Dominio y casos de uso
 ├── Infrastructure/ # Repositorios y DbContext
 ├── WebApi/         # Controladores, Program.cs
 ├── FileUpload.sln  # Solución principal
 ├── docker-compose.yml
 ├── README.md
```

---

## ⚙️ **Tecnologías**

- .NET 8 (ASP.NET Core)
- PostgreSQL 14
- Entity Framework Core
- Docker & Docker Compose
- Clean Architecture

---

## 🚀 **Cómo levantar el proyecto**

### 1️⃣ Clona el repositorio

```bash
git clone https://github.com/tuusuario/dotnet-file-portal.git
cd dotnet-file-portal
```

---

### 2️⃣ Configura variables de entorno

Crea un archivo `.env` en la raíz y define:

```env
CONNECTION_STRING=Host=postgres_fileupload_db;Database=YOUR_DB_NAME;Username=YOUR_DB_USER;Password=YOUR_DB_PASSWORD
DB_USER=YOUR_DB_USER
DB_PASSWORD=YOUR_DB_PASSWORD
DB_NAME=YOUR_DB_NAME
```

---

### 3️⃣ Levanta los contenedores

```bash
docker-compose up --build
```

- 📦 `dotnetfp`: API .NET expuesta en `http://localhost:5000`
- 🐘 `fileuploaddb`: PostgreSQL escuchando en `5432`

---

### 4️⃣ Prueba la API

Usa **Postman**, **curl** o tu frontend React/HTML:

- **Subir archivo:**

  ```bash
  curl -X POST http://localhost:5000/api/files/upload        -H "Content-Type: multipart/form-data"        -F "files=@path/to/your/file.pdf"
  ```

- **Listar archivos:**

  ```bash
  curl http://localhost:5000/api/files
  ```

- **Descargar archivo:**

  ```bash
  curl -X GET http://localhost:5000/api/files/{id}/download --output archivo.pdf
  ```

---

## 🗄️ **Base de datos**

- Usamos PostgreSQL con persistencia de metadatos.
- Los archivos se almacenan en `/app/files` dentro del contenedor `dotnetfp` mediante volumen `dotnetfp_files`.

---

## 🧩 **Arquitectura**

**Dotnet File Portal** implementa **Clean Architecture**:

| Capa | Descripción |
|------|--------------|
| **Core** | Entidades (`FileDocument`), Interfaces (`IFileRepository`), Casos de uso. |
| **Infrastructure** | Implementación de repositorios (`FileRepository`), `MyDbContext`. |
| **WebApi** | Controladores (`FilesController`), Program.cs, configuración DI. |

---

## 🔐 **Seguridad**

- Soporta extracción de `userId` desde Claims.
- Se puede extender con JWT + Identity.

---

## 📌 **Ramas Git**

- `main` → Rama principal estable.
- `Core` → Dominio y lógica de aplicación.
- `Infrastructure` → Persistencia y almacenamiento.
- `WebApi` → API REST.
- `Docker` → Infraestructura de contenedores.

---

## 📝 **Licencia**

MIT — Puedes usar este proyecto como base para fines educativos y empresariales.

---

## 🙌 **Autor**

**JulianGT-2001**  
📧 juliant.2001@outlook.com  
🔗 [LinkedIn](https://www.linkedin.com/in/julian-dario-gonzalez-toledo-402482223/)
