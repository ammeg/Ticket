INSERT INTO [dbo].[Sistema]
           ([DataAtualizacao]
           ,[Ativo]
           ,[UsuarioCadId]
           ,[DataHoraCad])
     VALUES
           ('12/02/2014'--<DataAtualizacao, datetime,>
           ,'true' --<Ativo, bit,>
           ,1 --<UsuarioCadId, int,>
           ,getdate()) --<DataHoraCad, datetime,>)

go

delete from departamento

go

/* Para impedir possíveis problemas de perda de dados, analise este script detalhadamente antes de executá-lo fora do contexto do designer de banco de dados.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Departamento ADD CONSTRAINT
	FK_Departamento_Departamento FOREIGN KEY
	(
	DepartamentoMasterId
	) REFERENCES dbo.Departamento
	(
	DepartamentoId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Departamento SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Departamento', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Departamento', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Departamento', 'Object', 'CONTROL') as Contr_Per 

go 

/* Para impedir possíveis problemas de perda de dados, analise este script detalhadamente antes de executá-lo fora do contexto do designer de banco de dados.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.UsuarioDepartamento
	DROP CONSTRAINT FK_UsuarioDepartamento_Departamento
GO
ALTER TABLE dbo.Departamento SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Departamento', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Departamento', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Departamento', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.UsuarioDepartamento
	DROP CONSTRAINT FK_UsuarioDepartamento_Usuario
GO
ALTER TABLE dbo.Usuario SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Usuario', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Usuario', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Usuario', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_UsuarioDepartamento
	(
	UsuarioId int NOT NULL,
	DepartamentoId int NOT NULL,
	Responsavel bit NOT NULL,
	Ativo bit NOT NULL,
	UsuarioCadId int NOT NULL,
	DataHoraCad datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_UsuarioDepartamento SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.UsuarioDepartamento)
	 EXEC('INSERT INTO dbo.Tmp_UsuarioDepartamento (UsuarioId, DepartamentoId, Responsavel)
		SELECT UsuarioId, DepartamentoId, Responsavel FROM dbo.UsuarioDepartamento WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.UsuarioDepartamento
GO
EXECUTE sp_rename N'dbo.Tmp_UsuarioDepartamento', N'UsuarioDepartamento', 'OBJECT' 
GO
ALTER TABLE dbo.UsuarioDepartamento ADD CONSTRAINT
	PK_UsuarioDepartamento PRIMARY KEY CLUSTERED 
	(
	UsuarioId,
	DepartamentoId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.UsuarioDepartamento ADD CONSTRAINT
	FK_UsuarioDepartamento_Usuario FOREIGN KEY
	(
	UsuarioId
	) REFERENCES dbo.Usuario
	(
	UsuarioId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UsuarioDepartamento ADD CONSTRAINT
	FK_UsuarioDepartamento_Departamento FOREIGN KEY
	(
	DepartamentoId
	) REFERENCES dbo.Departamento
	(
	DepartamentoId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.UsuarioDepartamento', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.UsuarioDepartamento', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.UsuarioDepartamento', 'Object', 'CONTROL') as Contr_Per 

go

/* Para impedir possíveis problemas de perda de dados, analise este script detalhadamente antes de executá-lo fora do contexto do designer de banco de dados.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Usuario SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Usuario', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Usuario', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Usuario', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.UsuarioDepartamento ADD CONSTRAINT
	FK_UsuarioDepartamento_Usuario_Cad FOREIGN KEY
	(
	UsuarioCadId
	) REFERENCES dbo.Usuario
	(
	UsuarioId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UsuarioDepartamento SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.UsuarioDepartamento', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.UsuarioDepartamento', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.UsuarioDepartamento', 'Object', 'CONTROL') as Contr_Per 

go
