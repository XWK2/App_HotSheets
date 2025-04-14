

/*** Email Templates ***/
DECLARE @EmailTemplates TABLE(Name VARCHAR(100), Subject VARCHAR(100), Body VARCHAR(max))

INSERT INTO @EmailTemplates
VALUES
	('ApprovalRequestToManager', 'Hot Sheet - Solicitud de Aprobación a Gerente',
		'Estimad@ <b>{fullName}</b>
		<br/><br/>
		Se ah creado el siguiente Shipping Instruction, el cual requiere de su Aprobación como <b>Gerente</b>.
		<br/><br/>
		Folio Shipping: <b>{folio}</b><br/>
		Fecha de Creación: <b>{creationDate}</b><br/>
		Autor: <b>{author}</b><br/>
		Tipo: <b>{type}</b><br/>
		Cliente: <b>{customerName}</b><br/>
        '
	),
	('ApprovalRequestToAccountingStaff', 'Hot Sheet - Solicitud de Aprobación a Contabilidad',
		'Estimad@ <b>{fullName}</b>
		<br/><br/>
		Se ah creado el siguiente Shipping Instruction, el cual requiere de su Aprobación como personal de <b>Contabilidad</b>.
		<br/><br/>
		Folio Shipping: <b>{folio}</b><br/>
		Fecha de Creación: <b>{creationDate}</b><br/>
		Autor: <b>{author}</b><br/>
		Tipo: <b>{type}</b><br/>
		Cliente: <b>{customerName}</b><br/>
		'
	),
	('ApprovalRequestToImpoExpoStaff', 'Hot Sheet - Solicitud de Aprobación a Importación/Exportación',
		'Estimad@ <b>{fullName}</b>
		<br/><br/>
		Se ah creado el siguiente Shipping Instruction, el cual requiere de su Aprobación como personal de <b>Importación/Exportación</b>.
		<br/><br/>
		Folio Shipping: <b>{folio}</b><br/>
		Fecha de Creación: <b>{creationDate}</b><br/>
		Autor: <b>{author}</b><br/>
		Tipo: <b>{type}</b><br/>
		Cliente: <b>{customerName}</b><br/>
		'
	),
	('ApprovalRequestReminder', 'Hot Sheet - Recordatorio Solicitud de Aprobación',
		'Estimad@ <b>{fullName}</b>
		<br/><br/>
		Le recordamos que tiene los siguientes Hot Sheet pendientes por Aprobar, favor de revisarlos!
		<br/><br/>
		<table style="border: 1px solid black;">
			<thead>
				<tr><th style="color:black;">Folio</th><th style="color:black;">Fecha Creación</th><th style="color:black;">Autor</th><th style="color:black;">Tipo</th><th style="color:black;">Cliente</th></tr>
			</thead>
			<tbody>		
				{rows}
			</tbody>
		</table>		
		'
	),
	('SurveyRequest', 'Hot Sheet - Solicitud de Encuesta',
		'Estimad@ <b>{fullName}</b>
		<br/><br/>
		Se ah completado el proceso del siguiente Hot Sheet, por lo cual nos gustaría escuchar sus comentarios acerca de nuestro servicio.
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
