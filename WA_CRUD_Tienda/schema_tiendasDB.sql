--- Se crea la base de datos ---
CREATE DATABASE tiendas;

USE tiendas;
--- Se crean las tablas ---

--- Tabla Clientes ---
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'clientes')
BEGIN
	CREATE TABLE clientes (
		id_cliente INT not null IDENTITY(1,1) PRIMARY KEY,
		nombre_cliente VARCHAR (50),
		apelllidos_cliente VARCHAR(100),
		direccion_cliente VARCHAR(100),
		cliente_oculto BIT DEFAULT 0
	);
END

GO

--- Tabla Sucursales ---
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'sucursales')
BEGIN
	CREATE TABLE sucursales (
		codigo_sucursal INT not null IDENTITY(1,1) PRIMARY KEY,
		direccion_sucursal VARCHAR(100),
		sucursal_oculta BIT DEFAULT 0
	);
END

GO

--- Tabla Articulos ---
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'articulos')
BEGIN
	CREATE TABLE articulos (
		codigo_articulo INT not null IDENTITY(1,1) PRIMARY KEY,
		descripcion_articulo VARCHAR (100),
		precio_articulo DECIMAL(10,2) not null,
		imagen_articulo VARCHAR(255),
		stock_articulo INT,
		articulo_oculto BIT DEFAULT 0
	);
END

GO

--- Tabla relacion entre arituclos y tienda ---
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'articulo_tienda')
BEGIN
	CREATE TABLE articulo_tienda (
		codigo_articulo_at INT not null,
		codigo_sucursal_at INT not null,
		fecha_as DATE
		PRIMARY KEY (codigo_articulo_at, codigo_sucursal_at),
		FOREIGN KEY (codigo_articulo_at) REFERENCES articulos(codigo_articulo),
		FOREIGN KEY (codigo_sucursal_at) REFERENCES sucursales(codigo_sucursal),
	);
END

GO

--- Tabla relacion entre clientes y articulos ---
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'cliente_articulo')
BEGIN
	CREATE TABLE cliente_articulo (
		id_cliente_ca INT not null,
		codigo_articulo_ca INT not null,
		fecha_ac DATE
		PRIMARY KEY (id_cliente_ca, codigo_articulo_ca),
		FOREIGN KEY (id_cliente_ca) REFERENCES clientes(id_cliente),
		FOREIGN KEY (codigo_articulo_ca) REFERENCES articulos(codigo_articulo),
	);
END

GO

--- Tabla de usuarios ---
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'usuarios')
BEGIN
	CREATE TABLE usuarios (
		id_usuario INT not null IDENTITY(1,1) PRIMARY KEY,
		email_usuario VARCHAR(100) not null,
		pass_usuario VARCHAR(255) not null,
		rol_usuario VARCHAR(50) not null
	);
END

GO

