ALTER TABLE `le_goods`
	ADD COLUMN `QuotaBeginTime` DATETIME NULL COMMENT '�޹���ʼʱ��' AFTER `PriceScheme3`,
	ADD COLUMN `QuotaEndTime` DATETIME NULL COMMENT '�޹�����ʱ��' AFTER `QuotaBeginTime`;
