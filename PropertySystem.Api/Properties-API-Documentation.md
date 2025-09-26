# API de Propiedades con MongoDB

## Descripción
Esta implementación extiende el sistema existente con funcionalidad de filtrado de propiedades usando MongoDB como base de datos NoSQL, manteniendo la arquitectura Clean Architecture y el patrón CQRS ya establecido.

## Estructura Implementada

### Entidades de Dominio
- **Owner**: Propietario de las propiedades
- **Property**: Propiedades inmobiliarias
- **PropertyImage**: Imágenes de las propiedades
- **PropertyTrace**: Historial de transacciones de las propiedades

### Patrón de Arquitectura
- **Domain Layer**: Entidades y interfaces de repositorio
- **Application Layer**: Queries CQRS con MediatR
- **Infrastructure Layer**: Implementación de repositorios MongoDB
- **API Layer**: Controladores REST

## Endpoints Disponibles

### GET /api/properties/filter
Filtra propiedades basado en parámetros opcionales.

**Parámetros de Query:**
- `name` (opcional): Filtro por nombre de propiedad (búsqueda parcial, case-insensitive)
- `address` (opcional): Filtro por dirección (búsqueda parcial, case-insensitive)
- `minPrice` (opcional): Precio mínimo
- `maxPrice` (opcional): Precio máximo

**Ejemplo de uso:**
```
GET /api/properties/filter?name=casa&minPrice=100000&maxPrice=500000
```

**Respuesta:**
```json
[
  {
    "id": "guid",
    "name": "Casa en el centro",
    "address": "Calle Principal 123",
    "price": 250000,
    "codeInternal": "PROP001",
    "year": 2020,
    "ownerId": "guid",
    "ownerName": "Juan Pérez",
    "ownerAddress": "Calle Secundaria 456",
    "propertyImage": "imagen1.jpg"
  }
]
```

## Configuración

### Connection Strings
Agregar en `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "MongoConnection": "mongodb://localhost:27017"
  }
}
```

### Dependencias
- MongoDB.Driver 2.28.0 (ya agregado al proyecto Infrastructure)

## Características Implementadas

### Filtros Disponibles
1. **Filtro por Nombre**: Búsqueda parcial case-insensitive
2. **Filtro por Dirección**: Búsqueda parcial case-insensitive  
3. **Filtro por Rango de Precio**: Precio mínimo y máximo
4. **Combinación de Filtros**: Todos los filtros pueden combinarse

### DTOs de Respuesta
Cada propiedad retornada incluye:
- Información básica de la propiedad (Id, Name, Address, Price, etc.)
- Información del propietario (IdOwner, OwnerName, OwnerAddress)
- Una sola imagen (la primera disponible)

### Seguridad
- Endpoint protegido con autorización
- Requiere autenticación JWT

## Uso con Docker

MongoDB ya está configurado en el `docker-compose.yml` con:
- **Imagen**: mongo:7.0
- **Usuario**: admin
- **Contraseña**: mongodb123!
- **Base de datos**: PropertySystem
- **Puerto**: 27017
- **Volumen persistente**: ./containers/mongodb

### Comandos para usar:

```bash
# Levantar todos los servicios (incluyendo MongoDB)
docker-compose up -d

# Ver logs de MongoDB
docker-compose logs PropertySystem-mongodb

# Conectar a MongoDB desde línea de comandos
docker exec -it PropertySystem.MongoDB mongosh -u admin -p mongodb123! --authenticationDatabase admin
```

### Connection String configurada:
```json
"MongoConnection": "mongodb://admin:mongodb123!@PropertySystem-mongodb:27017/PropertySystem?authSource=admin"
```
