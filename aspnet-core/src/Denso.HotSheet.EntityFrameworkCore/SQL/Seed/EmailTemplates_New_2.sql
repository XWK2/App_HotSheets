
/*** Email Templates v2 ***/
DECLARE @EmailTemplates TABLE(Name VARCHAR(100), Subject VARCHAR(100), Body VARCHAR(max))

INSERT INTO @EmailTemplates
VALUES
	('ApprovalRequestRejected', 'Hot Sheet {folio} - Solicitud de Aprobación Rechazado',
		'Estimad@ <b>{fullName}</b>
		<br/><br/>
		Se ah rechazado su solicitud de aprobación del siguiente Shipping Instruction:
		<br/><br/>
		Folio Shipping: <b>{folio}</b><br/>
		Fecha de Creación: <b>{creationDate}</b><br/>
		Autor: <b>{author}</b><br/>
		Tipo: <b>{type}</b><br/>
		Cliente: <b>{customerName}</b><br/><br/>
		Rechazo Por: <b>{rejectedBy}</b><br/>
		Motivo del rechazo: {reasonRejection}<br/>
        '
	),
	('ApprovalRequestApproved', 'Hot Sheet {folio} - Solicitud de Aprobación Aprobado',
		'Estimad@ <b>{fullName}</b>
		<br/><br/>
		Su solicitud de aprobación del siguiente Shipping Instruction ah sido <b>Aprobada</b>:
		<br/><br/>
		Folio Shipping: <b>{folio}</b><br/>
		Fecha de Creación: <b>{creationDate}</b><br/>
		Autor: <b>{author}</b><br/>
		Tipo: <b>{type}</b><br/>
		Cliente: <b>{customerName}</b><br/>		
        '
	)

INSERT INTO DensoEmailTemplates(Name, Subject, Body, IsActive, CreationTime, CreatorUserId)
SELECT et.Name, et.Subject, et.Body, 1, GetDate(), 1
FROM @EmailTemplates et
	LEFT OUTER JOIN DensoEmailTemplates et2 ON et2.Name = et.Name
WHERE et2.Id IS NULL