--- Creacion de procedimietos almacenados ---
--- Consultar Clientes ---
IF OBJECT_ID('ConsultarClientes','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE ConsultarClientes
	AS
	BEGIN
		SELECT id_cliente, nombre_cliente, apelllidos_cliente, direccion_cliente
		FROM clientes
		WHERE cliente_oculto = 0;
	END')
END

GO

--- Consultar Clientes por id---
IF OBJECT_ID('ConsultarClientesID','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE ConsultarClientesID
	@id_cliente int
	AS
	BEGIN
		SELECT id_cliente, nombre_cliente, apelllidos_cliente, direccion_cliente
		FROM clientes
		WHERE id_cliente = @id_cliente and cliente_oculto = 0;
	END')
END

GO

--- Insertar Cliente ---
IF OBJECT_ID('InsertarCliente','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE InsertarCliente
		@nombre varchar,
		@apellidos varchar,
		@direccion varchar
	AS
	BEGIN
		INSERT INTO clientes(nombre_cliente, apelllidos_cliente, direccion_cliente)
		VALUES (@nombre, @apellidos,@direccion);
	END')
END

GO

--- Editar Cliente ---
IF OBJECT_ID('EditarCliente','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE EditarCliente
		@id_cliente int,
		@nombre varchar,
		@apellidos varchar,
		@direccion varchar
	AS
	BEGIN
		UPDATE clientes
		SET nombre_cliente = @nombre, apelllidos_cliente = @apellidos, direccion_cliente = @direccion
		WHERE id_cliente = @id_cliente;
	END')
END

GO

--- Eliminar Cliente ---
IF OBJECT_ID('EliminarCliente','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE EliminarCliente
		@id_cliente int
	AS
	BEGIN
		UPDATE clientes
		SET cliente_oculto = 1
		WHERE id_cliente = @id_cliente;
	END')
END

GO

--- Sucursales ---
--- Consultar sucursales ---
IF OBJECT_ID('ConsultarSucursales','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE ConsultarSucursales
	AS
	BEGIN
		SELECT codigo_sucursal, direccion_sucursal
		FROM sucursales
		WHERE sucursal_oculta = 0;
	END')
END

GO

--- Consultar Sucursales por id---
IF OBJECT_ID('ConsultarSucursalesID','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE ConsultarClientesID
	@codigo int
	AS
	BEGIN
		SELECT codigo_sucursal, direccion_sucursal
		FROM sucursales
		WHERE sucursal_oculta = 0 and codigo_sucursal = @codigo;
	END')
END

GO

--- Insertar Sucursal ---
IF OBJECT_ID('InsertarSucursal','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE InsertarSucursal
		@direccion varchar
	AS
	BEGIN
		INSERT INTO sucursales(direccion_sucursal)
		VALUES (@direccion);
	END')
END

GO

--- Editar Sucursal ---
IF OBJECT_ID('EditarSucursales','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE EditarSucursales
		@codigo int,
		@direccion varchar
	AS
	BEGIN
		UPDATE sucursales
		SET direccion_sucursal = @direccion
		WHERE codigo_sucursal = @codigo;
	END')
END

GO

--- Eliminar Sucursal ---
IF OBJECT_ID('EliminarSucursal','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE EliminarSucursal
		@codigo int
	AS
	BEGIN
		UPDATE sucursales
		SET sucursal_oculta = 1
		WHERE codigo_sucursal = @codigo;
	END')
END

GO

--- Articulos ---
--- Consultar articulos ---
IF OBJECT_ID('ConsultarAriticulos','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE ConsultarArticulos
	AS
	BEGIN
		SELECT codigo_articulo, descripcion_articulo, precio_articulo, imagen_articulo, stock_articulo
		FROM articulos
		WHERE articulo_oculto = 0;
	END')
END

GO

--- Consultar Articulos por id---
IF OBJECT_ID('ConsultarArticulosID','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE ConsultarArticulosID
		@codigo int
	AS
	BEGIN
		SELECT codigo_articulo, descripcion_articulo, precio_aticulo, imagen_articulo, stock_articulo
		FROM articulos
		WHERE codigo_articulo = @codigo, articulo_oculto = 0;
	END')
END

GO

--- Insertar Articulo ---
IF OBJECT_ID('InsertarArticulo','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE InsertarArticulo
		@descripcion varchar,
		@precio decimal,
		@imagen varchar,
		@stock int
	AS
	BEGIN
		INSERT INTO articulos(descripcion_articulo, precio_articulo, imagen_articulo, stock_articulo)
		VALUES (@descripcion, @precio, @imagen, @stock);
	END')
END

GO

--- Editar Articulo ---
IF OBJECT_ID('EditarArticulos','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE EditarArticulos
		@codigo int,
		@descripcion varchar,
		@precio decimal,
		@imagen varchar,
		@stock int
	AS
	BEGIN
		UPDATE articulos
		SET descripcion_articulo = @descripcion,
			precio_articulo = @precio,
			imagen_articulo = @imagen,
			stock_articulo = @stock
		WHERE codigo_articulo = @codigo;
	END')
END

GO

--- Eliminar Articulos ---
IF OBJECT_ID('EliminarArticulo','P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE EliminarArticulo
		@codigo int
	AS
	BEGIN
		UPDATE articulos
		SET articulo_oculto = 1
		WHERE codigo_articulo = @codigo;
	END')
END