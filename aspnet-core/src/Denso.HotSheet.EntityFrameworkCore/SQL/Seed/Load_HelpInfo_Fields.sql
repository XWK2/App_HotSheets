
/*** Help Info Fields ***/
DECLARE @FieldNames TABLE(FieldName VARCHAR(100))

INSERT INTO @FieldNames
VALUES ('DocumentType'), ('Carrier'), ('Services'), ('TermOfPayment'), ('Reason'),
	('AdditionalExplanation'), ('SpecialExpeditedReason'), ('ShippedBy'), ('SoldTo'), ('ShipTo'),
	('EmbarkedTo'), ('RmaNumber'), ('BNotice'), ('CourierAccountNumber'), ('CostOfMerchandisePaidBy'),
	('FreightCostPaidBy'),
	('Department'),('ManagerApproval'),('AccountingApproval'),('ImportExportStaff'),
	('PartNumber'),('MerchandiseDescription'),('MerchandiseDescriptionSpanish'),('UnitOfMeasure'),('ShippedQuantity'),
	('UnitPrice'),('Model'),('Serial'),('Maker'),('TechInfo	'),('PONumber'),('OriginCountry'),
	('AccountNumber'), ('PackagingDescription'), ('PackagingDimentions'), ('PackagingDimentionsLL'), ('PackagingDimentionsWA'),
	('PackagingDimentionsHA'), ('WeightPerBox'), ('BoxQuantity'), ('NetWeight'), ('GrossWeight')

INSERT INTO DensoHelpInfoFields(FieldName, IsActive, CreationTime, CreatorUserId)
SELECT t1.FieldName, 1, GetDate(), 1
FROM @FieldNames t1
	LEFT OUTER JOIN DensoHelpInfoFields t2 ON t2.FieldName = t1.FieldName
WHERE t2.Id IS NULL
