# N5 Challenge

### Instrucciones para dar de alta la app localmente:

Descargar el proyecto desde github y ubicarse en la raiz del proyecto en donde podran encontrar el archivo **docker-compose.yml** y abrimos una terminal cmd.

**ATENCION**
Para ejecutar el siguiente paso necesitamos que docket este corriendo localmente.

Una vez abierta la terminal y con docker ejecutandose, escribimos en la terminal el siguiente comando:

    docker-compose up -d

Una vez ejecutado tendremos distintos servicios ejecutandose localmente en los cuales podremos encontrar:

- api -> la api principal que utilizaremos para realizar nuestras peticiones http
- sqlserver -> el motor de base de datos donde guardaremos nuestra informacion
- elasticsearch -> un motor de busqueda donde se registrara la informacion de cada peticion
- kibana -> nos dara la opcion de visualizar los datos de elastic search desde un portal visual
- kafka -> aqui registraremos cada peticion para que quede un registro de la misma que pueda ser procesado desde otro entorno
- kafka-ui -> aqui podremos visualizar las peticiones mencionadas anteriormente.
- zookeeper -> trabaja en conjunto con kafka para resolver cuestiones internas entre estos servicios.

Ejecutando el siguiente comando veremos los servicios en consola con sus correspondientes puertos.

    docker ps

---

Una vez ejecurado y viendo que todo corre correctamente necesitaremos abrir nuestro **Administrador de Base de Datos** (SSMS o cualquier otro) y abrir el archivo DataBase.sql (los datos de accesos a la DB se encuentran en el archivo accesos.md)

Ejecutarlo y ya tendremos nuestra base de datos generada con algunos datos cargados.

Ingresamos a http://localhost/swagger/index.html y veremos nuestra api corriendo correctamente.

Seleccionamos el primer endpoint y lo ejecutamos, deberiamos conseguir una respuesta vacia ya que aun no tenemos permisos solicitados, **vamos a generar algunos!**

Para realizar una peticion de un permiso, necesitaremos buscar primero en nuestro motor de Base de Datos algo de informacion ejecutando la siguiente consulta.

    SELECT e.IdGuid AS EmpleadoIdGuid, tp.IdGuid AS TipoPermisoIdGuid
    FROM Empleado e
    CROSS JOIN TipoPermiso tp
    LEFT JOIN Permiso p ON e.Id = p.IdEmpleado AND tp.Id = p.IdTipoPermiso
    WHERE p.Id IS NULL;

Con ella recuperaremos todos los Ids (de tipo Guid) de los empleados junto con los permisos que aun no se les han asignado para poder copiar y pegar esta informacion en swagger.

Selecciomanos uno cualquiera de los conjuntos de datos y lo reemplazamos en el Json que nos solicita en el request ejecutamos y deberiamos ver el siguiente response:

    Permiso solicitado correctamente, aguarde a que sea aprobado.

Una vez solicitado el registro podemos ingresar a elastic y revisar que nuestro registro se haya generado con exito desde la siguiente url "http://localhost:5601" con los datos de acceso que se encuentran en el archivo accesos.md

Ademas tambien podremos ver desde kafka-ui que nuestro mensaje se haya escrito correctamente en la cola desde la siguiente url http://localhost:8080 en el apartado de topics a la izquierda de la interfaz.

---

Tambien tenemos la opcion de actualizar un permiso para que nuestro empleado pueda tener otro tipo de acceso desde el endpoint **/api/Permiso/Modificar**

Solamente necesitamos obtener el IdGuid desde el endpoint **/api/Permiso/Obtener** para poder seleccionar el empleado que queremos y el tipo de permiso que queremos otorgarle.

---

### Atencion

**Los tipos de permisos solamente vamos a poder tener acceso desde la DB para tener mas proteccion en el manejo de datos.**

**Por cuestiones de desarrollo el puerto en el que corre la DB es 1343 y no en el 1433.**

---

**Ante cualquier consulta sobre el proyecto no dudar en consultar:**

**Email: mateomaffeei@gmail.com**