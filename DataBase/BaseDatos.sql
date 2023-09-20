USE master
GO

IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'RetoBackend')
BEGIN
	DROP DATABASE RetoBackend
END

CREATE DATABASE RetoBackend
GO

USE RetoBackend
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'TblParametro') 
BEGIN
	CREATE TABLE dbo.TblParametro(
		nIdParametro INT IDENTITY,
		sNombre VARCHAR(200),
		nGrupo INT,
		nParametro INT,
		nValor INT,
		sValor VARCHAR(100),
		lVigente BIT DEFAULT 1
) 

	EXEC sys.sp_addextendedproperty
	@name = N'MS_Description',
    @value = N'Tabla donde se registran los parámetros configurables.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblParametro';

    EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Id del registro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblParametro',
    @level2type = N'COLUMN',@level2name = N'nIdParametro' 
	
	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Nombre del parámetro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblParametro',
    @level2type = N'COLUMN',@level2name = N'sNombre' 
	
	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Número del grupo del parámetro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblParametro',
    @level2type = N'COLUMN',@level2name = N'nGrupo' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Valor del parámetro, se utilizará para hacer join en todas las demás tablas.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblParametro',
    @level2type = N'COLUMN',@level2name = N'nParametro'
	
	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Valor numérico del parámetro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblParametro',
    @level2type = N'COLUMN',@level2name = N'nValor'
	
	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Valor en texto del parámetro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblParametro',
    @level2type = N'COLUMN',@level2name = N'sValor'

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Estado del registro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblParametro',
    @level2type = N'COLUMN',@level2name = N'lVigente' 

	ALTER TABLE dbo.TblParametro ADD CONSTRAINT PK_TblParametro PRIMARY KEY (nIdParametro)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'TblPersona') 
BEGIN
	CREATE TABLE dbo.TblPersona(
		nIdPersona INT IDENTITY,
		sNombres VARCHAR(200),
		nGenero INT,
		nEdad INT,
		cIdentificacion VARCHAR(10),
		cDireccion VARCHAR(200),
		cTelefono VARCHAR(12),
		lVigente BIT DEFAULT 1
) 

	EXEC sys.sp_addextendedproperty
	@name = N'MS_Description',
    @value = N'Tabla donde se registran los datos de las personas.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblPersona';

    EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Id del registro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblPersona',
    @level2type = N'COLUMN',@level2name = N'nIdPersona' 

    EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Nombres completos de la persona.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblPersona',
    @level2type = N'COLUMN',@level2name = N'sNombres' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Género de la persona TblParametros - 1: Masculino / 2: Femenino.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblPersona',
    @level2type = N'COLUMN',@level2name = N'nGenero' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Edad de la persona.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblPersona',
    @level2type = N'COLUMN',@level2name = N'nEdad' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Número de identificación de la persona.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblPersona',
    @level2type = N'COLUMN',@level2name = N'cIdentificacion' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Dirección de la persona.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblPersona',
    @level2type = N'COLUMN',@level2name = N'cDireccion' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Teléfono de la persona.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblPersona',
    @level2type = N'COLUMN',@level2name = N'cTelefono'

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Estado del registro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblPersona',
    @level2type = N'COLUMN',@level2name = N'lVigente' 

	ALTER TABLE dbo.TblPersona ADD CONSTRAINT PK_TblPersona PRIMARY KEY (nIdPersona)

END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'TblCliente') 
BEGIN
	CREATE TABLE dbo.TblCliente(
		nIdCliente INT IDENTITY,
		nIdPersona INT,
		sClave VARCHAR(200),
		lVigente BIT DEFAULT 1
) 

	EXEC sys.sp_addextendedproperty
	@name = N'MS_Description',
    @value = N'Tabla donde se registran los datos de los clientes.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCliente';

    EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Id del registro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCliente',
    @level2type = N'COLUMN',@level2name = N'nIdCliente' 

    EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Id de la tabla TblPersona.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCliente',
    @level2type = N'COLUMN',@level2name = N'nIdPersona' 

    EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Contraseña del Cliente.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCliente',
    @level2type = N'COLUMN',@level2name = N'sClave' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Estado del registro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCliente',
    @level2type = N'COLUMN',@level2name = N'lVigente' 

	ALTER TABLE dbo.TblCliente ADD CONSTRAINT PK_TblCliente PRIMARY KEY (nIdCliente)
	ALTER TABLE dbo.TblCliente ADD CONSTRAINT FK_TblCliente_TblPersona FOREIGN KEY (nIdPersona) REFERENCES TblPersona (nIdPersona)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'TblCuenta') 
BEGIN
	CREATE TABLE dbo.TblCuenta(
		nIdCuenta INT IDENTITY,
		nIdCliente INT,
		sNumCuenta VARCHAR(25),
		nTipoCuenta INT,
		nSaldoInicial DECIMAL(14,2),
		nSaldoActual DECIMAL(14,2),
		lVigente BIT DEFAULT 1
) 

	EXEC sys.sp_addextendedproperty
	@name = N'MS_Description',
    @value = N'Tabla donde se registran los datos de las cuentas.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCuenta';

    EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Id del registro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCuenta',
    @level2type = N'COLUMN',@level2name = N'nIdCuenta' 

    EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Id de la tabla TblCliente.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCuenta',
    @level2type = N'COLUMN',@level2name = N'nIdCliente' 

    EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Número de cuenta.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCuenta',
    @level2type = N'COLUMN',@level2name = N'sNumCuenta' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Tipo de cuenta - TblParametros - 4: Ahorros / 5: Corriente.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCuenta',
    @level2type = N'COLUMN',@level2name = N'nTipoCuenta' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Saldo inicial de la cuenta.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCuenta',
    @level2type = N'COLUMN',@level2name = N'nSaldoInicial' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Saldo actual de la cuenta.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCuenta',
    @level2type = N'COLUMN',@level2name = N'nSaldoActual' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Estado del registro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblCuenta',
    @level2type = N'COLUMN',@level2name = N'lVigente' 

	ALTER TABLE dbo.TblCuenta ADD CONSTRAINT PK_TblCuenta PRIMARY KEY (nIdCuenta)
	ALTER TABLE dbo.TblCuenta ADD CONSTRAINT FK_TblCuenta_TblCliente FOREIGN KEY (nIdCliente) REFERENCES TblCliente (nIdCliente)
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'TblMovimientos') 
BEGIN
	CREATE TABLE dbo.TblMovimientos(
		nIdMovimiento INT IDENTITY,
		nIdCuenta INT,
		dFechaMovimiento DATETIME,
		nTipoMovimiento INT,
		nSaldoInicial DECIMAL(14,2),
		nValor DECIMAL(14,2),
		lVigente BIT DEFAULT 1
) 

	EXEC sys.sp_addextendedproperty
	@name = N'MS_Description',
    @value = N'Tabla donde se registran los movimientos de las cuentas.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblMovimientos';

    EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Id del registro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblMovimientos',
    @level2type = N'COLUMN',@level2name = N'nIdMovimiento' 

    EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Id de la tabla TblCuenta.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblMovimientos',
    @level2type = N'COLUMN',@level2name = N'nIdCuenta' 

    EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Fecha del Movimiento.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblMovimientos',
    @level2type = N'COLUMN',@level2name = N'dFechaMovimiento' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Tipo de movimiento - TblParametros - 6: Retiro / 7: Depósito.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblMovimientos',
    @level2type = N'COLUMN',@level2name = N'nTipoMovimiento' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Saldo antes del movimiento.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblMovimientos',
    @level2type = N'COLUMN',@level2name = N'nSaldoInicial' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Valor del movimiento.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblMovimientos',
    @level2type = N'COLUMN',@level2name = N'nValor' 

	EXEC sys.sp_addextendedproperty 
	@name = N'MS_Description',
    @value = N'Estado del registro.',
    @level0type = N'SCHEMA',@level0name = N'dbo',
    @level1type = N'TABLE',	@level1name = N'TblMovimientos',
    @level2type = N'COLUMN',@level2name = N'lVigente' 

	ALTER TABLE dbo.TblMovimientos ADD CONSTRAINT PK_TblMovimientos PRIMARY KEY (nIdMovimiento)
	ALTER TABLE dbo.TblMovimientos ADD CONSTRAINT FK_TblMovimientos_TblCuenta FOREIGN KEY (nIdCuenta) REFERENCES TblCuenta (nIdCuenta)
END

IF NOT EXISTS (SELECT 1 FROM dbo.TblParametro WHERE nGrupo = 1 AND nParametro = 1)
BEGIN
    INSERT INTO dbo.TblParametro (sNombre, nGrupo, nParametro) VALUES ('Masculino', 1, 1)
END

IF NOT EXISTS (SELECT 1 FROM dbo.TblParametro WHERE nGrupo = 1 AND nParametro = 2)
BEGIN
    INSERT INTO dbo.TblParametro (sNombre, nGrupo, nParametro) VALUES ('Femenino', 1, 2)
END

IF NOT EXISTS (SELECT 1 FROM dbo.TblParametro WHERE nGrupo = 1 AND nParametro = 3)
BEGIN
    INSERT INTO dbo.TblParametro (sNombre, nGrupo, nParametro) VALUES ('No Especifica', 1, 3)
END

IF NOT EXISTS (SELECT 1 FROM dbo.TblParametro WHERE nGrupo = 2 AND nParametro = 4)
BEGIN
    INSERT INTO dbo.TblParametro (sNombre, nGrupo, nParametro) VALUES ('Ahorros', 2, 4)
END

IF NOT EXISTS (SELECT 1 FROM dbo.TblParametro WHERE nGrupo = 2 AND nParametro = 5)
BEGIN
    INSERT INTO dbo.TblParametro (sNombre, nGrupo, nParametro) VALUES ('Corriente', 2, 5)
END

IF NOT EXISTS (SELECT 1 FROM dbo.TblParametro WHERE nGrupo = 3 AND nParametro = 6)
BEGIN
    INSERT INTO dbo.TblParametro (sNombre, nGrupo, nParametro) VALUES ('Retiro', 3, 6)
END

IF NOT EXISTS (SELECT 1 FROM dbo.TblParametro WHERE nGrupo = 3 AND nParametro = 7)
BEGIN
    INSERT INTO dbo.TblParametro (sNombre, nGrupo, nParametro) VALUES ('Depósito', 3, 7)
END

IF NOT EXISTS (SELECT 1 FROM dbo.TblParametro WHERE nGrupo = 4 AND nParametro = 8)
BEGIN
    INSERT INTO dbo.TblParametro (sNombre, nGrupo, nParametro, nvalor, sValor) VALUES ('Límite Diario de retiro', 4, 8, 1000,'Cupo diario excedido')
END